using System;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Contexts;
using lab3;

internal static class MillerRabin
{
    private const byte TEST_ITERATIONS = 15;
    private static BigInteger CreateRandomNumber(BigInteger min, BigInteger max)
    {
        BigInteger result;
        Random random = new Random();
        byte[] bytes = max.ToByteArray();

        do
        {
            random.NextBytes(bytes);
            bytes[bytes.Length - 1] &= 0x7F;
            result = new BigInteger(bytes);
        } while (result < min || result > max);
        
        return result;
    }

    private static bool TestForComposition(BigInteger primeSuspect, BigInteger coeff, int power)
    {
        BigInteger randomNumber = CreateRandomNumber(2, primeSuspect - 2);

        BigInteger compositeSuspect = BigInteger.ModPow(randomNumber, coeff, primeSuspect);

        if (compositeSuspect == 1 || compositeSuspect == primeSuspect - 1)
        {
            return false;
        }

        for (int i = 1; i < power; i++)
        {
            compositeSuspect = BigInteger.ModPow(compositeSuspect, 2, primeSuspect);
            if (compositeSuspect == primeSuspect - 1)
            {
                return false;
            }
            if (compositeSuspect == 1)
            {
                return true;
            }
        }

        return true;
    }

    internal static bool TestForAptitude(BigInteger suspect)
    {
        if (suspect % 4 != 3) return false;
        if (suspect < 4 && suspect > 1) return true;
        if (suspect % 2 == 0 || suspect % 3 == 0) return false;

        bool isPrime = true;
        int powerOfTwo = 0;
        BigInteger suspectCoeff = suspect - 1;
        
        while (suspectCoeff % 2 == 0)
        {
            suspectCoeff /= 2;
            powerOfTwo++;
        }

        Parallel.For(0, TEST_ITERATIONS, (i, state) =>
        {
            if (TestForComposition(suspect, suspectCoeff, powerOfTwo))
            {
                isPrime = false;
                state.Stop();
            }
        });

        return isPrime;
    }
}

public sealed class CryptoSystem
{
    private readonly BigInteger p, q, n, b;
    private readonly BigInteger pPlus1Div4, qPlus1Div4;
    private readonly BigInteger twoInversed, qInversed;
    private readonly int blockSize;

    public CryptoSystem(BigInteger p, BigInteger q, BigInteger b)
    {
        this.p = p;
        this.q = q;
        this.n = p * q;
        this.b = b;

        blockSize = n.ToByteArray().Length;
        pPlus1Div4 = (p + 1) / 4;
        qPlus1Div4 = (q + 1) / 4;
        twoInversed = ModInverse(2, n);
        qInversed = ModInverse(q, p);
    }

    public byte[] EncryptFile(string inputFile)
    {
        byte[] inputData = File.ReadAllBytes(inputFile);
        byte[] outputData = new byte[inputData.Length * blockSize];

        Parallel.For(0, inputData.Length, i =>
        {
            BigInteger m = inputData[i];
            BigInteger c = (m * (m + b)) % n;
            byte[] bytes = c.ToByteArray();
            Buffer.BlockCopy(bytes, 0, outputData, i * blockSize, bytes.Length);
        });

        return outputData;
    }

    public byte[] DecryptFile(string inputFile)
    {
        byte[] inputData = File.ReadAllBytes(inputFile);
        int blocksCount = inputData.Length / blockSize;
        byte[] outputData = new byte[blocksCount];


        Parallel.For(0, blocksCount, i =>
        {
            byte[] block = new byte[blockSize];
            Buffer.BlockCopy(inputData, i * blockSize, block, 0, blockSize);
            BigInteger c = new BigInteger(block);
            outputData[i] = FindValidByte(Decrypt(c));
        });

        return outputData;
    }

    private static byte FindValidByte(BigInteger[] solutions)
    {
        foreach (var s in solutions)
        {
            if (s >= 0 && s <= 255)
                return (byte)s;
        }
        return 0;
    }

