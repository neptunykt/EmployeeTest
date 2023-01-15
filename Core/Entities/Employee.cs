using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Core.Entities
{
    [Table("Employee")]
    public class Employee
    {
        public Guid Id { get; set; }
        [StringLength(6)]
        public string PayRollNumber { get; set; }
        [StringLength(255)]
        public string Surname { get; set; }
        [StringLength(255)]
        public string Forenames { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }
        
        [StringLength(40)]
        public string Telephone { get; set; }
        
        [StringLength(40)]
        public string Mobile { get; set; }
        
        [StringLength(255)]
        public string Address { get; set; }
        
        [StringLength(255)]
        public string Address2 { get; set; }
        
        [StringLength(40)]
        public string PostCode { get; set; }
        
        [StringLength(255)]
        public string EmailHome { get; set; }
       
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }
        
    }
}