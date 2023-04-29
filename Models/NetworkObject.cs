using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Java.Util;
using SpectrumSprint.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumSprint.Models
{
    public class NetworkObject
    {
        public string playerName { get; set; }
        public long score { get; set; }
        public string RoomName { get; internal set; }

        public NetworkObject()
        {
        }

        public NetworkObject(string PlayerName, long score)
        {
            this.playerName = PlayerName;
            this.score = score;
        }
        public HashMap GetHashMapOfObject()
        {
            HashMap gNoHashMap = new HashMap();
            gNoHashMap.Put("Name", this.playerName);
            return gNoHashMap;
        }
        
    }
}