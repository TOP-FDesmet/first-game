using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public abstract class AnimatedSprite : IAnimatedSprite
{
  public int FrameCount { get; set; }
  public float TimePerFrame { get; set; }
  public int Frame { get; set; }
  public float TotalElapsed { get; set; }

  public Texture2D Texture { get; set; }

  public Vector2 Position { get; set; }

  public AnimatedSprite(Vector2 position)
  {
    Position = position;
  }

  public abstract void Load(ContentManager content);

  public void UpdateFrame(float elapsed)
  {
    TotalElapsed += elapsed;
    if (TotalElapsed > TimePerFrame)
    {
      Frame++;
      Frame %= FrameCount;
      TotalElapsed -= TimePerFrame;
    }
  }

  public virtual void DrawFrame(SpriteBatch spriteBatch)
  {
    int frameWidth = Texture.Width / FrameCount;
    Rectangle sourceRect = new(frameWidth * Frame, 0, frameWidth, Texture.Height);
    spriteBatch.Draw(
          Texture,
          Position,
          sourceRect,
          Color.White,
          0.0f,
          new Vector2(sourceRect.Width / 2, sourceRect.Height / 2),
          1.0f,
          SpriteEffects.None,
          0.0f);
  }
}
