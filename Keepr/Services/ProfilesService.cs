using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _repo;

        public ProfilesService(ProfilesRepository repo)
        {
            _repo = repo;
        }

        internal Account GetProfile(string id)
        {
            Account found = _repo.GetProfile(id);
            if (found.Id == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }

        internal List<Keep> GetKeeps(string id)
        {
            List<Keep> found = _repo.GetKeeps(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }

        internal List<Vault> GetVaults(string id)
        {
            List<Vault> found = _repo.GetVaults(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }
    }
}