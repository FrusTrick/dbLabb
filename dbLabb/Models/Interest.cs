namespace dbLabb.Models
{
    public class Interest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PersonId { get; set; }
        public ICollection<Link> Links { get; set; } = new List<Link>();
        public Person person { get; set; }

    }
}
