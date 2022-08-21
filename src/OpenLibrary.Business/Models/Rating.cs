namespace OpenLibrary.Business.Models
{
    public class Rating : Entity
    {
        public int TotalRating { get; set; }

        /* EF Relation */
        public Book Book { get; set; }
        public IEnumerable<UserRating> UserRatings { get; set; }
    }
}
