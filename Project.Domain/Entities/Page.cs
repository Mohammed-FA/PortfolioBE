using System.ComponentModel.DataAnnotations.Schema;
using Project.Domain.Entities.Common;

namespace Project.Domain.Entities
{
    public class Page : BaseEntity
    {

        public int WebsitesId { get; set; }



        [ForeignKey(nameof(WebsitesId))]
        public Websites? websites { get; set; }


        public ICollection<Sections>? Sections { get; set; }
    }
}
