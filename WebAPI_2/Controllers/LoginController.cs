using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebAPI_2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public ActionResult<LoginResponse> Login(LoginRequest request)
        {
            if (request == null)
            {
                return new LoginResponse(false, null);
            }
            else
            {
                if (request.UserName == "admin" && request.PassWord == "12345678")
                {
                    var items = Process.GetProcesses().Select(p => new ProcessInfo(p.Id, p.ProcessName, p.WorkingSet)).ToArray();
                    return new LoginResponse(true, items);
                }
                else
                {
                    return new LoginResponse(false, null);
                }
            }
        }
    }


    public record LoginRequest(string UserName, string PassWord);

    public record ProcessInfo(long Id, string Name, long WorkingSet);
    public record LoginResponse(bool OK, ProcessInfo[]? ProcessInfos);
}
