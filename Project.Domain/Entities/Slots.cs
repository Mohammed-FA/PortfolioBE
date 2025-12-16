using System.ComponentModel.DataAnnotations.Schema;
using Project.Domain.Entities.Common;
using Project.Domain.Enum;

namespace Project.Domain.Entities
{
    public class Slots : SectionStyle
    {

        public int Id { get; set; }

        public int ColumnId { get; set; }
        public string? Label { get; set; }
        public string? Href { get; set; }
        public string? Content { get; set; }

        // Image
        public string? Url { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public ComponentType type { get; set; } = ComponentType.Text;

        public ICollection<ListItems>? Items { get; set; }
        [ForeignKey(nameof(ColumnId))]
        public Columns? columns { get; set; }
    }
    public class ListItems : BaseEntity
    {
        public string? label { get; set; }

        public int SlotsId { get; set; }

        [ForeignKey(nameof(SlotsId))]
        public Slots? Slots { get; set; }
    }
}
