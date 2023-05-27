using Android.App;
using Android.Content;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;

namespace SpectrumSprint.Handlers
{
    public static class ConnectionHandler
    {
        static ISharedPreferences preferences = Application.Context.GetSharedPreferences("userinfo", FileCreationMode.Private);
        static ISharedPreferencesEditor editor;
        static FirebaseFirestore database;
        public static FirebaseFirestore GetFirestore()
        {
            if (database!=null)
            {
                return database;
            }
            var app = FirebaseApp.InitializeApp(Application.Context);

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("bagrot-e5280")
                    .SetApplicationId("bagrot-e5280")
                    .SetApiKey("AIzaSyAMOnLE92w_UXJaxGoDyvALWiOpW9VfdaE")
                    .SetDatabaseUrl("https://bagrot-e5280.firebaseio.com")
                    .SetStorageBucket("bagrot-e5280.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options, "Bagrot");
                database = FirebaseFirestore.GetInstance(app);
            }
            else
            {
                database = FirebaseFirestore.GetInstance(app);
            }
            return database;
        }
        public static FirebaseAuth GetFirebaseAuthentication()
        {
            FirebaseAuth firebaseAuthentication;
            var app = FirebaseApp.InitializeApp(Application.Context);
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("bagrot-e5280")
                    .SetApplicationId("bagrot-e5280")
                    .SetApiKey("AIzaSyAMOnLE92w_UXJaxGoDyvALWiOpW9VfdaE")
                    .SetDatabaseUrl("https://bagrot-e5280.firebaseio.com")
                    .SetStorageBucket("bagrot-e5280.appspot.com")
                    .Build();
                app = FirebaseApp.InitializeApp(Application.Context, options);
                firebaseAuthentication = FirebaseAuth.Instance;
            }
            else
            {
                firebaseAuthentication = FirebaseAuth.Instance;
            }
            return firebaseAuthentication;
        }

        public static void SaveUserId(string userId)
        {
            editor = preferences.Edit();
            editor.PutString("userId", userId);
            editor.Apply();
        }

        public static string GetUserId()
        {
            string userId = "";
            userId = preferences.GetString("userId", "");
            return userId;
        }
    }
}