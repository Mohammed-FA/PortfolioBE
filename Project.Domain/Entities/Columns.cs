using System.ComponentModel.DataAnnotations.Schema;
using Project.Domain.Entities.Common;

namespace Project.Domain.Entities
{
    public class Columns : BaseEntity
    {

        public int SectionId { get; set; }


        public ICollection<Slots>? Slots { get; set; }

        [ForeignKey(nameof(SectionId))]
        public SectionModel? Section { get; set; }

    }
}
