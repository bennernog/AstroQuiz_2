using System;

using Android.App;
using Android.Util;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace AstroQuiz_2
{
	[Activity (Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen", Label = "Astro")]			
	public class QuizActivity : Activity
	{
		Button button1, button2, button3, button4;
		TextView tvQuestion;
		LinearLayout mainLL;
		
		string[] sourceString;
		string image, question, correctAnswer, wrongAnswer1, wrongAnswer2, wrongAnswer3;
		List<string> quiz;
		int[] imageId = new int[]{
			Resource.Drawable.vraag00, Resource.Drawable.vraag01, Resource.Drawable.vraag02, Resource.Drawable.vraag03,
			Resource.Drawable.vraag04, Resource.Drawable.vraag05, Resource.Drawable.vraag06, Resource.Drawable.vraag07,
			Resource.Drawable.vraag08, Resource.Drawable.vraag09, Resource.Drawable.vraag10, Resource.Drawable.vraag11,
			Resource.Drawable.vraag12, Resource.Drawable.vraag13, Resource.Drawable.vraag14, Resource.Drawable.vraag15,
			Resource.Drawable.vraag16, Resource.Drawable.vraag17, Resource.Drawable.vraag18, Resource.Drawable.vraag19,
		};
		int count = 0, score = 0, points = 0;
		
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			if (bundle!=null) score = bundle.GetInt("SCORE",0);
			SetContentView (Resource.Layout.Main);
			sourceString = Resources.GetStringArray(Resource.Array.quiz);
			quiz = new List<string>();
			foreach (string s in sourceString)
			{
				quiz.Add(s);
			}
			quiz=Shuffle(quiz);
			
			mainLL = FindViewById<LinearLayout> (Resource.Id.LLmain);
			tvQuestion = FindViewById<TextView> (Resource.Id.textView1);
			button1 = FindViewById<Button> (Resource.Id.btn1);
			button2 = FindViewById<Button> (Resource.Id.btn2);
			button3 = FindViewById<Button> (Resource.Id.btn3);
			button4 = FindViewById<Button> (Resource.Id.btn4);
			
			button1.Click += button_Click;
			button2.Click += button_Click;
			button3.Click += button_Click;
			button4.Click += button_Click;
			
			newQuestion(quiz);
		}
		
		public void newQuestion(List<string> list)
		{
			string[] stringArrayForQuestion = list.ToArray();
			string stringForQuestion = stringArrayForQuestion[count];
			string[] newQuestionArray = stringForQuestion.Split('_');
			
			image 			= newQuestionArray[0];
			question 		= newQuestionArray[1];
			correctAnswer 	= newQuestionArray[2];
			wrongAnswer1  	= newQuestionArray[3];
			wrongAnswer2  	= newQuestionArray[4];
			wrongAnswer3  	= newQuestionArray[5];
			
			setImage(image);
			setQuestion(question);
			setAnswers(correctAnswer, wrongAnswer1, wrongAnswer2, wrongAnswer3); 
			
		}
		
		public void setImage (string imageString)
		{
			int index = Convert.ToInt32(imageString);
			mainLL.SetBackgroundResource(imageId[index]);
		}
		public void setQuestion (string questionString)
		{
			string q = String.Format("{0}. {1}", (count+1).ToString(),questionString);
			tvQuestion.Text = q;
		}
		
		public void setAnswers (string cAnswer, string wAnswer1, string wAnswer2, string wAnswer3)
		{
			List<string> answers = new List<string>();
			answers.Add(cAnswer);
			answers.Add(wAnswer1);
			answers.Add(wAnswer2);
			answers.Add(wAnswer3);
			answers=Shuffle(answers);
			string [] mixedAnswers = answers.ToArray();
			
			button1.Text = mixedAnswers[0];
			button2.Text = mixedAnswers[1];
			button3.Text = mixedAnswers[2];
			button4.Text = mixedAnswers[3];
			
		}
		
		private void setScore ()
		{
			switch (points) {
			case 0:
				score += 50;
				break;
			case 1:
				score += 40;
				break;
			case 2:
				score += 30;
				break;
			case 3:
				score += 20;
				break;
			default:
				score += 0;
				break;
			}
			points = 0;
		}
		private string answerText ()
		{
			string text;
			switch (points) {
			case 0:
				text = "Perfect!";
				break;
			case 1:
				text = "Well done";
				break;
			case 2:
				text = "Good guess";;
				break;
			case 3:
				text = "About time!!";
				break;
			default:
				text = "Seriously?";
				break;
			}
			return text;
		}
		public static List<T> Shuffle<T>( List<T> list)
		{
			RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
			int n = list.Count;
			while (n > 1)
			{
				byte[] box = new byte[1];
				do provider.GetBytes(box);
				while (!(box[0] < n * (Byte.MaxValue / n)));
				int k = (box[0] % n);
				n--;
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
			return list;
		}
		
		private void button_Click (object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			if (btn.Text.Equals (correctAnswer)) {
				count++;
				Toast.MakeText(this, answerText(), ToastLength.Short).Show();
				setScore();
				
				if (count<10)
				{
					newQuestion(quiz);
				}
				else
				{
					Intent i = new Intent(this, typeof(EndActivity));
					i.PutExtra("SCORE", score);
					StartActivity(i);
				}
				
				
			} else 
			{
				
				Toast.MakeText(this, "Try Again", ToastLength.Short).Show();
				points++;
				
			}
			
		}  
		protected override void OnSaveInstanceState (Bundle outState)
		{
			outState.PutInt ("SCORE", score);
			base.OnSaveInstanceState (outState);
		}
	}
}

