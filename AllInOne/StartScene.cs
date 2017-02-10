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
    public class StartScene : GameScene
    {
        private MenuComponent myMenuComponent;

        public MenuComponent MyMenuComponent
        {
            get
            {
                return myMenuComponent;
            }

            set
            {
                myMenuComponent = value;
            }
        }

        private SpriteBatch spriteBatch;
        string[] menus = {  "Start Game",
                            "Help",
                            "High Score",
                            "About",
                            "How To Play",
                            "Quit"};

        public StartScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;

            //check this part very carefully. see the use of game reference
            myMenuComponent = new MenuComponent(game, spriteBatch,
                game.Content.Load<SpriteFont>("fonts/regularFont"),
                game.Content.Load<SpriteFont>("fonts/hilightFont"),
                menus);
            this.Components.Add(myMenuComponent);


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
            base.Draw(gameTime);
        }
    }
}
