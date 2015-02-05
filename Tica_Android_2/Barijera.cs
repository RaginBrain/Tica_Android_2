using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Tica_Android_2
{



	class Stit : Sprite
	{

		public void Update(Player igrac, int broj, LevelUp lvl)
		{
			rectangle.X -= brzina_kretanja+1;
			if (rectangle.Intersects(igrac.rectangle))
			{
				igrac.stit = true;
				rectangle.Y = 1000;
			}

			if ((rectangle.X < -50) && igrac.stit==false)
			{
				rectangle.X = 2000 + (int)(lvl.bonus * 1.8);
				rectangle.Y = broj;
			}
		}
		public Stit(Texture2D tex, Rectangle rect)
		{
			texture = tex;
			rectangle = rect;
			brzina_kretanja = (int)3;

		}

	}
	class LevelUp : Sprite
	{

		public int level;
		public int bonus;
		public void Update(Player igrac, int broj)
		{
			rectangle.X -= brzina_kretanja;


			if (rectangle.X < -50)
			{
				rectangle.X =1000+bonus;
				bonus += (int)(bonus*1.4);
				rectangle.Y = broj;
				level += 1;

			}
		}
		public LevelUp(Texture2D tex, Rectangle rect)
		{
			texture = tex;
			rectangle = rect;
			brzina_kretanja = (int)3;
			level = 1;
			bonus = 400;
		}
	}
	class Barijera : Sprite
	{
		protected Random r = new Random();

		public virtual void Update(Player igrac,int broj,int udaljenost,int sirina)
		{


			rectangle.X -= brzina_kretanja;
			if (Dodir(igrac))
			if (igrac.stit == true)
			{
				rectangle.Y = udaljenost;
				igrac.stit = false;
			}
			else
				igrac.alive = false;

			if (rectangle.X < -50)
			{
				rectangle.X = sirina;
				rectangle.Y = broj;
			}
		}
		public Barijera(Texture2D tex, Rectangle rect)
		{
			texture = tex;
			rectangle = rect;
			brzina_kretanja =(int) 3;
		}
		public Barijera()
		{
		}

		public bool Dodir(Player igrac)
		{
			if( rectangle.Intersects(igrac.rectangle))
				return true;
			else
				return false;
		}
	}

	class PokretnaBarijera : Barijera
	{

		int visina;
		public bool gori;
		public int brzina_gibanja;
		public PokretnaBarijera(Texture2D tex, Rectangle rect,bool x,int v)
		{
			gori = x;
			texture = tex;
			rectangle = rect;
			brzina_kretanja = 3;
			brzina_gibanja = r.Next(1, 4);
			visina = v;

		}

		public override void Update(Player igrac,int broj,int udaljenost,int sirina)
		{
			base.Update (igrac,broj,udaljenost,sirina);
			if (rectangle.X < -20)
			{
				brzina_gibanja = r.Next(1, 3);
			}
			if (rectangle.Y > visina- visina/3)
				gori=true;
			if (rectangle.Y < 0)
				gori = false;

			if (gori)
				rectangle.Y -= brzina_gibanja;
			else
				rectangle.Y += brzina_gibanja;
		}
	}
}
