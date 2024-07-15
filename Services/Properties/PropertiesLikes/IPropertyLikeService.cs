namespace JWT53.Services.Properties.PropertiesLikes;

public interface IPropertyLikeService
{
    Task<bool> TogglePropertyLikeAsync(string userId, Guid propertyId);

}
