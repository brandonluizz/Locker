﻿using Locker.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Infrastructure.Repositories.Interface
{
    public interface ISectorRepository
    {
        void Add(Sector sector);

        IEnumerable<Sector> GetAll(int traderId);
    }
}
