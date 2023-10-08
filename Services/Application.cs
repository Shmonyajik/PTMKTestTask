using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PTMKTestTask.Entities;
using PTMKTestTask.Repository;
using System.Diagnostics;



namespace PTMKTestTask.Services
{
    public class Application
    {
        private readonly IDynamicTableCreationService _dynamicTableCreationService;
        private readonly IBaseRepository<Employee> _employeeRepository;
        private readonly ILogger<Application> _logger;

        public Application(IDynamicTableCreationService dynamicTableCreationService, IBaseRepository<Employee> employeeRepository,ILogger<Application> logger)
        {
            _dynamicTableCreationService = dynamicTableCreationService;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        public void Run(string[] args)
        {

            if (args.Length > 0 && int.TryParse(args[0], out int flag))
            {
                try
                {
                    switch (flag)
                    {
                        case 1:
                            _dynamicTableCreationService.CreateDynamicTable();
                            break;
                        case 2:
                            Console.WriteLine(args[1]);
                            if (args.Length >= 4)
                            {
                                
                                try
                                {
                                    var emp = new Employee(args[1], args[2], args[3]);
                                    
                                    _employeeRepository.Create(emp);
                                    Console.WriteLine($"Сотрудник {emp.FullName} успешно добавлен!");
                                   
                                    
                                }
                                catch(ArgumentException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                               
                                
                            }
                            else
                            {
                                Console.WriteLine("Не указаны все необходимые аргументы");
                            }
                              
                            break;
                        case 3:
                            var uniqueEmployees = _employeeRepository.GetAll()
                                .GroupBy(x => new { x.FullName, x.Birthday })
                                .Select(group => group.OrderBy(x => x.Id).FirstOrDefault())
                                .ToList().OrderBy(x=>x.FullName);

                            if (uniqueEmployees.Any())
                            {
                                foreach (var emp in uniqueEmployees)
                                {
                                    Console.WriteLine($"Имя: {emp.FullName}," +
                                        $" Дата рождения: {emp.Birthday}," +
                                        $" Пол: {emp.Sex}," +
                                        $" Полных лет: {emp.GetFullYears(emp.Birthday)}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("В базе данных отсутствуют записи");
                            }
                            break;
                        case 4:
                            Random rnd = new Random();

                            int rows = 1000000;
                            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                            var randomEmployees = new Employee[rows+100];
                            int j = 0;
                            int nextletter = rows / alphabet.Length ;
                            for (int i = 0; i < rows; i++)
                            {
                                
                                randomEmployees[i] = new Employee
                                {
                                    FullName = $"{alphabet[j]}amiliya Name Otchestvo",
                                    Sex = i%2==0?"male":"female"
                                };
                                if (i == nextletter * (j + 1) && j+1!=alphabet.Length)
                                {
                                    j++;
                                }
                                
                            }
                            for (int i = rows; i < rows+100; i++)
                            {
                                randomEmployees[i] = new Employee("Familiya Name Otchestvo ", "2000-05-10", "male");
                            }
                            _employeeRepository.CreateMultiple(randomEmployees);
                            _dynamicTableCreationService.CreateIndex();
                            
                                break;
                        case 5:
                            var sw = new Stopwatch();

                            sw.Start();
                            var employees = _employeeRepository.GetAll().Where(e => EF.Functions.Like(e.FullName, "F%")).ToList();
                            sw.Stop();

                            if (employees.Count==0)
                                break;
                            foreach (var emp in employees)
                            {
                                Console.WriteLine($"Имя: {emp.FullName}," +
                                        $" Дата рождения: {emp.Birthday}," +
                                        $" Пол: {emp.Sex}," +
                                        $" Полных лет: {emp.GetFullYears(emp.Birthday)}");
                            }
                            Console.WriteLine($"Время выполнения: {sw.ElapsedMilliseconds} мс");
                            _logger.LogInformation($"{DateTime.Now} Время выполнения: {sw.ElapsedMilliseconds} мс");
                            break;
                        
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Runtime Error:{ex}");
                }
            }
            else
            {
                Console.WriteLine("Некорректный аргумент!");
            }
            
        }

       
    }
}
