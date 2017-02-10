using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AllInOne
{
    public class HowToPlayScene : GameScene
        {
            private SpriteBatch spriteBatch;
            private Texture2D tex;

        public HowToPlayScene(Game game, SpriteBatch spriteBatch) : base(game)
            {
                this.spriteBatch = spriteBatch;
                tex = game.Content.Load<Texture2D>("images/howtoplay");
            }

            public override void Initialize()
            {
                base.Initialize();
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
            }

            public override void Draw(GameTime gameTime)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(tex, Vector2.Zero, Color.White);
                spriteBatch.End();

                base.Draw(gameTime);
            }
        }
    }

