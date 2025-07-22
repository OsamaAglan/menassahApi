using System.Security.Cryptography;
using System.Text;

namespace Menassah.Shared
{
    public class DSP_Encryptor
    {
        public static string Encrypt(string sourceData, byte[] key, byte[] iv)
        {
            try
            {
                var key2 = new byte[24];
                int length = key.Length < key2.Length ? key.Length : key2.Length;

                for (int i = 0; i < length; i++)
                {
                    key2[i] = key[i];
                }

                // convert data to byte array
                var sourceDataBytes = Encoding.ASCII.GetBytes(sourceData);
                // get target memory stream
                var tempStream = new MemoryStream();
                // get encryptor and encryption stream

                using (TripleDES tripleDes = TripleDES.Create())
                // Create a TripleDES encryptor from the key and IV
                using (ICryptoTransform encryptor1 = tripleDes.CreateEncryptor(key2, iv))
                {
                    var encryptionStream = new CryptoStream(tempStream, encryptor1, CryptoStreamMode.Write);
                    // encrypt data
                    encryptionStream.Write(sourceDataBytes, 0, sourceDataBytes.Length);
                    encryptionStream.FlushFinalBlock();
                    // put data into byte array
                    var encryptedDataBytes = tempStream.GetBuffer();
                    // convert encrypted data into string
                    return Convert.ToBase64String(encryptedDataBytes, 0, (int)tempStream.Length);

                }

            }
            catch
            {
                // Throw New StringEncryptorException("Unable to encrypt data.")
            }

            return default;
        }
        public static string Decrypt(string sourceData, byte[] key, byte[] iv)
        {

            try
            {
                var key2 = new byte[24];
                int length = key.Length < key2.Length ? key.Length : key2.Length;

                for (int i = 0; i < length; i++)
                {
                    key2[i] = key[i];
                }

                // convert data to byte array
                var encryptedDataBytes = Convert.FromBase64String(sourceData);

                // get source memory stream and fill it
                var tempStream = new MemoryStream(encryptedDataBytes, 0, encryptedDataBytes.Length);

                // get decryptor and decryption stream
                using (TripleDES tripleDes = TripleDES.Create())
                using (ICryptoTransform decryptor = tripleDes.CreateDecryptor(key2, iv))
                {
                    var encryptionStream = new CryptoStream(tempStream, decryptor, CryptoStreamMode.Read);
                    // decrypt data
                    var allDataReader = new StreamReader(encryptionStream);
                    return allDataReader.ReadToEnd();
                }

            }
            catch
            {
                // Throw New StringEncryptorException("Unable to decrypt data.")
            }

            return default;
        }

        public static string Encrypt(string sourceData)
        {
            var key = new byte[24];
            var iv = new byte[8];
            return Encrypt(sourceData, key, iv);

        }
        public static string Decrypt(string sourceData)
        {

            // set key and initialization vector values
            var key = new byte[24];
            var iv = new byte[8];
            return Decrypt(sourceData, key, iv);
        }

        public static string EncryptWithKey(string sourceData, string key)
        {
            // set key and initialization vector values
            var key1 = Encoding.ASCII.GetBytes(key);
            var iv = Encoding.ASCII.GetBytes(key);
            return Encrypt(sourceData, key1, iv);
        }
        public static string DecryptWithKey(string sourceData, string key)
        {
            // set key and initialization vector values
            var key1 = Encoding.ASCII.GetBytes(key);
            var iv = Encoding.ASCII.GetBytes(key);
            return Decrypt(sourceData, key1, iv);

        }

    }
}
