using System.ComponentModel.DataAnnotations;

namespace ModuleTracker.EntityFramework.DTOs
{
    public class ModuleDto
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public IList<SheetDto> Sheets { get; set; } = new List<SheetDto>();


    }
}
