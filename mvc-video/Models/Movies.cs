namespace mvc_video.Models
{
    public class Movies : BaseEntity
    {
        public string MovieTitle { get; set; }
        public string MovieDescription { get; set; }
        public string Director { get; set; }
        public DateTime MovieDate {  get; set; }
    }
}
