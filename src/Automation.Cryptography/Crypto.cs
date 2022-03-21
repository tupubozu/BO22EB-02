using System;
using System.Security.Cryptography;

namespace Automation.Cryptography
{
    public static class Crypto
    {
        public static Aes GetAes(byte[] iv = null)
        {
            var aesCrypto = Aes.Create();
            aesCrypto.BlockSize = 128;
            aesCrypto.KeySize = 256;
            aesCrypto.Mode = CipherMode.CBC;
            aesCrypto.Padding = PaddingMode.ISO10126;
            if (!(iv is null))
                aesCrypto.IV = iv;
            return aesCrypto;
        }
    }
}
