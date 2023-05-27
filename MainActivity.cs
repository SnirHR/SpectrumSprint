
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.TextField;
using SpectrumSprint.Activities;
using SpectrumSprint.Adapters;
using SpectrumSprint.Handlers;
using SpectrumSprint.Listeners;
using SpectrumSprint.Models;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace SpectrumSprint
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        List<NetworkObject> roomsList;

        RoomsEventListener roomsEventListener;
        RoomsAdapter roomsAdapter;
        ListView listViewRooms;
        EditText roomNameInput;
        Button createRoomButton;
 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Initialize();
        }
        private void Initialize()
        {
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            this.createRoomButton = FindViewById<Button>(Resource.Id.createRoomButton);
            this.listViewRooms = FindViewById<ListView>(Resource.Id.listViewRooms);
            this.roomNameInput = FindViewById<EditText>(Resource.Id.roomNameInput);
            this.roomsEventListener = new RoomsEventListener();
            this.createRoomButton.Click += CreateRoomButton_Click;
            this.roomsEventListener.OnRoomsRetrieved += RoomsEventListener_OnRoomsRetrieved; ;
        }

        private async void CreateRoomButton_Click(object sender, System.EventArgs e)
        {
            if (roomNameInput.Text != null)
            {
                if (await Networker.RoomExist(roomNameInput.Text) != true)
                {
                    Networker.CreateRoom(roomNameInput.Text);
                }
            }
        }

        private void RoomsEventListener_OnRoomsRetrieved(object sender, RoomsEventListener.RoomsEventArgs e)
        {
            this.roomsList = e.Rooms;
            if (this.roomsList != null)
            {
                this.roomsAdapter = new RoomsAdapter(this, this.roomsList);
                this.listViewRooms.Adapter = this.roomsAdapter;
            }
            return;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    Vibration.Vibrate(100);
                    return true;
                case Resource.Id.navigation_leaderboard:
                    Intent leaderboardIntent = new Intent(this, typeof(LeaderboardActivity));
                    StartActivity(leaderboardIntent);
                    return true;
                case Resource.Id.navigation_account:
                    Intent AccountIntent = new Intent(this, typeof(AccountActivity));
                    StartActivity(AccountIntent);
                    return true;
            }
            return false;
        }
    }
}

