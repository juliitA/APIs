using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyConversionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : ControllerBase
    {
        [HttpGet("{dollarAmount}")]
        public async Task<IActionResult> ConvertDollarToPeso(decimal dollarAmount)
        {
            try
            {
                // Llama a la API anterior para obtener la cotización del dólar
                var currency = new { Code = "Bolsa" }; // Cambia el código si quieres otro tipo de dólar
                var jsonObject = JsonSerializer.Serialize(currency);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7238"); // Cambia la URL si tu API anterior está en otro puerto
                var response = await client.PostAsync("/api/Quote", content);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Error de conexión al obtener la cotización del dólar.");
                }

                var str = await response.Content.ReadAsStringAsync();
                var quoteResponse = JsonSerializer.Deserialize<QuoteCurrencyResponse>(str);

                if (quoteResponse == null || quoteResponse.Venta <= 0)
                {
                    return BadRequest("Cotización no válida obtenida de la API.");
                }

                // Realiza la conversión
                decimal pesoAmount = dollarAmount * quoteResponse.Venta;
                return Ok(new { DollarAmount = dollarAmount, PesoAmount = pesoAmount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }
        }
    }

}
