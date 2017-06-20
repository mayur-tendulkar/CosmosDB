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
	public partial class StoreInfoDetails : ContentPage
	{
        bool isNewItem;

        public StoreInfoDetails (bool isNew)
		{
			InitializeComponent ();
            isNewItem = isNew;
        }


        async void OnSaveActivated(object sender, EventArgs e)
        {
            var storeInfo = (StoreInfo)BindingContext;
            await App.StoreInfoManager.SaveStoreInfoAsync(storeInfo, isNewItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteActivated(object sender, EventArgs e)
        {
            var storeInfo = (StoreInfo)BindingContext;
            await App.StoreInfoManager.DeleteStoreInfoAsync(storeInfo);
            await Navigation.PopAsync();
        }

        async void OnCancelActivated(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}
