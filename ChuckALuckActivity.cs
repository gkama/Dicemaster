/*
 *  Author: Georgi Kamacharov
 *  Date: 4/15/2015
 *  Description: View about screen/activity
 * 
*/

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;

namespace Dicemaster
{
	[Activity (Label = "Dice Master", Icon = "@drawable/Dice", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class ChuckALuckActivity : Activity, GestureDetector.IOnGestureListener
	{
		private GestureDetector gestureDetector;

		private static int SWIPE_THRESHOLD = 100;
		private static int SWIPE_VELOCITY_THRESHOLD = 100;
		public static String CALDG_DATA = "CALDGData";

		private static readonly Random randomNum = new Random();
		private static readonly object syncLock = new object();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ChuckALuckScreen);

			int match = 0;

			var numberResult1 = FindViewById<TextView> (Resource.Id.numberResult1);
			var numberResult2 = FindViewById<TextView> (Resource.Id.numberResult2);
			var numberResult3 = FindViewById<TextView> (Resource.Id.numberResult3);

			var matchText = FindViewById<TextView> (Resource.Id.matchText);
			var errorText = FindViewById<TextView> (Resource.Id.errorText);
			var currentAmountText = FindViewById<TextView> (Resource.Id.currentAmountText);

			var currentAmount = FindViewById<EditText> (Resource.Id.currentAmount);
			var userInput = FindViewById<EditText> (Resource.Id.userInput);
			var betAmount = FindViewById<EditText> (Resource.Id.betAmount);

			userInput.Text = "Input:1-6";

			Button diceButton = FindViewById<Button> (Resource.Id.diceButton);

			numberResult1.TextSize = 110;
			numberResult2.TextSize = 110;
			numberResult3.TextSize = 110;
			matchText.TextSize = 20;

			int currentAmountInt = 0;
			int betAmountInt = 0;

			int totalAmountLost = 0;
			int totalAmountWon = 0;
			int totalMatches = 0;

			int[,] CALDGStats = new int[6,6];
			String[,] CALDGStatsString = new string[6, 6];

			for (int i = 0; i < 6; i++) {
				CALDGStats [i, 0] = i + 1;
			}

			ISharedPreferences CALDGPrefs = GetSharedPreferences (CALDG_DATA, FileCreationMode.Private);
			ISharedPreferencesEditor CALDGEditor = CALDGPrefs.Edit ();

			// Roll the dice button
			diceButton.Click += delegate {
				// Check user input
				if((Int32.TryParse(currentAmount.Text.ToString(), out currentAmountInt)) && (Int32.TryParse(betAmount.Text.ToString(), out betAmountInt))){
					if(currentAmountInt == 0 && betAmountInt == 0) { Toast.MakeText (this, "Fields are empty", ToastLength.Long).Show(); }
					else if(currentAmountInt == 0) { Toast.MakeText (this, "Total Amount is 0", ToastLength.Long).Show(); }
					else if(betAmountInt == 0) { Toast.MakeText (this, "Bet is 0", ToastLength.Long).Show(); }
					else if(betAmountInt > currentAmountInt) { Toast.MakeText (this, "Bet is bigger than Total Amount", ToastLength.Long).Show(); }
					else{
						if((userInput.Text.ToString() == "1") || (userInput.Text.ToString() == "2") || (userInput.Text.ToString() == "3") ||
							(userInput.Text.ToString() == "4") || (userInput.Text.ToString() == "5") || (userInput.Text.ToString() == "6")){

							currentAmount.Enabled = false;
							currentAmount.Focusable = false;

							InputMethodManager closeKeyboard = (InputMethodManager)GetSystemService(Context.InputMethodService);
							closeKeyboard.HideSoftInputFromWindow(userInput.WindowToken, 0);

							int userInputInt = Int32.Parse(userInput.Text.ToString());

							errorText.Text = "";

							int resultR1 = RandomNumber (1, 6);
							int resultR2 = RandomNumber (1, 6);
							int resultR3 = RandomNumber (1, 6);

							////////// 
							int totalR = resultR1 + resultR2 + resultR3;

							numberResult1.Text = resultR1.ToString ();
							numberResult2.Text = resultR2.ToString ();
							numberResult3.Text = resultR3.ToString ();

							if(resultR1.ToString() == userInput.Text.ToString()) {
								match += 1;
								totalMatches += 1;
								matchText.Text = "MATCHES: " + match;
							}
							if(resultR2.ToString() == userInput.Text.ToString()){
								match += 1;
								totalMatches += 1;
								matchText.Text = "MATCHES: " + match;
							}
							if(resultR3.ToString() == userInput.Text.ToString()){ 
								match += 1;
								totalMatches += 1;
								matchText.Text = "MATCHES: " + match;
							}
							if(resultR1.ToString() != userInput.Text.ToString() && resultR2.ToString() != userInput.Text.ToString() &&
								resultR3.ToString() != userInput.Text.ToString()){
								match = 0;
								matchText.Text = "MATCHES: " + match;
							}
							if(match == 0){
								currentAmountInt -= betAmountInt;
								totalAmountLost += betAmountInt;
								currentAmount.Text = currentAmountInt.ToString();
								if(currentAmountInt <= 0){
									currentAmount.Text = "0";
									currentAmountText.Text = "Game Over!";
								} else{
									currentAmountText.Text = currentAmountInt.ToString();
								}
							}
							else{
								currentAmountInt += (match * betAmountInt);
								totalAmountWon += (match * betAmountInt);
								currentAmount.Text = currentAmountInt.ToString();
								currentAmountText.Text = currentAmountInt.ToString();
							}

							// Populate stats
							for(int i = 0; i < 6; i++){
								if(CALDGStats[i, 0] == userInputInt){
									// Latest Amount
									CALDGStats[i, 1] = currentAmountInt;
									// Latest Bet
									CALDGStats[i, 2] = betAmountInt;
									// Total Lost for integer user input
									CALDGStats[i, 3] = totalAmountLost;
									// Total Won for integer user input
									CALDGStats[i, 4] = totalAmountWon;
									// Total Matches
									CALDGStats[i, 5] = totalMatches;
								}
							}
							// Store stats in shared preferences
							int k = 0;
							for(int i = 0; i < 6; i++){ 
								for(int j = 0; j < 6; j++){
									CALDGStatsString[i,j] = CALDGStats[i,j].ToString();
									CALDGEditor.PutString("CALDGStatsString" + k, CALDGStatsString[i,j]);
									k++;
								}
							}
							CALDGEditor.Apply();
							match = 0;
						}
						else{
							errorText.Text = "Invalid Input! Try Again";
						}
					}
				}
				else{
					Toast.MakeText (this, "Invalid Input! Try Again", ToastLength.Long).Show();
				}
			};

			// Gesture Detection
			gestureDetector = new GestureDetector(this);
		}

		// Randomizer
		private int RandomNumber (int minRange, int maxRange){
			lock(syncLock) {
				return randomNum.Next(minRange, maxRange + 1);
			}
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
