using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canon_Shooter
{
    class Bullet
    {
        public int x, y, dps;
        public bool alive;
        public Bullet(int x, int y)
        {
            System.Random Random = new System.Random();
            dps = Random.Next(1, 40);
            this.x = x + 35;
            this.y = y + 75;
            alive = !alive;
            System.Threading.Thread BulletThread = new System.Threading.Thread(new System.Threading.ThreadStart(moveBullet));
            BulletThread.Start();
        }

        public void moveBullet()
        {
            while (alive)
            {
                if (y <= 600)
                {
                    y += 10;
                    System.Threading.Thread.Sleep(25);
                }
                else // Kill the bullet once it is out of the screen view to free up memory
                {
                    alive = !alive;
                }
            }
        }
    }
}
