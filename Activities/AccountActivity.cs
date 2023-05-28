using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using SpectrumSprint.Constants;
using SpectrumSprint.Handlers;
using SpectrumSprint.Models;
using System;
using System.Resources;
using Xamarin.Essentials;

namespace SpectrumSprint.Activities
{
    [Activity(Label = "AccountActivity")]
    public class AccountActivity : Activity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private ISharedPreferences shared;
        private LinearLayout profileLayout, LoginContainer;
        public Button submitButton,Signout;
        public EditText username, email, password;
        protected TextView pageToggleText, profileName;
        private Login login;
        public int pageState = 0; //represents the current login state 0 = user is prompted to log in. 1 = prompted to register. 2 = currently logged in
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_account);
            Initialize();
        }

        public void Initialize()
        {
            this.profileLayout = FindViewById<LinearLayout>(Resource.Id.profileContainer);
            this.LoginContainer = FindViewById<LinearLayout>(Resource.Id.LoginContainer);
            this.submitButton = FindViewById<Button>(Resource.Id.buttonSubmit);
            this.Signout = FindViewById<Button>(Resource.Id.signOutButton);
            this.pageToggleText = FindViewById<TextView>(Resource.Id.pageToggleText);
            this.profileName = FindViewById<TextView>(Resource.Id.ProfileName);
            this.username = FindViewById<EditText>(Resource.Id.userNameInput);
            this.email = FindViewById<EditText>(Resource.Id.emailInput);
            this.password = FindViewById<EditText>(Resource.Id.passwordInput);
            this.login = new Login(this, this);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigationAccount);
            navigation.SetOnNavigationItemSelectedListener(this);
            shared = Application.Context.GetSharedPreferences(PathConstants.CURRENT_USER_FILE, FileCreationMode.Private);
            switch (shared.GetString("Email", ""))
            {
                case "":
                    this.pageState = 0;
                    break;
                default:
                    this.pageState = 2;
                    break;
            }
            TogglePage(pageState);
            pageToggleText.Click += PageToggleText_Click;
            submitButton.Click += SubmitButton_Click;
            Signout.Click += Signout_Click;
        }

        private async void Signout_Click(object sender, EventArgs e)
        {
            await Networker.Logout();
            TogglePage(0);
        }

        private void PageToggleText_Click(object sender, EventArgs e)
        {
            switch (pageState)
            {
                case 0:
                    pageState = 1;
                    TogglePage(pageState);
                    break;
                case 1:
                    pageState = 0;
                    TogglePage(pageState);
                    break;
                default:
                    break;
            }
        }

        private async void SubmitButton_Click(object sender, EventArgs e)
        {
            if (pageState == 1)
            {
                if (await this.login.Register() == true)
                {
                    Toast.MakeText(this, "Register Successful!", ToastLength.Long).Show();
                    TogglePage(2);
                }
                else
                {
                    Toast.MakeText(this, "Register Failed :(", ToastLength.Long).Show();
                }
            }
            else if (pageState == 0)
            {
                if (await this.login.SignIn() == true)
                {
                    Toast.MakeText(this, "Login In Successful!", ToastLength.Long).Show();
                    TogglePage(2);
                }
                else
                {
                    Toast.MakeText(this, "Login Failed :(", ToastLength.Long).Show();
                }
            }
        }
        public void TogglePage(int pageState)
        {
            this.pageState = pageState;
            switch (this.pageState)
            {
                case 0:
                    ToggleLogin();
                    break;
                case 1:
                    ToggleRegister();
                    break;
                case 2:
                    toggleProfile();
                    break;
                default:
                    break;
            }
        }
        public void TogglePage()
        {
            switch (shared.GetString("Email", ""))
            {
                case "":
                    this.pageState = 0;
                    break;
                default:
                    this.pageState = 2;
                    break;
            }
            switch (this.pageState)
            {
                case 0:
                    ToggleLogin();
                    break;
                case 1:
                    ToggleRegister();
                    break;
                case 2:
                    toggleProfile();
                    break;
                default:
                    break;
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

        private void toggleProfile()
        {
            LoginContainer.Visibility = ViewStates.Gone;
            profileLayout.Visibility = ViewStates.Visible;
            profileName.Text = shared.GetString(PathConstants.USER_NAME, "Name");

        }
        private void ToggleLogin()
        {
            LoginContainer.Visibility = ViewStates.Visible;
            profileLayout.Visibility = ViewStates.Gone;
            username.Visibility = ViewStates.Gone;
            submitButton.Text = "Sign In";
            pageToggleText.Text = "Don't have an account?";
        }

        private void ToggleRegister()
        {
            LoginContainer.Visibility = ViewStates.Visible;
            profileLayout.Visibility = ViewStates.Gone;
            username.Visibility = ViewStates.Visible;
            submitButton.Text = "Register";
            pageToggleText.Text = "Already have an account?";
        }

    }
}