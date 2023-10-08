using System.ComponentModel.DataAnnotations;
using System.Globalization;

using System.Text.RegularExpressions;



namespace PTMKTestTask.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^(?:[A-Z][a-z'-]+(?:\s|$)){3,}$", ErrorMessage = "Неверный формат ФИО")]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        [MaxLength(10)]
        public string Sex { get; set; }

        public Employee(string fullName, string birthday, string sex)
        {
            
            string pattern = @"^(?:[A-Z][a-z'-]+(?:\s|$)){3,}$";
            string dateFormat = "yyyy-MM-dd";

            if (!Regex.IsMatch(fullName, pattern))
            {
                
                throw new ArgumentException("Некорректный формат ФИО");
            }
            if (!DateTime.TryParseExact(birthday, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                
                throw new ArgumentException("Некорректный формат даты");
            }
            var fullYears = GetFullYears(result);
            Console.WriteLine($"fullYears: {fullYears}");
            if (fullYears < 0|| fullYears>100)
            {
                throw new ArgumentException("Укажите корректную дату");
            }
            if (sex.ToLower() != "male" && sex.ToLower() != "female")
            {
               
                throw new ArgumentException("Некорректный формат пола (male или female)");
            }
            
            FullName = fullName;
            this.Birthday = result;
            Sex = sex.ToLower();
        }
        public Employee() { }

        public int GetFullYears(DateTime birthday)
        {

            int years = DateTime.Now.Year - birthday.Year;

            
            if (DateTime.Now.Month < birthday.Month || (DateTime.Now.Month == birthday.Month && DateTime.Now.Day < birthday.Day))
            {
                years--;
            }
            return years;
        }

        //[NotMapped] 
        //public string LastName
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(FullName))
        //        {
        //            var parts = FullName.Split(' ');
        //            if (parts.Length >= 1)
        //            {
        //                return parts[parts.Length - 1];
        //            }
        //        }
        //        return string.Empty;
        //    }
        //    private set { }
        //}


    }
    
}
