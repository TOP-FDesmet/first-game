using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame.Characters;

public class Hero : Sprite
{
  private float Speed { get; set; }
  private int deadZone;

  public Hero(Texture2D texture, float positionX, float positionY)
    : base(texture, positionX, positionY)
  {
    Position = new(positionX, positionY);
    Speed = 100f;
    deadZone = 4096;
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
}
