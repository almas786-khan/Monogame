using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllInOne
{
    public class HighScoreScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        SpriteFont font;
        Game game;
        //Scores - connected to a name 
        //name
        //points
        public static List<HighScore> myHighScoreList;
           
            public HighScoreScene(Game game,
                SpriteBatch spriteBatch,
                Texture2D tex,
                SpriteFont font) : base(game)
            {
                this.font = font;
               this.game = game;
                this.spriteBatch = spriteBatch;
                this.tex = tex;
                myHighScoreList = new List<HighScore>();
                Console.WriteLine(myHighScoreList.Count);
                //Load();
            }

            //add a new score (and keep the scores in order)
            public bool Add(HighScore myNewestScore)
            {
                bool returnValue = true;
            
                if (!myHighScoreList.Contains(myNewestScore))
                {
                    myHighScoreList.Add(myNewestScore);
                    myHighScoreList.Sort();
                }


                if (myHighScoreList.Count > 10)
                    myHighScoreList.RemoveAt(10);
            Game1.sat = true;
                Save();
           
                return returnValue;
            }
            //save high score
            public static bool Save()
            {
                bool returnValue = true;

                try
                {
                    StreamWriter writer = new StreamWriter("saveFile.txt");

                    int n = myHighScoreList.Count;
                    writer.WriteLine(n);

                    for (int i = 0; i < n; i++)
                    {
                        writer.WriteLine(myHighScoreList.ElementAt(i).Points);
                        writer.WriteLine(myHighScoreList.ElementAt(i).Name);
                    }

                    writer.Close();
                }
                catch (Exception e)
                {
                    returnValue = false;
                }

           
            return returnValue;
            
            }

            public bool Load()
            {
                bool returnValue = true;

                try
                {
                    StreamReader reader = new StreamReader("saveFile.txt");

                    int n = int.Parse(reader.ReadLine());

                    for (int i = 0; i < n; i++)
                    {
                        int points = int.Parse(reader.ReadLine());
                        string name = reader.ReadLine();
                        myHighScoreList.Add(new HighScore(points, name));
                    }

                    reader.Close();
                
                }
                catch (Exception e)
                {
                    returnValue = false;
                }


                return returnValue;
            }
        //load high score


       
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
            spriteBatch.Draw(game.Content.Load<Texture2D>("images/scorepage"), Vector2.Zero, Color.White);
            Vector2 test = new Vector2(20,20);
            foreach (HighScore item in myHighScoreList)
            {
                spriteBatch.DrawString(font, item.Name +"  :  "+ Convert.ToString(item.Points),
                    new Vector2(160,40)+test, Color.White);
                test.Y += 30;

            }
            spriteBatch.End();

            



            base.Draw(gameTime);
        }
    }
    }

