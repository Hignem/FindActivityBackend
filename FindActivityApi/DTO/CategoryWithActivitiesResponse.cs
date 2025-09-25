namespace FindActivityApi.DTO
{
    public class CategoryWithActivitiesResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<ActivityResponse> Activities { get; set; }

        public bool HaveFavActivity { get; set; }
    }
}
