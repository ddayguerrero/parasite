﻿using Windows.Phone.UI.Input; 
using Microsoft.Xna.Framework; 
using Microsoft.Xna.Framework.Input.Touch; 
using Microsoft.Xna.Framework.Media; 
using Windows.UI.Popups; 


namespace Parasite
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private DrawState _drawState;
        private MenuScreen _menuScreen; 
        private GameScreen _gameScreen; 


        /// <summary>
        /// Set-Up Settings (lol.)
        /// </summary>
        public Game1()
        {
            var graphics = new GraphicsDeviceManager(this);
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            Content.RootDirectory = "Content";

            HardwareButtons.BackPressed += OnBackButton;
        }

        /// <summary>
        /// On Back Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBackButton(object sender, BackPressedEventArgs e)
        {
            if(_gameScreen == null)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;

                var dlg = new MessageDialog("Are you sure you want to quit the game?", "Quit?");
                dlg.Commands.Add(new UICommand("Yes", command =>
                {
                    _gameScreen.Cleanup();
                    _gameScreen = null;
                }));

                dlg.Commands.Add(new UICommand("No"));
                dlg.CancelCommandIndex = 1;
                dlg.ShowAsync();
            }
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

            // Scale Touch Input according to display
            TouchPanel.DisplayWidth = _drawState.ScreenBoundaries.Width;
            TouchPanel.DisplayHeight = _drawState.ScreenBoundaries.Height;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {            
            _drawState = new DrawState(GraphicsDevice);

           

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

            if (_gameScreen != null) 
                 _gameScreen.Update(gameTime, touchState); 
             else 
                 _menuScreen.Update(gameTime, touchState); 

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

            if (_gameScreen != null) 
                _gameScreen.Draw(gameTime, _drawState); 
            else 
                _menuScreen.Draw(gameTime, _drawState); 

            base.Draw(gameTime);
        }
    }
}
