using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.DTO.OrderItem
{
    public class GetOrderItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double PriceAtOrder { get; set; }
    }
}
