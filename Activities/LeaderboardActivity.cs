using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using SpectrumSprint.Adapters;
using SpectrumSprint.Constants;
using SpectrumSprint.Handlers;
using SpectrumSprint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace SpectrumSprint.Activities
{
    [Activity(Label = "LeaderboardActivity")]
    public class LeaderboardActivity : Activity, BottomNavigationView.IOnNavigationItemSelectedListener
    {

        LeaderboardAdapter LeaderboardAdapter;
        List<NetworkObject> playersList;
        ListView listPlayers;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_leaderboard);
            Initialize();
        }
        public async void Initialize()
        {
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigationLeaderboard);
            navigation.SetOnNavigationItemSelectedListener(this);
            listPlayers = FindViewById<ListView>(Resource.Id.leaderboardView);
            playersList = await Networker.GetLeaderboard();
            playersList.Sort((p1, p2) => p2.score.CompareTo(p1.score));
            LeaderboardAdapter = new LeaderboardAdapter(this, playersList);
            listPlayers.Adapter = LeaderboardAdapter;

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
                    Vibration.Vibrate(100);
                    return true;
                case Resource.Id.navigation_account:
                    Intent account = new Intent(this, typeof(AccountActivity));
                    StartActivity(account);
                    return true;
            }
            return false;
        }

    }
}