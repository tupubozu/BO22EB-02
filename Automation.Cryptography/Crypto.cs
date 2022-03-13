using System;
using System.Security.Cryptography;

namespace Automation.Cryptography
{
    public static class Crypto
    {
        public static Aes GetAes512(byte[] iv)
        {
            var aesCrypto = Aes.Create();
            aesCrypto.BlockSize = 512;
            aesCrypto.KeySize = 512;
            aesCrypto.Mode = CipherMode.CBC;
            aesCrypto.Padding = PaddingMode.ISO10126;
            aesCrypto.IV = iv;
            return aesCrypto;
        }
    }
}
