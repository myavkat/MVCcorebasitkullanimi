using System.ComponentModel.DataAnnotations;

namespace Deneme1.Models
{
    public class Ogrenci
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
    }
}
