using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SpectrumSprint.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumSprint.Activities
{
    [Activity(Label = "AccountActivity")]
    public class AccountActivity : Activity
    {
        LinearLayout profileLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_account);
            profileLayout = FindViewById<LinearLayout>(Resource.Id.profileContainer);
            Initialize();
        }

        public void Initialize()
        {
            var editor = Application.Context.GetSharedPreferences(PathConstants.CURRENT_USER_FILE, FileCreationMode.Private);
            if (editor.GetString("Email", "") == "")
            {
                profileLayout.Visibility = ViewStates.Gone;
            }
        }
    }
}