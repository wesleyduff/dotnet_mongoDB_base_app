﻿using Domain;
using System.Collections.Generic;

namespace BikeStoreApi.Models
{
    public class DistributorModels
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Brand> Brands { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public List<Offers> Offers { get; set; }
        public List<ReceiptType> ReceiptTpesOffered { get; set; }
        public List<BikeModels> Inventory { get; set; }
        
    }
}