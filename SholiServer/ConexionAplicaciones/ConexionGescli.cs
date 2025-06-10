using CL_EN;
using Newtonsoft.Json;
using System.Text;

namespace SholiServer.ConexionAplicaciones
{
    public class ConexionGescli
    {
        public ENobtenerSlugProducto ObtenerSlugProductoIndividual(string nombreProducto)
        {
            ENobtenerSlugProducto ent = new ENobtenerSlugProducto();
            try
            {
                string url = "https://misistemaperu.com/gescli/api/ApiIntegracion/ObtenerNombreSlugProducto";
                var datos = new
                {
                    Token = "ViajeEnEltiempo941888483",
                    NombreProducto = nombreProducto
                };

                string json = JsonConvert.SerializeObject(datos);

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(url, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        ent = JsonConvert.DeserializeObject<ENobtenerSlugProducto>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                ent.MensajeConsulta = "ERROR: " + ex.Message;
                ent.ValorConsulta = "-99";
            }
            return ent;
        }
    }
}
