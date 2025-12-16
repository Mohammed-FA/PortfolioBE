namespace Project.Application.Dto
{
    public class DashboardStatsDto
    {
        public int TotalUsers { get; set; }
        public int TotalWebsites { get; set; }
        public int ActiveUsers { get; set; }
        public List<UserDto> RecentActivity { get; set; } = new();
    }
}
