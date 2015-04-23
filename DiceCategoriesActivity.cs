/*
 *  Author: Georgi Kamacharov
 *  Date: 4/15/2015
 *  Description: View about screen/activity
 * 
*/

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
	[Activity (Label = "Dice Master", Icon = "@drawable/Dice", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class DiceCategoriesActivity : Activity, GestureDetector.IOnGestureListener
	{
		private GestureDetector gestureDetector;

		private static int SWIPE_THRESHOLD = 100;
		private static int SWIPE_VELOCITY_THRESHOLD = 100;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.DiceCategoriesScreen);

			Button oneSix = FindViewById<Button>(Resource.Id.oneSix);
			Button oneTwelve = FindViewById<Button>(Resource.Id.oneTwelve);
			Button oneEighteen = FindViewById<Button>(Resource.Id.oneEighteen);
			Button oneTwentyfour = FindViewById<Button>(Resource.Id.oneTwentyfour);
			Button oneThirty = FindViewById<Button>(Resource.Id.oneThirty);
			Button oneThirtysix = FindViewById<Button>(Resource.Id.oneThirtysix);

			TextView chooseacategoryText = FindViewById<TextView> (Resource.Id.chooseacategoryText);

			chooseacategoryText.TextSize = 35;

			oneSix.TextSize = 15;
			oneTwelve.TextSize = 15;
			oneEighteen.TextSize = 15;
			oneTwentyfour.TextSize = 15;
			oneThirty.TextSize = 15;
			oneThirtysix.TextSize = 15;

			oneSix.Click += delegate {
				string categoryMax = "6";
				Intent slideIntent = new Intent(this, typeof(DiceActivity));
				slideIntent.PutExtra("catMax", categoryMax);
				Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim1, Resource.Animation.Anim2).ToBundle();
				StartActivity(slideIntent, slideAnim);
			};
			oneTwelve.Click += delegate {
				string categoryMax = "12";
				Intent slideIntent = new Intent(this, typeof(DiceActivity));
				slideIntent.PutExtra("catMax", categoryMax);
				Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim1, Resource.Animation.Anim2).ToBundle();
				StartActivity(slideIntent, slideAnim);
			};
			oneEighteen.Click += delegate {
				string categoryMax = "18";
				Intent slideIntent = new Intent(this, typeof(DiceActivity));
				slideIntent.PutExtra("catMax", categoryMax);
				Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim1, Resource.Animation.Anim2).ToBundle();
				StartActivity(slideIntent, slideAnim);
			};
			oneTwentyfour.Click += delegate {
				string categoryMax = "24";
				Intent slideIntent = new Intent(this, typeof(DiceActivity));
				slideIntent.PutExtra("catMax", categoryMax);
				Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim1, Resource.Animation.Anim2).ToBundle();
				StartActivity(slideIntent, slideAnim);
			};
			oneThirty.Click += delegate {
				string categoryMax = "30";
				Intent slideIntent = new Intent(this, typeof(DiceActivity));
				slideIntent.PutExtra("catMax", categoryMax);
				Bundle slideAnim = ActivityOptions.MakeCustomAnimation(Application.Context, Resource.Animation.Anim1, Resource.Animation.Anim2).ToBundle();
				StartActivity(slideIntent, slideAnim);
			};
			oneThirtysix.Click += delegate {
				string categoryMax = "36";
				Intent slideIntent = new Intent(this, typeof(DiceActivity));
				slideIntent.PutExtra("catMax", categoryMax);
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

