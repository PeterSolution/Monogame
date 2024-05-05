using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WolffAstro
{
    internal class aaa
    {
        Vector2 position;
        Random random;
        public aaa() 
        {
            position = new Vector2(100,300);

        }

        void move()
        {
            position.Y -= 5;
        }

        Vector2 getposition()
        {
            return position;
        }

    }
}
