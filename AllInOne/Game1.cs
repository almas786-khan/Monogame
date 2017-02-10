using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;

namespace AllInOne
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
   

        string s;
        private StartScene startScene;
        //other scene declaration here
        private NameScene nameScene;
        private ActionScene actionScene;
        private HelpScene helpScene;
        private HowToPlayScene howToPlayScene;
        private HighScoreScene highScoreScene;
       
        private AboutScene aboutScene;

        private Song bgMusic;
        private Song startMusic;


        Texture2D tex;
        private SpriteFont myFont;
        public static bool sat=true;
        private Texture2D texb;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 506;
            graphics.PreferredBackBufferHeight = 410;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);

           
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            tex = Content.Load<Texture2D>("images/bg");
            myFont = Content.Load<SpriteFont>("fonts/regularFont");
            texb = Content.Load<Texture2D>("images/bg1");
            //Background bg = new Background(this,spriteBatch,tex,new Vector2(0,0));
            
            //this.Components.Add(bg);

            nameScene = new NameScene(this,spriteBatch);
            this.Components.Add(nameScene);

            startScene = new StartScene(this, spriteBatch);
            this.Components.Add(startScene);

            //create other scenes here;
            actionScene = new AllInOne.ActionScene(this, spriteBatch);
            this.Components.Add(actionScene);

            helpScene = new AllInOne.HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);
            aboutScene = new AllInOne.AboutScene(this, spriteBatch);
            this.Components.Add(aboutScene);
            highScoreScene = new HighScoreScene(this,spriteBatch,tex,myFont);
            this.Components.Add(highScoreScene);
            highScoreScene.Load();
            //credit scene need  to be added
            howToPlayScene = new HowToPlayScene(this,spriteBatch);
            this.Components.Add(howToPlayScene);
            //make the startscene visible-enabled
            nameScene.show();
            // startScene.show();
            bgMusic = Content.Load<Song>("sounds/theme");
            startMusic = Content.Load<Song>("sounds/epic1");
            MediaPlayer.Play(startMusic);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void hideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent  item in this.Components )
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.hide();
                    MediaPlayer.Stop();
                }
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
           

            // TODO: Add your update logic here
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (nameScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.End))
                {
                    startScene.Enabled = true;
                    startScene.Visible = true;
                    nameScene.Enabled = false;
                    nameScene.Visible = false;

                }

            }
           
            if (startScene.Enabled)
            {
                //NameScene.messageString = "";
                selectedIndex = startScene.MyMenuComponent.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    MediaPlayer.Play(bgMusic);
                    MediaPlayer.IsRepeating = true;
                    //hide all scenes
                    
                    //make action scene show
                    actionScene.show();
                }
                if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    //hide all scenes
                    hideAllScenes();
                    // make help scene show
                    helpScene.show();
                    MediaPlayer.Play(startMusic);
                    MediaPlayer.IsRepeating = true;
                }
                if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    
                    //hide all scenes
                    hideAllScenes();
                   
                    s = NameScene.messageString.Replace(" ", "");
                    if (!sat)
                    {
                        highScoreScene.Add(new HighScore(Score.playerScore, s));
                       
                    }

                    highScoreScene.show();
                    MediaPlayer.Play(startMusic);
                    MediaPlayer.IsRepeating = true;
                    //highScoreScene.Draw(gameTime);



                }
                if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    //hide all scenes
                    hideAllScenes();
                    // make help scene show
                    aboutScene.show();
                    MediaPlayer.Play(startMusic);
                    MediaPlayer.IsRepeating = true;
                }
                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    //hide all scenes
                    hideAllScenes();
                    // make help scene show
                    howToPlayScene.show();
                    MediaPlayer.Play(startMusic);
                    MediaPlayer.IsRepeating = true;
                }

                //check other scenes here

                if (selectedIndex == 5 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }


            }

            if (actionScene.Enabled || helpScene.Enabled 
                || highScoreScene.Enabled || howToPlayScene.Enabled || aboutScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                    //MediaPlayer.Stop();
                    MediaPlayer.Play(startMusic);
                    MediaPlayer.IsRepeating = true;

                }
            }




            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

    }
}
