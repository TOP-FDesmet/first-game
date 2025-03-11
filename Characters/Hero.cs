using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame.Characters;

public class Hero : IAnimatedSprite
{
  private Vector2 position;
  private float Speed { get; set; }
  public Texture2D Texture { get; set; }
  public float PositionX { get; set; }
  public float PositionY { get; set; }

  public int FrameCount { get; set; }
  public float TimePerFrame { get; set; }
  public int Frame { get; set; }
  public float TotalElapsed { get; set; }
  public float Rotation { get; set; }
  public float Scale { get; set; }
  public float Depth { get; set; }
  public Vector2 Origin { get; set; }
  public bool flip = false;

  private int deadZone;

  public Hero(float positionX, float positionY, Vector2 origin, float rotation, float scale, float depth)
  {
    position = new(positionX, positionY);
    Speed = 100f;
    deadZone = 4096;
    Origin = origin;
    Rotation = rotation;
    Scale = scale;
    Depth = depth;
  }

  public void Load(ContentManager content, string asset, int frameCount, int framesPerSec)
  {
    FrameCount = frameCount;
    Texture = content.Load<Texture2D>(asset);
    TimePerFrame = (float)1 / framesPerSec;
    Frame = 0;
    TotalElapsed = 0;
  }

  public void UpdateFrame(float elapsed)
  {
    TotalElapsed += elapsed;
    if (TotalElapsed > TimePerFrame)
    {
      Frame++;
      Frame %= FrameCount;
      TotalElapsed -= TimePerFrame;
    }
  }

  public void Move(float elapsed, GraphicsDeviceManager graphics)
  {
    float updateSpeed = Speed * elapsed;

    var kstate = Keyboard.GetState();

    if (kstate.IsKeyDown(Keys.Up))
    {
      position.Y -= updateSpeed;
    }
    if (kstate.IsKeyDown(Keys.Down))
    {
      position.Y += updateSpeed;
    }
    if (kstate.IsKeyDown(Keys.Left))
    {
      flip = false;
      position.X -= updateSpeed;
    }
    if (kstate.IsKeyDown(Keys.Right))
    {
      flip = true;
      position.X += updateSpeed;
    }

    if (Joystick.LastConnectedIndex == 0)
    {
      JoystickState jstate = Joystick.GetState((int)PlayerIndex.One);

      if (jstate.Axes[1] < -deadZone)
      {
        position.Y -= updateSpeed;
      }
      if (jstate.Axes[1] > deadZone)
      {
        position.Y += updateSpeed;
      }
      if (jstate.Axes[0] < -deadZone)
      {
        position.X -= updateSpeed;
      }
      if (jstate.Axes[0] > deadZone)
      {
        position.X += updateSpeed;
      }
    }

    if (position.X > graphics.PreferredBackBufferWidth - Texture.Width / 2)
    {
      position.X = graphics.PreferredBackBufferWidth - Texture.Width / 2;
    }
    else if (position.X < Texture.Width / 2)
    {
      position.X = Texture.Width / 2;
    }

    if (position.Y > graphics.PreferredBackBufferHeight - Texture.Height / 2)
    {
      position.Y = graphics.PreferredBackBufferHeight - Texture.Height / 2;
    }
    else if (position.Y < Texture.Height / 2)
    {
      position.Y = Texture.Height / 2;
    }
  }

  public void DrawFrame(SpriteBatch spriteBatch)
  {
    int frameWidth = Texture.Width / FrameCount;
    Rectangle sourceRect = new(frameWidth * Frame, 0, frameWidth, Texture.Height);
    if (flip)
    {
      spriteBatch.Draw(
            Texture,
            position,
            sourceRect,
            Color.White,
            Rotation,
            new Vector2(sourceRect.Width / 2, sourceRect.Height / 2),
            Scale,
            SpriteEffects.FlipHorizontally,
            Depth);

      return;
    }
    spriteBatch.Draw(
          Texture,
          position,
          sourceRect,
          Color.White,
          Rotation,
          new Vector2(sourceRect.Width / 2, sourceRect.Height / 2),
          Scale,
          SpriteEffects.None,
          Depth);

  }
}
