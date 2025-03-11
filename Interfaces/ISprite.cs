using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public interface ISprite
{
  public Texture2D Texture { get; }
  public Vector2 Position { get; }

}
