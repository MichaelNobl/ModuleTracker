using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.EntityFramework.DTOs
{
    public class SheetDto
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ModuleId { get; set; }

        public int SheetNumber { get; set; }

        public int NumOfExercises { get; set; }
    }
}
