using Common.DTOs;
using Common.DTOs.QB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IQBItemsService
    {
        public Task<Item> CreateItem(QBItemsRequestDTO request);
    }
}
