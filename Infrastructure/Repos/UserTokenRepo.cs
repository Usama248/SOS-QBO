﻿using Application.IRepos;
using Common.DTOs;
using Common.DTOs.Shared;
using Data.DBContext;
using Data.Models;

namespace Infrastructure.Repos
{
    public class UserTokenRepo : IUserTokenRepo
    {
        AppDBContext _appDbContext;
        public UserTokenRepo(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public ResponseDTO<AddTokenDTO> AddToken(AddTokenDTO addTokenDTO)
        {
            AuthToken authToken = new AuthToken()
            {
                AccountId = addTokenDTO.AccountId,
                AccessToken = addTokenDTO.AccessToken,
                RefreshToken = addTokenDTO.RefreshToken,
                ExpiresIn = addTokenDTO.AccessTokenExpireIn,
                Type = addTokenDTO.Type,
                IsActive = true,
                CreatedDate = DateTime.Now,
                CreatedDateUTC = DateTime.UtcNow,
            };

            var previousActiveRecords = _appDbContext.AuthTokens.Where(x => x.IsActive == true).ToList();
            if(previousActiveRecords.Count == 0)
            {
                _appDbContext.AuthTokens.Add(authToken);
            } else
            {
                previousActiveRecords.ForEach(x => x.IsActive = false);
                _appDbContext.AuthTokens.Add(authToken);
            }
            _appDbContext.SaveChanges();
            return new ResponseDTO<AddTokenDTO> { Data = addTokenDTO, Message = "Added Successfully", Status = 200 };
        }

        public TokenDTO GetLatestToken()
        {
            var latestToken = _appDbContext.AuthTokens.Where(x => x.IsActive == true).FirstOrDefault();
            return new TokenDTO
            {
                access_token = latestToken.AccessToken,
                refresh_token = latestToken.RefreshToken,
                token_type = latestToken.Type,
                expires_in = latestToken.ExpiresIn,
            };
        }
    }
}