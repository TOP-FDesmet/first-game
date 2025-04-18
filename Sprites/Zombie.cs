using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Sprites;

public class Zombie : AnimatedSprite
{
  public Zombie(Texture2D texture, Vector2 position) : base(texture, position)
  {
  }

  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);
  }
}
