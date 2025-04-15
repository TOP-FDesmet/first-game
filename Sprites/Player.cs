using System;
using System.Diagnostics;
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

public class Player : Sprite
{
  private float SPEED = 200.0f;
  private KeyboardState oldKstate;

  public Player(Texture2D texture, Vector2 position) : base(texture, position)
  {

  }

  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);

    float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

    var kstate = Keyboard.GetState();

    if (!oldKstate.IsKeyDown(Keys.Space))
    {
      if (kstate.IsKeyDown(Keys.Space))
      {
        Console.WriteLine("Space is pressed !");
      }
    }

    oldKstate = kstate;

    if (kstate.IsKeyDown(Keys.Up))
    {
      position.Y -= SPEED * elapsed;
    }

    if (kstate.IsKeyDown(Keys.Down))
    {
      position.Y += SPEED * elapsed;
    }

    if (kstate.IsKeyDown(Keys.Left))
    {
      position.X -= SPEED * elapsed;
    }

    if (kstate.IsKeyDown(Keys.Right))
    {
      position.X += SPEED * elapsed;
    }
  }
}
