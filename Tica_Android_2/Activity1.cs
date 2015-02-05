using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Microsoft.Xna.Framework;

namespace Tica_Android_2
{
	[Activity (Label = "Tica_Android_2",
		MainLauncher = true,
		Icon = "@drawable/icon",
		Theme = "@style/Theme.Splash",
		AlwaysRetainTaskState = true,
		LaunchMode = Android.Content.PM.LaunchMode.SingleInstance,
		ScreenOrientation=ScreenOrientation.Landscape)
	]
	public class Activity1 : AndroidGameActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create our OpenGL view, and display it
			var g = new Game1 ();
			SetContentView (g.Services.GetService<View> ());
			g.Run ();
		}
		
	}
}


