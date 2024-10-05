using Combat.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Combat
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Player _player = new Player();
        public Enemy crab = new Enemy();
        public Quast quast = new Quast();
        Texture2D map, board;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 450;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            map = Content.Load<Texture2D>("Map");
            board = Content.Load<Texture2D>("Board");
            _player.Load(Content, "Player-Sheet");
            quast.Load(Content, "Quest1");
            crab.Load(Content, "Enemy_SpriteSheet");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            quast.OpenQuast(_player, new Vector2(920, 520));
            crab.Behavior(gameTime, crab);
            // TODO: Add your update logic here
            _player.Behavior(gameTime, crab);
            
            base.Update(gameTime);
        }
        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState:SamplerState.PointClamp);

            _spriteBatch.Draw(map, Vector2.Zero - _player.camera.cameraPos, new Rectangle(0, 0, 1600, 900), Color.White, 0.0f, Vector2.Zero,1.0f, SpriteEffects.None, 0.0f);
            
            _spriteBatch.Draw(board, new Vector2(920, 520) - Camera.objectPos, new Rectangle(0, 0, 48, 32), Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            

            _player.Draw(_spriteBatch);
            crab.Draw(_spriteBatch);
            quast.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
