using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.Framework
{
    public class Sprite
    {
        #region [ Members ]
        public Texture2D Texture { get; protected set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        #endregion


        #region [ Constructor ]
        public Sprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public Sprite(Texture2D texture) : this(texture, Vector2.Zero) { }
        #endregion


        #region [ Update ]
        public virtual void Update(GameTime gameTime) { }
        #endregion


        #region [ Draw ]
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color);
        }
        #endregion


    }
}
