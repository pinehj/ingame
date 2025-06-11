using System.Security.Cryptography;
using System.Text;

public class CryptoUtil
{
    public static string Encryption(string plainText, string salt)
    {
        SHA256 sha256 = SHA256.Create();

        // �ü�� Ȥ�� ���α׷��� ���� string ǥ���ϴ� ����� �� �ٸ��Ƿ�
        // UTF8 ���� ����Ʈ�� �迭�� �ٲ���Ѵ�.
        byte[] bytes = Encoding.UTF8.GetBytes(plainText + salt);
        byte[] hash = sha256.ComputeHash(bytes);

        string resultText = string.Empty;
        foreach (byte b in hash)
        {
            // byte�� �ٽ� string���� �ٲ㼭 �̾���̱�
            resultText += b.ToString("X2");
        }

        return resultText;
    }

    public static bool Verify(string plainText, string hash, string salt = "")
    {
        return Encryption(plainText, salt) == hash;
    }
}
