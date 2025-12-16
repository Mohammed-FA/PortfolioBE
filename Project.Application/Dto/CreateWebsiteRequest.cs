using Project.Domain.Entities;

namespace Project.Application.Dto
{
    public class CreateWebsiteRequest
    {

        public string Name { get; set; } = null!;
        public List<PageModel> Pages { get; set; }
    }
}
