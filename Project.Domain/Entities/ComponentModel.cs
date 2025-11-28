using System.ComponentModel.DataAnnotations.Schema;
using Project.Domain.Entities.Common;

namespace Project.Domain.Entities
{
    public class ComponentModel : BaseEntity
    {
        public int SlotId { get; set; }

        public string Type { get; set; } // text, image, button ...

        [ForeignKey(nameof(SlotId))]
        public LayoutSlot? Slot { get; set; }

        public ICollection<ComponentProp>? Props { get; set; }
    }
}
