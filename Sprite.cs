using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public class Sprite
{
  private float SCALE = 2.0f;
  protected int SIZE = 32;
  public Texture2D texture;
  public Vector2 position;

  public Rectangle Rect
  {
    get
    {
      return new Rectangle((int)position.X, (int)position.Y, SIZE * (int)SCALE, SIZE * (int)SCALE);
    }
  }

  public Sprite(Texture2D texture, Vector2 position)
  {
    this.texture = texture;
    this.position = position;
  }

  public virtual void Update(GameTime gameTime)
  {

  }

  public virtual void Draw(SpriteBatch spriteBatch)
  {
    spriteBatch.Draw(texture, Rect, Color.White);
  }
}
