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
    public class GameOver : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Vector2 position;
        Texture2D tex;
        SpriteFont font;
        public GameOver(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            SpriteFont font

            ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.tex = tex;
            this.font = font;
        }
        public override void Update(GameTime gameTime)
        {
         
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            //HighScoreScene.Save();
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.DrawString(font, "Your Score : " + Score.playerScore.ToString(), new Vector2(180, 180), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
