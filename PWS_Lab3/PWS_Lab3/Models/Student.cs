using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PWS_Lab3.Models
{
    public class Student
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(256)]
        [Required(ErrorMessage = "Name is a required value.")]
        public string Name { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Phone is a required value.")]
        public string Phone { get; set; }
    }
}