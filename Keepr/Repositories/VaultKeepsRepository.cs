using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
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

        internal VaultKeep Get(int id)
        {
            string sql = "SELECT * FROM vaultKeeps WHERE id = @id";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }

        internal VaultKeep Create(VaultKeep vaultKeepData)
        {
            string sql = @"
           INSERT INTO vaultKeeps
           (vaultId, keepId, creatorId)
           VALUES
           (@VaultId, @KeepId, @CreatorId);
           SELECT LAST_INSERT_ID();
           ";
            vaultKeepData.Id = _db.ExecuteScalar<int>(sql, vaultKeepData);

            return vaultKeepData;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaultKeeps WHERE id = @Id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}