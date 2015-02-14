using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;


namespace Tica_Android_2
{
	public class Sprite
	{

		public int brzina_kretanja;
		public Texture2D texture;
		public Rectangle rectangle;
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, rectangle, Color.White);
		}

	}

	class Scrolling : Sprite
	{
		public Scrolling(Texture2D newTexture, Rectangle newRectangle,int brzina)
		{
			texture = newTexture;
			rectangle = newRectangle;
			brzina_kretanja = brzina;
		}

		public void Update()
		{
			rectangle.X -= brzina_kretanja;
		}

	}

	public  class Player : Sprite
	{
		public Animation playerAnimation=new Animation();

		public TouchCollection touchCollection;
		public Texture2D texture_stit;
		public float speed;

		private Vector2 Velocity;

		public int score;
		public bool alive;
		public bool stit;



		private int udajenost_X;
		private int udaljenost_Y;
		private Point centar;


		public float Speed
		{
			get { return speed; }
			set { speed = value; }
		}

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime,int visina_ekrana, int duljina)
		{

			playerAnimation.active = true;

			//kretanje sa ubrzanim gibanjem *******************************************
			touchCollection=TouchPanel.GetState();

			foreach (TouchLocation tl in touchCollection) 
			{

				if ((tl.State == TouchLocationState.Pressed)
					|| (tl.State == TouchLocationState.Moved))
				{
					centar = rectangle.Center;
					udaljenost_Y =(int) Math.Abs (centar.Y - tl.Position.Y);
					udajenost_X =(int) Math.Abs (centar.X - tl.Position.X);


					float faktor_X;
					float faktor_Y;

					if (udajenost_X / 100 < 1f)
						faktor_X = 0.5f + udajenost_X / (100);
					else
						faktor_X = 1.5f;

					if (udajenost_X / 100 < 1f)
						faktor_Y = 0.5f + udaljenost_Y / (100);
					else
						faktor_Y = 1.5f;



					if (tl.Position.Y < centar.Y-rectangle.Height/2)
					{
						if (Velocity.Y > 0)
							Velocity.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f*faktor_Y;
						Velocity.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
					}

					else if(tl.Position.Y>centar.Y+rectangle.Height/2)
					{
						if (Velocity.Y < 0)
							Velocity.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f*faktor_Y;
						Velocity.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds*faktor_Y;
					}

					if (tl.Position.X < centar.X-rectangle.Width/2) 
					{
						if (Velocity.X > 0)
							Velocity.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f*faktor_X;
						Velocity.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds*faktor_X;
					}
					else if(tl.Position.X > centar.X+rectangle.Width/2)
					{
						if (Velocity.X < 0)
							Velocity.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f*faktor_X;
						Velocity.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds*faktor_X;
					}
				}

			}



			rectangle.X += (int)(Velocity.X );
			rectangle.Y += (int)(Velocity.Y);
			//kretanje zavrsava ovdje ***************************************************

			if (alive)
				score += 1;

			//uvjet za odbijanje, svi faktori su empiristički uštimani*******************
			if (rectangle.Y > visina_ekrana - visina_ekrana/4.3f)
			{
				rectangle.Y -=(int) (Velocity.Y * 2.5f);
				Velocity.Y = -Velocity.Y * 0.65f;
			}
			if (rectangle.Y < -rectangle.Height/2)
			{
				rectangle.Y -= (int)(Velocity.Y * 2.5f);
				Velocity.Y = -Velocity.Y * 0.65f;
			}

			if ((rectangle.X < -5 && Velocity.X < 0) || rectangle.X>duljina-rectangle.Width && Velocity.X>0)
				Velocity.X = 0;



			//***************************************************************************




			playerAnimation.Position=new Vector2(rectangle.Location.X,rectangle.Location.Y-(int)(rectangle.Height)/3);
			playerAnimation.Update(gameTime);
		}


		public Player(Texture2D tex,Texture2D tex_stit,Rectangle rect)
		{
			speed = 6;
			texture = tex;
			texture_stit = tex_stit;
			rectangle = rect;
			alive = true;
			score = 0;
			stit = false;
			playerAnimation.Image = tex;
		}
	}


	public class Znamenka:Sprite
	{
		public bool vidljiv;
		public int broj;
		public int pozicija;
		public List<Texture2D> lista_textura;
		public Znamenka()
		{
			lista_textura = new List<Texture2D> ();
		}

	}

	public class Score
	{

		public List<Znamenka> lista_znamenki;

		public void Update(int rezultat)
		{
			string s = rezultat.ToString();
			for (int i = 0; i < 7; i++)
			{
				if (i < s.Length) 
				{
					lista_znamenki [i].vidljiv = true;
					int ind = int.Parse (s [i].ToString ());
					lista_znamenki [i].pozicija = i;
					lista_znamenki [i].broj = ind;
					lista_znamenki [i].texture = lista_znamenki [i].lista_textura [ind];
				} 
				else
					lista_znamenki [i].vidljiv = false;
			}
		}

		public Score(List<Texture2D> teksture)
		{
			lista_znamenki = new List<Znamenka> ();
			for (int i = 0; i < 7; i++)
			{
				Znamenka x = new Znamenka ();
				x.lista_textura = teksture;
				lista_znamenki.Add (x);
			} 
		}


		public void Draw(SpriteBatch sb,int x,int y, int sirina,int visina)
		{
			int poz_X = x;
			foreach(Znamenka z in lista_znamenki)
			{
				if (z.vidljiv) 
				{
					sb.Draw (z.texture, new Rectangle (poz_X, y, sirina, visina), Color.White);
					poz_X += sirina;
				}
			}
		}


	}
}
