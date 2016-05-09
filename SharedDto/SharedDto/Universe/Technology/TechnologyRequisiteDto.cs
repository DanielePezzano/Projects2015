using SharedDto.BaseClasses;
using SharedDto.Interfaces;

namespace SharedDto.Universe.Technology
{
    public class TechnologyRequisiteDto : BaseDto, IDto
    {
        public string RequisiteType { get; set; }
    }
}