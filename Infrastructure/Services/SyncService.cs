using Application.IRepos;
using Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SyncService : ISyncService
    {
        private readonly IQBTokenRepo _iQBTokenRepo;
        public SyncService(IQBTokenRepo iQBTokenRepo)
        {
            _iQBTokenRepo = iQBTokenRepo;
        }
        public (bool qbConnection, bool sosConnection) IsBothTypesLoggedIn()
        {
            return _iQBTokenRepo.IsBothUsersLoggedIn();
        }
    }
}
