﻿using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OwnerRepositoryAsync :RepositoryBase<Owner>, IOwnerRepositoryAsync
    {
        public OwnerRepositoryAsync(RepositoryContext repositoryContext)
       : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await FindAll()
               .OrderBy(ow => ow.Name)
               .ToListAsync();
        }
        public async Task<Owner> GetOwnerByIdAsync(Guid ownerId)
        {
            return await FindByCondition(owner => owner.Id.Equals(ownerId))
                .FirstOrDefaultAsync();
        }
        public async Task<Owner> GetOwnerWithDetailsAsync(Guid ownerId)
        {
            return await FindByCondition(owner => owner.Id.Equals(ownerId))
                .Include(ac => ac.Accounts)
                .FirstOrDefaultAsync();
        }
        public void CreateOwner(Owner owner)
        {
            Create(owner);
        }
        public void UpdateOwner(Owner owner)
        {
            Update(owner);
        }

        public void DeleteOwner(Owner owner)
        {
            Delete(owner);
        }

        public async Task EventOccured(OwnerDto owner, string evt)
        { 
            var ownerForModification = await GetOwnerByIdAsync(owner.Id);
            if (ownerForModification != null)
            { 
                ownerForModification.Name  +=  $" evt: {evt}";
                RepositoryContext.ChangeTracker.Clear();
                UpdateOwner(ownerForModification);
                RepositoryContext.SaveChanges();
            }
            await Task.CompletedTask;
        }
    }
}
