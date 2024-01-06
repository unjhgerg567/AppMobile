using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class Tour
    {
        public int TourId { get; set; }

        public string TourName { get; set; }

        public string Destination { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string HotelName { get; set; }
    }
}
