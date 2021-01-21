using Coins.Constants;
using Coins.Models;
using Coins.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Coins.Services.CoinsService
{
    public class CoinsService : ICoinsService
    {
        private readonly IGenericRepository _genericRepository;
        public IGenericRepository GenericRepository;
        public CoinsService()
        {
            GenericRepository = new GenericRepository();
        }

        public async Task<List<CoinsModel.Value>> GetAllCoinsAsync()
        {
            string uri = $"{ApiConstants.BaseApiUrl}" + ApiConstants.Moedas;
            var coins = await GenericRepository.GetAsync<CoinsModel>(uri);

            var listCoins = coins.value;

            return listCoins;
        }

    }
}
