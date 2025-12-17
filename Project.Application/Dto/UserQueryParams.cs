using Project.Domain.Entities.Enm;

namespace Project.Application.Dto
{
    public class UserQueryParams
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10; // 5, 10, 25
        public string? Search { get; set; }
        public UserType? Type { get; set; }

        public int ShowBlock { get; set; } = 1;//1 show all 2 show blocked 3/ show un blocked 
    }
}
