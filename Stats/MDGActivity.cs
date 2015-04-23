
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
	[Activity (Label = "MDGActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class MDGActivity : Activity
	{
		public static String MDG_DATA = "MDGData";

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			//ScrollView scrollView = new ScrollView (this);
			TextView MDGStatsView = new TextView (this);

			//scrollView.AddView (MDGStatsView);

			SetContentView (MDGStatsView);

			ISharedPreferences HLDGPref = GetSharedPreferences (MDG_DATA, FileCreationMode.Private);
			ISharedPreferencesEditor HLDGEditor = HLDGPref.Edit ();

			// Category 1-6
			int totalScoreSix = HLDGPref.GetInt ("totalScoreSix", 0);
			float numberOfClicksSix = HLDGPref.GetFloat ("numberOfClicksSix", 0);
			float numberOfMathcesSix = HLDGPref.GetFloat ("numberOfMathcesSix", 0);

			// Category 1-12
			int totalScoreTwelve = HLDGPref.GetInt ("totalScoreTwelve", 0);
			float numberOfClicksTwelve = HLDGPref.GetFloat ("numberOfClicksTwelve", 0);
			float numberOfMathcesTwelve = HLDGPref.GetFloat ("numberOfMathcesTwelve", 0);

			// Category 1-18
			int totalScoreEighteen = HLDGPref.GetInt ("totalScoreEighteen", 0);
			float numberOfClicksEighteen = HLDGPref.GetFloat ("numberOfClicksEighteen", 0);
			float numberOfMathcesEighteen = HLDGPref.GetFloat ("numberOfMathcesEighteen", 0);

			// Category 1-24
			int totalScoreTwentyfour = HLDGPref.GetInt ("totalScoreTwentyfour", 0);
			float numberOfClicksTwentyfour = HLDGPref.GetFloat ("numberOfClicksTwentyfour", 0);
			float numberOfMathcesTwentyfour = HLDGPref.GetFloat ("numberOfMathcesTwentyfour", 0);

			// Category 1-30
			int totalScoreThirty = HLDGPref.GetInt ("totalScoreThirty", 0);
			float numberOfClicksThirty = HLDGPref.GetFloat ("numberOfClicksThirty", 0);
			float numberOfMathcesThirty = HLDGPref.GetFloat ("numberOfMathcesThirty", 0);

			// Category 1-36
			int totalScoreThirtysix = HLDGPref.GetInt ("totalScoreThirtysix", 0);
			float numberOfClicksThirtysix = HLDGPref.GetFloat ("numberOfClicksThirtysix", 0);
			float numberOfMathcesThirtysix = HLDGPref.GetFloat ("numberOfMathcesThirtysix", 0);



			MDGStatsView.Text = "MATCHING DICE GAME LATEST GAME SCORES:\n" +
				"Category: 1-6\n" +
				"Total Score: " + totalScoreSix + "\n" +
				"Total Number of Clicks: " + numberOfClicksSix + "\n" +
				"Total Number of Matches: " + numberOfMathcesSix + "\n\n" +

				"Category: 1-12\n" +
				"Total Score: " + totalScoreTwelve + "\n" +
				"Total Number of Clicks: " + numberOfClicksTwelve + "\n" +
				"Total Number of Matches: " + numberOfMathcesTwelve + "\n\n" +

				"Category: 1-18\n" +
				"Total Score: " + totalScoreEighteen + "\n" +
				"Total Number of Clicks: " + numberOfClicksEighteen + "\n" +
				"Total Number of Matches: " + numberOfMathcesEighteen + "\n\n" +

				"Category: 1-24\n" +
				"Total Score: " + totalScoreTwentyfour + "\n" +
				"Total Number of Clicks: " + numberOfClicksTwentyfour + "\n" +
				"Total Number of Matches: " + numberOfMathcesTwentyfour + "\n\n" +

				"Category: 1-30\n" +
				"Total Score: " + totalScoreThirty + "\n" +
				"Total Number of Clicks: " + numberOfClicksThirty + "\n" +
				"Total Number of Matches: " + numberOfMathcesThirty + "\n\n" +

				"Category: 1-36\n" +
				"Total Score: " + totalScoreThirtysix + "\n" +
				"Total Number of Clicks: " + numberOfClicksThirtysix + "\n" +
				"Total Number of Matches: " + numberOfMathcesThirtysix + "\n";
		}
	}
}

