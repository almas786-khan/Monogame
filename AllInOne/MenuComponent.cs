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
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, hilightFont;
        private List<string> menuItems;
        private int selectedIndex = 0;
        Game game;

        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }

            set
            {
                selectedIndex = value;
            }
        }

        private Vector2 position;
        private Color regularColor = Color.Black;
        private Color hilightColor = Color.Black;
        private KeyboardState oldState; // why? ... later...

        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            SpriteFont hilightFont,
            string[] menus) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.hilightFont = hilightFont;
            menuItems = new List<string>();
            menuItems = menus.ToList(); // check it later

            //for (int i = 0; i < menus.Length; i++)
            //{
            //    menuItems.Add(menus[i]);
            //}
            position = new Vector2(Shared.stage.X/3 + 15, Shared.stage.Y - 360); 


        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }

            oldState = ks;




            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempos = position;

            spriteBatch.Begin();
            spriteBatch.Draw(game.Content.Load<Texture2D>("images/bg1"), new Vector2(0, 0), Color.White);
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(hilightFont, menuItems[i],
                        tempos, hilightColor);
                    tempos.Y += hilightFont.LineSpacing + 32;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i],
                      tempos, regularColor);
                    tempos.Y += regularFont.LineSpacing + 32;
                }

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
