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
	[Activity (Label = "Dice Master", MainLauncher = true, Icon = "@drawable/Dice", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class MainActivity : Activity, GestureDetector.IOnGestureListener
	{
		private GestureDetector gestureDetector;

		private static int SWIPE_THRESHOLD = 100;
		private static int SWIPE_VELOCITY_THRESHOLD = 100;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			Button taketoDiceButton = FindViewById<Button> (Resource.Id.taketoDiceButton);
			Button taketoRegularDiceGameButton = FindViewById<Button> (Resource.Id.taketoRegularDiceGameButton);
			Button taketoHighLowDieGameButton = FindViewById<Button> (Resource.Id.taketoHighLowDieGameButton);
			Button taketoChuckALuckGameButton = FindViewById<Button> (Resource.Id.taketoChuckALuckGameButton);
			Button aboutButton = FindViewById<Button> (Resource.Id.aboutButton);
			Button statsButton = FindViewById<Button> (Resource.Id.statsButton);

			TextView dicemasterTitle = FindViewById<TextView> (Resource.Id.dicemasterTitle);
			dicemasterTitle.TextSize = 40;

			taketoRegularDiceGameButton.TextSize = 15;
			taketoDiceButton.TextSize = 15;
			taketoHighLowDieGameButton.TextSize = 15;
			taketoChuckALuckGameButton.TextSize = 15;
			aboutButton.TextSize = 15;
			statsButton.TextSize = 15;

			taketoDiceButton.Click += delegate {
				Intent slideIntent = new Intent(this, typeof(DiceCategoriesActivity));
				Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim1, Resource.Animation.Anim2).ToBundle();
				StartActivity(slideIntent, slideAnim);
			};
			taketoRegularDiceGameButton.Click += delegate {
				Intent slideIntent = new Intent(this, typeof(RegularDiceActivity));
				Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim1, Resource.Animation.Anim2).ToBundle();
				StartActivity(slideIntent, slideAnim);
			};

			taketoHighLowDieGameButton.Click += delegate {
				Intent slideIntent = new Intent(this, typeof(HighLowDiceActivity));
				Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim1, Resource.Animation.Anim2).ToBundle();
				StartActivity(slideIntent, slideAnim);
			};

			taketoChuckALuckGameButton.Click += delegate {
				Intent slideIntent = new Intent(this, typeof(ChuckALuckActivity));
				Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim1, Resource.Animation.Anim2).ToBundle();
				StartActivity(slideIntent, slideAnim);
			};

			aboutButton.Click += delegate {
				Intent slideIntent = new Intent(this, typeof(AboutActivity));
				Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim1, Resource.Animation.Anim2).ToBundle();
				StartActivity(slideIntent, slideAnim);
			};

			statsButton.Click += delegate {
				Intent slideIntent = new Intent(this, typeof(StatsActivity));
				Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim1, Resource.Animation.Anim2).ToBundle();
				StartActivity(slideIntent, slideAnim);
			};

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
						// Left Swipe
					}
					else
					{
						// Right Swipe
						Intent slideIntent = new Intent(this, typeof(StatsActivity));
						Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim1, Resource.Animation.Anim2).ToBundle();
						StartActivity(slideIntent, slideAnim);
						Finish ();
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


