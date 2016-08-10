using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canon_Shooter
{
    class Enemy
    {
        public int hp = 100, x, y;
        public Enemy(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
