using MyTeaCompany.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTeaCompany.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StoreList : ContentPage
	{
		public StoreList ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await App.StoreInfoManager.CreateDatabase(Constants.DatabaseName);
            await App.StoreInfoManager.CreateDocumentCollection(Constants.DatabaseName, Constants.CollectionName);
            var data = await App.StoreInfoManager.GetStoreInfoAsync();
            StoreInfoList.ItemsSource = data;
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StoreInfoDetails(true)
            {
                BindingContext = new StoreInfo
                {
                    Id = Guid.NewGuid().ToString()
                }
            });
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new StoreInfoDetails(false)
            {
                BindingContext = e.SelectedItem as StoreInfo
            });
        }

    }
}
