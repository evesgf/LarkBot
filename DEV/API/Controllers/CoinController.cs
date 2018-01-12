using Business.Coin;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [EnableCors("any")] //设置跨域处理的代理
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class CoinController : Controller
    {
        private readonly IhuobiService _IhuobiService;

        public CoinController(IhuobiService huobiService)
        {
            _IhuobiService = huobiService;
        }

        // GET api/values
        [HttpGet]
        public async Task<OTCPrice> OTCPrice()
        {
            var reModel=new OTCPrice();

            var huobiBuy1 = await _IhuobiService.LegalTenderBuy();
            var huobiSell1 = await _IhuobiService.LegalTenderSell();

            reModel.Access = true;
            reModel.Data = new[]{ string.Format("火币买一:{0}CNY,卖一:{1}CNY", huobiBuy1.Result, huobiSell1.Result) };

            return reModel;
        }
    }
}