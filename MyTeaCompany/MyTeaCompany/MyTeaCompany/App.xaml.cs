using MyTeaCompany.Services;
using MyTeaCompany.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MyTeaCompany
{
	public partial class App : Application
	{
        public static StoreInfoManager StoreInfoManager { get; private set; }
		public App ()
		{
			InitializeComponent();
            StoreInfoManager = new StoreInfoManager(new DocumentDBService());
            MainPage = new NavigationPage(new StoreList());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
