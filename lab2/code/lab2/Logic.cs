using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

internal static class Logic
{
    private const byte BIT_IN_BYTE = 8;
    private const byte INT_OFFSET = 29;
    private const byte TO_BIT_35 = 29;
    private const byte TO_BIT_2 = 62;
    private const byte TO_BIT_1 = 63;

    private static Task showPartBits(TextBox textBox, BitArray bits)
    {
        var sb = new StringBuilder(165);

        for (int i = 0; i < 80; i++)
        {
            sb.Append(bits[i] ? '1' : '0');
        }

        sb.Append("\r\n...\n");

        for (int i = bits.Length - 80; i < bits.Length; i++)
        {
            sb.Append(bits[i] ? '1' : '0');
        }

        textBox.AppendText(sb.ToString());
        textBox.Refresh();

        return Task.CompletedTask;
    }
    private static Task showFullBits(TextBox textBox, BitArray bits)
    {
        int expectedId = 0;
        int batchSize = 2000;
        int numBatches = Math.Max(bits.Length / batchSize, 1);
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
                int end = Math.Min(start + batchSize, bits.Length);

                var sb = new StringBuilder(batchSize);
                for (int i = start; i < end; i++)
                {
                    sb.Append(bits[i] ? '1' : '0');
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
    internal static async Task showBits(TextBox textBox, BitArray bits)
    {
        if (bits.Length > 30000)
        {
            await showPartBits(textBox, bits);
            return;
        }

        await showFullBits(textBox, bits);
    }

    internal static void generateKey(string key)
    {
        bool shiftedBit, extraShiftedBit, xorResult;

        ulong register;

        byte[] registerBytes = new byte[8];

        int length = InitialText.Length * BIT_IN_BYTE;

        BitArray initState = new BitArray(64);
        BitArray resultKey = new BitArray(length);

        for(int i = INT_OFFSET; i < initState.Length; i++)
        {
            initState[i] = key[i - INT_OFFSET] == '1' ? true : false;
        }

        initState.CopyTo(registerBytes, 0);
        register = (ulong)BitConverter.ToInt64(registerBytes, 0);

        for (int i = 0; i < resultKey.Length; i++)
        {
            shiftedBit = (register & (1UL << TO_BIT_35)) != 0;
            extraShiftedBit = (register & (1UL << TO_BIT_2)) != 0;

            xorResult = shiftedBit ^ extraShiftedBit;

            register = register >> 1;
            register |= xorResult ? (1UL << TO_BIT_1) : 0UL;

            resultKey[i] = shiftedBit;
        }

        Key = resultKey;
    }

    internal static void generateResult()
    {
        BitArray text = new BitArray(InitialText);
        BitArray result = new BitArray(text.Length);

        int batchSize = 64;
        int numBatches = text.Length / batchSize;

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = 10
        };

        Dictionary<int, string> batches = new Dictionary<int, string>(numBatches);

        Parallel.For(0, numBatches + 1, options, id =>
        {
            int start = id * batchSize;
            int end = Math.Min(start + batchSize, text.Length);

            for (int i  = start; i < end; i++)
            {
                result[i] = text[i] ^ Key[i];
            }
        }
        );

        ResultText = new byte[result.Length / 8];
        result.CopyTo(ResultText, 0);
    }

    internal static void handleError(string message)
    {
        MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
    }

    internal static BitArray Key { get; private set; }

    internal static byte[] InitialText { get; set; }

    internal static byte[] ResultText { get; private set; }
}

