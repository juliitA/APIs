using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Dolar.Resource;
using Dolar.Model;


namespace Dolar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        [HttpGet(Name = "GetQuote")]
        public string GetQuote()
        {
            DolarAPI api = new DolarAPI();
            return api.GetQuote().Result;
        }

        [HttpPost(Name = "GetSpecificQuote")]

        public async Task<string> GetSpecificQuote([FromBody] Currency Currency)
        {
            DolarAPI api = new DolarAPI();
            return await api.GetSpecificQuote(Currency);
        }


    }
}