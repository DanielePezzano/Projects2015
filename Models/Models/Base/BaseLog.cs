﻿using Models.Base.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Base
{
    public class BaseLogEntity : BaseEntity,IBaseLog
    {
        public string MethodCaller { get; set; }
        [Display(Name="Description", ResourceType=typeof(Resources))]
        public string Description { get; set; }
        public string ParametersValue { get; set; }       
    }
}