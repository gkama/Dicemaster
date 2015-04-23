
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Dicemaster
{
	[Activity (Label = "CALDGActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class CALDGActivity : Activity
	{
		public static String CALDG_DATA = "CALDGData";

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			ScrollView scrollView = new ScrollView (this);
			TextView CALDGStatsView = new TextView (this);
			scrollView.AddView (CALDGStatsView);

			SetContentView (scrollView);

			ISharedPreferences CALDGPref = GetSharedPreferences (CALDG_DATA, FileCreationMode.Private);
			ISharedPreferencesEditor CALDGEditor = CALDGPref.Edit ();

			String userInputOne = CALDGPref.GetString ("CALDGStatsString0", "0");
			String latestAmountOne = CALDGPref.GetString ("CALDGStatsString1", "0");
			String latestBetOne = CALDGPref.GetString ("CALDGStatsString2", "0");
			String totalLostOne = CALDGPref.GetString ("CALDGStatsString3", "0");
			String totalWonOne = CALDGPref.GetString ("CALDGStatsString4", "0");
			String totalMatchesOne = CALDGPref.GetString ("CALDGStatsString5", "0");

			String userInputTwo = CALDGPref.GetString ("CALDGStatsString6", "0");
			String latestAmountTwo = CALDGPref.GetString ("CALDGStatsString7", "0");
			String latestBetTwo = CALDGPref.GetString ("CALDGStatsString8", "0");
			String totalLostTwo = CALDGPref.GetString ("CALDGStatsString9", "0");
			String totalWonTwo = CALDGPref.GetString ("CALDGStatsString10", "0");
			String totalMatchesTwo = CALDGPref.GetString ("CALDGStatsString11", "0");

			String userInputThree = CALDGPref.GetString ("CALDGStatsString12", "0");
			String latestAmountThree = CALDGPref.GetString ("CALDGStatsString13", "0");
			String latestBetThree = CALDGPref.GetString ("CALDGStatsString14", "0");
			String totalLostThree = CALDGPref.GetString ("CALDGStatsString15", "0");
			String totalWonThree = CALDGPref.GetString ("CALDGStatsString16", "0");
			String totalMatchesThree = CALDGPref.GetString ("CALDGStatsString17", "0");

			String userInputFour = CALDGPref.GetString ("CALDGStatsString18", "0");
			String latestAmountFour = CALDGPref.GetString ("CALDGStatsString19", "0");
			String latestBetFour = CALDGPref.GetString ("CALDGStatsString20", "0");
			String totalLostFour = CALDGPref.GetString ("CALDGStatsString21", "0");
			String totalWonFour = CALDGPref.GetString ("CALDGStatsString22", "0");
			String totalMatchesFour = CALDGPref.GetString ("CALDGStatsString23", "0");

			String userInputFive = CALDGPref.GetString ("CALDGStatsString24", "0");
			String latestAmountFive = CALDGPref.GetString ("CALDGStatsString25", "0");
			String latestBetFive = CALDGPref.GetString ("CALDGStatsString26", "0");
			String totalLostFive = CALDGPref.GetString ("CALDGStatsString27", "0");
			String totalWonFive = CALDGPref.GetString ("CALDGStatsString28", "0");
			String totalMatchesFive = CALDGPref.GetString ("CALDGStatsString29", "0");

			String userInputSix = CALDGPref.GetString ("CALDGStatsString30", "0");
			String latestAmountSix = CALDGPref.GetString ("CALDGStatsString31", "0");
			String latestBetSix = CALDGPref.GetString ("CALDGStatsString32", "0");
			String totalLostSix = CALDGPref.GetString ("CALDGStatsString33", "0");
			String totalWonSix = CALDGPref.GetString ("CALDGStatsString34", "0");
			String totalMatchesSix = CALDGPref.GetString ("CALDGStatsString35", "0");

			CALDGStatsView.Text = "CHUCK-A-LUCK LATEST GAME SCORES:\n" +
				"User Input: " + userInputOne + "\n" +
				"Latest Amount: " + latestAmountOne + "\n" +
				"Latest Bet: " + latestBetOne + "\n" +
				"Total Lost: " + totalLostOne + "\n" +
				"Total Won: " + totalWonOne + "\n" +
				"Total Matches: " + totalMatchesOne + "\n\n" +

				"User Input: " + userInputTwo + "\n" +
				"Latest Amount: " + latestAmountTwo + "\n" +
				"Latest Bet: " + latestBetTwo + "\n" +
				"Total Lost: " + totalLostTwo + "\n" +
				"Total Won: " + totalWonTwo + "\n" +
				"Total Matches: " + totalMatchesTwo + "\n\n" +

				"User Input: " + userInputThree + "\n" +
				"Latest Amount: " + latestAmountThree + "\n" +
				"Latest Bet: " + latestBetThree + "\n" +
				"Total Lost: " + totalLostThree + "\n" +
				"Total Won: " + totalWonThree + "\n" +
				"Total Matches: " + totalMatchesThree + "\n\n" +

				"User Input: " + userInputFour + "\n" +
				"Latest Amount: " + latestAmountFour + "\n" +
				"Latest Bet: " + latestBetFour + "\n" +
				"Total Lost: " + totalLostFour + "\n" +
				"Total Won: " + totalWonFour + "\n" +
				"Total Matches: " + totalMatchesFour + "\n\n" +

				"User Input: " + userInputFive + "\n" +
				"Latest Amount: " + latestAmountFive + "\n" +
				"Latest Bet: " + latestBetFive + "\n" +
				"Total Lost: " + totalLostFive + "\n" +
				"Total Won: " + totalWonFive + "\n" +
				"Total Matches: " + totalMatchesFive + "\n\n" +

				"User Input: " + userInputSix + "\n" +
				"Latest Amount: " + latestAmountSix + "\n" +
				"Latest Bet: " + latestBetSix + "\n" +
				"Total Lost: " + totalLostSix + "\n" +
				"Total Won: " + totalWonSix + "\n" +
				"Total Matches: " + totalMatchesSix + "\n";

		}
	}
}

