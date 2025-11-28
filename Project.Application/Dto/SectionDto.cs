namespace Project.Application.Dto
{
    public class SectionDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string? Width { get; set; }
        public string? Height { get; set; }
        public int PageId { get; set; }
    }
}
