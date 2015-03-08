﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;


namespace Parasite
{
    class MenuScreen
    {
        public event Action OnStart;
        private readonly Button _startButton;
        private readonly Texture2D _backgroundTexture;
        private readonly Texture2D _titleTexture;

        public MenuScreen(ContentManager content, Rectangle screenBoundaries)
        {
            _backgroundTexture = content.Load<Texture2D>("ui/title_screen");
            _titleTexture = content.Load<Texture2D>("ui/title_text");

            var center = screenBoundaries.Center;
            var startTexture = content.Load<Texture2D>("ui/start");

            _startButton = new Button(startTexture, center.X - (startTexture.Width / 2), center.Y + 200);
        }

        public void Update(GameTime gameTime, TouchCollection touchState)
        {
            if (_startButton.WasPressed(ref touchState))
            {
                OnStart();
            }
        }

        public void Draw(GameTime gameTime, DrawState state)
        {
            // Start 
            state.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, state.ScreenXform);

            var center = state.ScreenBoundaries.Center.ToVector2();

            // Draws the background and title
            state.SpriteBatch.Draw(_backgroundTexture, center - _backgroundTexture.GetHalfSize(), Color.White);
            state.SpriteBatch.Draw(_titleTexture, center - _titleTexture.GetHalfSize(), Color.White);

            // Draw button based on a random value on total time giving a blinking effect
            if ((gameTime.TotalGameTime.TotalSeconds % 1.5f) > 0.45f)
            {
                _startButton.Draw(state);
            }

            state.SpriteBatch.End();
        }
    }

}
