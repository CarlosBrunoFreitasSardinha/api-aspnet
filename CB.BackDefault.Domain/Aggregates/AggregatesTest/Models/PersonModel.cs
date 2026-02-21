using System.ComponentModel.DataAnnotations.Schema;

namespace CB.BackDefault.Domain.Aggregates.AggregatesTest.Models
{
    [Table("Person")]
    public class PersonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
