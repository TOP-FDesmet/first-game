using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public interface IAnimatedTexture
{
  public void UpdateFrame(float elapsed);
  public void DrawFrame(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects);
}
