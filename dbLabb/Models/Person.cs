namespace dbLabb.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Interest> Interests { get; set; } = new List<Interest>();
    }
}
