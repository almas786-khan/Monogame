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

    public class Score : DrawableGameComponent
    {
        public static int playerScore,levelNo;
        public int screenWidth, screenHeight;
        SpriteFont spriteFont;
        SpriteBatch spriteBatch;
        Game game;
        Vector2 playerScorePos;
        bool showScore;



        public int ScreenWidth
        {
            get
            {
                return screenWidth;
            }

            set
            {
                screenWidth = value;
            }
        }

        public int ScreenHeight
        {
            get
            {
                return screenHeight;
            }

            set
            {
                screenHeight = value;
            }
        }


        public Score(Game game,
            SpriteBatch spriteBatch,
            SpriteFont font) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = font;
            this.game = game;
            playerScore = 0;
            screenWidth = 506;
            screenHeight = 410;
            showScore = true;
            playerScorePos = new Vector2(screenWidth - 230, 20);

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
            if (showScore)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(game.Content.Load<Texture2D>("images/bar"), new Vector2(240,10), Color.White);
                spriteBatch.DrawString(spriteFont, "Score  - " + playerScore + "  Level : "+ levelNo, playerScorePos, Color.Black);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}

