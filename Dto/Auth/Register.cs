using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JWT53.Dto.Auth
{
    public class Register
    {

        public string FullName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        public string Password { get; set; } = string.Empty;

        public string Role {  get; set; } = string.Empty;

        public string PhoneNumber {  get; set; } = string.Empty;



    }
}
