using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public interface IStaticSprite : ISprite
{
  public void Draw(SpriteBatch spriteBatch);
}
