using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Characters;

public class Hero : Sprite
{
  public Hero(Texture2D texture, float positionX, float positionY)
    : base(texture, positionX, positionY) { }
}
