using Project.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities
{
    public class LayoutSlot : BaseEntity
    {
        public int LayoutSectionId { get; set; }

        public string SlotName { get; set; }  // مثال: "left", "right", "col1"

        [ForeignKey(nameof(LayoutSectionId))]
        public LayoutSection? LayoutSection { get; set; }

        public ICollection<ComponentModel>? Components { get; set; }
    }
}
