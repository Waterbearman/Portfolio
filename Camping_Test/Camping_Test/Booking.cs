using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camping_Test
{
    public class Booking
    {
        public string BookingNum { get; set; }
        public string Area { get; set; }
        public DateTime BookingDate { get; set; }
        public bool Checking { get; set; }
        
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
