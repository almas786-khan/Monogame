using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AllInOne
{
    public class ActionScene : GameScene
    {
        

        private SpriteBatch spriteBatch;
        Player ship;
        int enemyBulletDamage;
        Game game1;
        int gameState = 2;
        float timer = 2;         //Initialize a 10 second timer
        const float TIMER = 2;
      
        bool yes = false;

        Random random = new Random();

        List<Asteroids> asteroidList = new List<Asteroids>();
        List<Enemy> enemyShipList = new List<Enemy>();
        List<Explossion> explossionList = new List<Explossion>();
        List<Die> dieList = new List<Die>();

        private SpriteFont font;
        Score score;
        ScrollingBackground sb1;
        Level2Background sb2;
        Texture2D gameoverTex,texL,texR,tex1,levelTex, spaceTex, faceTex;
        private SoundEffect bombSound;
        
        private SoundEffect shotSound;

        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            game1 = game;

            bombSound = game.Content.Load<SoundEffect>("sounds/bomb");
            shotSound = game.Content.Load<SoundEffect>("sounds/gunshot");
            

            texL = game.Content.Load<Texture2D>("images/player");
            texR = game.Content.Load<Texture2D>("images/playerR");
            tex1 = game.Content.Load<Texture2D>("images/1");
            faceTex = game.Content.Load<Texture2D>("images/face");
            Texture2D tex4 = game.Content.Load<Texture2D>("images/ball");
            Texture2D healthTex = game.Content.Load<Texture2D>("images/healthbar");
            gameoverTex = game.Content.Load<Texture2D>("images/gameoverpage");
            sb1 = new ScrollingBackground(game, spriteBatch,
                tex1,
                new Vector2(0, 0),
                new Vector2(0, -410),
                new Vector2(0, 2));

            enemyBulletDamage = 10;


            ship = new Player(game, spriteBatch, texL,texR,faceTex, healthTex, new Vector2(0, tex1.Height-texL.Height), new Vector2(10), tex4,shotSound);
           this.Components.Add(sb1);
            this.Components.Add(ship);
            Player.isVisible = true;

            font = game.Content.Load<SpriteFont>("fonts/myfont");
            score = new Score(game, spriteBatch, font);
            this.Components.Add(score);
            levelTex = game.Content.Load<Texture2D>("images/level");
            spaceTex = game.Content.Load<Texture2D>("images/space");


            sb2 = new Level2Background(game1, spriteBatch,
                                    spaceTex,
                                    new Vector2(0, 0),
                                    new Vector2(0, -410),
                                    new Vector2(0, 2));
            

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case 1:
                    {
                         
                        KeyboardState ks = Keyboard.GetState();
                        if (ks.IsKeyDown(Keys.Enter))
                        {
                            Player.isVisible = true;
                            
                            Score.levelNo = 1;
                            ship.Health = 200;
                            Score.playerScore = 0;
                            gameState = 2; // play level 1
                            enemyShipList.Clear();
                            asteroidList.Clear();
                            dieList.Clear();
                            explossionList.Clear();
                            ship.bulletList.Clear();
                            ship.Position = new Vector2(0, tex1.Height - texL.Height);
                        }

                        break;
                    }
                case 2:

                    {
                        
                        Score.levelNo = 1;

                        //asteroids update, check for collisions

                        foreach (Asteroids a in asteroidList)
                        {
                            if (a.BoundingBox.Intersects(ship.BoundingBox))
                            {
                                //Ship.isVisible = false;
                                //player die
                                dieList.Add(new Die(game1, spriteBatch,
                                    game1.Content.Load<Texture2D>("images/dieUpdated"),
                                    new Vector2(ship.Position.X, ship.Position.Y+15), 5));




                                ship.Position = new Vector2(0, tex1.Height - texL.Height);
                                ship.Health -= 20;
                                a.IsVisible = false;


                            }
                            //check through bullet list
                            for (int i = 0; i < ship.bulletList.Count; i++)
                            {
                                if (a.BoundingBox.Intersects(ship.bulletList[i].BoundingBox))
                                {
                                    bombSound.Play();
                                    explossionList.Add(new Explossion(game1, spriteBatch,
                                        game1.Content.Load<Texture2D>("images/explosion3"),
                                        new Vector2(a.Position.X, a.Position.Y)));
                                    Score.playerScore += 10;
                                    a.IsVisible = false;
                                    ship.bulletList.ElementAt(i).IsVisible = false;
                                }
                            }

                            //die.Update(gameTime);
                            a.Update(gameTime);
                        }
                        if (ship.Health <= 0)
                        {
                            Game1.sat = false;
                            gameState = 1;// gameover
                        }
                        if (Score.playerScore == 200)
                        {
                              gameState = 3; //complete level 1
                            
                        }
                        foreach (Die d in dieList)
                        {
                            d.Update(gameTime);
                        }
                        foreach (Explossion ex in explossionList)
                        {
                            ex.Update(gameTime);
                        }
                        DieManager();
                        ExplossionManager();
                        LoadAsteroids();
                        base.Update(gameTime);
                                                                     
                        break;
                    }
                case 3:
                    {


                        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                        timer -= elapsed;
                        if (timer < 0)
                        {
                            yes = true;
                            //Timer expired, execute action
                            timer = TIMER;   //Reset Timer
                        }
                        //ship.Health = 200;
                        //Score.playerScore = 0;
                        Score.levelNo = 2;
                        //base.Update(gameTime);
                        sb2.Update(gameTime);
                        asteroidList.Clear();
                        dieList.Clear();
                        explossionList.Clear();
                        ship.bulletList.Clear();
                        ship.Position = new Vector2(0, tex1.Height - texL.Height);

                        //gameState = 4; //level 2
                        break;
                    }
                case 4:
                    {
                        foreach (Enemy e in enemyShipList)
                        {
                            //enemyship collision with player ship
                            if (e.BoundingBox.Intersects(ship.BoundingBox))
                            {
                                if (ship.Health != 0)
                                {
                                    ship.Health -= 40;
                                    e.IsVisible = false;
                                }
                            }
                            //enemy bullet collission with player ship
                            for (int i = 0; i < e.bulletList.Count; i++)
                            {
                                if (ship.BoundingBox.Intersects(e.bulletList[i].BoundingBox))
                                {
                                    //player die
                                    dieList.Add(new Die(game1, spriteBatch,
                                        game1.Content.Load<Texture2D>("images/dieUpdated"),
                                        new Vector2(ship.Position.X, ship.Position.Y+15), 5));

                                    ship.Health -= enemyBulletDamage;
                                    e.bulletList[i].IsVisible = false;

                                    e.IsVisible = false;
                                    ship.Position = new Vector2(0, tex1.Height - texL.Height);


                                }
                            }

                            //player bullet collission with enemy ship
                            for (int i = 0; i < ship.bulletList.Count; i++)
                            {
                                if (ship.bulletList[i].BoundingBox.Intersects(e.BoundingBox))
                                {
                                    bombSound.Play();
                                    explossionList.Add(new Explossion(game1, spriteBatch,
                                        game1.Content.Load<Texture2D>("images/explosion3"),
                                        new Vector2(e.Position.X, e.Position.Y)));
                                    Score.playerScore += 20;
                                    ship.bulletList[i].IsVisible = false;
                                    e.IsVisible = false;

                                }
                            }
                            e.Update(gameTime);

                        }

                        foreach (Die d in dieList)
                        {
                            d.Update(gameTime);
                        }

                        foreach (Explossion ex in explossionList)
                        {
                            ex.Update(gameTime);
                        }

                        if (ship.Health <= 0)
                        {
                            Game1.sat = false; // game over
                            gameState = 1;
                           
                        }
                        DieManager();
                        ExplossionManager();
                        LoadAsteroids();
                        LoadEnemy();
                        sb2.Update(gameTime);
                        base.Update(gameTime);
                        break;
                    }
               
            }

                       
           
           
        }

       

        public override void Draw(GameTime gameTime)
        {
            switch (gameState)
            {
                case 1:
                    {
                        GameOver over = new AllInOne.GameOver(game1, spriteBatch, gameoverTex, new Vector2(0, 0),font);
                        over.Draw(gameTime);
                
                        gameState = 2;
                       
                        break;
                    }
                case 2:
                    {
                        sb1.Draw(gameTime);
                        foreach (Die di in dieList)
                        {
                            di.Draw(gameTime);
                        }

                        foreach (Explossion ex in explossionList)
                        {
                            ex.Draw(gameTime);
                        }
                        foreach (Asteroids a in asteroidList)
                        {
                            a.Draw(gameTime);
                        }


                        
                        ship.Draw(gameTime);
                        score.Draw(gameTime);
                        //die.Draw(gameTime);
                        break;
                    }
                case 3:
                    {
                        Level level1 = new Level(game1, spriteBatch, levelTex, new Vector2(0, 0));
                        level1.Draw(gameTime);
                        if (yes)
                        {

                            sb2.Draw(gameTime);

                            ship.Draw(gameTime);
                            score.Draw(gameTime);
                            yes = false;
                            gameState = 4;
                        }
                       
                        break;
                    }
                case 4:
                    {
                        sb2.Draw(gameTime);
                        foreach (Die di in dieList)
                        {
                            di.Draw(gameTime);
                        }

                        foreach (Explossion ex in explossionList)
                        {
                            ex.Draw(gameTime);
                        }
                        foreach (Enemy e in enemyShipList)
                        {
                            e.Draw(gameTime);
                        }
                        //base.Draw(gameTime);

                        ship.Draw(gameTime);
                        score.Draw(gameTime);
                        //die.Draw(gameTime);
                        break;
                    }
            }
            //base.Draw(gameTime);

        }

        public void LoadAsteroids()
        {
            int randX = random.Next(10, 450);
            int randY = random.Next(-300, -50);

            if (asteroidList.Count < 8)
            {
                asteroidList.Add(new Asteroids(game1, spriteBatch
                    , game1.Content.Load<Texture2D>("images/asteroid"), new Vector2(randX, randY)));
            }

            for (int i = 0; i < asteroidList.Count; i++)
            {
                if (!asteroidList[i].IsVisible)
                {
                    asteroidList.RemoveAt(i);
                    i--;
                }
            }
        }
        public void LoadEnemy()
        {
            int randX = random.Next(50, 450);
            int randY = random.Next(-300, -50);

            if (enemyShipList.Count < 5)
            {
                enemyShipList.Add(new Enemy(game1, spriteBatch
                    , game1.Content.Load<Texture2D>("images/enemy"), new Vector2(randX, randY), 5,
                    game1.Content.Load<Texture2D>("images/enemyBullet")));
            }

            for (int i = 0; i < enemyShipList.Count; i++)
            {
                if (!enemyShipList[i].IsVisible)
                {
                    enemyShipList.RemoveAt(i);
                    i--;
                }
            }
        }
        public void DieManager()
        {
            for (int i = 0; i < dieList.Count; i++)
            {
                if (!dieList[i].IsVisible)
                {
                    dieList.RemoveAt(i);
                    i--;
                }

            }


        }
        public void ExplossionManager()
        {
            for (int i = 0; i < explossionList.Count; i++)
            {
                if (!explossionList[i].IsVisible)
                {
                    explossionList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}

