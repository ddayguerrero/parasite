using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parasite
{
    /// <summary>
    /// Holds common resources during game drawing. Used for sending.
    /// </summary>
    class DrawState
    {
        public readonly Rectangle ScreenBoundaries;
        // Device Scaling Variables
        public readonly SpriteBatch SpriteBatch;
        public readonly Matrix ScreenXform;

        public DrawState(GraphicsDevice device)
        {
            // Create a new SpriteBatch, which can be used to draw textures. 
            SpriteBatch = new SpriteBatch(device);

            var screenScale = device.PresentationParameters.BackBufferHeight / 1080.0f;
            ScreenXform = Matrix.CreateScale(screenScale, screenScale, 1.0f);

            ScreenBoundaries = new Rectangle(0, 0, 
            (int)Math.Round(device.PresentationParameters.BackBufferWidth / screenScale), 
            (int)Math.Round(device.PresentationParameters.BackBufferHeight / screenScale)); 

        }

    }
}
