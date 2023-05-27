using Random = System.Random;
using SpectrumSprint.Constants;
using SpectrumSprint.Models;
using Android.Gms.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System;
using Java.Lang;
using Java.Util;
using Firebase.Firestore;
using Android.Content;
using Android.App;
using System.Linq;

namespace SpectrumSprint.Handlers
{
    public class Networker
    {
        public static async void CreateLeaderboardScore(int score)
        {
            var shared = Application.Context.GetSharedPreferences(PathConstants.CURRENT_USER_FILE, FileCreationMode.Private);
            FirebaseFirestore firestore = ConnectionHandler.GetFirestore();
            CollectionReference collectionRef = firestore.Collection(PathConstants.LEADERBOARD_COLLECTION);
            HashMap newScore = new HashMap();
            newScore.Put("Name",PathConstants.USER_NAME);
            newScore.Put("Score", score);

            await collectionRef.Add(newScore);
        }
        public static async void CreateRoom(string roomName)
        {
            FirebaseFirestore firestore = ConnectionHandler.GetFirestore();
            CollectionReference collectionRef = firestore.Collection(PathConstants.ROOMS_COLLECTION);
            HashMap newRoom = new HashMap();
            long seed = MakeSeed(roomName);
            GameConstants.SEED = seed;
            newRoom.Put("RoomName", roomName);
            newRoom.Put("Seed", seed);

            await collectionRef.Add(newRoom);
        }
        private static long MakeSeed(string input)
        {
            Random rnd = new Random();
            string seed = input + rnd.Next(1, 999); // מוסיף לשם החדר מספר אקראי בכדי לוודא שאותו השם לא יצור את אותו השלב
            seed = Convert.ToBase64String(Encoding.UTF8.GetBytes(seed)); // ממיר את ה-Seed ל-Base64
            long sum = 0;
            foreach (char c in seed) // הופך את המחרוזת לסכום של נתוני ה-ASCII של כל תו
            {
                sum += (long)c;
            }
            return sum; 
        }
        public static async Task<string> GetName(string email)
        {
            FirebaseFirestore database = ConnectionHandler.GetFirestore();
            try
            {
                CollectionReference collection = database.Collection(PathConstants.USER_COLLECTION);

                QuerySnapshot querySnapshot = (QuerySnapshot) await collection.WhereEqualTo(PathConstants.Email_FIELD, email).Get();

                if (querySnapshot.Documents.Count > 0)
                {
                    DocumentSnapshot documentSnapshot = querySnapshot.Documents[0];
                    if (documentSnapshot.Contains(PathConstants.USER_NAME))
                    {
                        string name = documentSnapshot.GetString(PathConstants.USER_NAME);
                        return name;
                    }
                    else
                    {
                        return "Name not found";
                    }
                }
                else
                {
                    return "User not found";
                }
            }
            catch
            {
                return "Error";
            }
        }
        public static async Task<long> GetSeed(string roomName)
        {
            long seed = 0;
            FirebaseFirestore firestore = ConnectionHandler.GetFirestore();
            DocumentReference docRef = firestore.Collection(PathConstants.ROOMS_COLLECTION).Document(roomName);
            DocumentSnapshot docSnapshot = (DocumentSnapshot)await docRef.Get();
            
            if (docSnapshot.Contains(PathConstants.SEED_FIELD))
            {
                 seed = (long) docSnapshot.Get(PathConstants.SEED_FIELD);
            }
            return seed;


        }

        public static async Task<List<NetworkObject>> GetLeaderboard()
        {
            FirebaseFirestore firestore = ConnectionHandler.GetFirestore();
            CollectionReference collectionRef = firestore.Collection(PathConstants.LEADERBOARD_COLLECTION);
            QuerySnapshot querySnapshot = (QuerySnapshot) await collectionRef.Get();

            List<NetworkObject> leaderboard = new List<NetworkObject>();

            foreach (var documentSnapshot in querySnapshot.Documents)
            {
                NetworkObject player = new NetworkObject();
                player.playerName = documentSnapshot.GetString("Name");
                player.score = (long)documentSnapshot.GetLong("Score");

                leaderboard.Add(player);
            }

            return leaderboard;
        }

        public static async Task<bool> RoomExist(string roomName)
        {
            FirebaseFirestore database = ConnectionHandler.GetFirestore();
            try
            {
                Java.Lang.Object obj = await database.Collection(PathConstants.ROOMS_COLLECTION).WhereEqualTo(PathConstants.ROOM_NAME, roomName).Get();
                QuerySnapshot querySnapshot = (QuerySnapshot)obj;
                if (querySnapshot.IsEmpty)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        public static async Task<bool> Logout()
        {
            try
            {
                var editor = Application.Context.GetSharedPreferences(PathConstants.CURRENT_USER_FILE, FileCreationMode.Private).Edit();
                editor.PutString("Email", "");
                editor.PutString("Name", "");
                editor.PutString("Password", "");
                editor.Apply();
                ConnectionHandler.GetFirebaseAuthentication().SignOut();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static async Task<NetworkObject> GetNetWorkObject(string PlayerName,string collection)
        {
            NetworkObject networkObject = null;
            try
            {
                FirebaseFirestore firebaseFirestore = ConnectionHandler.GetFirestore();
                Java.Lang.Object obj = await firebaseFirestore.Collection(collection).WhereEqualTo("Name", PlayerName).Get();
                QuerySnapshot querySnapshot = (QuerySnapshot)obj;
                networkObject = new NetworkObject();
            }
            catch
            {
                return null;
            }
            return networkObject;
        }

    }
}