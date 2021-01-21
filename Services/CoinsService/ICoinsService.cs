using Coins.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Services.CoinsService
{
    public interface ICoinsService
    {
        Task<List<CoinsModel.Value>> GetAllCoinsAsync();
    }
}

