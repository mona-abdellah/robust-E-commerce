using Robust.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.App.Contracts
{
    public interface IOrderRepo:IGenericRepo<Order,int>
    {
    }
}
