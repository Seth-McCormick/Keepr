using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        private Vault ValidateOwnership(int id, string userId)
        {
            Vault original = Get(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            return original;
        }

        private Vault Get(int id)
        {
            Vault found = _repo.Get(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }

        internal Vault GetById(int id, string userId)
        {
            Vault vault = _repo.GetById(id);
            if (vault.IsPrivate == true)
            {
                throw new Exception("Vault is set to Private");
            }

            return vault;
        }

        internal Vault Create(Vault vaultData)
        {
            return _repo.Create(vaultData);
        }

        internal Vault Edit(Vault vaultData, string userId)
        {
            Vault original = ValidateOwnership(vaultData.Id, vaultData.CreatorId);
            if (vaultData.CreatorId != userId)
            {
                throw new Exception("You can't edit this!");
            }
            original.Name = vaultData.Name ?? original.Name;
            original.Description = vaultData.Description ?? original.Description;
            original.IsPrivate = vaultData.IsPrivate ?? original.IsPrivate;

            _repo.Edit(original);
            return original;

        }

        internal Vault GetVaultByAccount(string userId)
        {
            Vault vault = _repo.GetVaultByAccount(userId);
            if (vault.CreatorId != userId)
            {
                throw new Exception("Not Your Vault");
            }
            return vault;
        }

        internal void Delete(int id, string userId)
        {
            Vault original = GetById(id, userId);
            if (original.CreatorId != userId)
            {
                throw new Exception("Can't Delete");
            }
            _repo.Delete(id);
        }
    }
}