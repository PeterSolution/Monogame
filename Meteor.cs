using Windows.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

namespace WolffAstro
{
    internal class Meteor 
    { 
        Texture2D texture2d;
        Vector2 position;
        public Meteor(Texture2D texture)
        {
            texture2d= texture;
            position= new Vector2(100,10);
        }
        public void changeposition(int x)
        {
            this.position.Y = 10;
            this.position.X = x;
        }
        public Vector2 getposition()
        {
            return position;
        }
        public void MoveL()
        {
            if (position.X - 5 > 10)
            {
                position.X -= 1;
            }
        }
        public void MoveR()
        {
            if (position.X + 5 < 420)
            {
                position.X += 1;
            }
        }
        public void MoveU() 
        { 
            if(position.Y - 5 > 80)
            {
                position.Y-= 1;
            }
        }
        public void MoveD()
        {
            if (position.Y + 5 < 790)
            {
                position.Y += 1;
            }
        }
        
    }
}
