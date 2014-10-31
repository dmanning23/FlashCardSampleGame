using BasicPrimitiveBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using ResolutionBuddy;
using TouchScreenBuddy;

namespace FlashCardSampleGame
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private readonly DummyScreenManager _ScreenManager;

		XNABasicPrimitive prim;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
			Resolution.Init(ref graphics);
			Content.RootDirectory = "Content";

			//create the touch manager component
			var touches = new TouchManager(this, Resolution.ScreenToGameCoord);
			Components.Add(touches);

			// Create the screen manager component.
			_ScreenManager = new DummyScreenManager(this);
			_ScreenManager.ClearColor = new Color(0.1f, 0.5f, 0.1f);
			Components.Add(_ScreenManager);

			// Activate the first screens.
			_ScreenManager.AddScreen(_ScreenManager.GetMainMenuScreenStack(), null);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// Change Virtual Resolution
			Resolution.SetDesiredResolution(1280, 720);

			//set the desired resolution
			Resolution.SetScreenResolution(640, 360, false);

			// TODO: Add your initialization logic here
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			prim = new XNABasicPrimitive(GraphicsDevice, spriteBatch);
			prim.Thickness = 3.0f;
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			// TODO: Add your update logic here
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			// Clear to Black
			graphics.GraphicsDevice.Clear(_ScreenManager.ClearColor);

			// Calculate Proper Viewport according to Aspect Ratio
			Resolution.ResetViewport();

			// The real drawing happens inside the screen manager component.
			base.Draw(gameTime);

			spriteBatch.Begin();
#if WINDOWS
			var mouse = Mouse.GetState();
			var mousePos = new Vector2((float)mouse.X, (float)mouse.Y);

			//draw a circle around the mouse cursor
			prim.Circle(mousePos, 5.0f, Color.Red);
#endif
			//go though the points that are being touched
			TouchCollection touchCollection = TouchPanel.GetState();
			foreach (var touch in touchCollection)
			{
				if ((touch.State == TouchLocationState.Pressed) || (touch.State == TouchLocationState.Moved))
				{
					//draw a circle around each touch point
					prim.Circle(touch.Position, 40.0f, new Color(1.0f, 1.0f, 1.0f, 0.25f));
				}
			}
			spriteBatch.End();
		}
	}
}

