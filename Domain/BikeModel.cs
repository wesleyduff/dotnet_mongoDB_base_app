using System;
using System.Collections.Generic;

namespace Domain
{
    public class BikeModel
    {
        public string Year { get; set; }
        public string Name { get; set; }
        public  List<Feature> Features { get; set; }
        public string Description { get; set; }
    }
}
