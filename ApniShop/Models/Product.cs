using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace ApniShop.Models
{
    public class Product
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Availability { get; set; }
        public int Demand { get; set; }
        public int Rating { get; set; }
        public int Rate { get; internal set; }

        public static explicit operator JProperty(Product v)
        {
            throw new NotImplementedException();
        }
    }
}