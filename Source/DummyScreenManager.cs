using MenuBuddy;
using Microsoft.Xna.Framework;

#if OUYA
using Ouya.Console.Api;
#endif

namespace FlashCardSampleGame
{
	public class DummyScreenManager : ScreenManager
	{
		#region Properties

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="Filibuster.FilibusterScreenManager"/> class.
		/// </summary>
		/// <param name="game">Game.</param>
		public DummyScreenManager(Game game)
			: base(game,
				"ArialBlack72",
				"ArialBlack48",
				"ArialBlack24",
				"MenuMove",
				"MenuSelect")
		{
#if ANDROID
			TouchMenus = true;
#endif
		}

		/// <summary>
		/// Get the set of screens needed for the main menu
		/// </summary>
		/// <returns>The gameplay screen stack.</returns>
		public override GameScreen[] GetMainMenuScreenStack()
		{
			return new GameScreen[] { new MainMenuScreen() };
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		#endregion //Methods
	}
}