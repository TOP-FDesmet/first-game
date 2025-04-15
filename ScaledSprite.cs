using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public class ScaledSprite : Sprite
{
  public Rectangle Rect
  {
    get
    {
      return new Rectangle((int)position.X, (int)position.Y, 64, 64);
    }
  }
  public ScaledSprite(Texture2D texture, Vector2 position) : base(texture, position)
  {
  }
}
