using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Canon_Shooter
{
    public partial class Form1 : Form
    {
        private int score = 0;
        private bool run = true;
        Canon[] Canons = new Canon[1];
        Enemy[] Enemies = new Enemy[0];
        Block[] Blocks = new Block[0];
        Powerup Powerup = new Powerup(0, 0, 0);
        System.Random Random = new System.Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                for (int i = 0; i < Canons.Length; i++)
                {
                    e.Graphics.FillRectangle(Brushes.Black, Canons[0].x, Canons[0].y, 70, 70);
                    for (int j = 0; j < Canons[i].Bullets.Length; j++)
                    {
                        e.Graphics.FillRectangle(Brushes.Black, Canons[i].Bullets[j].x, Canons[i].Bullets[j].y, 5, 5);
                    }
                }
                for (int k = 0; k < Blocks.Length; k++)
                {
                    e.Graphics.FillRectangle(Brushes.Green, Blocks[k].x, Blocks[k].y, 50, 20);
                }
                for (int j = 0; j < Enemies.Length; j++)
                {
                    e.Graphics.FillRectangle(Brushes.Red, Enemies[j].x, Enemies[j].y, 40, 40);
                }
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            CheckForIllegalCrossThreadCalls = false; // Bad practise (replace it later)
            Canons[0] = new Canon();
            System.Threading.Thread runGame = new System.Threading.Thread(new System.Threading.ThreadStart(SimulateGame));
            runGame.Start();
            System.Threading.Thread spawnEnemies = new System.Threading.Thread(new System.Threading.ThreadStart(SpawnEnemies));
            spawnEnemies.Start();
            System.Threading.Thread spawnBlocks = new System.Threading.Thread(new System.Threading.ThreadStart(SpawnBlocks));
            spawnBlocks.Start();
            //System.Threading.Thread spawnPowerups = new System.Threading.Thread(new System.Threading.ThreadStart(SpawnPowerups));
            //spawnPowerups.Start();
        }

        private void SpawnPowerups()
        {
            while (run)
            {
                System.Threading.Thread.Sleep(20000);
                createPowerup();
            }
        }

        private void SpawnBlocks()
        {
            while (run)
            {
                createBlock();
                System.Threading.Thread.Sleep(5000);
            }
        }

        private void SpawnEnemies()
        {
            while (run)
            {
                createEnemy();
                System.Threading.Thread.Sleep(5000);
            }
        }

        private void SimulateGame()
        {
            while (run)
            {
                checkCollisions();
                this.Invalidate();
                this.Text = "Canon Shooter. Score: " + this.score;
                System.Threading.Thread.Sleep(25);
            }
        }

        private void checkCollisions()
        {
            checkBlockCollision();
            checkEnemyCollision();
        }

        private void checkBlockCollision()
        {
            try
            {
                if (Blocks.Length > 0)
                {
                    for (int i = 0; i < Canons[0].Bullets.Length; i++)
                    {
                        for (int j = 0; j < Blocks.Length; j++)
                        {
                            if (Canons[0].Bullets[i].y >= Blocks[j].y && (Canons[0].Bullets[i].x >= Blocks[j].x && Canons[0].Bullets[i].x <= Blocks[j].x + 50))
                            {
                                Canons[0].Bullets[i].alive = false;
                                Blocks[j] = Blocks[Blocks.Length - 1];
                                Array.Resize(ref Blocks, Blocks.Length - 1);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void checkEnemyCollision()
        {
            try
            {
                if (Enemies.Length > 0)
                {
                    for (int i = 0; i < Canons[0].Bullets.Length; i++)
                    {
                        for (int j = 0; j < Enemies.Length; j++)
                        {
                            if (Canons[0].Bullets[i].y >= Enemies[j].y && (Canons[0].Bullets[i].x >= Enemies[j].x && Canons[0].Bullets[i].x <= Enemies[j].x + 40))
                            {
                                Enemies[j].hp -= Canons[0].Bullets[i].dps;
                                Canons[0].Bullets[i].alive = false;
                            }
                        }
                    }
                    for (int i = 0; i < Enemies.Length; i++)
                    {
                        if (Enemies[i].hp <= 0) // Enemy killed
                        {
                            Enemies[i] = Enemies[Enemies.Length - 1];
                            Array.Resize(ref Enemies, Enemies.Length - 1);
                            this.score += 10;
                            createEnemy();
                        }
                    }
                }
            }
            catch { }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            bool flag = false;

            if (e.KeyCode == Keys.Left)
            {
                if (Canons[0].x > 1)
                    Canons[0].x -= 10;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (Canons[0].x < (800 - 90))
                    Canons[0].x += 10;
            }
            if (e.KeyCode == Keys.Up)
            {
                if (Canons[0].y > 1)
                    Canons[0].y -= 10;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (Canons[0].y < (600 - 115))
                    Canons[0].y += 10;
            }
        }

        private void createPowerup()
        {
            if (Powerup.type == 0)
            {
                Powerup = new Powerup(Random.Next(0, 720), Random.Next(0, 300), Random.Next(1, 3));
            }
        }

        private void createBlock()
        {
            int BlockNumber = Random.Next(1, 6);
            for (int i = 0; i < BlockNumber; i++)
            {
                Array.Resize(ref Blocks, Blocks.Length + 1);
                if (i == 0)
                    Blocks[Blocks.Length - 1] = new Block(Random.Next(0, 720), Random.Next(200, 300));
                else Blocks[Blocks.Length - 1] = new Block(Blocks[Blocks.Length - 2].x + 50, Blocks[Blocks.Length - 2].y);
            }
        }

        private void createEnemy()
        {
            Array.Resize(ref Enemies, Enemies.Length + 1);
            Enemies[Enemies.Length - 1] = new Enemy(Random.Next(0, 720), Random.Next(400, 520));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Handle exiting better (make sure the threads are dead before quitting)
            run = false;
            Application.Exit();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Canons[0].createBullet();
                this.Invalidate();
            }
        }
    }
}
