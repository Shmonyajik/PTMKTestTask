using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

namespace PTMKTestTask.Services
{
    
    public class DynamicTableCreationService: IDynamicTableCreationService
    {
        
        private readonly AppDbContext _context;
        public DynamicTableCreationService(AppDbContext context)
        {
            _context = context;
        }

        public void CreateDynamicTable()
        {
            //Console.WriteLine("Create!");
            try
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();

               


                Console.WriteLine($"Таблица  успешно создана!");
            }
            catch (Exception ex)//Microsoft.Data.SqlClient.SqlException: 
            {

                Console.WriteLine($"не удалось создать таблицу! {ex.Message}");
            }
                
            
        }

        public void CreateIndex()
        {
            try
            {
                _context.Database.ExecuteSqlRaw(
                "USE PTMKTestTaskDB; create index IX_Employee_FullName on Employees (FullName) include (Birthday, Sex)"
               );
                Console.WriteLine("Создан индекс IX_Employee_FullName");

            }
            catch (Exception ex )
            {

                Console.WriteLine($"не удалось создать индекс! {ex.Message}");
            }
            
                
        }
    }
}
