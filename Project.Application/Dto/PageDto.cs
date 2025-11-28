namespace Project.Application.Dto
{
    public class PageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WebsiteId { get; set; }
    }

    public class CreatePageDto
    {
        public string Name { get; set; }
        public long WebsiteId { get; set; }
    }

    public class UpdatePageDto
    {
        public string Name { get; set; }
    }
}
