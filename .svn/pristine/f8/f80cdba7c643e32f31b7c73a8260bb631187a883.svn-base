using Microsoft.Extensions.Configuration;

namespace CL_AD
{
    public class MiConexion
    {
        private static readonly IConfigurationRoot _config;

        static MiConexion()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // importante para consola y backend
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public string TraerMiConexion()
        {
            //Aqui debo de tener la cadena de conexion encriptaba en el archivo appsettings.json
            string cadenaConexion = _config.GetConnectionString("CadenaConexionSQL");

            EncryptAndDecryptFile enc = new EncryptAndDecryptFile();
            cadenaConexion = enc.DecryptFromString(cadenaConexion);
            string[] cnxDescompuesta = cadenaConexion.Split(';');

            string cadenaConexionFinal = "Data Source=" + cnxDescompuesta[2].ToString() +
                       ";Initial Catalog=" + cnxDescompuesta[5].ToString() +
                       ";Persist Security Info=True;User ID=" + cnxDescompuesta[4].ToString() +
                       ";Password=" + cnxDescompuesta[1].ToString() +
                       ";Encrypt=True;TrustServerCertificate=True;";

            return cadenaConexionFinal;

        }

    }
}
