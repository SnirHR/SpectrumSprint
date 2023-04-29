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
using SpectrumSprint.Activities;
using SpectrumSprint.Models;

namespace SpectrumSprint.Adapters
{
    public class RoomsAdapter : BaseAdapter<NetworkObject>
    {

        Context context;
        List<NetworkObject> roomsList;
        public RoomsAdapter(Context context, List<NetworkObject> roomsList)
        {
            this.context = context;
            this.roomsList = roomsList;
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
            LayoutInflater layoutInflater = ((MainActivity)this.context).LayoutInflater;
            View view = layoutInflater.Inflate(Resource.Layout.room_row, parent, false);
            TextView txtViewListviewRowRoomName = view.FindViewById<TextView>(Resource.Id.roomName);
            Button buttonListviewRowJoinRoom = view.FindViewById<Button>(Resource.Id.joinRoomButton);
            buttonListviewRowJoinRoom.Click += ButtonListviewRowJoinRoom_Click;
            NetworkObject currentNetworkObject = this.roomsList[position];
            if(currentNetworkObject!=null)
            {
                txtViewListviewRowRoomName.Text = currentNetworkObject.RoomName;
                buttonListviewRowJoinRoom.Tag = position.ToString();
            }
            return view;
        }

        private void ButtonListviewRowJoinRoom_Click(object sender, EventArgs e)
        {
            int position = int.Parse(((Button)sender).Tag.ToString());
            NetworkObject networkObject = this.roomsList[position];
            string roomName =networkObject.RoomName;
            //GameNetworkObject gameNetworkObject = new GameNetworkObject(roomName);
            //if (!await gameNetworkObject.ExistRoom())
            //{
            //    Toast.MakeText(this, "this room does not exist", ToastLength.Long).Show();
            //    return;
            //}
            Intent intent = new Intent((MainActivity)this.context, typeof(GameActivity));
            intent.PutExtra("RoomName", roomName);
            ((MainActivity)this.context).StartActivityForResult(intent, 100);

        }
        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return this.roomsList.Count;
            }
        }

        public override NetworkObject this[int position]//מחזיר את  העצם של מיקום מסויים
        {
            get
            {
                return this.roomsList[position];
            }
        }
    }

    class RoomsAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
    }
}