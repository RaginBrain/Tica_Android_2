#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;

#endregion

namespace Tica_Android_2
{

	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{

		enum GameState { Start, InGame, GameOver };
		GameState currentGameState = GameState.InGame;

		public void LelevUp()
		{
			scrolling1.brzina_kretanja += 1;
			scrolling2.brzina_kretanja += 1;
			barijera1.brzina_kretanja += 1;
			barijera2.brzina_kretanja += 1;
			barijera3.brzina_kretanja += 1;
			barijera.brzina_kretanja += 1;
			player1.speed += 1;
			sljedeciLevel += 1;
			maca.brzina_kretanja += 1;

		}
		IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForDomain();
		int high_score;

		Score rezultat;
		List<Texture2D> lista_txtr;

		Sprite start_button;

		Sprite game_over;
		Stopwatch sat;

		Barijera maca;
		Stit stit;
		LevelUp lvlUp;
		public GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Barijera barijera,barijera2;
		PokretnaBarijera barijera1, barijera3;
		Random r = new Random();
		int mjera;

		int sljedeciLevel;
		Scrolling scrolling1;
		Scrolling scrolling2;
		Player player1;


		TouchCollection touchCollection;
		int sirina;
		int visina;

		public Game1()
		{

			graphics = new GraphicsDeviceManager(this);

			graphics.ApplyChanges();
			graphics.IsFullScreen = true;
			sirina = graphics.PreferredBackBufferWidth;
			visina = graphics.PreferredBackBufferHeight;
			Content.RootDirectory = "Content";
		}




		//:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
			player1.Initialize();
			player1.playerAnimation.Initialize();
			sljedeciLevel = lvlUp.level + 1;
			sat = new Stopwatch ();
			currentGameState = GameState.Start;


			//janje1f
			if (savegameStorage.FileExists("high_score.txt")) 
			{
				IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream ("high_score.txt", FileMode.OpenOrCreate,FileAccess.Read);
				using (StreamReader sr = new StreamReader (isoStream)) 
				{
					high_score = int.Parse (sr.ReadLine ());
				}	
			} 
			else
				high_score = 0;
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			//TODO: use this.Content to load your game content here 
			scrolling1 = new Scrolling(Content.Load<Texture2D>("bg1"), new Rectangle(0, 0, sirina, visina), 3);
			scrolling2 = new Scrolling(Content.Load<Texture2D>("bg2"), new Rectangle(sirina, 0,sirina, visina), 3);

			player1 = new Player(Content.Load<Texture2D>("tica_gotova"),Content.Load<Texture2D>("tica_stit"), new Rectangle(0, 0, 50, 57));
			//			score = Content.Load<SpriteFont> ("SpriteFont1");


			//dodavanje znamenaka
			lista_txtr = new List<Texture2D> ();
			lista_txtr.Add (Content.Load<Texture2D> ("Znamenke/0"));
			lista_txtr.Add (Content.Load<Texture2D> ("Znamenke/1"));
			lista_txtr.Add (Content.Load<Texture2D> ("Znamenke/2"));
			lista_txtr.Add (Content.Load<Texture2D> ("Znamenke/3"));
			lista_txtr.Add (Content.Load<Texture2D> ("Znamenke/4"));
			lista_txtr.Add (Content.Load<Texture2D> ("Znamenke/5"));
			lista_txtr.Add (Content.Load<Texture2D> ("Znamenke/6"));
			lista_txtr.Add (Content.Load<Texture2D> ("Znamenke/7"));
			lista_txtr.Add (Content.Load<Texture2D> ("Znamenke/8"));
			lista_txtr.Add (Content.Load<Texture2D> ("Znamenke/9"));

			rezultat = new Score (lista_txtr);




			start_button = new Sprite ();
			start_button.rectangle = new Rectangle (0, 0, 500, 238);
			start_button.texture = Content.Load<Texture2D> ("oblak_start");

			game_over = new Sprite ();
			game_over.rectangle = new Rectangle (-visina, 0, 300, 409);
			game_over.texture=Content.Load<Texture2D>("game_over");

			maca = new Barijera(Content.Load<Texture2D>("maca"), new Rectangle(2200, 0,  80,  80));
			stit = new Stit(Content.Load<Texture2D>("stit"), new Rectangle(2000, 50, 64, 64));
			lvlUp = new LevelUp(Content.Load<Texture2D>("be_ready"), new Rectangle(1000, 250,150, 50));
			barijera = new Barijera(Content.Load<Texture2D>("barijera"), new Rectangle(1000, 0, 35, 100));
			barijera1 = new PokretnaBarijera(Content.Load<Texture2D>("barijera"), new Rectangle(1250, 100, 35, 100), false, visina);
			barijera2 = new Barijera(Content.Load<Texture2D>("barijera"), new Rectangle(1500, 400, 35, 100));
			barijera3 = new PokretnaBarijera(Content.Load<Texture2D>("barijera"), new Rectangle(1750, 500, 35, 100), true, sirina);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			switch (currentGameState)
			{
			case GameState.Start:

				rezultat.Update (high_score);
				start_button.rectangle.X = sirina / 2 - start_button.rectangle.Width/2;
				touchCollection = TouchPanel.GetState ();
				Rectangle pozicija_dodira;
				foreach (TouchLocation tl in touchCollection) 
				{

					if ((tl.State == TouchLocationState.Pressed) || (tl.State == TouchLocationState.Moved)) 
					{
						pozicija_dodira = new Rectangle ((int)tl.Position.X, (int)tl.Position.Y, 1, 1);

						if (tl.State == TouchLocationState.Pressed && start_button.rectangle.Intersects (pozicija_dodira))
							currentGameState = GameState.InGame;
					}

				}
				break;
			case GameState.InGame:

				// Pozadina*********************************************************************
				if (scrolling1.rectangle.X + scrolling1.rectangle.Width <= 0)
					scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling1.rectangle.Width;
				if (scrolling2.rectangle.X + graphics.PreferredBackBufferWidth <= 0)
					scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.rectangle.Width;
				scrolling1.Update ();
				scrolling2.Update ();
				//******************************************************************************



				rezultat.Update (player1.score);

				stit.Update (player1, 350, lvlUp);
				player1.Update (gameTime, graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);

				int t = r.Next (5, 30);
				maca.Update (player1, t, graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth * 3);

				//triba uštimat s velicinom sprite-a
				///////////////////////////////////////
				int interval = (int)(graphics.PreferredBackBufferWidth - graphics.PreferredBackBufferWidth / 2);
				barijera.Update (player1, r.Next (0, interval), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
				barijera1.Update (player1, r.Next (0, interval), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
				barijera2.Update (player1, r.Next (0, interval), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
				barijera3.Update (player1, r.Next (0, interval), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
				lvlUp.Update (player1, 300);

				if (sljedeciLevel == lvlUp.level)
					LelevUp ();

				touchCollection = TouchPanel.GetState ();

				//jst.Update (touchCollection);



				if (player1.stit)
					player1.playerAnimation.Image = player1.texture_stit;
				else
					player1.playerAnimation.Image = player1.texture;
				if (player1.alive == false) {
					currentGameState = GameState.GameOver;
				}
				break;

			case GameState.GameOver:
				//cekaj
				if (sat.ElapsedMilliseconds == 0 || sat.ElapsedMilliseconds > 2400)
					sat.Restart ();


				game_over.rectangle.X = player1.rectangle.X - game_over.rectangle.Width / 2;
				if (game_over.rectangle.X < 0)
					game_over.rectangle.X = 0;

				if (game_over.rectangle.X > sirina - game_over.rectangle.Width)
					game_over.rectangle.X = sirina - game_over.rectangle.Width;

				if (game_over.rectangle.Y > visina / 2 - game_over.rectangle.Height / 2)
					game_over.rectangle.Y -= 1;

				if (sat.ElapsedMilliseconds > 2300) 
				{
					//janje2
					if (player1.score > high_score) 
					{
						IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream ("high_score.txt", FileMode.OpenOrCreate,FileAccess.Write);
						using (StreamWriter sw = new StreamWriter(isoStream)) 
						{
							sw.Flush ();
							sw.WriteLine (player1.score.ToString ());
						}	

					}
					Initialize ();
				}
				break;
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			switch (currentGameState) 
			{
			case GameState.Start:
				spriteBatch.Begin (SpriteSortMode.Deferred, BlendState.NonPremultiplied);

				scrolling1.Draw (spriteBatch);
				scrolling2.Draw (spriteBatch);
				spriteBatch.Draw (start_button.texture, start_button.rectangle, Color.White);
				try{
					rezultat.Draw (spriteBatch, sirina/2,visina-visina/3, 30, 80);
				}
				catch{}
				spriteBatch.End ();

				break;
			case GameState.InGame:
				{

					GraphicsDevice.Clear (Color.CornflowerBlue);

					// TODO: Add your drawing code here
					spriteBatch.Begin (SpriteSortMode.Deferred, BlendState.NonPremultiplied);
					scrolling1.Draw (spriteBatch);
					scrolling2.Draw (spriteBatch);
					lvlUp.Draw (spriteBatch);

					try{
						rezultat.Draw (spriteBatch, 5, 5, 15, 40);
					}
					catch{}
					//spriteBatch.Draw(jst.texture,jst.rectangle,Color.White);
					//maca.Draw(spriteBatch);
					stit.Draw (spriteBatch);
					barijera.Draw (spriteBatch);
					barijera1.Draw (spriteBatch);
					barijera2.Draw (spriteBatch);
					barijera3.Draw (spriteBatch);
					spriteBatch.Draw (maca.texture, new Rectangle (maca.rectangle.X, maca.rectangle.Y, 80, 80), Color.White);
					// stit.Draw(spriteBatch);

					player1.playerAnimation.Draw (spriteBatch);
					spriteBatch.End ();
					break;
				}
			case GameState.GameOver:
				GraphicsDevice.Clear (Color.CornflowerBlue);

				// TODO: Add your drawing code here
				spriteBatch.Begin (SpriteSortMode.Deferred, BlendState.NonPremultiplied);

				scrolling1.Draw (spriteBatch);
				scrolling2.Draw (spriteBatch);
				spriteBatch.Draw (game_over.texture, game_over.rectangle, Color.White);
				spriteBatch.End ();

				break;
			}






			base.Draw(gameTime);
		}
	}
}