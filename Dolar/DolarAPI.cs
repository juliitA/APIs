using Dolar.Model;

namespace Dolar.Resource
{
    public class DolarAPI
    {
        //Método para obtener la cotización del dólar blue
        public async Task<string> GetQuote()
        {
            string responseBody = string.Empty; //Variable para almacenar la respuesta de la API 
            using (var client = new HttpClient()) // se inicializa un cliente HTTP
            {
                client.BaseAddress = new Uri("https://dolarapi.com/v1/dolares/blue"); //URL del dolar blue
                client.DefaultRequestHeaders.Accept.Clear(); //limpia cualquier encabezado anterior en la solicitud
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); //define el encabezado para esperar JSON
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress); //realiza la solicitud GET a la URL base
                response.EnsureSuccessStatusCode(); //lanza excepción si la solicitud no fue exitosa

                responseBody = response.Content.ReadAsStringAsync().Result; //lee y almacena el contenido de la respuesta como string
            }
            return responseBody; //retorna el cuerpo de la respuesta en formato JSON
        }

        //Método para obtener cotización de un tipo específico de dólar según el código de moneda ingresado
        public async Task<string> GetSpecificQuote(Currency Currency)
        {
            string responseBody = string.Empty; //variable para almacenar la respuesta de la API
            using (var client = new HttpClient()) //se inicializa un cliente HTTP
            {
                //selecciona el endpoint segun el codigo de moneda en mayusculas
                switch (Currency.Code.ToUpper()) //convierte el codigo a mayusculas
                {
                    case "BOLSA":
                        client.BaseAddress = new Uri("https://dolarapi.com/v1/dolares/bolsa"); //URL del dolar bolsa
                        break;
                    case "BLUE":
                        client.BaseAddress = new Uri("https://dolarapi.com/v1/dolares/blue"); //URL del dolar blue
                        break;
                    case "CRIPTO":
                        client.BaseAddress = new Uri("https://dolarapi.com/v1/dolares/cripto"); //URL del dolar cripto
                        break;

                    default:
                        throw new ArgumentException("Currency Type Error"); //lanza una excepcion si el codigo no es valido
                }

                client.DefaultRequestHeaders.Accept.Clear(); //limpia cualquier encabezado previo en la solicitud
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); //define el encabezado para esperar JSON

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress); //realiza la solicitud GET a la URL seleccionada 
                response.EnsureSuccessStatusCode(); //lanza una excepcion si la solicitud no fue exitosa

                responseBody = response.Content.ReadAsStringAsync().Result; //lee y almacena el contenido de la respuesta como string de forma asinc porque el metodo es asinc.


            }
            return responseBody;//retorna el cuerpo de la respuesta en formato JSON
        }

       
    }
}
