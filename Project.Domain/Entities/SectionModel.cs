using System.ComponentModel.DataAnnotations.Schema;
using Project.Domain.Entities.Common;

namespace Project.Domain.Entities
{
    public class SectionModel : BaseEntity
    {
        public int PageId { get; set; }

        public string Type { get; set; }

        public string? Width { get; set; }
        public string? Height { get; set; }

        [ForeignKey(nameof(PageId))]
        public PageModel? Page { get; set; }

        // إذا كانت Layout سيتم تعبئتها
        public LayoutSection? Layout { get; set; }

    }
}
