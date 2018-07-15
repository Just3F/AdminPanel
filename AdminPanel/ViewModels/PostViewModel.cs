namespace AdminPanel.ViewModels
{
    public class PostViewModel
    {
        public long PKID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public string CategoryName { get; set; }
        public long? CategoryId { get; set; }
    }
}
