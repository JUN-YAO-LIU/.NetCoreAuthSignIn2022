using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace OAuthSignIn.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignInGoogle(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action("Callback", controller: "Home", values: new { returnUrl });
            return new ChallengeResult(provider, new AuthenticationProperties { RedirectUri = redirectUrl ?? "/" });
        }

        public IActionResult Callback(string returnUrl = null, string remoteError = null)
        {
            var claims = HttpContext.User;
            // 略...後續流程可直接參考官方範例，或自訂
            return Ok();
        }
    }
}