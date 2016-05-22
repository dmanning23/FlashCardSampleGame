using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FlashCardSampleGame
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : TouchGame
	{
		public Game1()
		{
		}

		public override IScreen[] GetMainMenuScreenStack()
		{
			return new IScreen[] { new MainMenuScreen() };
		}

		protected override void InitStyles()
		{
			base.InitStyles();

			//StyleSheet.

			//use the spanich font for menu entry styles
			StyleSheet.LargeFontResource = @"Fonts\ArialBlack48Spanish";
			StyleSheet.MediumFontResource = @"Fonts\ArialBlack48Spanish";
			StyleSheet.SmallFontResource = @"Fonts\ArialBlack48Spanish";
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
	}
}
