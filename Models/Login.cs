using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SpectrumSprint.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectrumSprint.Models
{
    public class Login
    {
        protected ProgressDialog progressDialog;
        protected AccountActivity accountA;
        protected Context instance;
        protected User user;
        protected string name, email, password;
        public Login(Context instance, AccountActivity accountA)
        {
            this.instance = instance;
            this.accountA = accountA;
        }
        public async Task<bool> SignIn()
        {
                if (accountA.email.Text != "")
                {
                    this.email = accountA.email.Text;
                    if (accountA.password.Text != "")
                    {
                        this.password = accountA.password.Text;
                        this.user = new User(email, password);
                        this.ShowProgressDialog("Logging in...");
                        dynamic state = await this.user.Login();
                        try
                        {
                            if ((bool)state != false)
                            {
                                this.progressDialog.Dismiss();
                                return true;
                            }
                        }
                        catch
                        {
                            Toast.MakeText(instance, state, ToastLength.Long);
                            this.progressDialog.Dismiss();
                            return false;
                        }
                    }
                    this.progressDialog.Dismiss();
                    Toast.MakeText(instance, "Please Enter Password", ToastLength.Long);
                    return false;
                }
                this.progressDialog.Dismiss();
                Toast.MakeText(instance, "Please Enter E-mail", ToastLength.Long);
                return false;
        }

        public async Task<bool> Register()
        {
            if (accountA.username.Text != "" || accountA.username.Text.Length > 1)
            {
                this.name = accountA.username.Text;
                if (accountA.email.Text != "")
                {
                    this.email = accountA.email.Text;
                    if (accountA.password.Text != "" || accountA.password.Text.Length > 7)
                    {
                        this.password = accountA.password.Text;
                        this.user = new User(name, email, password);
                        this.ShowProgressDialog("Registering...");
                        dynamic state = await this.user.Register();
                        try
                        {
                            if ((bool)state != false)
                            {
                                this.progressDialog.Dismiss();
                                await this.user.Login();
                                return true;
                            }
                        }
                        catch
                        {
                            Toast.MakeText(instance, state, ToastLength.Long);
                            this.progressDialog.Dismiss();
                            return false;
                        }
                    }
                    this.progressDialog.Dismiss();
                    Toast.MakeText(instance, "Password must contain atleast 8 characters", ToastLength.Long);
                    return false;
                }
                this.progressDialog.Dismiss();
                Toast.MakeText(instance, "Please Enter E-mail", ToastLength.Long);
                return false;
            }
            this.progressDialog.Dismiss();
            Toast.MakeText(instance, "Please a valid name", ToastLength.Long);
            return false;
        }

        void ShowProgressDialog(string status)
        {
            progressDialog = new ProgressDialog(instance);
            progressDialog.SetCancelable(false);
            progressDialog.SetMessage(status);
            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressDialog.Show();
        }
    }
}