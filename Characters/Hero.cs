using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame.Characters;

public class Hero : AnimatedSprite
{
  private const float speed = 100f;
  private SpriteEffects flip;
  private const int deadZone = 4076;

  public Hero(Vector2 position)
    : base(position)
  {
    FrameCount = 6;
    TimePerFrame = (float)1 / 8;
    Frame = 0;
    TotalElapsed = 0;
  }

  public override void Load(ContentManager content)
  {
    Texture = content.Load<Texture2D>("Player_idle");
    flip = SpriteEffects.FlipHorizontally;
  }

  public void Move(float elapsed, GraphicsDeviceManager graphics)
  {
    float updateSpeed = speed * elapsed;

    var kstate = Keyboard.GetState();

    if (kstate.IsKeyDown(Keys.Up))
    {
      Position = new Vector2(Position.X, Position.Y - updateSpeed);
    }
    if (kstate.IsKeyDown(Keys.Down))
    {
      Position = new Vector2(Position.X, Position.Y + updateSpeed);
    }
    if (kstate.IsKeyDown(Keys.Left))
    {
      flip = SpriteEffects.None;
      Position = new Vector2(Position.X - updateSpeed, Position.Y);
    }
    if (kstate.IsKeyDown(Keys.Right))
    {
      flip = SpriteEffects.FlipHorizontally;
      Position = new Vector2(Position.X + updateSpeed, Position.Y);
    }

    if (Joystick.LastConnectedIndex == 0)
    {
      JoystickState jstate = Joystick.GetState((int)PlayerIndex.One);

      if (jstate.Axes[1] < -deadZone)
      {
        Position = new Vector2(Position.X, Position.Y - updateSpeed);
      }
      if (jstate.Axes[1] > deadZone)
      {
        Position = new Vector2(Position.X, Position.Y + updateSpeed);
      }
      if (jstate.Axes[0] < -deadZone)
      {
        Position = new Vector2(Position.X - updateSpeed, Position.Y);
      }
      if (jstate.Axes[0] > deadZone)
      {
        Position = new Vector2(Position.X + updateSpeed, Position.Y);
      }
    }

    if (Position.X > graphics.PreferredBackBufferWidth - Texture.Width / 2)
    {
      Position = new Vector2(graphics.PreferredBackBufferWidth - Texture.Width / 2, Position.Y);
    }
    else if (Position.X < Texture.Width / 2)
    {
      Position = new Vector2(Texture.Width / 2, Position.Y);
    }

    if (Position.Y > graphics.PreferredBackBufferHeight - Texture.Height / 2)
    {
      Position = new Vector2(Position.X, graphics.PreferredBackBufferHeight - Texture.Height / 2);
    }
    else if (Position.Y < Texture.Height / 2)
    {
      Position = new Vector2(Position.X, Texture.Height / 2);
    }
  }

  public override void DrawFrame(SpriteBatch spriteBatch)
  {
    int frameWidth = Texture.Width / FrameCount;
    Rectangle sourceRect = new(frameWidth * Frame, 0, frameWidth, Texture.Height);
    spriteBatch.Draw(
          Texture,
          Position,
          sourceRect,
          Color.White,
          0.0f,
          new Vector2(sourceRect.Width / 2, sourceRect.Height / 2),
          1.0f,
          flip,
          0.0f);
  }
}
