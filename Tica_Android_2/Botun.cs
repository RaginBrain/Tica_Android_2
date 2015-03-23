using System;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
namespace Tica_Android_2
{
	public class Botun:Sprite
	{
		private Texture2D clicked_tex;
		private Texture2D selected_tex;
		private Rectangle pozicija_dodira;
		public TouchCollection touchCollection;

		public bool clicked;
		public Botun (Texture2D tex,Texture2D clicked, Rectangle rect,float scale)
		{
			clicked_tex = clicked;
			rectangle = new Rectangle (rect.X, rect.Y, (int)(rect.Width * scale), (int)(rect.Height * scale));
			texture = tex;
			selected_tex = texture;
		}

		public virtual void Update()
		{
			touchCollection = TouchPanel.GetState ();
			foreach (TouchLocation tl in touchCollection) 
			{
				if ((tl.State == TouchLocationState.Pressed) || (tl.State == TouchLocationState.Moved)) 
				{
					pozicija_dodira = new Rectangle ((int)tl.Position.X, (int)tl.Position.Y, 1, 1);
					if (tl.State == TouchLocationState.Pressed && rectangle.Intersects (pozicija_dodira))
					{
						clicked = true;
					}
				}
			}
			if (clicked)
				selected_tex = clicked_tex;
			else
				selected_tex = texture;
		}

		public void Draw(SpriteBatch sp)
		{
			sp.Draw (selected_tex, rectangle, Color.White);
		}
	}
}

