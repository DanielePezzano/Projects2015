﻿using System.Collections.Generic;
using SharedDto.Universe.Stars;

namespace SharedDto.Universe.Sector
{
    public class SectorGenerationDto
    {
        public SectorGenerationResult GenerationResult;
        public int StarsGenerated;
        public int MaxStarsAllowed;
        public int TotalPlanetsAdded;
        public int HabitablePlanetsAdded;
        public List<StarDto> GeneratedStarsList { get; set; }
    }
}