using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canon_Shooter
{
    class Powerup
    {
        public int x, y, type;
        public Powerup(int x, int y, int type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
    }
}
