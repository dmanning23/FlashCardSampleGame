using FlashCards;
using MenuBuddy;
using Microsoft.Xna.Framework;
using InputHelper;

namespace FlashCardSampleGame
{
	/// <summary>
	/// The main menu screen is the first thing displayed when the game starts up.
	/// </summary>
	internal class MainMenuScreen : MenuScreen, IMainMenu
	{
		#region Fields

		int _score = 0;

		Deck _cards;

		#endregion //Fields

		#region Initialization

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public MainMenuScreen()
			: base("Score: 0")
		{
		}

		public override void LoadContent()
		{
			base.LoadContent();

			// Create our menu entries.
			var questionMenuEntry = new MenuEntry("Ask Question");
			var exitMenuEntry = new MenuEntry("Exit");

			// Hook up menu event handlers.
			questionMenuEntry.OnClick += QuestionMenuEntrySelected;
			exitMenuEntry.OnClick += OnExit;

			// Add entries to the menu.
			AddMenuEntry(questionMenuEntry);
#if !__IOS__ && !ANDROID && !WINDOWS_UAP
			AddMenuEntry(exitMenuEntry); //TODO: only remove this entry for the demo
#endif

			_cards = new Deck(@"Spanish\Colors.xml");
			_cards.ReadXmlFile();

			//Add the Numbers deck too
			var nums = new Deck(@"Spanish\Numbers.xml");
			nums.ReadXmlFile();
			_cards.AddDeck(nums);

			base.LoadContent();
		}

		#endregion //Initialization

		#region Handle Input

		/// <summary>
		/// Event handler for when the Ask Question menu entry is selected.
		/// </summary>
		private void QuestionMenuEntrySelected(object sender, ClickEventArgs e)
		{
			//Ask a simple question.
			ScreenManager.AddScreen(new QuestionScreen(QuestionAnswered, _cards));
		}

		/// <summary>
		/// When the user cancels the main menu, ask if they want to exit the sample.
		/// </summary>
		protected void OnExit(object sender, ClickEventArgs e)
		{
			const string message = "Are you sure you want to exit?";
			var confirmExitMessageBox = new MessageBoxScreen(message);
			confirmExitMessageBox.OnSelect += ConfirmExitMessageBoxAccepted;
			ScreenManager.AddScreen(confirmExitMessageBox, e.PlayerIndex);
		}

		/// <summary>
		/// Event handler for when the user selects ok on the "are you sure
		/// you want to exit" message box.
		/// </summary>
		private void ConfirmExitMessageBoxAccepted(object sender, ClickEventArgs e)
		{
#if !__IOS__
			ScreenManager.Game.Exit();
#endif
		}

		public void QuestionAnswered(bool correct)
		{
			if (correct)
			{
				_score++;
			}

			ScreenName = string.Format("Score: {0}", _score);
		}

		#endregion
	}
}