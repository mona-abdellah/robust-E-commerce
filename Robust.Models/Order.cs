using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.Models
{
    public class Order:BaseEntity<int>
    {
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public Status Status { get; set; }
        public ICollection<OrderItem> orderItems { get; set; }

    }
    public enum Status
    {
        Pending=0,
        Completed=1,
        Canceled=2
    }
}
