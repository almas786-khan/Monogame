using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AllInOne
{
    public class Enemy : DrawableGameComponent
    {
        private Rectangle boundingBox;
        
        
        int health, bulletDelay, level;
        Vector2 speed;
        bool isVisible;
        public List<Bullet> bulletList;
        

        private SpriteBatch spriteBatch;
        public Texture2D tex, bulletTex;
        private Vector2 position;
        private Vector2 dimension;
        private List<Rectangle> frames;
       // private int frameIndex = -1;
        private int frameIndex = 0;
        private int delay;
        private int delayCounter;
        private const int ROW = 1;
        private const int COL = 4;

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return boundingBox;
            }

            set
            {
                boundingBox = value;
            }
        }

      
        public bool IsVisible
        {
            get
            {
                return isVisible;
            }

            set
            {
                isVisible = value;
            }
        }

        public Enemy(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            int delay,
            Texture2D bulletTex) : base(game)
        {
            this.spriteBatch = spriteBatch;
            bulletList = new List<Bullet>();
            this.tex = tex;
            this.bulletTex = bulletTex;
            health = 5;
            this.position = position;
            bulletDelay = 40;
            level = 1;
            speed = new Vector2(3);
            isVisible = true;
            this.delay = delay;

            dimension = new Vector2(tex.Width / COL, tex.Height / ROW);

            //this.Enabled = false;
            //this.Visible = false;
            //create frames here
            createFrames();
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    frames.Add(r);
                }
            }

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > ROW * COL - 1)
                {
                    frameIndex = -1;
                    this.Enabled = false;
                    this.Visible = false;
                }
                delayCounter = 0;
            }
            //update collision
            boundingBox = new Rectangle((int)position.X,
                (int)position.Y, tex.Width, tex.Height);

            //update enemyship movement
            position.Y += speed.Y;
            position.X += speed.X;

            //move back to top
            if (position.Y >= 420)
            {
                position.Y = -75;
            }
            if (position.X <= 0)
            {
                speed.X = Math.Abs(speed.X);
            }
            if (position.X >= Shared.stage.X - 10)
            {
                speed.X = -Math.Abs(speed.X);
            }
            EnemyShoot();
            BulletUpdate();
            base.Update(gameTime);
        }
        public void BulletUpdate()
        {
            foreach (Bullet bullet in bulletList)
            {
                bullet.BoundingBox = new Rectangle((int)bullet.Position.X,
                    (int)bullet.Position.Y, bullet.TexBullet.Width,
                    bullet.TexBullet.Height);

                //set movement of bullet
                bullet.Position = new Vector2(
                     bullet.Position.X,
                     bullet.Position.Y + bullet.Speed);

                if (bullet.Position.Y >= 420)
                {
                    bullet.IsVisible = false;
                }
            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].IsVisible)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }
        //shoot
        public void EnemyShoot()
        {
            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }
            //if bulletdelay is at zero create new bullet
            if (bulletDelay <= 0)
            {
                Bullet newBullet = new Bullet(bulletTex);
                newBullet.Position = new Vector2(position.X
                    + tex.Width / 8 - bulletTex.Width / 2,
                    position.Y + 30);
                newBullet.IsVisible = true;

                if (bulletList.Count() < 20)
                {
                    bulletList.Add(newBullet);
                }
                //reset bullet delay
                if (bulletDelay == 0)
                {
                    bulletDelay = 40;
                }
            }


        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //v 4
            if (frameIndex >= 0)
            {
                spriteBatch.Draw(tex, position, frames[frameIndex], Color.White);
            }
            foreach (Bullet b in bulletList)
            {
                b.Draw(spriteBatch);

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
