namespace TimeWellSpent.Models;

public class UserToActivity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ActivityId { get; set; }
    public int CategoryId { get; set; }
    public int MoodId { get; set; }
    public decimal HoursSpent { get; set; }
    public int WeekId { get; set; }
    public string? Description { get; set; }

}
