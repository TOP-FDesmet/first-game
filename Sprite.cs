using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public abstract class Sprite : ISprite
{
  public Texture2D Texture { get; set; }
  public float PositionX { get; set; }
  public float PositionY { get; set; }
  private Vector2 Position { get; set; }

  public Sprite(Texture2D texture, float positionX, float positionY)
  {
    Texture = texture;
    PositionX = positionX;
    PositionY = positionY;
    Position = new(PositionX, PositionY);
  }

  protected Sprite(Texture2D texture, Vector2 position)
  {
    Texture = texture;
    Position = position;
  }

  public void Draw(SpriteBatch spriteBatch)
  {
    spriteBatch.Draw(
            Texture,
            Position,
            null,
            Color.White,
            0f,
            new Vector2(Texture.Width / 2, Texture.Height / 2),
            Vector2.One,
            SpriteEffects.None,
            0f);
  }
}
