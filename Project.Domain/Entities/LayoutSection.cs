using System.ComponentModel.DataAnnotations.Schema;
using Project.Domain.Entities.Common;

namespace Project.Domain.Entities
{
    public class LayoutSection : BaseEntity
    {
        public int SectionId { get; set; }

        public string Variant { get; set; } // one-col, two-col, etc

        [ForeignKey(nameof(SectionId))]
        public SectionModel? Section { get; set; }

        public ICollection<LayoutSlot>? Slots { get; set; }
    }
}
