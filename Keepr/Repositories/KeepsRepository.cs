using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Keep> GetAllKeeps()
        {
            string sql = @"
            SELECT 
            a.*,
            k.* 
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            ";
            return _db.Query<Account, Keep, Keep>(sql, (profile, keep) =>
            {
                keep.Creator = profile;
                return keep;
            }).ToList();
        }

        internal Keep GetById(int id)
        {
            string sql = @"
            SELECT
             a.*,
             k.* 
             FROM keeps k
             JOIN accounts a ON a.id = k.creatorId
              WHERE k.id = @id";
            return _db.Query<Account, Keep, Keep>(sql, (profile, keep) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }).FirstOrDefault();
        }

        internal void increaseViews(Keep found)
        {
            string sql = @"
            UPDATE keeps
            SET
            views = @Views+1
            WHERE id = @Id;
            ";
            _db.Execute(sql, found);
        }

        internal void IncreaseKept(Keep foundKeep)
        {
            string sql = @"
           UPDATE keeps
           SET 
           kept = @Kept+1
           WHERE id = @Id
           ";
            _db.Execute(sql, foundKeep);
        }

        internal Keep CreateKeep(Keep keepData)
        {
            string sql = @"
         INSERT INTO keeps
         (name, description, img, views, kept, creatorId)
         VALUES
         (@Name, @Description, @Img, @Views, @Kept, @CreatorId);
         SELECT LAST_INSERT_ID();
         ";

            int id = _db.ExecuteScalar<int>(sql, keepData);
            keepData.Id = id;
            keepData.CreatedAt = new DateTime();
            keepData.UpdatedAt = new DateTime();
            return keepData;
        }

        internal List<Keep> GetKeepByAccount(string userId)
        {
            string sql = "SELECT * FROM keeps WHERE keeps.creatorId = @userId";
            return _db.Query<Keep>(sql, new { userId }).ToList();
        }

        internal void Edit(Keep update)
        {
            string sql = @"
           UPDATE keeps
           SET 
                name = @Name,
                description = @Description,
                 img = @Img
           WHERE id = @Id;
           ";
            _db.Execute(sql, update);
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";

            _db.Execute(sql, new { id });
        }
    }
}