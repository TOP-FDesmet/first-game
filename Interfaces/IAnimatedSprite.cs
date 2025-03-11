using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public interface IAnimatedSprite : ISprite
{
  public int FrameCount { get; set; }
  public float TimePerFrame { get; set; }
  public int Frame { get; set; }
  public float TotalElapsed { get; set; }
  public float Rotation { get; set; }
  public float Scale { get; set; }
  public float Depth { get; set; }
  public Vector2 Origin { get; set; }

  public void UpdateFrame(float elapsed);
  public void DrawFrame(SpriteBatch spriteBatch);
}
