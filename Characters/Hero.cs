using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame.Characters;

enum HeroStates
{
  Idle,
  Run,
  Knocked,
  Hit,
  Death
}

public class Hero
{
  private Vector2 Position { get; set; }
  private int heroWidth;
  private int heroHeight;
  private const float speed = 200f;

  private int frame;
  private int frameCount;
  private float timePerFrame;
  private float totalElapsed;

  private AnimatedTexture animatedTexture;

  private SpriteEffects flip;
  private HeroStates heroState;
  private HeroStates newHeroState;
  private const int deadZone = 4076;

  private KeyboardState oldKstate;

  public Hero(Vector2 position)
  {
    Position = position;
    frame = 0;
    frameCount = 6;
    heroState = HeroStates.Idle;
    timePerFrame = (float)1 / 8;
    totalElapsed = 0;
  }

  public void Load(ContentManager content)
  {
    animatedTexture = new();
    animatedTexture.LoadTexture(content, "Player_idle", frame, frameCount, timePerFrame, totalElapsed);
    heroWidth = animatedTexture.Width / frameCount;
    heroHeight = animatedTexture.Height;
    flip = SpriteEffects.FlipHorizontally;
  }

  public void Update(ContentManager content, GraphicsDeviceManager graphics, float elapsed)
  {
    if (newHeroState != heroState)
    {
      SwitchState(content);
      newHeroState = heroState;
    }
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

    if (!oldKstate.IsKeyDown(Keys.E))
    {
      if (kstate.IsKeyDown(Keys.E))
      {
        Console.WriteLine("E is pressed !");
      }
    }
    oldKstate = kstate;

    if (kstate.IsKeyUp(Keys.Up) ||
        kstate.IsKeyDown(Keys.Down) ||
        kstate.IsKeyDown(Keys.Left) ||
        kstate.IsKeyDown(Keys.Right))
    {
      heroState = HeroStates.Idle;
    }

    if (kstate.IsKeyDown(Keys.Up))
    {
      Position = new Vector2(Position.X, Position.Y - updateSpeed);
      heroState = HeroStates.Run;
    }

    if (kstate.IsKeyDown(Keys.Down))
    {
      Position = new Vector2(Position.X, Position.Y + updateSpeed);
      heroState = HeroStates.Run;
    }

    if (kstate.IsKeyDown(Keys.Left))
    {
      flip = SpriteEffects.None;
      Position = new Vector2(Position.X - updateSpeed, Position.Y);
      heroState = HeroStates.Run;
    }

    if (kstate.IsKeyDown(Keys.Right))
    {
      flip = SpriteEffects.FlipHorizontally;
      Position = new Vector2(Position.X + updateSpeed, Position.Y);
      heroState = HeroStates.Run;
    }


    if (Joystick.LastConnectedIndex == 0)
    {
      JoystickState jstate = Joystick.GetState((int)PlayerIndex.One);

      if (jstate.Axes[1] < -deadZone)
      {
        Position = new Vector2(Position.X, Position.Y - updateSpeed);
        heroState = HeroStates.Run;
      }

      if (jstate.Axes[1] > deadZone)
      {
        Position = new Vector2(Position.X, Position.Y + updateSpeed);
        heroState = HeroStates.Run;
      }

      if (jstate.Axes[0] < -deadZone)
      {
        Position = new Vector2(Position.X - updateSpeed, Position.Y);
        heroState = HeroStates.Run;
      }

      if (jstate.Axes[0] > deadZone)
      {
        Position = new Vector2(Position.X + updateSpeed, Position.Y);
        heroState = HeroStates.Run;
      }
    }

    if (Position.X > graphics.PreferredBackBufferWidth - heroWidth / 2)
    {
      Position = new Vector2(graphics.PreferredBackBufferWidth - heroWidth / 2, Position.Y);
    }
    else if (Position.X < heroWidth / 2)
    {
      Position = new Vector2(heroWidth / 2, Position.Y);
    }

    if (Position.Y > graphics.PreferredBackBufferHeight - heroHeight / 2)
    {
      Position = new Vector2(Position.X, graphics.PreferredBackBufferHeight - heroHeight / 2);
    }
    else if (Position.Y < heroHeight / 2)
    {
      Position = new Vector2(Position.X, heroHeight / 2);
    }
  }

  private void SwitchState(ContentManager content)
  {
    switch (heroState)
    {
      case HeroStates.Idle:
        animatedTexture.ChangeTexture(content, "Player_idle", 6);
        break;
      case HeroStates.Run:
        animatedTexture.ChangeTexture(content, "Player_run", 8);
        break;
      case HeroStates.Knocked:
        animatedTexture.ChangeTexture(content, "Player_knocked", 6);
        break;
      case HeroStates.Hit:
        animatedTexture.ChangeTexture(content, "Player_hit", 3);
        break;
      case HeroStates.Death:
        animatedTexture.ChangeTexture(content, "Player_death", 8);
        break;
      default:
        break;
    }
  }
}
