using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public class AnimatedTexture : IAnimatedTexture
{
  private Texture2D Texture { get; set; }
  public int Width { get; private set; }
  public int Height { get; private set; }

  private int frame;
  private int frameCount;
  private float timePerFrame;
  private float totalElapsed;

  public void LoadTexture(ContentManager content, string asset, int frame, int frameCount, float timePerFrame, float totalElapsed)
  {
    Texture = content.Load<Texture2D>(asset);
    Width = Texture.Width;
    Height = Texture.Height;

    this.frame = frame;
    this.frameCount = frameCount;
    this.timePerFrame = timePerFrame;
    this.totalElapsed = totalElapsed;
  }

  public void UpdateFrame(float elapsed)
  {
    totalElapsed += elapsed;
    if (totalElapsed > timePerFrame)
    {
      frame++;
      frame %= frameCount;
      totalElapsed -= timePerFrame;
    }
  }

  public void DrawFrame(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
  {
    int frameWidth = Texture.Width / frameCount;
    Rectangle sourceRect = new(frameWidth * frame, 0, frameWidth, Texture.Height);
    spriteBatch.Draw(
          Texture,
          position,
          sourceRect,
          Color.White,
          0.0f,
          new Vector2(sourceRect.Width / 2, sourceRect.Height / 2),
          1.0f,
          spriteEffects,
          0.0f);
  }

  public void ChangeTexture(ContentManager content, string newAsset, int newFrameCount)
  {
    Texture = content.Load<Texture2D>(newAsset);
    frameCount = newFrameCount;
  }
}
