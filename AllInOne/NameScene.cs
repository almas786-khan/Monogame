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
    public class NameScene : GameScene
    {
        SpriteBatch spriteBatch;
        Texture2D tex;
        SpriteFont myFont;
        public static string messageString;
        public static string playerName="";
        Keys[] keys;
        Game game;

        public NameScene(Game game,
            SpriteBatch spriteBatch
            ) : base(game)
        {
            this.game = game;
            myFont= game.Content.Load<SpriteFont>("fonts/regularFont");
            this.spriteBatch = spriteBatch;
            messageString = "";
            keys = new Keys[0];

        }
        public override void Initialize()
        {
             
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(game.Content.Load<Texture2D>("images/namepage"), new Vector2(0,0), Color.White);
                spriteBatch.DrawString(myFont, messageString, new Vector2(180, 210), Color.White);
                spriteBatch.End();    
            }
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape))
            {
                
                game.Exit();
            }



            Keys[] keyPress;
            keyPress = keyState.GetPressedKeys();
            //messageString = "";

            for (int i = 0; i < keyPress.Length; i++)
            {
                bool found = false;
                for (int j = 0; j < keys.Length; j++)
                {
                    if (keyPress[i] == keys[j])
                        found = true;
                }
                if (found == false)
                {
                    
                    messageString += keyPress[i].ToString() + " ";
                    
                }
            }
            //playerName = messageString;
            //Console.WriteLine(playerName);
            keys = keyPress;

            base.Update(gameTime);
        }
    }
}
