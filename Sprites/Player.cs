using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame.Sprites;

enum HeroStates
{
  Idle,
  Run,
  Knocked,
  Hit,
  Death
}

public class Player : AnimatedSprite
{
  private float SPEED = 200.0f;
  private KeyboardState oldKstate;
  private List<AnimatedSprite> collisionGroup;

  public Player(Texture2D texture, Vector2 position, List<AnimatedSprite> collisionGroup) : base(texture, position)
  {
    this.collisionGroup = collisionGroup;
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

    float changeX = 0;
    float changeY = 0;

    if (kstate.IsKeyDown(Keys.Left))
    {
      changeX -= SPEED * elapsed;
    }

    if (kstate.IsKeyDown(Keys.Right))
    {
      changeX += SPEED * elapsed;
    }
    position.X += changeX;

    if (kstate.IsKeyDown(Keys.Up))
    {
      changeY -= SPEED * elapsed;
    }

    if (kstate.IsKeyDown(Keys.Down))
    {
      changeY += SPEED * elapsed;
    }
    position.Y += changeY;

    foreach (var sprite in collisionGroup)
    {
      if (sprite != this && sprite.Rect.Intersects(Rect))
      {
        position.X -= changeX;
        position.Y -= changeY;
      }
    }
  }
}
