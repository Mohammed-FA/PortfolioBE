using System.ComponentModel.DataAnnotations.Schema;
using Project.Domain.Entities.Common;

namespace Project.Domain.Entities
{
    public class Websites : BaseEntity
    {

        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserModel? UserModel { get; set; }

        public ICollection<Page>? Pages { get; set; }
    }
}
