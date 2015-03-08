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
	public  class CoinWizzard
	{
		public float chance_buffer;
		Random r = new Random();
		protected Texture2D tex_coin;
		protected Texture2D tex_dijamant;
		protected SoundEffect coin_zvuk;


		public CoinWizzard(SoundEffect zvuk,Texture2D tex_c,Texture2D tex_d)
		{
			coin_zvuk = zvuk;
			tex_coin=tex_c;
			tex_dijamant=tex_d;
			chance_buffer = 0;
		}

		public void Update()
		{
			chance_buffer += 0.03f;
		}
		public void Ubaci_Coine(List<Coin> Coini, int visina, float resize_scale,Barijera bar)
		{	
			int opcija;
			int gornji_prostor,donji_prostor;
			bool dijamant_in;
			opcija = r.Next (1,99);

			if (bar.coiniziran==false) 
			{
				dijamant_in = false;
				gornji_prostor = bar.rectangle.Y;
				donji_prostor = (int)((visina - (visina / 4.35)) - (bar.rectangle.Y + bar.rectangle.Height));

				if ((donji_prostor > gornji_prostor && gornji_prostor < (100 * resize_scale) && gornji_prostor > (45 * resize_scale))
					||(donji_prostor < gornji_prostor && donji_prostor < (85 * resize_scale)&& donji_prostor > (30 * resize_scale)))
					dijamant_in=true;

				if (bar.tip == "obicna" && opcija < 3 * chance_buffer && dijamant_in) {
					Dijamant (Coini, donji_prostor, gornji_prostor, tex_dijamant, resize_scale, visina, bar);
					chance_buffer = -5f;
				}

				else if((bar.tip=="obicna")  && opcija<chance_buffer){
					Dijamant_norm (Coini, donji_prostor, gornji_prostor, tex_dijamant, resize_scale, visina,bar);
					chance_buffer=-5f;
					}
				else if (opcija <50)
					Skup_Coina (Coini, donji_prostor, gornji_prostor, tex_coin, resize_scale, visina,r.Next(1,5),r.Next(1,7),bar);
				else
					Skup_Coina (Coini, donji_prostor, gornji_prostor, tex_coin, resize_scale, visina,r.Next(2,3),r.Next(2,6),bar);
			}
		}



		public void Skup_Coina (List<Coin> Coini,int donji_p,int gornji_p,Texture2D tex,float resize_scale,int visina,int redovi,int stupci,Barijera bar)
		{
			int pocetni_Y;
			int pocetni_X;
			int temp_X;
			int temp_Y;

			if (donji_p < gornji_p)
				pocetni_Y = r.Next (0, bar.rectangle.Y - (int)(redovi*30 * resize_scale));
			else
				pocetni_Y=r.Next((bar.rectangle.Y+bar.rectangle.Height),(int)(visina - (visina / 4.35))-(int)(redovi*30 * resize_scale));

			pocetni_X = (bar.rectangle.X + bar.rectangle.Width / 2) - (int)(5 * 33 * resize_scale);

			temp_X = pocetni_X;
			temp_Y = pocetni_Y;

			for(int i=0;i<redovi;i++)
			{
				temp_Y=pocetni_Y+(int)(i*33*resize_scale);
				for(int j=0;j<stupci;j++)
				{
					temp_X=pocetni_X+(int)(j*34*resize_scale);
					Coini.Add (new Coin (tex, new Rectangle (temp_X, temp_Y, 33, 30), resize_scale,coin_zvuk));
				}
			}
		}
		public void Dijamant (List<Coin> Coini,int donji_p,int gornji_p,Texture2D tex,float resize_scale,int visina,Barijera bar)
		{
			int pocetni_Y;
			int pocetni_X;
			int temp_X;
			int temp_Y;


			if (donji_p > gornji_p && gornji_p < (100 * resize_scale) && gornji_p > (45 * resize_scale))
				pocetni_Y = r.Next (0, bar.rectangle.Y-(int)(35*resize_scale));
			else if (donji_p < gornji_p && donji_p < (85 * resize_scale)&& donji_p > (30 * resize_scale))
				pocetni_Y = r.Next ((bar.rectangle.Y + bar.rectangle.Height), (int)(visina - (visina / 4.35)));
			else
				pocetni_Y = visina*2;
			pocetni_X = (bar.rectangle.X + bar.rectangle.Width / 2);

			temp_X =r.Next( (int)(pocetni_X-170*resize_scale),pocetni_X+(int)(150*resize_scale));
			temp_Y = pocetni_Y;

			Coini.Add (new Diamond (tex, new Rectangle (temp_X, temp_Y, 55, 50), resize_scale,bar));
		}


		public void Dijamant_norm (List<Coin> Coini,int donji_p,int gornji_p,Texture2D tex,float resize_scale,int visina,Barijera bar)
		{

			int temp_X;
			int temp_Y;

			if(donji_p%2==0)
				temp_X = bar.rectangle.X +(int)(r.Next(5+bar.rectangle.Width,20+bar.rectangle.Width)*resize_scale);
			else
				temp_X = bar.rectangle.X -(int)(r.Next(75,85)*resize_scale);
			temp_Y = bar.rectangle.Y+ bar.rectangle.Height/2;


			Coini.Add (new Diamond (tex, new Rectangle (temp_X, temp_Y, 55, 50), resize_scale,bar));
		}
	}

	
}
