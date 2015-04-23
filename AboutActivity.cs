/*
 *  Author: Georgi Kamacharov
 *  Date: 4/15/2015
 *  Description: View about screen/activity
 * 
*/

using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Animation;

namespace Dicemaster
{
	[Activity (Label = "Dice Master", Icon = "@drawable/Dice", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class AboutActivity : Activity, GestureDetector.IOnGestureListener
	{
		private GestureDetector gestureDetector;

		private static int SWIPE_THRESHOLD = 100;
		private static int SWIPE_VELOCITY_THRESHOLD = 100;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var scrollView = FindViewById<TextView> (Resource.Id.scrollView);

			SetContentView (Resource.Layout.AboutScreen);

			var regulardieText = FindViewById<TextView> (Resource.Id.regulardieText);
			var matchingdicegameText = FindViewById<TextView> (Resource.Id.matchingdicegameText);
			var highlowdicegameText = FindViewById<TextView> (Resource.Id.highlowdicegameText);
			var chuckaluckdicegameText = FindViewById<TextView> (Resource.Id.chuckaluckdicegameText);
			var authorText = FindViewById<TextView> (Resource.Id.authorText);

			regulardieText.Text = "Regular Die: That is a simple Regular Fair Die with 6 sides, 1, 2, 3, 4, 5, and 6. " +
				"You can use it for any game or bet that involves rolling a die!\n" +
				"Recommended for Single and Multi player!\n";

			matchingdicegameText.Text = "Matching Dice Game: In the game of Matching Dice the basic concept involves the user rolling a die and trying to match " +
				"it with a randomly generated number between 1 and 36. There are 6 difference categories to choose from. Selecting a smaller category is generally " +
				"more difficult to match simply because you have a bigger restriction on the numbers generated and the default random number has a much smaller one. " +
				"And visa versa. Also, the game keeps track of your Total, Number of Clicks, and Number of Matches with Accuracy. The Total is simply the current sum of all the rolls.\n" +
				"Recommended for Single player!\n";

			highlowdicegameText.Text = "High-Low Dice Game: In the game of High-Low, a pair of fair dice are rolled and the outcome can be three different " +
				"cases:\n" +
				"High if the sum is 8, 9, 10, 11, or 12.\n" +
				"Low if the sum is 2, 3, 4, 5, or 6.\n" +
				"Seven if the sum is 7.\n" +
				"A player can bet on any of the three outcomes. The payoff for a bet of High or for a bet of Low is 1:1. The payoff for a bet of seven is 4:1.\n" +
				"Recommended for Single/Multi player!\n";

			chuckaluckdicegameText.Text = "Chuck-A-Luck Dice Game: In the game of Chuck-A-Luck the player selects an integer from 1 to 6, and then 3 dice are rolled. " +
				"If exactly k dice show the player's number, the payoff is k:1. A player can select the outcome via the provided radio buttons. A mathematical assumption " +
				"is that the dice are fair. Additionally, the game shows the number of matches in the bottom of the screen.\n" +
				"Recommended for Single/Multi player!\n\n" +

				"Additional Information:\n" +
				"STATS: There is a STATS screen where it shows the latest game scores for each game. They differ with each game and show your latest progress. " +
				"Also, the statistics will be overwritten once the game is restarted, but fields that are not played will not get overwritten, they will show your latest game scores.\n\n" +

				"You can swipe right on main screen to go to the STATS screen or you can simply press the button. " +
				"Also, you can swipe left where applicable to go back to the previous opened screen except this one!";

			authorText.TextSize = 15;
			authorText.Text = "\nAuthor: Georgi Kamacharov\n";

			// Gesture Detection
			gestureDetector = new GestureDetector(this);
		}

		// Gestures
		public override bool OnTouchEvent(MotionEvent e)
		{
			gestureDetector.OnTouchEvent(e);
			return true;
		}
		public bool OnDown(MotionEvent e) {return true;}

		// Used for Swiping
		public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			float diffY = e2.GetY() - e1.GetY();
			float diffX = e2.GetX() - e1.GetX();

			if (Math.Abs(diffX) > Math.Abs(diffY))
			{
				if (Math.Abs(diffX) > SWIPE_THRESHOLD && Math.Abs(velocityX) > SWIPE_VELOCITY_THRESHOLD)
				{
					if (diffX > 0)
					{
						// Left Swipe - go back
						Intent slideIntent = new Intent(this, typeof(MainActivity));
						Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim3, Resource.Animation.Anim4).ToBundle();
						StartActivity(slideIntent, slideAnim);
						Finish ();
					}
					else
					{
						// Right Swipe
					}
				}
			}
			else if (Math.Abs(diffY) > SWIPE_THRESHOLD && Math.Abs(velocityY) > SWIPE_VELOCITY_THRESHOLD)
			{
				if (diffY > 0)
				{
					// Top swipe
				}
				else
				{
					// Bottom swipe
				}
			}
			return true;
		}
		//
		public void OnLongPress(MotionEvent e) {}
		public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY) {return false;}
		public void OnShowPress(MotionEvent e) {}
		public bool OnSingleTapUp(MotionEvent e) {return false;}
	}
}


