﻿using System;

namespace BandCentral.Models.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class DisplayAttribute : System.Attribute
    {
        public string Name { get; private set; }

        public DisplayAttribute(string name)
        {
            this.Name = name;
        }
    }
}
