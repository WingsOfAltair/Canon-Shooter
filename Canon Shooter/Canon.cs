using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canon_Shooter
{
    class Canon
    {
        public Bullet[] Bullets = new Bullet[0];
        public int x, y, hp;
        public Canon()
        {
            hp = 100;
            x = 30;
            y = 30;
            System.Threading.Thread cleanUP = new System.Threading.Thread(new System.Threading.ThreadStart(killBullet));
            cleanUP.Start();
        }
        public void createBullet()
        {
            Array.Resize(ref Bullets, Bullets.Length + 1);
            Bullets[Bullets.Length - 1] = new Bullet(x, y);
        }
        public void killBullet()
        {
            while (true)
            {
                for (int i = 0; i < Bullets.Length; i++)
                {
                    try // To prevent errors from checks to kill the newly created bullets before the constructor is called
                    {
                        if (!Bullets[i].alive)
                        {
                            Bullets[i] = Bullets[Bullets.Length - 1];
                            Array.Resize(ref Bullets, Bullets.Length - 1);
                        }
                    }
                    catch { }
                }
            }
        }
    }
}
