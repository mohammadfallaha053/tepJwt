﻿using JWT53.Enum;

namespace JWT53.Models;

public class Property
{
    public int Id { get; set; }
    public string Name_Ar { get; set; }
    public string Name_En { get; set; }
    public string Name_Ku { get; set; }
    public string Description_Ar { get; set; }
    public string Description_En { get; set; }
    public string Description_Ku { get; set; }
    public int NumberOfRooms { get; set; }
    public int NumberOfBathrooms { get; set; }
    public int Area { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountedPrice { get; set; }
    public bool IsActive { get; set; }
    public bool IsShowInMainPage { get; set; }
    public bool IsShowInAdPage { get; set; }
    public PropertyType PropertyType { get; set; } // تحديث إلى enum

    public Double Lat {  get; set; }
    
    public Double Long { get; set; }

    public string StreetName { get; set; }


    // Foreign keys
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public int CityId { get; set; }
    public City City { get; set; }


    public string UserId { get; set; }
    public ApplicationUser User { get; set; }


    // Navigation property for files
    public ICollection<MyFile> Files { get; set; }
}
