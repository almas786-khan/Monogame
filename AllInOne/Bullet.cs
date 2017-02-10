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
    public class Bullet
    {

        Rectangle boundingBox;
        Texture2D texBullet;

        private Vector2 position;
        bool isVisible;
        float speed;

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

        public float Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
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

        public Texture2D TexBullet
        {
            get
            {
                return texBullet;
            }

            set
            {
                texBullet = value;
            }
        }

        public Bullet(Texture2D newTexture)
        {
            speed = 10;
            texBullet = newTexture;
            isVisible = false;

        }
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texBullet, position, Color.White);
        }
    }
}

