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
    /// Utility methods for calculations
    /// </summary>
   static class Util
    {
       // Calcalte midpoints of texture's dimensions
       static public Vector2 GetHalfSize(this Texture2D text)
       {
           return new Vector2(text.Width / 2.0f, text.Height / 2.0f);
       }

       // Ease Effect
       static public float EaseInOut(float step)
       {
           if (step < 0.5)
           {
               return (float)(Math.Sin(step * Math.PI - (Math.PI * 0.5f)) + 1) * 0.5f;
           }
           else
           {
               return (float)(Math.Sin(step * Math.PI - (Math.PI * 0.5f)) + 1) * 0.5f;
           }
       }
    }
}
