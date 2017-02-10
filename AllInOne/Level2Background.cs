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
    public class Level2Background : DrawableGameComponent
    {
            private SpriteBatch spriteBatch;
            private Texture2D tex;


            private Vector2 speed;
            private Vector2 position1, position2;


            public Level2Background(Game game,
                SpriteBatch spriteBatch,
                Texture2D tex,

                Vector2 position1,
                Vector2 position2,
                Vector2 speed) : base(game)
            {
                this.spriteBatch = spriteBatch;
                this.tex = tex;

                this.speed = speed;
                this.position1 = position1;
                this.position2 = position2;
              
            }

            public override void Initialize()
            {
                base.Initialize();

            }
            public override void Update(GameTime gameTime)
            {
                position1.Y = position1.Y + speed.Y;
                position2.Y = position2.Y + speed.Y;
                if (position1.Y >= 410)
                {
                    position1.Y = 0;
                    position2.Y = -410;

                }

     



                base.Update(gameTime);
            }
            public override void Draw(GameTime gameTime)
            {

                spriteBatch.Begin();
                spriteBatch.Draw(tex, position1, Color.White);
                spriteBatch.Draw(tex, position2, Color.White);
                spriteBatch.End();
                base.Draw(gameTime);
            }
        }
    }



