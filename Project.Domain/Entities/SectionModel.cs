using Project.Domain.Entities.Common;
using Project.Domain.Enum;

namespace Project.Domain.Entities
{
    public class SectionModel : SectionStyle
    {

        public int Id { get; set; }
        public int PageId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public LayoutVariant Type { get; set; }

        public ICollection<Columns>? Columns { get; set; }


    }
}
