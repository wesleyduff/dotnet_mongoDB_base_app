using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeStoreApi.Models
{
    public class BrandModels
    {
        public string Name { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
    }
}