using Coins.Models;
using Coins.Services.CoinsService;
using Coins.Sqlite;
using Coins.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Coins.ViewModels
{
    public class CoinsViewModel:BaseViewModel
    {
        #region Sqlite
        private readonly LocalDataService _local = LocalDataService.Current;
        
        #endregion

        #region Atributtes
        public INavigation _navigationService;
        public ICoinsService _iCoinsService;
        public Command LoadItemsCommand { get; }
        //
        ObservableCollection<CoinsModel.Value> _coins;
        public ObservableCollection<CoinsModel.Value> Coins
        {
            get { return _coins; }
            set { _coins = value; OnPropertyChanged(); }
        }
        ObservableCollection<CoinSqliteModel> _selectedItems;
        public ObservableCollection<CoinSqliteModel> SelectedItems
        {
            get { return _selectedItems; }
            set { _selectedItems = value; OnPropertyChanged(); }
        }

        #endregion

        #region Properties   
      
        #endregion

        #region commands
        CoinsModel.Value selectedCoins;
        public CoinsModel.Value SelectedCoin
        {
            get { return selectedCoins; }
            set
            {
                if (SelectedCoin != value)
                {
                    selectedCoins = value;
                    OnPropertyChanged();                   
                }

                var objSelected = new CoinSqliteModel
                {
                    simbolo = selectedCoins.simbolo,
                    nomeFormatado = selectedCoins.nomeFormatado,
                    tipoMoeda = selectedCoins.tipoMoeda
                };

                 _local.InsertInList(objSelected);//Insert
                  RefreshSelected();
            }
        }

        public ICommand DeleteAllCoinsSelected
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        _local.Delete<CoinSqliteModel>();
                        RefreshSelected();
                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
        }


        CoinSqliteModel _deleteCoinCommand;
        public CoinSqliteModel DeleteCoinCommand
        {
            get
            {
                return _deleteCoinCommand;
            }
            set
            {
                if (_deleteCoinCommand != value)
                {
                    _deleteCoinCommand = value;
                }

                var obj = new CoinSqliteModel()
                {
                    ID = _deleteCoinCommand.ID,
                    nomeFormatado = _deleteCoinCommand.nomeFormatado,
                    simbolo = _deleteCoinCommand.simbolo,
                    tipoMoeda = _deleteCoinCommand.tipoMoeda
                };

                _local.Delete(obj);
                RefreshSelected();
            }

            


        }
        #endregion

        public CoinsViewModel()
        {
            Title = "Coins";
            Coins = new  ObservableCollection<CoinsModel.Value>();
            SelectedItems = new ObservableCollection<CoinSqliteModel>();
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            
                try
                {

                    var result = await CoinsService.GetAllCoinsAsync();

                    foreach (var x in result)
                    {
                        Coins.Add(new CoinsModel.Value
                        {
                            nomeFormatado = x.nomeFormatado,
                            tipoMoeda = x.tipoMoeda,
                            simbolo = x.simbolo
                        });
                    }
                    LoadSelectedCoins();                                          

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    IsBusy = false;
                }

        }

        private async Task LoadSelectedCoins()
        {
            var cartItems = _local.Query<CoinSqliteModel>().ToList();//Read

            if (cartItems != null && cartItems.Count >= 1)
            {
                foreach (var x in cartItems)
                {
                    SelectedItems.Add(new CoinSqliteModel
                    {
                        nomeFormatado = x.nomeFormatado,
                        tipoMoeda = x.tipoMoeda,
                        simbolo = x.simbolo
                    });

                }
            }
        }

        private async Task RefreshSelected()
        {
            SelectedItems.Clear();
            var cartItems = _local.Query<CoinSqliteModel>().ToList();

            if (cartItems != null && cartItems.Count >= 1)
            {
                foreach (var x in cartItems)
                {
                    SelectedItems.Add(new CoinSqliteModel
                    {
                        nomeFormatado = x.nomeFormatado,
                        tipoMoeda = x.tipoMoeda,
                        simbolo = x.simbolo
                    });

                }
            }
        }
    }    
}
