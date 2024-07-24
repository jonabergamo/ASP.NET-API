using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SegundaAPI.Model
{
    [Table("employee")]
    public class Employee
    {
        [Key]
        [Column("id")]
        public string Id { get; private set; }
        
        [Required]
        [Column("name")]
        public string Name { get; private set; }
        
        [Column("age")]
        public int Age { get; private set; }
        
        [Column("photo")]
        public string? Photo { get; private set; }

        public Employee(string name, int age, string photo)
        {
            Id = Guid.NewGuid().ToString();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Age = age;
            Photo = photo;
        }
    }
}
