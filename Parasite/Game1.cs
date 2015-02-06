using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using Microsoft.Xna.Framework.Input.Touch;

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

        // HUD Controls - Buttons
        private Button _buttonN;
        private Button _buttonE;
        private Button _buttonS;
        private Button _buttonW;

        private Texture2D buttonNorth;
        private Texture2D buttonEast;
        private Texture2D buttonSouth;
        private Texture2D buttonWest;

        private float _scrollPos; // Position
        private int _scrollOutRoom = -1; // Room scrolling out
        private int _scrollInRoom = -1; // Room scrolling in
        private Vector2 _scrollInStart; // Start Position of Scroll
        private Vector2 _scrollOutEnd; // End Position of Scroll

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
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
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

            //Scale Touch Input according to display
            TouchPanel.DisplayWidth = _screenBounds.Width;
            TouchPanel.DisplayHeight = _screenBounds.Height;
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

            buttonNorth = Content.Load<Texture2D>("ui/north");
            buttonEast = Content.Load<Texture2D>("ui/east");
            buttonSouth = Content.Load<Texture2D>("ui/south");
            buttonWest = Content.Load<Texture2D>("ui/west");

            _buttonN = new Button(buttonNorth, _screenBounds.Center.X - (buttonNorth.Width / 2), 0);
            _buttonE = new Button(buttonEast, _screenBounds.Center.X + (_northClosedWall.Width / 2) - buttonEast.Width, _screenBounds.Center.Y - (buttonNorth.Height / 2));
            _buttonS = new Button(buttonSouth, _screenBounds.Center.X - (buttonSouth.Width / 2), _screenBounds.Bottom - buttonSouth.Height);
            _buttonW = new Button(buttonWest, _screenBounds.Center.X - (_northClosedWall.Width / 2), _screenBounds.Center.Y - (buttonWest.Height / 2));

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
            var touchState = TouchPanel.GetState(); // Get Touch State

            // Check which button was called
            if (_buttonN.WasPressed(ref touchState))
            {
                _map.MovePlayerNorth((curr, next) =>
                {

                });
            }
            else if (_buttonE.WasPressed(ref touchState))
            {
                _map.MovePlayerEast((curr, next) =>
                {

                });
            }
            else if (_buttonS.WasPressed(ref touchState))
            {
                _map.MovePlayerSouth((curr, next) =>
                {

                });
            }
            else if (_buttonW.WasPressed(ref touchState))
            {
                _map.MovePlayerWest((curr, next) =>
                {

                });
            }

            // Animate the scroll 
            _scrollPos = MathHelper.Clamp(_scrollPos + (float)gameTime.ElapsedGameTime.TotalSeconds * 2.0f, 0, 1);

            // Scroll state
            if (_scrollOutRoom != -1) // Still scrolling
            {
                if (_scrollPos == 1.0f) // Done scrolling
                {
                    _scrollOutRoom = -1;
                    _scrollPos = 0; // Not scrolled in or out
                }
            }
            else if (_scrollInRoom != -1)
            {
                if (_scrollPos == 1.0f)
                    _scrollInRoom = -1; 
            }
 
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

        /// <summary>
        /// Draw HUD information
        /// </summary>
        private void DrawPlayerHud()
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _screenForm);

            var room = _map.PlayerRoom;

            // Only draw the movement buttons if the scene is not animating.
            if (room.NorthRoom != -1)
                _buttonN.Draw(_spriteBatch);
            if (room.EastRoom != -1)
                _buttonE.Draw(_spriteBatch);
            if (room.SouthRoom != -1)
                _buttonS.Draw(_spriteBatch);
            if (room.WestRoom != -1)
                _buttonW.Draw(_spriteBatch);

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

            if (_scrollOutRoom != -1) // Scrolling out?
            {
                room = _map[_scrollOutRoom];
                var offset = Vector2.Lerp(Vector2.Zero, _scrollOutEnd, _scrollPos);
                center += offset; // Offset center point
            }
            else if (_scrollInRoom != -1) // Scrolling in?
            {
                room = _map[_scrollInRoom];
                var offset = Vector2.Lerp(_scrollInStart, Vector2.Zero, _scrollPos);
                center += offset; // Offset center point
            }

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _screenForm); // Enable drawing of textures and whatnot within generated screen scale

            // Drawing the floor based on ground point center
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
