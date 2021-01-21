using Coins.Services;
using Coins.Services.CoinsService;
using Coins.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Coins
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<CoinsService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
