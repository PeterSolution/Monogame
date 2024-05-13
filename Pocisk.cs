using Windows.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

namespace WolffAstro
{
    internal class Pocisk
    {
        Texture2D texture2d;
        Vector2 position;
        public Pocisk(Texture2D texture)
        {
            texture2d = texture;
        }
        public void setpoz(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }
        public Vector2 getposition()
        {
            return position;
        }
        public void MoveL()
        {

            position.X -= 10;

        }
        public void MoveR()
        {

            position.X += 10;

        }
        public void MoveU()
        {

            position.Y -= 10;

        }
        public void MoveD()
        {

            position.Y += 10;

        }

    }
}
