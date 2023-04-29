using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SpectrumSprint.Activities;
using SpectrumSprint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumSprint.Adapters
{
    public class LeaderboardAdapter : BaseAdapter<NetworkObject>
    {

        private List<NetworkObject> playersList;
        Context context;

        public LeaderboardAdapter(Context context, List<NetworkObject> playersList)
        {
            this.context = context;
            this.playersList = playersList;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layoutInflater = ((LeaderboardActivity)this.context).LayoutInflater;
            View view = layoutInflater.Inflate(Resource.Layout.leaderboard_row, parent, false);
            TextView textViewPlayer = view.FindViewById<TextView>(Resource.Id.playerName);
            TextView textViewScore = view.FindViewById<TextView>(Resource.Id.playerScore);
            NetworkObject currentNetWorkObject = this.playersList[position];
            if (currentNetWorkObject != null)
            {
                textViewPlayer.Text = currentNetWorkObject.playerName;
                textViewScore.Text = currentNetWorkObject.score.ToString();
            }

            return view;
        }
        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return this.playersList.Count;
            }
        }

        public override NetworkObject this[int position]
        {
            get
            {
                return playersList[position];
            }
        }
    }

    internal class AdapterLeaderboardsViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}