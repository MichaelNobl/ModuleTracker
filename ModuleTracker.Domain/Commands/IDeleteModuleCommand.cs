using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Domain.Commands
{
    public interface IDeleteModuleCommand
    {
        Task Execute(Guid id);
    }
}
