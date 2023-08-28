using ModuleTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTracker.EntityFramework.DTOs
{
    public static class SheetDtoExtensions
    {

        public static SheetDto ToDto(this Sheet sheet)
        {
            return new SheetDto()
            {
                Id = sheet.Id,
                ModuleId = sheet.ModuleId,
                SheetNumber = sheet.SheetNumber,
                Exercises = sheet.Exercises.Select(e => e.ToDto()).ToList(),
                PdfFilePath = sheet.PdfFilePath
            };
        }

        public static Sheet ToSheet(this SheetDto sheetDto)
        {
            return new Sheet(sheetDto.Id, sheetDto.ModuleId, sheetDto.SheetNumber, sheetDto.Exercises.Select(e => e.ToExercise()).OrderBy(e => e.Number).ToList(), sheetDto.PdfFilePath);
        }
    }
}
