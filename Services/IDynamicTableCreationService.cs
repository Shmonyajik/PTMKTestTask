using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMKTestTask.Services
{
    public interface IDynamicTableCreationService
    {
        
        public void CreateDynamicTable();

        public void CreateIndex();
    }
}
