using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault GetById(int id)
        {
            string sql = @"
           SELECT
           a.*,
           v.*
           FROM vaults v
           JOIN accounts a ON a.id = v.creatorId
           WHERE v.id = @id
           ";
            return _db.Query<Account, Vault, Vault>(sql, (profile, vault) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }).FirstOrDefault();
        }

        // internal Vault Get(int id)
        // {
        //     string sql = @"
        //     SELECT 
        //     a.*,
        //     v.*
        //     FROM vaults v
        //     JOIN accounts a ON a.id = v.creatorId
        //     WHERE v.id = @id";
        //     return _db.Query<Account, Vault, Vault>(sql, (profile, vault) =>
        //    {
        //        vault.Creator = profile;
        //        return vault;
        //    }, new { id }).FirstOrDefault();
        // }

        internal Vault Create(Vault vaultData)
        {
            string sql = @"
           INSERT INTO vaults
           (name, description, img, isPrivate, creatorId)
           VALUES
           (@Name, @Description, @Img,  @IsPrivate, @CreatorId);
           SELECT LAST_INSERT_ID();
           ";

            int id = _db.ExecuteScalar<int>(sql, vaultData);
            vaultData.Id = id;
            return vaultData;
        }

        internal void Edit(Vault original)
        {
            string sql = @"
          UPDATE vaults 
          SET
            name = @Name,
            description = @Description,
            img = @Img,
            isPrivate = @IsPrivate
          ";
            _db.Execute(sql, original);

        }
        internal List<VaultKeepsViewModel> GetKeepsByVaultId(int id, string userId)
        {
            string sql = @"
            SELECT
            a.*,
            k.*,
            vk.id AS VaultKeepId
             FROM vaultKeeps vk
             JOIN accounts a ON a.id = vk.creatorId
             JOIN keeps k ON k.id = vk.keepId
             WHERE vk.vaultId = @id";

            return _db.Query<Account, VaultKeepsViewModel, VaultKeepsViewModel>(sql, (profile, vaultKeep) =>
            {
                vaultKeep.Creator = profile;
                return vaultKeep;

            }, new { id }).ToList();
        }

        internal List<Vault> GetVaultByAccount(string userId)
        {
            string sql = "SELECT * FROM vaults WHERE vaults.creatorId = @userId";
            return _db.Query<Vault>(sql, new { userId }).ToList();
        }
        // internal Vault GetVaultByAccount(string userId)
        // {
        //     string sql = @"
        //    SELECT
        //     a.*,
        //     v.*
        //     FROM vaults v
        //     JOIN accounts a ON a.id = v.creatorId
        //     WHERE v.creatorId = userId
        //    ";
        //     return _db.Query<Account, Vault, Vault>(sql, (profile, vault) =>
        //     {
        //         vault.Creator = profile;
        //         return vault;
        //     }, new { userId }).FirstOrDefault();
        // }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}