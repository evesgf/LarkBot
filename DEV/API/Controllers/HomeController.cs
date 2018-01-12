using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("any")] //设置跨域处理的代理
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
    }
}