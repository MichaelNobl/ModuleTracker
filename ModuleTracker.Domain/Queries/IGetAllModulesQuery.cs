using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Domain.Queries
{
    public interface IGetAllModulesQuery
    {
        Task<IEnumerable<Module>> Execute();

    }
}
