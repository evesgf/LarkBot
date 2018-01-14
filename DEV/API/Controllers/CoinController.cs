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

            var huobiBuy_BTC = await _IhuobiService.LegalTender(1,1);
            var huobiSell_BTC = await _IhuobiService.LegalTender(1,0);
            var huobiBuy_USDT = await _IhuobiService.LegalTender(2, 1);
            var huobiSell_USDT = await _IhuobiService.LegalTender(2, 0);

            reModel.Access = true;
            reModel.Data = new[]{ string.Format("火币买一:{0}CNY/{2}USDT,卖一:{1}CNY/{3}USDT", huobiBuy_BTC.Result, huobiSell_BTC.Result, huobiBuy_USDT.Result, huobiSell_USDT.Result) };

            return reModel;
        }
    }
}