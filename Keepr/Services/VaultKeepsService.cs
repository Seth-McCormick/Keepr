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
        private readonly KeepsService _ks;

        private readonly KeepsRepository _kr;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vs, KeepsService ks, KeepsRepository kr)
        {
            _repo = repo;
            _vs = vs;
            _ks = ks;
            _kr = kr;
        }

        private VaultKeep Get(int id)
        {
            return _repo.Get(id);
        }

        internal List<VaultKeepsViewModel> GetKeepsByVaultId(int vaultId, string userId)
        {
            Vault found = _vs.GetById(vaultId, userId);
            List<VaultKeepsViewModel> keeps = _repo.GetKeepsByVaultId(vaultId, userId);

            if (found.IsPrivate == true && found.CreatorId != userId)
            {
                throw new Exception("Vault is private");
            }

            return keeps;

        }

        internal VaultKeep Create(VaultKeep vaultKeepData, string userId)
        {
            Vault foundVault = _vs.GetById(vaultKeepData.VaultId, userId);
            Keep foundKeep = _kr.GetById(vaultKeepData.KeepId);
            _kr.IncreaseKept(foundKeep);
            if (foundVault.CreatorId != userId)
            {
                throw new Exception("Can't Add a keep to a vault");
            }

            // write a function to your repo layer that will edit the keep 
            // make sure to send in the entire keep object
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