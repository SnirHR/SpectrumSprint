using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using SpectrumSprint.Constants;
using Xamarin.Essentials;

namespace SpectrumSprint.Activities
{
    [Activity(Label = "AccountActivity")]
    public class AccountActivity : Activity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        LinearLayout profileLayout, SigninContainer;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_account);
            profileLayout = FindViewById<LinearLayout>(Resource.Id.profileContainer);
            SigninContainer = FindViewById<LinearLayout>(Resource.Id.SigninContainer);
            Initialize();
        }

        public void Initialize()
        {
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigationAccount);
            navigation.SetOnNavigationItemSelectedListener(this);
            var editor = Application.Context.GetSharedPreferences(PathConstants.CURRENT_USER_FILE, FileCreationMode.Private);
            if (editor.GetString("Email", "") == "")
            {
                SigninContainer.Visibility = ViewStates.Visible;
                profileLayout.Visibility = ViewStates.Gone;
            }
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    Intent main = new Intent(this, typeof(MainActivity));
                    StartActivity(main);
                    return true;
                case Resource.Id.navigation_leaderboard:
                    Intent account = new Intent(this, typeof(LeaderboardActivity));
                    StartActivity(account);
                    return true;
                case Resource.Id.navigation_account:
                    Vibration.Vibrate(100);
                    return true;
            }
            return false;
        }
    }
}