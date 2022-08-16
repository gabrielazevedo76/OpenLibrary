namespace OpenLibrary.Business.Models
{
    public class UserRating : Entity
    {
        public Guid RatingId { get; set; }
        public Guid UserId { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public DateTime Created { get; set; }

        /* EF Relation */
        public Rating Rating { get; set; }
    }
}
