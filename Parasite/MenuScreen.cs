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
        private readonly Texture2D _backgroundTex;
        private readonly Texture2D _titleTex;

        public MenuScreen(ContentManager content, Rectangle screenBoundaries)
        {
            _backgroundTex = content.Load<Texture2D>("ui/title_screen");
            _titleTex = content.Load<Texture2D>("ui/title_text");

            var center = screenBoundaries.Center;
        }

        public void Update(GameTime gameTime, TouchCollection touchState)
        { }

        public void Draw(GameTime gameTime, DrawState state)
        { }
    }

}
