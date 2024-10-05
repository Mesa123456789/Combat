using Combat.Sprite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Combat
{
    public class Quast
    {
        MouseState mouseSt;
        Vector2 pos;
        bool open;
        Texture2D myTexture;

        int[] indexQuast = new int[28];

        Random rQuast;

        Vector2 Q1 = new Vector2(165, 128);
        Vector2 Q2 = new Vector2(335, 128);
        Vector2 Q3 = new Vector2(505, 128);
        public void Load(ContentManager content, string asset)
        {
            myTexture = content.Load<Texture2D>(asset);
        }
        public void OpenQuast(Player player, Vector2 posQuast) 
        {
            pos = posQuast;

            Rectangle box = new Rectangle((int)pos.X, (int)pos.Y, 96, 64);
            Rectangle exitbox = new Rectangle((int)(752 + Camera.objectPos.X), (int)(0 + Camera.objectPos.Y), 48, 48);

            Rectangle Q1box = new Rectangle((int)(Q1.X + Camera.objectPos.X), (int)(Q1.Y + Camera.objectPos.Y), 48, 48);
            Rectangle Q2box = new Rectangle((int)(Q2.X + Camera.objectPos.X), (int)(Q2.Y + Camera.objectPos.Y), 48, 48);
            Rectangle Q3box = new Rectangle((int)(Q3.X + Camera.objectPos.X), (int)(Q3.Y + Camera.objectPos.Y), 48, 48);

            mouseSt = Mouse.GetState();

            if (player.mouseCheck.Intersects(box) && mouseSt.RightButton == ButtonState.Pressed && open == false)
            {
                open = true;

            }
            if(player.mouseCheck.Intersects(exitbox) && mouseSt.LeftButton == ButtonState.Pressed && open == true)
            {
                Debug.WriteLine(" borad = " + (pos));
                open = false;
            }
        }
        public void Draw(SpriteBatch Batch)
        {
            if(open == true)
            {
                Batch.Draw(myTexture, new Vector2(100,53), new Rectangle(0, 352, 608, 304), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
                Batch.Draw(myTexture, new Vector2(752, 0), new Rectangle(752, 0, 48, 48), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

                Batch.Draw(myTexture, new Vector2(Q1.X, Q1.Y), new Rectangle(640, 80, 144, 192), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
                Batch.Draw(myTexture, new Vector2(Q2.X, Q2.Y), new Rectangle(640, 80, 144, 192), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
                Batch.Draw(myTexture, new Vector2(Q3.X, Q3.Y), new Rectangle(640, 80, 144, 192), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }
            else
            {
                //Batch.Draw(board, Camera.objectPos, new Rectangle(0, 0, 48, 32), Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            }
            //Batch.Draw(board,pos - Camera.objectPos,Color.Red);
            
            Batch.End();
            Batch.Begin(samplerState: SamplerState.PointClamp);
        }

        public void RandomQuset()
        {

        }

    }
}
