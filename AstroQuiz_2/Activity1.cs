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
	[Activity (Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen", Label = "AstroQuiz2", MainLauncher = true)]
	public class Activity1 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.welcome);
			Button button = FindViewById<Button> (Resource.Id.btnStarQuiz);
			button.Click += delegate {
				StartActivity(typeof(QuizActivity));
				Finish();
			};
			
		}
	}
}


