using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Tica_Android_2
{
	public class Animation
	{
		public int frameCounter;
		public int switchFrame;

		public Vector2 position;
		Vector2 currentFrame;
		public Texture2D Image;
		public Rectangle suorceRect;
		public bool active;
		public Vector2 origin; //postavlja se iz klase Igrac

		public Vector2 CurrentFrame
		{
			get { return currentFrame; }
			set { currentFrame = value; }
		}

		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		public int FrameWith
		{
			get { return Image.Width / 5; }
		}

		public int FrameHeight
		{
			get { return Image.Height / 3; }
		}





		public void Initialize()
		{
			active = false;

			switchFrame = Image.Width;
		}

		public void Update(GameTime gameTime)
		{
			if (active)
				frameCounter += 7*(int)gameTime.ElapsedGameTime.TotalMilliseconds;
			else
				frameCounter = 0;
			//uvjeti za pribacivanje
			if (frameCounter >= switchFrame)
			{
				frameCounter = 0;
				currentFrame.X += Image.Width/5;
				if (currentFrame.X >= Image.Width)
				{
					currentFrame.X =0;
					currentFrame.Y +=Image.Height/3;
				}
				if (currentFrame.Y >= Image.Height)
					currentFrame.Y = 0;
			}


			suorceRect = new Rectangle((int)currentFrame.X, (int)currentFrame.Y, FrameWith, FrameHeight);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Image, position, suorceRect, Color.White);
		}

	}
}
