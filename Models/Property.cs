namespace JWT53.Models;

public class Property
{
    public  int Id { get; set; }

    public string Name { get; set; }
   
    public decimal Price { get; set; }

    public string Description { get; set; }

    public string UserId { get; set; }

    public ApplicationUser User { get; set; }
}
