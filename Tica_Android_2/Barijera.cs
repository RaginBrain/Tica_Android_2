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
		protected Random r = new Random();
		public bool coiniziran;
		public Texture2D coin_txt;
		public virtual void Update(Player igrac,ref int dodatak,int visina,int sirina,List<Barijera> lista, float speed_scale,Song stit_off,List<Coin> lista_c)
		{


			speed_buffer += brzina_kretanja*speed_scale;
			if (speed_buffer > 1) {
				rectangle.X -=(int)speed_buffer;
				speed_buffer-=(float)((int)speed_buffer);
			}

			if (Dodir (igrac))
			if (igrac.stit == true) {
				MediaPlayer.Play (stit_off);
				rectangle.Y = 2 * visina;
				igrac.colision_rect = new Rectangle (igrac.colision_rect.X, igrac.colision_rect.Y, (int)(igrac.rectangle.Width * 0.8f), (int)(igrac.rectangle.Height * 0.6f));
				igrac.stit = false;
			}
			else
				igrac.alive = false;



			if (rectangle.X < (-50*speed_scale))
			{
				igrac.score+=25;
				rectangle.X = lista[lista.Count-1].rectangle.X+dodatak;
				int visina_zadnje = lista [lista.Count - 1].rectangle.Y;
				if (visina_zadnje > visina / 2)
					rectangle.Y = r.Next (0, (int)(visina/2)-rectangle.Height);
				else
					rectangle.Y = r.Next ((int)(visina/2), (int)(visina-visina/4.7f));

				lista.RemoveAt (0);
				lista.Add (this);
				dodatak = (int)(dodatak * 0.985f);
				Ubaci_Coine (lista_c, visina, coin_txt, speed_scale);
			}
		}
		public Barijera(Texture2D tex, Rectangle rect,float resize_scale, Texture2D coin)
		{
			texture = tex;
			coin_txt = coin;
			coiniziran = false;
			rectangle =  new Rectangle(rect.X, (int)(resize_scale*rect.Y), (int)Math.Round(rect.Width*resize_scale) , (int)Math.Round(rect.Height*resize_scale));
			brzina_kretanja =(int) 3;
			speed_buffer = 0;

		}
		public Barijera()
		{
		}

		public bool Dodir(Player igrac)
		{
			if( rectangle.Intersects(igrac.colision_rect))
				return true;
			else
				return false;
		}



		//COINI
		public void Ubaci_Coine(List<Coin> Coini, int visina,Texture2D tex, float resize_scale)
		{	int opcija;
			int gornji_prostor,donji_prostor;

			opcija = r.Next (1,99);
			if (coiniziran==false) 
			{
				gornji_prostor = rectangle.Y;
				donji_prostor = (int)((visina - (visina / 4.35)) - (rectangle.Y + rectangle.Height));

			if(opcija<60)
				Skup_Coina (Coini, donji_prostor, gornji_prostor, tex, resize_scale, visina,r.Next(1,5),r.Next(1,7));
			else
				Skup_Coina (Coini, donji_prostor, gornji_prostor, tex, resize_scale, visina,r.Next(2,3),r.Next(2,6));


			}
		}

		public void Skup_Coina (List<Coin> Coini,int donji_p,int gornji_p,Texture2D tex,float resize_scale,int visina,int redovi,int stupci)
		{

			int pocetni_Y;
			int pocetni_X;
			int temp_X;
			int temp_Y;



			if (donji_p < gornji_p)
				pocetni_Y = r.Next (0, rectangle.Y - (int)(redovi*30 * resize_scale));
			else
				pocetni_Y=r.Next((rectangle.Y+rectangle.Height),(int)(visina - (visina / 4.35))-(int)(redovi*30 * resize_scale));

			pocetni_X = (rectangle.X + rectangle.Width / 2) - (int)(5 * 33 * resize_scale);

			temp_X = pocetni_X;
			temp_Y = pocetni_Y;

			for(int i=0;i<redovi;i++)
			{
				temp_Y=pocetni_Y+(int)(i*33*resize_scale);
				for(int j=0;j<stupci;j++)
				{
					temp_X=pocetni_X+(int)(j*34*resize_scale);
					Coini.Add (new Coin (tex, new Rectangle (temp_X, temp_Y, 33, 30), resize_scale));
				}
			}
		}

	}

	class PokretnaBarijera : Barijera
	{
		float buffer2;
		int visina;
		public bool gori;
		public int brzina_gibanja;
		public PokretnaBarijera(Texture2D tex, Rectangle rect,bool x,int vis,float resize_scale,Texture2D coin)
		{
			gori = x;
			coin_txt = coin;
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
