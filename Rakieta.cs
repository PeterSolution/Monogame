using Windows.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

namespace WolffAstro
{
    internal class Rakieta
    {
        Texture2D texture2d;
        Vector2 position;
        public Rakieta(Texture2D texture)
        {
            texture2d= texture;
            position= new Vector2(210,480);
        }
        public Vector2 getposition()
        {
            return position;
        }
        public void MoveL()
        {
            if (position.X - 5 > 10)
            {
                position.X -= 5;
            }
        }
        public void MoveR()
        {
            if (position.X + 5 < 420)
            {
                position.X += 5;
            }
        }
        public void MoveU() 
        { 
            if(position.Y - 5 > 80)
            {
                position.Y-= 5;
            }
        }
        public void MoveD()
        {
            if (position.Y + 5 < 600)
            {
                position.Y += 5;
            }
        }
        
    }
}
