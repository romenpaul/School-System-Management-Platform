using System.ComponentModel.DataAnnotations;

namespace SE_1st_projects.Models
{
    public class DepartmentModel
    {
        [Key]
        public int Id { get; set; }

        public string DepartmentName { get; set; }
    }
}