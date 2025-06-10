
using System.Security.Cryptography;
using System.Text;

namespace CL_AD
{
    public class EncryptAndDecryptFile
    {
        public string DecryptFromString(String text)
        {

            try
            {
                String keyEncrit = "Pr4s3nS0luc10n3s";

                // Definir el tamaño de la clave y el vector de inicio a utilizarse
                int keySize = 32;
                int ivSize = 16;

                // Convertir la llave y el vector de inicio a su representación en bytes
                byte[] Key = UTF8Encoding.UTF8.GetBytes(keyEncrit);
                byte[] IV = UTF8Encoding.UTF8.GetBytes(keyEncrit);

                // Garantizar el tamaño correcto de la clave y el vector de inicio
                // mediante substring o padding
                Array.Resize<byte>(ref Key, keySize);
                Array.Resize<byte>(ref IV, ivSize);

                // Obtener la representación en bytes del texto cifrado
                byte[] cipherTextBytes = Convert.FromBase64String(text);

                // Crear un arreglo de bytes para almacenar los datos descifrados
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                    using (MemoryStream msPlain = new MemoryStream())
                    {
                        csDecrypt.CopyTo(msPlain);
                        plainTextBytes = msPlain.ToArray();
                    }
                }
                return Encoding.UTF8.GetString(plainTextBytes);


            }
            catch (Exception ex)
            {

                return "Horror";
            }


        }
    }
}
