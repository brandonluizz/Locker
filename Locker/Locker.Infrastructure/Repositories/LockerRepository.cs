﻿using Locker.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel;
using System.Data.SqlClient;

namespace Locker.Infrastructure.Repositories
{
    public class LockerRepository : ILockerRepository
    {
        private readonly IDbSet<DomainModel.Locker> dbSet;

        public LockerRepository(IDbSet<DomainModel.Locker> dbSet)
        {
            this.dbSet = dbSet;
        }

        public void Add(DomainModel.Locker locker)
        {
            if (locker == null) { throw new ArgumentNullException(nameof(locker)); }

            this.dbSet.Add(locker);
        }

        public IList<DomainModel.Locker> GetAll()
        {
            return this.dbSet.ToList();
        }

        public IList<DomainModel.Locker> GetAllLockersByLockerBlockId(int lockerBlockId)
        {
            return this.dbSet.Where(l => l.LockerBlockId == lockerBlockId).ToList();
        }
    }
}