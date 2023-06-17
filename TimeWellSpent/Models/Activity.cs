namespace TimeWellSpent.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Mood? Mood { get; set; }
        public Category? Category { get; set; }
        public Week? Week { get; set; }
        public User? User { get; set; }
    }
}
