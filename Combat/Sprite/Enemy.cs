using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combat.Sprite
{
    public class Enemy : Sprite
    {
        public Camera camera = new Camera();
        //Vector2 render;
        public override void Load(ContentManager content, string asset)
        {
            myTexture = content.Load<Texture2D>(asset);
            position = new Vector2(300, 300);

            framePerSec = 7;
            timePerFream = (float)1 / framePerSec;
            frame = 0;
        }
        public override void Behavior(GameTime gameTime, Enemy enemy)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            UpdateFream((float)gameTime.ElapsedGameTime.TotalSeconds);
        }
        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(myTexture, position - Camera.objectPos, new Rectangle(32 * frame, 0, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.None, 0.0f);
        }

        void UpdateFream(float elapsed)
        {
            totalElapsed += elapsed;
            if (totalElapsed > timePerFream)
            {
                frame = (frame + 1) % 5;
                totalElapsed -= timePerFream;
            }
        }
    }
}
