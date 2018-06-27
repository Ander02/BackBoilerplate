using System;
using System.Linq;
using System.Security.Cryptography;
using Utility.Extensions;

namespace Utility.Services
{
    public class EncryptService : IDisposable
    {
        private RandomNumberGenerator RandomNumberGenerator { get; }

        public EncryptService()
        {
            this.RandomNumberGenerator = RandomNumberGenerator.Create();
        }

        public string Encrypt(string password, string salt) => Encrypt(Convert(password), Convert(salt));

        public string Encrypt(string password, byte[] salt) => Encrypt(Convert(password), salt);

        public string Encrypt(byte[] password, byte[] salt)
        {
            byte[] toEncrypt = salt.Concat(password).ToArray();
            using (var sha = SHA512.Create()) return Convert(sha.ComputeHash(toEncrypt));
        }

        public (string password, string salt) Encrypt(string password)
        {
            var salt = GenerateSalt();
            var pass = Encrypt(password, salt);
            return (pass, Convert(salt));
        }

        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[64];
            RandomNumberGenerator.GetBytes(salt);
            return salt;
        }

        public string Convert(byte[] bytes) => bytes.ToBase64();

        public byte[] Convert(string str) => str.FromBase64();

        public void Dispose() => RandomNumberGenerator.Dispose();
    }
}

