using System.ComponentModel.DataAnnotations.Schema;
using Project.Domain.Entities.Common;

namespace Project.Domain.Entities
{
    public class Sections : BaseEntity
    {
        public int Pageid { get; set; }

        public string? Width { get; set; }
        public string? Hieght { get; set; }
        public string? Type { get; set; }

        [ForeignKey(nameof(Pageid))]
        public Page? Page { get; set; }

    }
}
