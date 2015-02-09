using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace Parasite
{
    /// <summary>
    /// HUD - Touch Input
    /// </summary>
    class Button
    {
        private Rectangle _location; // Touch Point
        private readonly Texture2D _pressTex;
        private int _lastTouchId; // Last Screen Touch
        private bool _isPressed;

        public Button(Texture2D pressTex, int posX, int posY)
        {
            _pressTex = pressTex;
            _location = new Rectangle(posX, posY, pressTex.Width, pressTex.Height);
        }

        /// <summary>
        /// Check state of button (whether it is touched or not)
        /// </summary>
        /// <param name="screenTouches"></param>
        /// <returns></returns>
        public bool WasPressed(ref TouchCollection screenTouches)
        {
            foreach (var touch in screenTouches)
            {
                if (touch.Id == _lastTouchId)
                    continue;
                if (touch.State != TouchLocationState.Pressed)
                    continue;

                if (_location.Contains(touch.Position))
                {
                    _lastTouchId = touch.Id;
                    _isPressed = true;
                    return true;
                }
            }

            _isPressed = false;

            return _isPressed;
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(DrawState state)
        {
            state.SpriteBatch.Draw(_pressTex, _location, _isPressed ? Color.DarkGray : Color.White);
        }
    }
}
