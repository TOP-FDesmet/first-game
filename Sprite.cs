using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public abstract class Sprite : ISprite
{
  public Texture2D Texture { get; set; }
  public float PositionX { get; set; }
  public float PositionY { get; set; }
  public Vector2 Position { get; protected set; }

  public Sprite(float positionX, float positionY)
  {
    PositionX = positionX;
    PositionY = positionY;
  }

  public virtual void Draw(SpriteBatch spriteBatch)
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
