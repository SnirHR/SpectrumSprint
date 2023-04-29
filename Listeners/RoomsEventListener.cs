using System;
using System.Collections.Generic;
using Firebase.Firestore;
using SpectrumSprint.Handlers;
using SpectrumSprint.Models;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SpectrumSprint.Listeners
{
    public class RoomsEventListener :Java.Lang.Object,IEventListener
    {
        private List<NetworkObject> RoomsList;
        public event EventHandler<RoomsEventArgs> OnRoomsRetrieved;
        public class RoomsEventArgs : EventArgs
        {
            public List<NetworkObject> Rooms { get; set; }
        }
        
        public RoomsEventListener()
        {
            ConnectionHandler.GetFirestore().Collection("Rooms").AddSnapshotListener(this);
        }
        public void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
        {
            QuerySnapshot querySnapshot = (QuerySnapshot) value;
            if (querySnapshot.IsEmpty)
            {
                return;
            }
            this.RoomsList = new List<NetworkObject>();
            foreach (DocumentSnapshot document in querySnapshot.Documents )
            {
                NetworkObject networkObject = new NetworkObject();
                if (document.Get("RoomName") != null)
                {
                    networkObject.RoomName = document.Get("RoomName").ToString();
                }
                else
                {
                    networkObject.RoomName = "";
                }
                
                this.RoomsList.Add(networkObject);
            }
            if (this.OnRoomsRetrieved!=null)
            {
                RoomsEventArgs e = new RoomsEventArgs();
                e.Rooms = this.RoomsList;
                this.OnRoomsRetrieved.Invoke(this, e);
            }
        }
    }
}