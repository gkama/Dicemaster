
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
	[Activity (Label = "HLDGActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class HLDGActivity : Activity
	{
		public static String HLDG_DATA = "HLDGData";

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			TextView HLDGStatsView = new TextView (this);

			SetContentView (HLDGStatsView);

			ISharedPreferences HLDGPref = GetSharedPreferences (HLDG_DATA, FileCreationMode.Private);
			ISharedPreferencesEditor HLDGEditor = HLDGPref.Edit ();

			int totalAmount = HLDGPref.GetInt ("totalAmount", 0);
			int totalBet = HLDGPref.GetInt ("totalBet", 0);
			int totalHighMatches = HLDGPref.GetInt ("totalHighMatches", 0);
			int totalSevenMatches = HLDGPref.GetInt ("totalSevenMatches", 0);
			int totalLowMatches = HLDGPref.GetInt ("totalLowMatches", 0);
			int totalAmountWon = HLDGPref.GetInt ("totalAmountWon", 0);
			int totalAmountLost = HLDGPref.GetInt ("totalAmountLost", 0);
			int totalSumRoll = HLDGPref.GetInt ("totalSumRoll", 0);


			HLDGStatsView.Text = "HIGH-LOW LATEST GAME SCORES:\n" +
				"Latest Amount: " + (totalAmount + totalBet) + "\n" +
				"Latest Bet: " + totalBet + "\n" +
				"Lates Sum Roll: " + totalSumRoll + "\n" +
				"Total High Matches: " + totalHighMatches + "\n" +
				"Total Seven Matches: " + totalSevenMatches + "\n" +
				"Total Low Matches: " + totalLowMatches + "\n" +
				"Total Amount Won: " + totalAmountWon + "\n" +
				"Total Amount Lost: " + totalAmountLost + "\n";
		}
	}
}

