using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PAKDial.Common
{
    public class CommonHashing
    {

        public static string ComputeHashSHA1(string plainText)
        {
            try
            {
                string salt = string.Empty;


                salt = ComputeHash(plainText, "SHA1", null);

                return salt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static string ComputeHash(string plainText, string hashAlgorithm, byte[] saltBytes)
        {
            try
            {
                if (saltBytes == null)
                {
                    int minSaltSize = 4;
                    int maxSaltSize = 8;

                    Random random = new Random();
                    int saltSize = random.Next(minSaltSize, maxSaltSize);
                    saltBytes = new byte[saltSize];
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    rng.GetNonZeroBytes(saltBytes);
                }

                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                byte[] plainTextWithSaltBytes =
                        new byte[plainTextBytes.Length + saltBytes.Length];

                for (int i = 0; i < plainTextBytes.Length; i++)
                    plainTextWithSaltBytes[i] = plainTextBytes[i];

                for (int i = 0; i < saltBytes.Length; i++)
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

                HashAlgorithm hash;

                hash = CreateHashAlgoFactory(hashAlgorithm);

                byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

                byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                    saltBytes.Length];

                for (int i = 0; i < hashBytes.Length; i++)
                    hashWithSaltBytes[i] = hashBytes[i];

                for (int i = 0; i < saltBytes.Length; i++)
                    hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

                string hashValue = Convert.ToBase64String(hashWithSaltBytes);

                return hashValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static HashAlgorithm CreateHashAlgoFactory(string hashAlgorithm)
        {
            try
            {
                HashAlgorithm hash = null; ;
                switch (hashAlgorithm)
                {
                    case "SHA1":
                        hash = new SHA1Managed();
                        break;

                    case "SHA256":
                        hash = new SHA256Managed();
                        break;

                    case "SHA384":
                        hash = new SHA384Managed();
                        break;

                    case "SHA512":
                        hash = new SHA512Managed();
                        break;

                    default:
                        hash = new MD5CryptoServiceProvider();
                        break;
                }
                return hash;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool VerifyHashSha1(string plainText, string compareWithSalt)
        {
            bool result = false;

            try
            {
                result = VerifyHash(plainText, "SHA1", compareWithSalt);

            }
            catch (CryptographicException)
            {
                result = false;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        private static bool VerifyHash(string plainText,
                                string hashAlgorithm,
                                string hashValue)
        {
            try
            {
                byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);
                int hashSizeInBits, hashSizeInBytes;
                hashSizeInBits = InitializeHashSize(hashAlgorithm);
                hashSizeInBytes = hashSizeInBits / 8;
                if (hashWithSaltBytes.Length < hashSizeInBytes)
                    return false;
                byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];
                for (int i = 0; i < saltBytes.Length; i++)
                    saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

                string expectedHashString = ComputeHash(plainText, hashAlgorithm, saltBytes);
                return (hashValue == expectedHashString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private static int InitializeHashSize(string hashAlgorithm)
        {
            try
            {
                int hashSizeInBits = 0;
                switch (hashAlgorithm)
                {
                    case "SHA1":
                        hashSizeInBits = 160;
                        break;

                    case "SHA256":
                        hashSizeInBits = 256;
                        break;

                    case "SHA384":
                        hashSizeInBits = 384;
                        break;

                    case "SHA512":
                        hashSizeInBits = 512;
                        break;

                    default:
                        hashSizeInBits = 128;
                        break;
                }
                return hashSizeInBits;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
