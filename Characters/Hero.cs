using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame.Characters;

public class Hero
{
  private Vector2 Position { get; set; }
  private const float speed = 100f;

  private int frame;
  private int frameCount;
  private float timePerFrame;
  private float totalElapsed;

  private AnimatedTexture animatedTexture;

  private SpriteEffects flip;
  private bool heroMoves;
  private const int deadZone = 4076;

  public Hero(Vector2 position)
  {
    Position = position;
    frame = 0;
    frameCount = 6;
    heroMoves = false;
    timePerFrame = (float)1 / 8;
    totalElapsed = 0;
  }

  public void Load(ContentManager content)
  {
    animatedTexture = new();
    animatedTexture.LoadTexture(content, "Player_idle", frame, frameCount, timePerFrame, totalElapsed);
    flip = SpriteEffects.FlipHorizontally;
  }

  public void Update(GraphicsDeviceManager graphics, float elapsed)
  {
    animatedTexture.UpdateFrame(elapsed);
    Move(graphics, elapsed);
  }

  public void Draw(SpriteBatch spriteBatch)
  {
    animatedTexture.DrawFrame(spriteBatch, Position, flip);
  }

  private void Move(GraphicsDeviceManager graphics, float elapsed)
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

    if (Position.X > graphics.PreferredBackBufferWidth - animatedTexture.Width / 2)
    {
      Position = new Vector2(graphics.PreferredBackBufferWidth - animatedTexture.Width / 2, Position.Y);
    }
    else if (Position.X < animatedTexture.Width / 2)
    {
      Position = new Vector2(animatedTexture.Width / 2, Position.Y);
    }

    if (Position.Y > graphics.PreferredBackBufferHeight - animatedTexture.Height / 2)
    {
      Position = new Vector2(Position.X, graphics.PreferredBackBufferHeight - animatedTexture.Height / 2);
    }
    else if (Position.Y < animatedTexture.Height / 2)
    {
      Position = new Vector2(Position.X, animatedTexture.Height / 2);
    }
  }
}
