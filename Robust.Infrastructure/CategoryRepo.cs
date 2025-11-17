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
    public class CategoryRepo : GenericRepository<Category, int>, ICategoryRepo
    {
        public CategoryRepo(RobustContext _robustContext) : base(_robustContext)
        {
        }
    }
}
