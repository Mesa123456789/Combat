using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Combat.Sprite
{
    public class Player : Sprite
    {

        public Vector2 renderPlayer;
        Texture2D sword, effect;
        Vector2 direction;
        bool isMove, isHit;
        bool isCheck, isClick, isAttack;

        KeyboardState kb;
        MouseState mouseSt;

        Vector2 mousepos;
        Vector2 distance = new();
        Vector2 posMouse = new();
        Vector2 knockbackdirection;
        public Camera camera = new Camera();

        public Rectangle mouseCheck;

        public override void Load(ContentManager content, string asset)
        {
            sword = content.Load<Texture2D>("Sword");
            effect = content.Load<Texture2D>("Effect");
            myTexture = content.Load<Texture2D>(asset);
            position = new Vector2(800, 450);
            camera.cameraPos.X = position.X - 400;
            camera.cameraPos.Y = position.Y - 225;
            renderPlayer = position - camera.cameraPos;

            framePerSec = 10;
            timePerFream = (float)1 / framePerSec;
            frame = 0;
        }
        public override void Behavior(GameTime gameTime, Enemy enemy)
        {
            kb = Keyboard.GetState();
            speed = 3;

            if (!(kb.IsKeyDown(Keys.W)) && !(kb.IsKeyDown(Keys.S)) && !(kb.IsKeyDown(Keys.A)) && !(kb.IsKeyDown(Keys.D)))
            {
                isMove = false;
            }

            if (kb.IsKeyDown(Keys.A))
            {
                isMove = true;
                position.X -= speed;
                direction.X = 1;
                camera.cameraPos.X -= speed;
            }
            if (kb.IsKeyDown(Keys.D))
            {
                isMove = true;
                position.X += speed;
                direction.X = 0;
                camera.cameraPos.X += speed;
            }
            if (kb.IsKeyDown(Keys.W))
            {
                isMove = true;
                position.Y -= speed;
                direction.Y = 1;
                camera.cameraPos.Y -= speed;
            }
            if (kb.IsKeyDown(Keys.S))
            {
                isMove = true;
                position.Y += speed;
                direction.Y = 0;
                camera.cameraPos.Y += speed;
            }

            renderPlayer = position - camera.cameraPos;


            if (position.X <= 400)
            {
                camera.cameraPos.X = 0;
            }
            if (position.X >= 1200)
            {
                camera.cameraPos.X = 800;
            }
            if (position.Y <= 225)
            {
                camera.cameraPos.Y = 0;
            }
            if (position.Y >= 675)
            {
                camera.cameraPos.Y = 450;
            }


            hitbox = new Rectangle((int)position.X, (int)position.Y, 32, 32);

            camera.WorldPos(camera.cameraPos.X, camera.cameraPos.Y);

            mouseSt = Mouse.GetState();
            mousepos = Mouse.GetState().Position.ToVector2();
            posMouse = new Vector2(mousepos.X + (camera.cameraPos.X), mousepos.Y + (camera.cameraPos.Y));
            mouseCheck = new Rectangle((int)posMouse.X, (int)posMouse.Y, 24, 24);

            distance = new Vector2(posMouse.X - position.X, posMouse.Y - position.Y);

            rotation = (float)Math.Atan2(distance.Y, distance.X);


            enemy.Behavior(gameTime, null);
            Attack(enemy);

            Debug.WriteLine("position enemy : " + enemy.position);
            //Debug.WriteLine("Player : " + hitbox);
            //Debug.WriteLine("Camera : " + camera.cameraPos);
            //Debug.WriteLine("MousePos : " + mouseCheck);
            Debug.WriteLine("hitbox enemy : " + enemy.hitbox);

            UpdateFream((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Attack(Enemy enemy)
        {
            //
            

            if (mouseSt.LeftButton == ButtonState.Pressed)
            {
                if (isClick == false)
                {
                    if (state <= 3)
                    {
                        state = state + 1;
                        count = 0;
                        isClick = true;
                        isAttack = true;

                        //------------------- ตีแล้วพุ่งไปด้านหน้า/-------------------
                        //knockbackdirection = posmouse - position;
                        //if (knockbackdirection != vector2.zero)
                        // {
                        //     knockbackdirection.normalize();
                        // }

                        //if(isattack == true)
                        //{
                        //     position += knockbackdirection * 15;
                        //     camera.camerapos.x += knockbackdirection.x * 15;
                        //     camera.camerapos.y += knockbackdirection.y * 15;
                        //
                    }
                }
            }

            if (mouseSt.LeftButton == ButtonState.Released)
            {
                postrotation = rotation;
                isClick = false;
                isAttack = false;
            }

            if (state > 3 || count > 20)
            {
                state = 0;
            }
            if (state < 4)
            {
                count++;
            }

            if (count < 15)
            {
                isAttack = true;

            }
            else
            {
                isAttack = false;
                isCheck = false;
            }

            Rectangle attck_top_rignt = new Rectangle((int)position.X + 16, (int)position.Y - 32, 48, 48);
            Rectangle attck_top_left = new Rectangle((int)position.X - 32, (int)position.Y - 32, 48, 48);
            Rectangle attck_bot_rignt = new Rectangle((int)position.X + 16, (int)position.Y + 16, 48, 48);
            Rectangle attck_bot_left = new Rectangle((int)position.X - 32, (int)position.Y + 16, 48, 48);

            // attck_top_rignt
            if ((mousepos.X >= renderPlayer.X && mousepos.Y <= renderPlayer.Y) && attck_top_rignt.Intersects(enemy.hitbox) && isAttack)
            {
                isCheck = true;
                knockBackDirection = enemy.position - position;
                if (knockBackDirection != Vector2.Zero)
                {
                    knockBackDirection.Normalize();
                }
                enemy.position += knockBackDirection * 20;
            }
            //attck_top_left
            if ((mousepos.X <= renderPlayer.X && mousepos.Y <= renderPlayer.Y) && attck_top_left.Intersects(enemy.hitbox) && isAttack)
            {
                isCheck = true;
                knockBackDirection = enemy.position - position;
                if (knockBackDirection != Vector2.Zero)
                {
                    knockBackDirection.Normalize();
                }
                enemy.position += knockBackDirection * 20;
            }
            //attck_bot_rignt
            if ((mousepos.X >= renderPlayer.X && mousepos.Y >= renderPlayer.Y) && attck_bot_rignt.Intersects(enemy.hitbox) && isAttack)
            {
                isCheck = true;
                knockBackDirection = enemy.position - position;
                if (knockBackDirection != Vector2.Zero)
                {
                    knockBackDirection.Normalize();
                }
                enemy.position += knockBackDirection * 20;
            }
            //attck_bot_left
            if ((mousepos.X <= renderPlayer.X && mousepos.Y >= renderPlayer.Y) && attck_bot_left.Intersects(enemy.hitbox) && isAttack)
            {
                isCheck = true;
                knockBackDirection = enemy.position - position;
                if (knockBackDirection != Vector2.Zero)
                {
                    knockBackDirection.Normalize();
                }
                enemy.position += knockBackDirection * 20;
            }



            Debug.WriteLine("count : " + enemy.position);
        }
    



        public override void Draw(SpriteBatch batch)
        {
            //Attack
            if (state == 1 && isAttack == true)
            {
                batch.Draw(effect, renderPlayer, new Rectangle(0, 0, 48, 48), Color.White, rotation, new Vector2(24, 24), 2.0f, SpriteEffects.FlipVertically, 0.0f);
                batch.Draw(sword, renderPlayer, new Rectangle(0, 0, 48, 48), Color.White, rotation, new Vector2(24, 24), 2.0f, SpriteEffects.None, 0.0f);
            }
            //Idle
            if (isMove == false && direction.X == 0 && direction.Y == 0)
            {
                batch.Draw(myTexture, renderPlayer * camera.scroll_Factor, new Rectangle(32 * frame, 0, 32, 32), Color.White, 0.0f, new Vector2 (16,16), 2.0f, SpriteEffects.None, 0.0f);
            }
            if (isMove == false && direction.X == 1 && direction.Y == 0)
            {
                batch.Draw(myTexture, renderPlayer, new Rectangle(32 * frame, 0, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.FlipHorizontally, 0.0f);
            }
            if (isMove == false && direction.X == 0 && direction.Y == 1)
            {
                batch.Draw(myTexture, renderPlayer, new Rectangle(32 * frame, 32, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.None, 0.0f);
            }
            if (isMove == false && direction.X == 1 && direction.Y == 1)
            {
                batch.Draw(myTexture, renderPlayer, new Rectangle(32 * frame, 32, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.FlipHorizontally, 0.0f);
            }

            //Run
            if (isMove == true && direction.X == 0 && direction.Y == 0)
            {
                batch.Draw(myTexture, renderPlayer, new Rectangle(32 * frame, 64, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.None, 0.0f);
            }
            if (isMove == true && direction.X == 1 && direction.Y == 0)
            {
                batch.Draw(myTexture, renderPlayer, new Rectangle(32 * frame, 64, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.FlipHorizontally, 0.0f);
            }
            if (isMove == true && direction.X == 0 && direction.Y == 1)
            {
                batch.Draw(myTexture, renderPlayer, new Rectangle(32 * frame, 96, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.None, 0.0f);
            }
            if (isMove == true && direction.X == 1 && direction.Y == 1)
            {
                batch.Draw(myTexture, renderPlayer, new Rectangle(32 * frame, 96, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.FlipHorizontally, 0.0f);
            }
            //Attack 2
            if (state == 2 && isAttack == true)
            {
                postrotation = rotation;
                batch.Draw(effect, renderPlayer, new Rectangle(0, 0, 48, 48), Color.White, rotation, new Vector2(24, 24), 2.0f, SpriteEffects.None, 0.0f);
                batch.Draw(sword, renderPlayer, new Rectangle(0, 0, 48, 48), Color.White, rotation + 135.0f, new Vector2(24, 24), 2.0f, SpriteEffects.FlipHorizontally, 0.0f);
            }
            //Attack 3
            if (state == 3 && isAttack == true)
            {
                postrotation = rotation;
                batch.Draw(effect, renderPlayer, new Rectangle(0, 0, 48, 48), Color.White, rotation, new Vector2(24, 24), 2.0f, SpriteEffects.None, 0.0f);
                batch.Draw(sword, renderPlayer, new Rectangle(0, 0, 48, 48), Color.White, rotation - 190.0f, new Vector2(24, 24), 2.0f, SpriteEffects.FlipHorizontally, 0.0f);
            }

            // mouse
            //batch.Draw(myTexture, renderPlayer, new Rectangle(32 * frame, 0, 32, 32), Color.Brown, rotation, new Vector2(16, 16), 2.0f, SpriteEffects.None, 0.0f);

            if (isCheck == true)
            {
                batch.Draw(myTexture, mousepos, new Rectangle(32 * frame, 0, 32, 32), Color.Yellow, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.None, 0.0f);
            }
            else
            {
                batch.Draw(myTexture, mousepos, new Rectangle(32 * frame, 0, 32, 32), Color.Blue, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.None, 0.0f);
            }
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
