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
    public class Explossion : DrawableGameComponent
    {
        Texture2D tex;
        Vector2 position;
        float timer;
        float interval;
        Vector2 origin;
        int currentFrame, spriteWidth, spriteHeight;
        Rectangle srcRect;
        bool isVisible;
        SpriteBatch spriteBatch;

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

        public Explossion(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            timer = 0f;
            interval = 20f;
            currentFrame = 1;
            spriteWidth = 128;
            spriteHeight = 128;
            isVisible = true;

        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            //increase the timer
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //check the timer 
            if (timer > interval)
            {
                currentFrame++;
                timer = 0f;
            }
            if (currentFrame == 17)
            {
                isVisible = false;
                currentFrame = 0;
            }
            srcRect = new Rectangle(currentFrame * spriteWidth,
                0, spriteWidth, spriteHeight);
            origin = new Vector2(srcRect.Width / 2, srcRect.Height / 2);


            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //v 4
            if (isVisible)
            {
                spriteBatch.Draw(tex, position, srcRect, Color.White, 0f,
                    origin, 1.0f, SpriteEffects.None, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

