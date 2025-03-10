using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public interface ISprite
{
  public Texture2D Texture { get; set; }
  public float PositionX { get; set; }
  public float PositionY { get; set; }

}
