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

    private Dictionary<Vector2, int> tilemap;
    private List<Rectangle> textureStore;
    private Texture2D textureAtlas;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        tilemap = LoadMap("Data/map.csv");
        textureStore = new() {
            new Rectangle(0,32,32,32),
            new Rectangle(32,64,32,32)
        };
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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        foreach (var item in tilemap)
        {
            Rectangle dest = new(
                (int)item.Key.X * 32,
                (int)item.Key.Y * 32,
                32,
                32
            );

            Rectangle src = textureStore[item.Value - 1];

            _spriteBatch.Draw(textureAtlas, dest, src, Color.White);
        }
        hero.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
