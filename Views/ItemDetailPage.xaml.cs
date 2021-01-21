using Coins.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Coins.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}