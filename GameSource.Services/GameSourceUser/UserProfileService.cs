﻿using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser
{
    public class UserProfileService : BaseService<UserProfile>, IUserProfileService
    {
        private IUserProfileRepository repo;
        private IUserRepository userRepo;

        public UserProfileService(IUserProfileRepository repo, IUserRepository userRepo)
        {
            this.repo = repo;
            this.userRepo = userRepo;
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return repo.GetAll();
        }

        public UserProfile GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public UserProfile GetByUserID(int id)
        {
            User user = userRepo.GetByID(id);
            return repo.GetByID(user.UserProfile.ID);
        }

        public void Insert(UserProfile userProfile)
        {
            repo.Insert(userProfile);
        }

        public void Update(UserProfile userProfile)
        {
            repo.Update(userProfile);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public async Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<UserProfile> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task<UserProfile> GetByUserIDAsync(int id)
        {
            User user = await userRepo.GetByIDAsync(id);
            return await repo.GetByIDAsync(user.UserProfile.ID);
        }

        public async Task InsertAsync(UserProfile userProfile)
        {
            await repo.InsertAsync(userProfile);
        }

        public async Task UpdateAsync(UserProfile userProfile)
        {
            await repo.UpdateAsync(userProfile);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
