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
        private Map _map;
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;
    
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

        private readonly Rectangle _screenBounds;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            var spriteBatch = new SpriteBatch(GraphicsDevice);
           
            // Load Textures
            _northOpenWall = Content.Load<Texture2D>("north-open");
            _northClosedWall = Content.Load<Texture2D>("north-closed");
            _eastOpenWall = Content.Load<Texture2D>("east-open");
            _eastClosedWall = Content.Load<Texture2D>("east-closed");
            _southOpenWall = Content.Load<Texture2D>("south-open");
            _southClosedWall = Content.Load<Texture2D>("south-closed");
            _westOpenWall = Content.Load<Texture2D>("west-open");
            _westClosedWall = Content.Load<Texture2D>("west-closed");

            _floorTexture = Content.Load<Texture2D>("floor");

            // Setup the map. 
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

            base.Draw(gameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawRoom()
        {
            var screen = _screenBounds;
            var center = screen.Center.ToVector2();
            var room = _map.PlayerRoom;

            _spriteBatch.Begin();

            // Draw the floor
            _spriteBatch.Draw(_floorTexture, center - new Vector2(_floorTexture.Width / 2, _floorTexture.Height / 2), Color.White);

            var roomHalfWidth = _northClosedWall.Width / 2.0f;
            var wallDepth = _northClosedWall.Height;
            var roomHalfHeight = _eastClosedWall.Height / 2.0f; 

        }
    }
}
