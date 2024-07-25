﻿using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data.RepositoryInterfaces
{
    public interface IWorkContentRepository : IRepository<WorkContent, long?>
    {
        IEnumerable<WorkContent> ReadAllWorkContentByBiddingPackage(long? Id);
    }
}
