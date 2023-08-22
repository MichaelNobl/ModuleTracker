using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.Domain.Models
{
    public class Exercise
    {
        public Exercise(Guid id, int number, bool isCompleted = false)
        {
            Id = id;
            Number = number;
            IsCompleted = isCompleted;
        }

        public Guid Id { get; }
               
        public int Number { get; }

        public bool IsCompleted { get; }


    }
}
