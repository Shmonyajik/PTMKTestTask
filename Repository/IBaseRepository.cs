using Microsoft.EntityFrameworkCore;
using PTMKTestTask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMKTestTask.Repository
{
    public interface IBaseRepository<T>
    {
        void Create(T model);
        void CreateMultiple(IEnumerable<T> model);

        IQueryable<T> GetAll();

        


    }
}
