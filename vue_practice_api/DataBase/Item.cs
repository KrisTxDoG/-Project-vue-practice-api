using System.ComponentModel.DataAnnotations;

namespace vue_practice_api.DataBase
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; } // 使用 Guid 作為主鍵

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}


