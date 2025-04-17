using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Sprites;

public class Zombie : Sprite
{
  AnimationManager animationManager;

  public Zombie(Texture2D texture, Vector2 position) : base(texture, position)
  {
    animationManager = new(6, 6, new Vector2(32, 32));
  }

  public override void Update(GameTime gameTime)
  {
    base.Update(gameTime);

    animationManager.Update();

  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    spriteBatch.Draw(texture, Rect, animationManager.GetFrame(), Color.White);
  }
}
