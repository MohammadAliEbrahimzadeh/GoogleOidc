using System.ComponentModel.DataAnnotations;

namespace GoogleOidcTest.Models;

public class BaseEntity
{
        public string? Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now; 
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;

}
