using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        // private Keep ValidateOwnership(int id, string userId)
        // {
        //     Keep original = GetById(id);
        //     if (original.CreatorId != userId)
        //     {
        //         throw new Exception("Forbidden");
        //     }
        //     return original;
        // }

        internal List<Keep> Get()
        {
            return _repo.GetAllKeeps();
        }

        internal Keep GetById(int id)
        {
            Keep found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            _repo.increaseViews(found);
            // write a function in your repo layer that will edit and increase the views property
            // send in the entire keep (found)
            return found;
        }



        internal Keep Create(Keep keepData)
        {
            return _repo.CreateKeep(keepData);
        }

        internal Keep Edit(Keep keepData)
        {
            Keep original = GetById(keepData.Id);
            if (keepData.CreatorId != original.CreatorId)
            {
                throw new Exception("You can't edit this!");
            }
            original.Name = keepData.Name ?? original.Name;
            original.Description = keepData.Description ?? original.Description;
            original.Img = keepData.Img ?? original.Img;

            _repo.Edit(original);
            original.UpdatedAt = new DateTime();
            return original;
        }


        internal void Delete(int id, string userId)
        {
            Keep original = GetById(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("Not Your Keep to Delete");
            }

            _repo.Delete(id);
        }
    }
}