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
    public class OrderRepo : GenericRepository<Order, int>, IOrderRepo
    {
        public OrderRepo(RobustContext _robustContext) : base(_robustContext)
        {
        }
    }
}
