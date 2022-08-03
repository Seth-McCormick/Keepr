using System;
using System.Collections.Generic;
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
        internal List<VaultKeepsViewModel> GetKeepsByVaultId(int vaultId, string userId)
        {
            Vault found = GetById(vaultId, userId);
            if (found.IsPrivate == true && found.CreatorId != userId)
            {
                throw new Exception("Vault is private");
            }

            return _repo.GetKeepsByVaultId(vaultId, userId);


        }

        private Vault ValidateOwnership(int id, string userId)
        {
            Vault original = GetById(id, userId);
            if (original.CreatorId != userId)
            {
                throw new Exception("Forbidden");
            }
            return original;
        }

        // private Vault Get(int id)
        // {
        //     Vault found = _repo.Get(id);
        //     if (found == null)
        //     {
        //         throw new Exception("Invalid Id");
        //     }
        //     return found;
        // }

        internal Vault GetById(int id, string userId)
        {
            Vault vault = _repo.GetById(id);
            if (vault == null)
            {
                throw new Exception("Invalid Id");
            }
            if (vault.IsPrivate == true && vault.CreatorId != userId)
            {
                throw new Exception("Vault is private");
            }

            return vault;
        }

        internal Vault Create(Vault vaultData)
        {
            return _repo.Create(vaultData);
        }

        internal Vault Edit(Vault vaultData, string userId)
        {
            Vault original = ValidateOwnership(vaultData.Id, userId);
            if (original.CreatorId != userId)
            {
                throw new Exception("You can't edit this!");
            }
            original.Name = vaultData.Name ?? original.Name;
            original.Description = vaultData.Description ?? original.Description;
            original.Img = vaultData.Img ?? original.Img;
            original.IsPrivate = vaultData.IsPrivate ?? original.IsPrivate;

            _repo.Edit(original);
            return original;

        }

        internal List<Vault> GetVaultByAccount(string userId)
        {
            return _repo.GetVaultByAccount(userId);

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