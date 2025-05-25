using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;

public static class CryptoUtils
{
    public static byte[] GenerateRandomKey(int lengthBytes = 32)
    {
        var key = new byte[lengthBytes];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(key);
        return key;
    }

    public static int GenerateSecureInt(int maxExclusive)
    {
        if (maxExclusive <= 0)
            throw new ArgumentException("maxExclusive must be > 0");

        return RandomNumberGenerator.GetInt32(maxExclusive);
    }

    public static string ComputeHMAC_SHA3(byte[] key, int number)
    {
        var hmac = new HMac(new Org.BouncyCastle.Crypto.Digests.Sha3Digest(256));
        hmac.Init(new KeyParameter(key));

        var inputBytes = BitConverter.GetBytes(number);
        hmac.BlockUpdate(inputBytes, 0, inputBytes.Length);

        var result = new byte[hmac.GetMacSize()];
        hmac.DoFinal(result, 0);

        return Hex.ToHexString(result).ToUpperInvariant();
    }

    public static string BytesToHex(byte[] bytes)
    {
        return Hex.ToHexString(bytes).ToUpperInvariant();
    }
}

