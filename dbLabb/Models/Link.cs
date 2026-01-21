namespace dbLabb.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int IntrestId { get; set; }
        public Interest Interest { get; set; }  
    }
}
