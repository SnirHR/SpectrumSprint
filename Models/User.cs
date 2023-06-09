﻿using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.Graphics;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using Java.Util;
using SpectrumSprint.Constants;
using SpectrumSprint.Handlers;
using System;
using System.Threading.Tasks;

namespace SpectrumSprint.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        FirebaseAuth firebaseAuthentication;
        FirebaseFirestore database;


        public User()
        {
            this.firebaseAuthentication = ConnectionHandler.GetFirebaseAuthentication();
            this.database = ConnectionHandler.GetFirestore();
        }
        public User(string email, string password)
        {
            this.Email = email;
            this.Password = password;
            this.firebaseAuthentication = ConnectionHandler.GetFirebaseAuthentication();
            this.database = ConnectionHandler.GetFirestore();
        }
        public User(string name,string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.firebaseAuthentication = ConnectionHandler.GetFirebaseAuthentication();
            this.database = ConnectionHandler.GetFirestore();
        }

        public async Task<bool> Login()
        {
            try
            {
                await this.firebaseAuthentication.SignInWithEmailAndPassword(this.Email, this.Password);
                var editor = Application.Context.GetSharedPreferences(PathConstants.CURRENT_USER_FILE, FileCreationMode.Private).Edit();
                this.Name = await Networker.GetName(this.Email);
                editor.PutString("Name", this.Name);
                editor.PutString("Email", this.Email);
                editor.PutString("Password", this.Password);
                editor.Apply();
            }
            catch 
            {
                return false;
            }
            return true;
        }
        public bool Logout()
        {
            try
            {
                var editor = Application.Context.GetSharedPreferences(PathConstants.CURRENT_USER_FILE, FileCreationMode.Private).Edit();
                editor.PutString("Email", "");
                editor.PutString("Name", "");
                editor.PutString("Password", "");
                editor.Apply();
                firebaseAuthentication.SignOut();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public async Task<dynamic> Register()
        {
            try
            {
                await firebaseAuthentication.CreateUserWithEmailAndPassword(this.Email, this.Password);

            }
            catch (Exception e)
            {
                return e.Message;
            }
            try
            {
                HashMap userMap = new HashMap();
                userMap.Put("Name", this.Name);
                userMap.Put("Email", this.Email);
                DocumentReference userReference = this.database.Collection(PathConstants.USER_COLLECTION).Document(this.firebaseAuthentication.CurrentUser.Uid);
                await userReference.Set(userMap);
            }
            catch (Exception e)
            {

                return e.Message;
            }
            return true;
        }


    }
}