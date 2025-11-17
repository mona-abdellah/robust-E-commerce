using Robust.App.Contracts;
using Robust.Context;
using Robust.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.Infrastructure
{
    public class OrderItemRepo : GenericRepository<OrderItem, int>, IOrderItem
    {
        public OrderItemRepo(RobustContext _robustContext) : base(_robustContext)
        {
        }
    }
}
