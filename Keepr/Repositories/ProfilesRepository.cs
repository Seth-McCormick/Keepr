using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class ProfilesRepository
    {
        private readonly IDbConnection _db;

        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Profile GetProfile(string id)
        {
            string sql = "SELECT * FROM accounts WHERE id = @id";
            return _db.QueryFirstOrDefault<Profile>(sql, new { id });
        }
        // internal Profile GetProfile(string id)
        // {
        //     string sql = @"
        //     SELECT 
        //     a.*,
        //     p.* 
        //     FROM profiles p
        //     JOIN accounts a ON a.id = p.id
        //     WHERE p.id = @id";
        //     return _db.Query<Account, Profile, Profile>(sql, (profile, p) =>
        //     {
        //         p.Id = profile.Id;
        //         return p;
        //     }, new { id }).FirstOrDefault();
        // }

        internal List<Keep> GetKeeps(string id)
        {
            string sql = @"
            SELECT 
            a.*,
            k.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.creatorId = @id";
            return _db.Query<Account, Keep, Keep>(sql, (profile, keep) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }).ToList();
        }

        internal List<Vault> GetVaults(string id)
        {
            string sql = @"
            SELECT
             a.*,
             v.*
             FROM vaults v
             JOIN accounts a ON a.id = v.creatorId 
             WHERE v.creatorId = @id";
            return _db.Query<Account, Vault, Vault>(sql, (profile, vault) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }).ToList();
        }
    }
}