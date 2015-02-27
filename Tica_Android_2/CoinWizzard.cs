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
		int opcija;
		Random r= new Random();

		public void Ubaci_Coine(List<Coin> Coini,List<Barijera> lista, int visina,Texture2D tex, float resize_scale)
		{
			int gornji_prostor,donji_prostor;
			opcija = r.Next (1,99);
			if (lista [lista.Count-1].coiniziran==false) 
			{
				gornji_prostor = (lista [lista.Count-1].rectangle.Y);
				donji_prostor = (int)((visina - (visina / 4.35)) - (lista [lista.Count-1].rectangle.Y) + (lista [lista.Count-1].rectangle.Height));


				Skup_Coina (Coini,(Barijera)lista [lista.Count-1], donji_prostor, gornji_prostor, tex, resize_scale, visina,4,7);
			}
		}

		public void Skup_Coina (List<Coin> Coini, Barijera bar,int donji_p,int gornji_p,Texture2D tex,float resize_scale,int visina,int redovi,int stupci)
		{

			int pocetni_Y;
			int pocetni_X;
			int temp_X;
			int temp_Y;



			if (donji_p > gornji_p)
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
					Coini.Add (new Coin (tex, new Rectangle (temp_X, temp_Y, 33, 30), resize_scale));
				}
			}
		}
	}
}