    private BigInteger[] Decrypt(BigInteger c)
    {
        BigInteger D = (b * b + 4 * c) % n;
        if (D < 0) D += n;

        BigInteger sqrt_p = BigInteger.ModPow(D % p, pPlus1Div4, p);
        BigInteger sqrt_q = BigInteger.ModPow(D % q, qPlus1Div4, q);

        var roots = new BigInteger[4];
        roots[0] = CRT(sqrt_p, sqrt_q);
        roots[1] = CRT(p - sqrt_p, sqrt_q);
        roots[2] = CRT(sqrt_p, q - sqrt_q);
        roots[3] = CRT(p - sqrt_p, q - sqrt_q);

        var results = new BigInteger[4];
        for (int i = 0; i < 4; i++)
        {
            results[i] = ((roots[i] - b) * twoInversed) % n;
            if (results[i] < 0) results[i] += n;
        }

        return results;
    }

    private BigInteger CRT(BigInteger a, BigInteger b)
    {
        BigInteger h = (qInversed * (a - b)) % p;
        if (h < 0) h += p;
        return (b + h * q) % n;
    }

    private static BigInteger ModInverse(BigInteger a, BigInteger m)
    {
        BigInteger x, y;
        ExtendedGcd(a, m, out x, out y);
        return (x % m + m) % m;
    }

    private static BigInteger ExtendedGcd(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
    {
        BigInteger x0 = 1, y0 = 0;
        BigInteger x1 = 0, y1 = 1;
        BigInteger q;

        while (b != 0)
        {
            q = a / b;
            (a, b) = (b, a % b);
            (x0, x1) = (x1, x0 - q * x1);
            (y0, y1) = (y1, y0 - q * y1);
        }

        x = x0;
        y = y0;
        return a;
    }
}

internal static class Logic
{
    internal static BigInteger InterpreteNumber(string strInt)
    {
        return strInt == "" ? 0 : BigInteger.Parse(strInt);
    }

    internal static void ShowWarning(string message)
    {
        MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
    }

    internal static bool CheckNumberB(BigInteger b, BigInteger n)
    {
        return b < n ? true : false;
    }

    internal static async Task ShowBytes(TextBox textBox, byte[] bytes)
    {
        if (bytes.Length > 5000)
        {
            await ShowByteParts(textBox, bytes);
            return;
        }

        await ShowFullBytes(textBox, bytes);
    }
    private static Task ShowByteParts(TextBox textBox, byte[] bytes)
    {
        bytes.ToString();
        var sb = new StringBuilder();

        for (int i = 0; i < 100; i++)
        {
            sb.Append(bytes[i].ToString() + " ");
        }

        sb.Append("\r\n...\r\n");

        for (int i = bytes.Length - 100; i < bytes.Length; i++)
        {
            sb.Append(bytes[i].ToString() + " ");
        }

        textBox.AppendText(sb.ToString());
        textBox.Refresh();

        return Task.CompletedTask;
    }
    private static Task ShowFullBytes(TextBox textBox, byte[] bytes)
    {
        int expectedId = 0;
        int batchSize = 2000;
        int numBatches = Math.Max(bytes.Length / batchSize, 1);
        object lockObject = new object();
        textBox.Clear();

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = 10
        };
        Dictionary<int, string> batches = new Dictionary<int, string>();

        return Task.Run(() =>
        {
            Parallel.For(0, numBatches + 1, options, id =>
            {
                int start = id * batchSize;
                int end = Math.Min(start + batchSize, bytes.Length);

                var sb = new StringBuilder(batchSize);
                for (int i = start; i < end; i++)
                {
                    sb.Append(bytes[i].ToString() + " ");
                }

                string result = sb.ToString();

                lock (lockObject)
                {
                    batches[id] = result;

                    while (batches.TryGetValue(expectedId, out var nextResult))
                    {
                        textBox.BeginInvoke((Action)(() =>
                        {
                            textBox.AppendText(nextResult);
                            textBox.Refresh();
                        }));

                        batches.Remove(expectedId);
                        sb = null;
                        expectedId++;
                    }
                }
            });
        });
    }
}

