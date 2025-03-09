using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame.Characters;

public class Hero : Sprite
{
  private float Speed { get; set; }
  private int deadZone;
  private int frameCount;
  private float timePerFrame;
  private int frame;
  private float totalElapsed;
  public float Rotation, Scale, Depth;
  public Vector2 Origin;

  public Hero(float positionX, float positionY, Vector2 origin, float rotation, float scale, float depth)
    : base(positionX, positionY)
  {
    Position = new(positionX, positionY);
    Speed = 100f;
    deadZone = 4096;
    Origin = origin;
    Rotation = rotation;
    Scale = scale;
    Depth = depth;
  }

  public void Load(ContentManager content, string sprite, int frameCount, int framePerSeconde)
  {
    this.frameCount = frameCount;
    Texture = content.Load<Texture2D>(sprite);
    timePerFrame = (float)1 / framePerSeconde;
  }

  public void Update(float elapsed)
  {
    totalElapsed += elapsed;
    if (totalElapsed > timePerFrame)
    {
      frame++;
      frame %= frameCount;
      totalElapsed -= timePerFrame;
    }
  }

  public void Move(GameTime gameTime, GraphicsDeviceManager graphics)
  {
    float updateSpeed = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

    var kstate = Keyboard.GetState();

    Vector2 newPosition = Position;

    if (kstate.IsKeyDown(Keys.Up))
    {
      newPosition.Y -= updateSpeed;
    }
    if (kstate.IsKeyDown(Keys.Down))
    {
      newPosition.Y += updateSpeed;
    }
    if (kstate.IsKeyDown(Keys.Left))
    {
      newPosition.X -= updateSpeed;
    }
    if (kstate.IsKeyDown(Keys.Right))
    {
      newPosition.X += updateSpeed;
    }

    if (Joystick.LastConnectedIndex == 0)
    {
      JoystickState jstate = Joystick.GetState((int)PlayerIndex.One);

      if (jstate.Axes[1] < -deadZone)
      {
        newPosition.Y -= updateSpeed;
      }
      if (jstate.Axes[1] > deadZone)
      {
        newPosition.Y += updateSpeed;
      }
      if (jstate.Axes[0] < -deadZone)
      {
        newPosition.X -= updateSpeed;
      }
      if (jstate.Axes[0] > deadZone)
      {
        newPosition.X += updateSpeed;
      }
    }

    if (newPosition.X > graphics.PreferredBackBufferWidth - Texture.Width / 2)
    {
      newPosition.X = graphics.PreferredBackBufferWidth - Texture.Width / 2;
    }
    else if (newPosition.X < Texture.Width / 2)
    {
      newPosition.X = Texture.Width / 2;
    }

    if (newPosition.Y > graphics.PreferredBackBufferHeight - Texture.Height / 2)
    {
      newPosition.Y = graphics.PreferredBackBufferHeight - Texture.Height / 2;
    }
    else if (newPosition.Y < Texture.Height / 2)
    {
      newPosition.Y = Texture.Height / 2;
    }

    Position = newPosition;
  }


  public override void Draw(SpriteBatch spriteBatch)
  {

  }

}
