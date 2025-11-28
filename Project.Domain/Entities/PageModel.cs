using System.ComponentModel.DataAnnotations.Schema;
using Project.Domain.Entities.Common;

namespace Project.Domain.Entities
{
    public class PageModel : BaseEntity
    {



        public string Name { get; set; }
        public int WebsitesId { get; set; }



        [ForeignKey(nameof(WebsitesId))]
        public Websites? websites { get; set; }


        public ICollection<SectionModel>? Sections { get; set; }
    }
}
