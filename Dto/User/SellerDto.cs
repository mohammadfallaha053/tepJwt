using JWT53.Dto.File;
using JWT53.Dto.Property;
using JWT53.Enum.Seller;

namespace JWT53.Dto.User;
public class SellerDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
  
    //public string Role { get; set; }

    public string ImageUrl { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public int CountOfProperties { get; set; }

    public bool EmailConfirmed { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public int AvailableForSaleCount { get; set; }
    public int SoldCount { get; set; }
    public int AvailableForRentCount { get; set; }
    public int RentedCount { get; set; }
    public ICollection<ResponsePropertyDto> Properties { get; set; }
}
