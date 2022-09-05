using App.Application.Entites;

namespace App.Microservices.Categories.Models.Entites
{
    public class Category : AuditableEntity
    {
        public Guid CategoryId { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public string Descrition { get; set; }
    }
}
