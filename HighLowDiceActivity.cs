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
using Android.Views.InputMethods;

namespace Dicemaster
{
	[Activity (Label = "Dice Master", Icon = "@drawable/Dice", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class HighLowDiceActivity : Activity, GestureDetector.IOnGestureListener
	{
		private GestureDetector gestureDetector;

		private static int SWIPE_THRESHOLD = 100;
		private static int SWIPE_VELOCITY_THRESHOLD = 100;
		public static String HLDG_DATA = "HLDGData";

		private static readonly Random randomNum = new Random();
		private static readonly object syncLock = new object();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.HighLowDiceScreen);

			var numberResult1 = FindViewById<TextView> (Resource.Id.numberResult1);
			var numberResult2 = FindViewById<TextView> (Resource.Id.numberResult2);
			var highlowText = FindViewById<TextView> (Resource.Id.highlowText);
			var highlowdieText = FindViewById<TextView> (Resource.Id.highlowdieText);
			var sumRoll = FindViewById<TextView> (Resource.Id.sumRoll);
			var betAmount = FindViewById<EditText> (Resource.Id.betAmount);
			var currentAmountText = FindViewById<TextView> (Resource.Id.currentAmountText);
			var currentAmount = FindViewById<EditText> (Resource.Id.currentAmount);

			Button diceButton = FindViewById<Button> (Resource.Id.diceButton);

			RadioButton highRadioButton = FindViewById<RadioButton>(Resource.Id.highRadioButton);
			RadioButton sevenRadioButton = FindViewById<RadioButton>(Resource.Id.sevenRadioButton);
			RadioButton lowRadioButton = FindViewById<RadioButton>(Resource.Id.lowRadioButton);

			numberResult1.TextSize = 110;
			numberResult2.TextSize = 110;
			highlowText.TextSize = 20;
			sumRoll.TextSize = 20;
			highlowdieText.TextSize = 20;

			int currentAmountInt = 0;
			int betAmountInt = 0;

			int totalHighMatches = 0;
			int totalSevenMatches = 0;
			int totalLowMatches = 0;

			int totalAmountWon = 0;
			int totalAmountLost = 0;

			highRadioButton.Click += RadioButtonClick;
			sevenRadioButton.Click += RadioButtonClick;
			lowRadioButton.Click += RadioButtonClick;

			ISharedPreferences HLDGPrefs = GetSharedPreferences (HLDG_DATA, FileCreationMode.Private);
			ISharedPreferencesEditor HLDGEditor = HLDGPrefs.Edit ();

			// Roll the dice button
			diceButton.Click += delegate {
				if((Int32.TryParse(currentAmount.Text.ToString(), out currentAmountInt)) && (Int32.TryParse(betAmount.Text.ToString(), out betAmountInt))){
					if(currentAmountInt == 0 && betAmountInt == 0) { Toast.MakeText (this, "Fields are 0", ToastLength.Long).Show(); }
					else if(currentAmountInt == 0) { Toast.MakeText (this, "Total Amount is 0", ToastLength.Long).Show();}
					else if(betAmountInt == 0) { Toast.MakeText (this, "Bet is 0", ToastLength.Long).Show(); }
					else if(betAmountInt > currentAmountInt) { Toast.MakeText (this, "Bet is bigger than Total Amount", ToastLength.Long).Show(); }
					else{

						currentAmount.Enabled = false;
						currentAmount.Focusable = false;

						InputMethodManager closeKeyboard = (InputMethodManager)GetSystemService(Context.InputMethodService);
						closeKeyboard.HideSoftInputFromWindow(betAmount.WindowToken, 0);

						int resultR1 = RandomNumber (1, 6);
						int resultR2 = RandomNumber (1, 6);
						int totalR = resultR1 + resultR2;

						numberResult1.Text = resultR1.ToString ();
						numberResult2.Text = resultR2.ToString ();

						if(totalR > 7){ highlowText.Text = "It's HIGH!";}
						else if(totalR < 7){ highlowText.Text = "It's LOW!";}
						else { highlowText.Text = "It's SEVEN!";}

						if(currentAmountInt > 0){
							currentAmountText.Text = currentAmountInt.ToString();

							// High selected
							if(highRadioButton.Checked == true && totalR > 7) {
								currentAmountInt += betAmountInt;

								currentAmount.Text = currentAmountInt.ToString();
								currentAmountText.Text = currentAmountInt.ToString();

								totalHighMatches += 1;
								totalAmountWon += betAmountInt;
								HLDGEditor.PutInt("totalHighMatches", totalHighMatches);
								HLDGEditor.PutInt("totalAmountWon", totalAmountWon);
							}
							//////

							// Seven selected
							else if(sevenRadioButton.Checked == true && totalR == 7) {
								currentAmountInt += (4*betAmountInt);

								currentAmount.Text = currentAmountInt.ToString();
								currentAmountText.Text = currentAmountInt.ToString();

								totalSevenMatches += 1;
								totalAmountWon += betAmountInt;
								HLDGEditor.PutInt("totalSevenMatches", totalSevenMatches);
								HLDGEditor.PutInt("totalAmountWon", totalAmountWon);
							}
							//////

							// Low selected
							else if(lowRadioButton.Checked == true && totalR < 7) {
								currentAmountInt += betAmountInt;

								currentAmount.Text = currentAmountInt.ToString();
								currentAmountText.Text = currentAmountInt.ToString();

								totalLowMatches += 1;
								totalAmountWon += betAmountInt;
								HLDGEditor.PutInt("totalLowMatches", totalLowMatches);
								HLDGEditor.PutInt("totalAmountWon", totalAmountWon);
							}

							else{
								currentAmountInt -= betAmountInt;

								currentAmount.Text = currentAmountInt.ToString();
								currentAmountText.Text = currentAmountInt.ToString();

								totalAmountLost += betAmountInt;
								HLDGEditor.PutInt("totalAmountLost", totalAmountLost);

								if(currentAmountInt <= 0){
									currentAmount.Text = "0";
									currentAmountText.Text = "Game Over!";
								} 
							}
						}

						HLDGEditor.PutInt("totalAmount", currentAmountInt - betAmountInt);
						HLDGEditor.PutInt("totalBet", betAmountInt);

						sumRoll.Text = totalR.ToString();
						HLDGEditor.PutInt("totalSumRoll", totalR);
					}
				}
				else{
					Toast.MakeText (this, "Invalid Input! Try Again", ToastLength.Long).Show();
				}
				HLDGEditor.Apply();
			};

			// Gesture Detection
			gestureDetector = new GestureDetector(this);
		}

		// Radio button event handler
		private void RadioButtonClick (object sender, EventArgs e)
		{
			RadioButton rb = (RadioButton) sender;
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