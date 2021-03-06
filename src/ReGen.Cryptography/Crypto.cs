using System;
using System.Security.Cryptography;

namespace ReGen.Cryptography
{
    public static class Crypto
    {
        public static Aes GetAes(byte[] iv = null)
        {
            var aesCrypto = Aes.Create();
            aesCrypto.BlockSize = 128;
            aesCrypto.KeySize = 256;
#if DEBUG
            aesCrypto.Mode = CipherMode.CBC;
#else
            aesCrypto.Mode = CipherMode.CFB;
#endif
            aesCrypto.Padding = PaddingMode.ISO10126;
            if (!(iv is null))
                aesCrypto.IV = iv;
            return aesCrypto;
        }
    }
}
