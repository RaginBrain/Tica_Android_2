using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Threading;


namespace Tica_Android_2
{
	public class Coin : Sprite
	{


		protected SoundEffect collect_zvuk;
		public Animation animacija=new Animation();
		protected float coin_scale;
		public int vrijednost;

		public bool pokupljen;
		protected float udaljenost_x;
		protected float udaljenost_y;

		public virtual void Update(Player igrac, float speed_scale, GameTime gameTime,int sirina, float brzina)
		{   animacija.Update (gameTime);

			brzina_kretanja = brzina;
			if (pokupljen) {
				if (rectangle.X >0)
					rectangle.X -= (int)(udaljenost_x / 19);
				if (rectangle.Y > 0)
					rectangle.Y -= (int)(udaljenost_y / 19);

				if (!(rectangle.X > 0) && !(rectangle.Y > 0))
					animacija.active = false;
			}
			speed_buffer += brzina_kretanja*speed_scale;
			if (speed_buffer > 1) {
				rectangle.X -=(int)speed_buffer;
				speed_buffer-=(float)((int)speed_buffer);
			}

			if (rectangle.Intersects (igrac.colision_rect) && pokupljen==false) 
			{

				collect_zvuk.Play ();
				udaljenost_x = (float)(Math.Abs (- rectangle.X) * speed_scale);
				udaljenost_y = (float)(Math.Abs (- rectangle.Y) * speed_scale);
				igrac.score += vrijednost;
				pokupljen = true;

			}

			if (rectangle.X > 0 && rectangle.X < sirina - rectangle.Width && pokupljen==false)
				animacija.active = true;
				
		}
		public Coin()
		{
		}

		public Coin (Texture2D tex,Rectangle rect,float resize_scale,SoundEffect coin_sound)
		{
			collect_zvuk = coin_sound;

			texture = tex;
			rectangle = new Rectangle(rect.X, rect.Y, (int)Math.Round(rect.Width*resize_scale) , (int)Math.Round(rect.Height*resize_scale));
			vrijednost = 10;
			pokupljen = false;

			animacija.active = true;
			animacija.brzina_animacije = 6;
			animacija.broj_framova_sirina = 10;
			animacija.broj_framova_visina = 1;
			animacija.Image = tex;
			animacija.origin = new Vector2 (tex.Width / 20, tex.Height / 2);
			animacija.switchFrame = tex.Width;

			coin_scale = ((float)rect.Width / ((float)texture.Width / 10f));
			speed_buffer = 0;
			brzina_kretanja = (int)3;
		}

		public virtual void Draw(SpriteBatch sp,float scale)
		{
			if(animacija.active)
			sp.Draw (
				texture
				, new Vector2(rectangle.X+(scale*animacija.FrameWith/2),rectangle.Y+(scale*animacija.FrameHeight/2))
				, animacija.suorceRect, Color.White, 0, animacija.origin,(coin_scale*scale), SpriteEffects.None, 0
			);
		}
	}






	public class Diamond:Coin
	{

		public Diamond (Texture2D tex,SoundEffect zvuk,Rectangle rect,float resize_scale,Barijera bar)
		{
			collect_zvuk = zvuk;
			brzina_kretanja = (int)3;
			texture = tex;
			rectangle = new Rectangle(rect.X, rect.Y, (int)Math.Round(rect.Width*resize_scale) , (int)Math.Round(rect.Height*resize_scale));
			vrijednost = 500;
			pokupljen = false;
			speed_buffer = 0;
		}

		public override void Draw (SpriteBatch spriteBatch,float scale)
		{
			spriteBatch.Draw (texture, rectangle,Color.White);
		}

		public override void Update(Player igrac, float speed_scale, GameTime gameTime,int sirina, float brzina)
		{
			if (pokupljen) {
				if (rectangle.X > -rectangle.Width)
					rectangle.X -= (int)(udaljenost_x / 19);
				if (rectangle.Y > -rectangle.Height)
					rectangle.Y -= (int)(udaljenost_y / 19);
			}
			brzina_kretanja = brzina;
			speed_buffer += brzina_kretanja*speed_scale;
			if (speed_buffer > 1) {
				rectangle.X -=(int)speed_buffer;
				speed_buffer-=(float)((int)speed_buffer);
			}

			if (rectangle.Intersects (igrac.colision_rect) && pokupljen==false) 
			{
				collect_zvuk.Play ();
				udaljenost_x = (float)(Math.Abs (- rectangle.X) * speed_scale);
				udaljenost_y = (float)(Math.Abs (- rectangle.Y) * speed_scale);
				igrac.score += vrijednost;
				pokupljen = true;

			}
		}
	}
}

