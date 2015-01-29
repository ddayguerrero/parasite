using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
namespace Parasite
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;

        private Map _map;
        private SpriteFont _gameFont;
        // Device Scaling Variables
        private readonly Rectangle _screenBounds;
        private readonly Matrix _screenForm;

        // Textures
        private Texture2D _northOpenWall;
        private Texture2D _northClosedWall;
        private Texture2D _eastOpenWall;
        private Texture2D _eastClosedWall;
        private Texture2D _southOpenWall;
        private Texture2D _southClosedWall;
        private Texture2D _westOpenWall;
        private Texture2D _westClosedWall;

        private Texture2D _floorTexture;

        /// <summary>
        /// Set-Up Settings (lol.)
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            var screenScale = graphics.PreferredBackBufferHeight / 1080.0f;
            _screenForm = Matrix.CreateScale(screenScale, screenScale, 1.0f); // No Z-axis

            // Define screen boundaries based on scale
            _screenBounds = new Rectangle(0, 0, (int)Math.Round(graphics.PreferredBackBufferWidth / screenScale), (int)Math.Round(graphics.PreferredBackBufferHeight / screenScale));

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures. 
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Textures Using Content Manager
            _northOpenWall = Content.Load<Texture2D>("north-open");
            _northClosedWall = Content.Load<Texture2D>("north-closed");
            _eastOpenWall = Content.Load<Texture2D>("east-open");
            _eastClosedWall = Content.Load<Texture2D>("east-closed");
            _southOpenWall = Content.Load<Texture2D>("south-open");
            _southClosedWall = Content.Load<Texture2D>("south-closed");
            _westOpenWall = Content.Load<Texture2D>("west-open");
            _westClosedWall = Content.Load<Texture2D>("west-closed");
            _floorTexture = Content.Load<Texture2D>("floor");

            _gameFont = Content.Load<SpriteFont>("HUDfont");
            // Setup the map 
            _map = new Map(Environment.TickCount);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            DrawRoom();
            DrawPlayerHud();
            base.Draw(gameTime);
        }

        private void DrawPlayerHud()
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _screenForm);

            var room = _map.PlayerRoom;

            var roomDescription = string.Format("Room: {0}", room.Index + 1);
            _spriteBatch.DrawString(_gameFont, roomDescription, new Vector2(20, 10), Color.WhiteSmoke);

            _spriteBatch.End();
        }

        /// <summary>
        /// Draw Room
        /// </summary>
        private void DrawRoom()
        {
            var screen = _screenBounds;
            var center = screen.Center.ToVector2();
            var room = _map.PlayerRoom;

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _screenForm); // enable drawing of textures and whatnot within generated screen scale

            // Drawing the floor
            _spriteBatch.Draw(_floorTexture, center - new Vector2(_floorTexture.Width / 2, _floorTexture.Height / 2), Color.White);

            var wallDepth = _northClosedWall.Height;
            var roomHalfWidth = _northClosedWall.Width / 2.0f;
            var roomHalfHeight = _eastClosedWall.Height / 2.0f;

            // Drawing walls depending of player's room index

            // North and South
            _spriteBatch.Draw(room.NorthRoom != -1 ? _northOpenWall : _northClosedWall, new Vector2(center.X - roomHalfWidth, center.Y - roomHalfHeight));
            _spriteBatch.Draw(room.SouthRoom != -1 ? _southOpenWall : _southClosedWall, new Vector2(center.X - roomHalfWidth, center.Y + roomHalfHeight - wallDepth));
            // East and West
            _spriteBatch.Draw(room.EastRoom != -1 ? _eastOpenWall : _eastClosedWall, new Vector2(center.X + roomHalfWidth - wallDepth, center.Y - roomHalfHeight));
            _spriteBatch.Draw(room.WestRoom != -1 ? _westOpenWall : _westClosedWall, new Vector2(center.X - roomHalfWidth, center.Y - roomHalfHeight));

            _spriteBatch.End();
        }
    }
}
