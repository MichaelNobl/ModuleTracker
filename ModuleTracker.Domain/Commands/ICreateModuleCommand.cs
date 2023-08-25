using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Domain.Queries
{
    public interface ICreateModuleCommand
    {
        Task Execute(Module module);
    }
}
