
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

namespace AstroQuiz_2
{
	[Activity (Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen", Label = "Score")]			
	public class EndActivity : Activity
	{
		int score;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.score);
			
			score = Intent.GetIntExtra("SCORE", 0);
			
			TextView tv = FindViewById<TextView>(Resource.Id.textView1);
			tv.Text = scoreText();
			
			Button btnAgain = FindViewById<Button> (Resource.Id.btnAgain);
			btnAgain.Click += delegate {
				StartActivity(typeof(QuizActivity));
				Finish();
			};
			
			Button btnQuit = FindViewById<Button> (Resource.Id.btnQuit);
			btnAgain.Click += delegate {
				Finish();
			};
		}
		public string scoreText()
		{
			string text, scoreString;
			if(score==500){
				text = "Perfect!";
			}else if(score<500 && score>=400){
				text = "Well done";
			}else if(score<400 && score>=300){
				text = "Not bad";
			}else if(score<300 && score>=200){
				text = "How about another go?";
			}else if(score<200 && score>=100){
				text = "Not a science fan?";
			}else{
				text = "Are you ok?";
			}
			scoreString = String.Format("\nYour Score:\n\n{0}\n\n{1}\n",score.ToString(), text);
			
			return scoreString;
		}
	}
}

