using System.ComponentModel.DataAnnotations.Schema;
using Project.Domain.Entities.Common;

namespace Project.Domain.Entities
{
    public class ComponentProp : BaseEntity
    {
        public int ComponentId { get; set; }

        public string Key { get; set; }   // مثل "content", "label", "url"
        public string? Value { get; set; }

        [ForeignKey(nameof(ComponentId))]
        public ComponentModel? Component { get; set; }
    }
}
