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

        internal Vault Get(int id)
        {
            string sql = "SELECT * FROM vaults WHERE id = @id";
            return _db.QueryFirstOrDefault<Vault>(sql, new { id });
        }

        internal Vault Create(Vault vaultData)
        {
            string sql = @"
           INSERT INTO vaults
           (name, description, isPrivate, creatorId)
           VALUES
           (@Name, @Description, @IsPrivate, @CreatorId);
           SELECT LAST_INSERT_ID();
           ";

            int id = _db.ExecuteScalar<int>(sql, vaultData);
            vaultData.Id = id;
            return vaultData;
        }

        internal void Edit(Vault update)
        {
            string sql = @"
          UPDATE vaults 
          SET
            name = @Name,
            description = @Description,
            isPrivate = @IsPrivate
            WHERE id = @Id;
          ";
            _db.Execute(sql, update);

        }

        internal Vault GetVaultByAccount(string userId)
        {
            string sql = @"
           SELECT
            a.*,
            v.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE v.creatorId = userId
           ";
            return _db.Query<Account, Vault, Vault>(sql, (profile, vault) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { userId }).FirstOrDefault();
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}