using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Combat.Sprite
{
    public class Sprite
    {
        public int hp;
        public Vector2 position;
        public int state = 0;
        public Rectangle hitbox;
        public Texture2D myTexture;
        public float rotation, postrotation, scale, depth;
        public Vector2 origin;
        public Vector2 knockBackDirection;

        public int speed;
        public int count;

        public int frame;
        public int framePerSec;
        public float totalElapsed;
        public float timePerFream;

        public virtual void Load(ContentManager content, string asset)
        {

        }
        public virtual void Behavior(GameTime gameTime, Enemy enemy)
        {
        }
        public virtual void Draw(SpriteBatch batch)
        {
            Rectangle sourcerect = new Rectangle();
            batch.Draw(myTexture, position, sourcerect, Color.White, rotation, origin, scale, SpriteEffects.None, depth);
        }
    }
}
