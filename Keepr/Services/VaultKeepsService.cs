using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vs;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vs)
        {
            _repo = repo;
            _vs = vs;
        }

        private VaultKeep Get(int id)
        {
            return _repo.Get(id);
        }

        internal List<VaultKeepsViewModel> GetKeepsByVaultId(int vaultId, string userId)
        {
            Vault found = _vs.GetById(vaultId, userId);
            if (found.IsPrivate == true)
            {
                throw new Exception("Vault is private");
            }

            List<VaultKeepsViewModel> keeps = _repo.GetKeepsByVaultId(vaultId, userId);

            return keeps;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData)
        {
            // VaultKeep found = _repo.Get(vaultKeepData.Id);
            // if (found.CreatorId == null)
            // {
            //     throw new Exception("Not your vault to add keeps too");
            // }
            return _repo.Create(vaultKeepData);
        }

        internal void Delete(int id, string userId)
        {
            VaultKeep found = _repo.Get(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            if (found.CreatorId != userId)
            {
                throw new Exception("Invalid Access");
            }
            _repo.Delete(id);
        }
    }
}