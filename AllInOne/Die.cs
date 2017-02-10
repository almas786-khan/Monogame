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
    public class Die : DrawableGameComponent
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
        private int frameIndex = 0;
        private int delay;
        private int delayCounter;
        private const int ROW = 1;
        private const int COL = 3;

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

        public Vector2 Position1
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

        public Die(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            int delay
            ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            
            health = 5;
            this.position = position;
            level = 1;
            speed = new Vector2(5);
            isVisible = true;
            this.delay = delay;

            dimension = new Vector2(tex.Width / COL, tex.Height / ROW);

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
                    //this.Enabled = false;
                    this.IsVisible = false;
                   // Ship.isVisible = true;
                }
                delayCounter = 0;
            }
            
            
            base.Update(gameTime);
        }
       
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //v 4
            if (frameIndex >= 0)
            {
                spriteBatch.Draw(tex, position, frames[frameIndex], Color.White);
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
