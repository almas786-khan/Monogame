using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AllInOne
{
    public class Player : DrawableGameComponent
    {
        Texture2D texL, bulletTexture, healthTexture,texR,tex, faceTex;
        Vector2 position;
        Vector2 speed;
        int health;
        SpriteBatch spriteBatch;
       
        public static bool isVisible;
        private Rectangle boundingBox, healthRectangle,faceRectangle;
        float bulletDelay;
        Vector2 healthBarPos, facePos;
        public List<Bullet> bulletList;
        SoundEffect shotSound;

        

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

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

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

        public Player(Game game,
            SpriteBatch spriteBatch,
            Texture2D texL,
            Texture2D texR,
            Texture2D faceTex,
            Texture2D heathTex,
            Vector2 position,
            Vector2 speed,
            Texture2D bulletTexture,
            SoundEffect shotSound
            ) : base(game)
        {
            this.faceTex = faceTex;
            this.tex = texL;
            this.texR = texR;
            this.position = position;
            this.speed = speed;
            this.texL = texL;
            this.spriteBatch = spriteBatch;
            this.shotSound = shotSound;

            bulletList = new List<Bullet>();
            this.bulletTexture = bulletTexture;
            bulletDelay = 1;
            health = 200;
            healthTexture = heathTex;
            healthBarPos = new Vector2(30, 10);
            facePos = new Vector2(10,10);


        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (isVisible)
            {
                spriteBatch.Draw(tex, position, Color.White);
            }
            
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
            spriteBatch.Draw(faceTex,faceRectangle,Color.White);
            foreach (Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {

            KeyboardState ks = Keyboard.GetState();
            //bounding box for ship
            boundingBox = new Rectangle((int)position.X,
                (int)position.Y, tex.Width,
                tex.Height);
            faceRectangle = new Rectangle((int)facePos.X,
                (int)facePos.Y,25,25);
            healthRectangle = new Rectangle((int)healthBarPos.X,
                (int)healthBarPos.Y, health, 25);
            if (ks.IsKeyUp(Keys.Space))
            {
                bulletDelay = 1;

            }

            if (ks.IsKeyDown(Keys.Space))
            {
                Shoot();

            }
            BulletUpdate();

           
            if (ks.IsKeyDown(Keys.A))
            {
                position.X = position.X - speed.X;
                tex = texR;
               
            }
           
            if (ks.IsKeyDown(Keys.D))
            {
                position.X = position.X + speed.X;
                tex = texL;
               
            }

            ///keep ship inside window
            if (position.X <= 0)
            {
                position.X = 0;
                tex = texL;

            }
            if (position.X >= 506 - tex.Width)
            {
                position.X = 506 - tex.Width;
                tex = texR;
            }
            if (position.Y <= 0)
            {
                position.Y = 0;
            }
            if (position.Y >= 410 - tex.Height)
            {
                position.Y = 410 - tex.Height;
            }
            base.Update(gameTime);
        }

        public void Shoot()
        {
            //shoot when bulletdelay reset
            if (bulletDelay >= 0)
            {
                bulletDelay--;
            }

            //if bulletdelay is at zero create new bullet
            if (bulletDelay <= 0)
            {
                shotSound.Play();
                Bullet newBullet = new Bullet(bulletTexture);
                newBullet.Position = new Vector2(position.X
                    + 32 - bulletTexture.Width / 2,
                    position.Y + 30);
                newBullet.IsVisible = true;
                if (bulletList.Count() < 20)
                {
                    bulletList.Add(newBullet);
                }
                //reset bullet delay
                if (bulletDelay == 0)
                {
                    bulletDelay = 20;
                }
            }


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
                     bullet.Position.Y - bullet.Speed);

                if (bullet.Position.Y <= 0)
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
    }
}

