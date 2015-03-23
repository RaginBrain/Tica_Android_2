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



	class Stit : Sprite
	{

		public void Update(Player igrac, int broj, LevelUp lvl, float speed_scale)
		{

			speed_buffer += brzina_kretanja*speed_scale;
			if (speed_buffer > 1) {
				rectangle.X -=(int)speed_buffer;
				speed_buffer-=(float)((int)speed_buffer);
			}

			if (rectangle.Intersects(igrac.colision_rect))
			{
				if (igrac.stit == true)
					igrac.score += 500;
				igrac.stit = true;
				igrac.colision_rect = new Rectangle (igrac.colision_rect.X, igrac.colision_rect.Y, (int)(igrac.rectangle.Width * 0.9f), (int)(igrac.rectangle.Height * 0.8f));
				rectangle.Y = (int)(1000*speed_scale);
			}

			if (rectangle.X < (-50*speed_scale))
			{

				rectangle.X = (int)((1500*speed_scale + (int)(lvl.bonus * 1.8))*speed_scale);
				rectangle.Y = broj;
			}
		}
		public Stit(Texture2D tex, Rectangle rect,float resize_scale)
		{
			texture = tex;
			rectangle =  new Rectangle((int)(rect.X*resize_scale),(int)(rect.Y*resize_scale), (int)Math.Round(rect.Width*resize_scale) , (int)Math.Round(rect.Height*resize_scale));
			brzina_kretanja = (int)3;
			speed_buffer = 0;

		}

	}
	class LevelUp : Sprite
	{

		public int level;
		public int bonus;
		public void Update(Player igrac, int broj, ref int udaljenost_barijera,float speed_scale)
		{
			speed_buffer += brzina_kretanja*speed_scale;
			if (speed_buffer > 1) {
				rectangle.X -=(int)speed_buffer;
				speed_buffer-=(float)((int)speed_buffer);
			}


			if (rectangle.X < (-50*speed_scale))
			{
				rectangle.X =(int)((1500+bonus)*speed_scale);
				bonus += (int)(bonus*1.6);
				level += 1;
				udaljenost_barijera = (int)(300*speed_scale)+(int)(15*level*speed_scale);
			}
		}
		public LevelUp(Texture2D tex, Rectangle rect,float resize_scale)
		{
			texture = tex;
			rectangle =  new Rectangle((int)(resize_scale*rect.X), rect.Y, (int)Math.Round(rect.Width*resize_scale) , (int)Math.Round(rect.Height*resize_scale));
			brzina_kretanja = (int)3;
			level = 1;
			bonus = (int)(400*resize_scale);
			speed_buffer = 0;
		}
	}
	public class Barijera : Sprite
	{
		public string tip;
		protected CoinWizzard CoinWizz;
		protected Random r = new Random();
		public bool coiniziran;

		public Barijera(Texture2D tex, Rectangle rect,float resize_scale,CoinWizzard CoinW)
		{
			tip="obicna";
			CoinWizz = CoinW;
			texture = tex;
			coiniziran = false;
			rectangle =  new Rectangle(rect.X, (int)(resize_scale*rect.Y), (int)Math.Round(rect.Width*resize_scale) , (int)Math.Round(rect.Height*resize_scale));
			brzina_kretanja =(int) 3;
			speed_buffer = 0;
		}
		public Barijera()
		{
		}

		public virtual void Update(Player igrac,ref int dodatak,int visina,int sirina,List<Barijera> lista, float speed_scale,Song stit_off,List<Coin> lista_c)
		{


			speed_buffer += brzina_kretanja*speed_scale;
			if (speed_buffer > 1) {
				rectangle.X -=(int)speed_buffer;
				speed_buffer-=(float)((int)speed_buffer);
			}

			if (Dodir (igrac))
			if (igrac.stit == true) {
				rectangle.Y = -2 * visina;
				igrac.colision_rect = new Rectangle (igrac.colision_rect.X, igrac.colision_rect.Y, (int)(igrac.rectangle.Width * 0.8f), (int)(igrac.rectangle.Height * 0.6f));
				igrac.stit = false;
			} else {
				if ((rectangle.Center.X) > igrac.colision_rect.Center.X)
					igrac.ubodena_s_liva = true;
				igrac.alive = false;
			}



			if (rectangle.X < (-50*speed_scale))
			{
				igrac.score+=25;
				rectangle.X = lista[lista.Count-1].rectangle.X+dodatak;
				int visina_zadnje = lista [lista.Count - 1].rectangle.Y;
				if (visina_zadnje > visina / 2)
					rectangle.Y = r.Next (0, (int)(visina/2)-rectangle.Height);
				else
					rectangle.Y = r.Next ((int)(visina/2), (int)(visina-visina/4.3f - rectangle.Height));

				lista.RemoveAt (0);
				lista.Add (this);
				dodatak = (int)(dodatak * 0.985f);
				CoinWizz.Ubaci_Coine (lista_c, visina, speed_scale,this);
			}
		}


		public bool Dodir(Player igrac)
		{
			if( rectangle.Intersects(igrac.colision_rect))
				return true;
			else
				return false;
		}
	}



	class PokretnaBarijera : Barijera
	{
		float buffer2;
		int visina;
		public bool gori;
		public int brzina_gibanja;

		public PokretnaBarijera(Texture2D tex, Rectangle rect,bool x,int vis,float resize_scale,CoinWizzard Wizz)
		{
			CoinWizz = Wizz;
			gori = x;
			texture = tex;
			rectangle =  new Rectangle(rect.X, rect.Y, (int)Math.Round(rect.Width*resize_scale) , (int)Math.Round(rect.Height*resize_scale));
			brzina_kretanja = 3;
			buffer2 = 0;
			brzina_gibanja = r.Next(0, 3);
			visina = vis;

		}

		public override void Update(Player igrac,ref int dodatak,int visina,int sirina,List<Barijera> lista, float speed_scale,Song stit_off,List<Coin> lista_c)
		{
			base.Update (igrac,ref dodatak,visina,sirina,lista,speed_scale,stit_off,lista_c);
			if (rectangle.X < -20)
			{
				brzina_gibanja = r.Next(0,3);
			}
			if (rectangle.Y > visina- visina/3)
				gori=true;
			if (rectangle.Y < 0)
				gori = false;

			buffer2 += brzina_gibanja*speed_scale;
			if (buffer2 > 1) 
			{
				if (gori)
					rectangle.Y-=(int)buffer2;
				else
					rectangle.Y += (int)buffer2;
				buffer2-=(float)((int)buffer2);

			}

		}
	}
}
