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

        public MenuScreen(ContentManager content, Rectangle screenBoundaries)
        {
            var center = screenBoundaries.Center;
        }

        public void Update(GameTime gameTime, TouchCollection touchState)
        { }

        public void Draw(GameTime gameTime, DrawState state)
        { }
    }

}
