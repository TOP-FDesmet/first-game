using System.Collections.Generic;
using System.IO;
using FirstGame.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame;

public class Game1 : Game
{
    private Hero hero;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Dictionary<Vector2, int> ground;
    private Dictionary<Vector2, int> wall;
    private Dictionary<Vector2, int> collisions;
    private Texture2D textureAtlas;

    private Vector2 camera;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        ground = LoadMap("Data/level1_ground_1.csv");
        wall = LoadMap("Data/level1_wall_1.csv");
        collisions = LoadMap("Data/level1_collisions.csv");
        camera = Vector2.Zero;
    }

    private Dictionary<Vector2, int> LoadMap(string filepath)
    {
        Dictionary<Vector2, int> result = new();

        StreamReader reader = new(filepath);

        int y = 0;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(',');

            for (int x = 0; x < items.Length; x++)
            {
                if (int.TryParse(items[x], out int value))
                {
                    if (value > 0)
                    {
                        result[new Vector2(x, y)] = value;
                    }
                }
            }

            y++;
        }

        return result;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        hero = new(new Vector2(
            _graphics.PreferredBackBufferWidth / 2,
            _graphics.PreferredBackBufferHeight / 2));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        textureAtlas = Content.Load<Texture2D>("atlas");
        hero.Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        hero.Update(Content, _graphics, elapsed);

        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            camera.X -= 5;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            camera.X += 5;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            camera.Y += 5;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            camera.Y -= 5;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        int displayTilesize = 32;
        int numTilesPerRow = 10;
        int pixelTilesize = 32;

        foreach (var item in ground)
        {
            Rectangle drect = new(
                (int)item.Key.X * displayTilesize + (int)camera.X,
                (int)item.Key.Y * displayTilesize + (int)camera.Y,
                displayTilesize,
                displayTilesize
            );

            int x = item.Value % numTilesPerRow;
            int y = item.Value / numTilesPerRow;

            Rectangle src = new(
                x * pixelTilesize,
                y * pixelTilesize,
                pixelTilesize,
                pixelTilesize
            );

            _spriteBatch.Draw(textureAtlas, drect, src, Color.White);
        }

        foreach (var item in wall)
        {
            Rectangle drect = new(
                (int)item.Key.X * displayTilesize + (int)camera.X,
                (int)item.Key.Y * displayTilesize + (int)camera.Y,
                displayTilesize,
                displayTilesize
            );

            int x = item.Value % numTilesPerRow;
            int y = item.Value / numTilesPerRow;

            Rectangle src = new(
                x * pixelTilesize,
                y * pixelTilesize,
                pixelTilesize,
                pixelTilesize
            );

            _spriteBatch.Draw(textureAtlas, drect, src, Color.White);
        }

        hero.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
