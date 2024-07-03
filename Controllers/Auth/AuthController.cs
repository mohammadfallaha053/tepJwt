//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using JWT53.Dto.Auth;
//using JWT53.Services.Auth;

//namespace JWT53.Controllers.Auth;

//[Route("api/auth")]
//[ApiController]
//public class AuthController : ControllerBase
//{
//    private readonly IAuthService _authService;

//    public AuthController(IAuthService authService)
//    {
//        _authService = authService;
//    }

//    [HttpPost("/register")]
//    public async Task<IActionResult> RegisterAsync([FromBody] Register model)
//    {
//        if (!ModelState.IsValid)
//            return BadRequest(ModelState);

//        var result = await _authService.RegisterAsync(model);

//        if (!result.IsAuthenticated)
//            return BadRequest(result.Message);

//        return Ok(new
//        {
//            token = result.Token,
//            expiersOn = result.ExpiresOn,
//            userName = result.Username,
//            email = result.Email,
//            roles = result.Roles,

//        });
//    }

//    [HttpPost("/login")]
//    public async Task<IActionResult> LoginAsync([FromBody] Login model)
//    {
//        if (!ModelState.IsValid)
//            return BadRequest(ModelState);

//        var result = await _authService.GetTokenAsync(model);

//        if (!result.IsAuthenticated)
//            return BadRequest(result.Message);

//        return Ok(result);
//    }

//}