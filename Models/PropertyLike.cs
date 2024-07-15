namespace JWT53.Models
{
    public class PropertyLike
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
    }

}
