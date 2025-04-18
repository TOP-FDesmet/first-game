using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public class AnimatedSprite : Sprite
{
  private AnimationManager animationManager;

  public AnimatedSprite(Texture2D texture, Vector2 position) : base(texture, position)
  {
    animationManager = new(texture.Width / SIZE, texture.Width / SIZE, new Vector2(32, 32));
  }

  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);

    animationManager.Update();
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    spriteBatch.Draw(
      texture,
      Rect,
      animationManager.GetFrame(),
      Color.White,
      0.0f,
      new Vector2(SIZE / 2, SIZE / 2),
      SpriteEffects.None, 0.0f
    );
  }
}
