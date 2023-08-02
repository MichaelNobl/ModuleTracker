using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.EntityFramework.DTOs
{
    public class ModuleDto
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<SheetDto> Sheets { get; set; }
    }
}
