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
    public class Asteroids : DrawableGameComponent
    {

        Rectangle boundingBox;
        Texture2D tex;
        Vector2 position;
       
        SpriteBatch spriteBatch;
    
        int speed;

        bool isVisible;
        Random random = new Random();
        //float randX, randY;
      //  randX = random.Next(50, 450);
          //  randY = random.Next(-300, -50);
       

        public bool IsVisible
        {
            get
            {
                return isVisible;
            }

            set
            {
                isVisible = value;
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return boundingBox;
            }

            set
            {
                boundingBox = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public Asteroids(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 newPosition) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            speed = 2;
            isVisible = true;
           
            position = newPosition;
           
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X,
                (int)position.Y, 45, 45);

           



            position.Y = position.Y + speed;
            if (position.Y >= 420)
            {
                position.Y = -50;
            }
          
            base.Update(gameTime);

           
        }

        public override void Draw(GameTime gameTime)
        {
            if (isVisible)
            {

                spriteBatch.Begin();


                spriteBatch.Draw(tex, position, null, Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }




    }
}

