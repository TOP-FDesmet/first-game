using Microsoft.Xna.Framework;

namespace FirstGame;

public class AnimationManager
{
  private int numFrames;
  private int numColumns;
  private Vector2 size;

  private int counter;
  private int activeFrame;
  private int interval;

  private int rowPos;
  private int colPos;

  public AnimationManager(int numFrames, int numColumns, Vector2 size)
  {
    this.numFrames = numFrames;
    this.numColumns = numColumns;
    this.size = size;

    counter = 0;
    activeFrame = 0;
    interval = 7;

    rowPos = 0;
    colPos = 0;
  }

  public void Update()
  {
    counter++;
    if (counter > interval)
    {
      counter = 0;
      NextFrame();
    }
  }

  private void NextFrame()
  {
    activeFrame++;
    colPos++;
    if (activeFrame >= numFrames)
    {
      ResetAnimation();
    }

    if (colPos >= numColumns)
    {
      colPos = 0;
      rowPos++;
    }
  }

  private void ResetAnimation()
  {
    activeFrame = 0;
    colPos = 0;
    rowPos = 0;
  }

  public Rectangle GetFrame()
  {
    return new Rectangle(
      colPos * (int)size.X,
      rowPos * (int)size.Y,
      (int)size.X,
      (int)size.Y
    );
  }
}
