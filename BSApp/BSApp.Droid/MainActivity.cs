using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using BSApi;
using BSApi.Data;

namespace BSApp.Droid
{
	[Activity (Label = "BSApp.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
	    private Button btnLoad;
	    private ListView lview;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			btnLoad = FindViewById<Button> (Resource.Id.btnLoad);
            btnLoad.Click += ButtonOnClick;

		    lview = FindViewById<ListView>(Resource.Id.listView1);
		}

	    private void ButtonOnClick(object sender, EventArgs eventArgs)
	    {
	        Toast.MakeText(this, "Loading...", ToastLength.Long).Show();
	        List<SeriesInformation> sList = Api.GetSeries();

            ArrayAdapter arrayAdapter = new ArrayAdapter<SeriesInformation>(
                this, Android.Resource.Layout.SimpleListItem1,
                sList);

	        lview.Adapter = arrayAdapter;

            Toast.MakeText(this, $"{sList.Count} Series loaded!", ToastLength.Long).Show();
	    }
	}
}


