using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace Tica_Android_2
{
	public class Coin : Sprite
	{
		public Animation animacija=new Animation();
		float coin_scale;
		public int vrijednost;

		public bool pokupljen;
		float udaljenost_x;
		float udaljenost_y;

		public void Update(Player igrac, float speed_scale, GameTime gameTime,int sirina)
		{   animacija.Update (gameTime);


			if (pokupljen) {
				if (rectangle.X > (30 * speed_scale))
					rectangle.X -= (int)(udaljenost_x / 19);
				if (rectangle.Y > (80 * speed_scale))
					rectangle.Y -= (int)(udaljenost_y / 19);

				if (!(rectangle.X > (30 * speed_scale)) && !(rectangle.Y > (80 * speed_scale)))
					animacija.active = false;
			}
			speed_buffer += brzina_kretanja*speed_scale;
			if (speed_buffer > 1) {
				rectangle.X -=(int)speed_buffer;
				speed_buffer-=(float)((int)speed_buffer);
			}

			if (rectangle.Intersects (igrac.colision_rect) && pokupljen==false) {

				udaljenost_x = (float)(Math.Abs ((30f * speed_scale) - rectangle.X) * speed_scale);
				udaljenost_y = (float)(Math.Abs ((80f * speed_scale) - rectangle.Y) * speed_scale);
				igrac.score += vrijednost;
				pokupljen = true;

			}

			if (rectangle.X > 0 && rectangle.X < sirina - rectangle.Width && pokupljen==false)
				animacija.active = true;
				
		}

		public Coin (Texture2D tex,Rectangle rect,float resize_scale)
		{
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

		public void Draw(SpriteBatch sp,float scale)
		{
			if(animacija.active)
			sp.Draw (
				texture
				, new Vector2(rectangle.X+(scale*animacija.FrameWith/2),rectangle.Y+(scale*animacija.FrameHeight/2))
				, animacija.suorceRect, Color.White, 0, animacija.origin,(coin_scale*scale), SpriteEffects.None, 0
			);
				
		}
	}
}

