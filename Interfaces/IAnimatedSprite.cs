using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public interface IAnimatedSprite : ISprite
{
  public int FrameCount { get; set; }
  public float TimePerFrame { get; set; }
  public int Frame { get; set; }
  public float TotalElapsed { get; set; }

  public void UpdateFrame(float elapsed);
  public void Draw(SpriteBatch spriteBatch);
}
