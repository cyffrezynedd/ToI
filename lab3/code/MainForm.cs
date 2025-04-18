using System;
using System.Windows.Forms;
using System.IO;
using System.Numerics;

namespace lab3
{
    public partial class MainForm : Form
    {
        private const byte ENCRYPT = 0;
        private const byte DECRYPT = 1;
        private const byte LIMIT = 255;
        private const byte OFFSET = 48;

        private const string FILTER =
                "Текстовые файлы (*.txt)|*.txt|" +
                "Файлы PDF (*.pdf)|*.pdf|" +
                "Аудио MP3 (*.mp3)|*.mp3|" +
                "Видео MP4 (*.mp4)|*.mp4|" +
                "Изображения (*.png; *.jpg)|*.png;*.jpg";

        private byte actionIndex = 255;
        private string InputFile { get; set; } = null;
        private byte[] OutputData { get; set; } = null;
        private byte Action 
        { 
            get => actionIndex;
            set => actionIndex = ActionEffect(value);
        }
        private bool IsInitTextClear { get => GetStatus(tbInitial.Text); }
        private bool IsNumPClear { get => GetStatus(tbNumP.Text); }
        private bool IsNumQClear { get => GetStatus(tbNumQ.Text); }
        private bool IsNumBClear { get => GetStatus(tbNumB.Text); }

        private bool GetStatus(string value)
        {
            return value == "";
        }

        private bool CanExecute()
        {
            return !IsInitTextClear && !IsNumPClear && !IsNumQClear && !IsNumBClear;
        }

        private byte ActionEffect(byte index)
        {
            if (Action == index) return index;

            tbInitial.Text = "";
            tbResult.Text = "";
            tbNumN.Text = "";
            tbNumB.Text = "";
            tbNumP.Text = "";
            tbNumQ.Text = "";

            switch (index)
            {
                case ENCRYPT:
                    lbInitial.Text = "Исходный текст";
                    lbResult.Text = "Шифротекст";
                    comboBox.SelectedIndex = index;
                    return 0;
                case DECRYPT:
                    lbResult.Text = "Исходный текст";
                    lbInitial.Text = "Шифротекст";
                    comboBox.SelectedIndex = index;
                    return 1;
            }
            return 255;
        }

        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();
            Action = 0;
        }

        private void tb_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if(!MillerRabin.TestForAptitude(Logic.InterpreteNumber(tb.Text)) && tb.Text.Length != 0)
            {
                Logic.ShowWarning("Значение не соответствует условиям:\n" +
                    "1) Число = 4 * k + 3\n" +
                    "2) Число простое");
                tb.Text = "";
                tb.Focus();
            }
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as TextBox;

            if (tb.Name == "tbNumP" || tb.Name == "tbNumQ")
            {
                if (MillerRabin.TestForAptitude(Logic.InterpreteNumber(tb.Text)) && tb.Text.Length != 0)
                {
                    var p = Logic.InterpreteNumber(tbNumP.Text);
                    var q = Logic.InterpreteNumber(tbNumQ.Text);
                    var n = p * q;

                    tbNumN.Text = n == 0 ? "" : n.ToString();
                    tbNumB.Enabled = !IsNumPClear && !IsNumQClear;
                }
                else
                {
                    tbNumB.Enabled = false;
                }

                int length = tb.Text.Length;

                switch (tb.Name)
                {
                    case "tbNumP":
                        lbCountP.Text = length.ToString();
                        return;
                    case "tbNumQ":
                        lbCountQ.Text = length.ToString();
                        return;
                }
            }

            btnExecute.Enabled = CanExecute();
        }

        private void tb_NumChangePQ(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            byte length = (byte)tb.Text.Length;

            if (e.KeyChar == (char)Keys.Enter)
            {
                SelectNextControl((Control)sender,true, true, false, true);
                return;
            }

            if (length == LIMIT && e.KeyChar != '\b' && e.KeyChar != 26 && e.KeyChar != 3
                || e.KeyChar != '\b' && e.KeyChar != 26 && e.KeyChar != 3 && (e.KeyChar - OFFSET < 0 || e.KeyChar - OFFSET > 9))
            {
                e.KeyChar = '\0';
            }
            else if(e.KeyChar != 3)
            {
                tbNumN.Text = "";
                tbResult.Text = "";
                tbNumB.Text = "";
            }
        }

        private void tb_NumChangeB(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            var b = Logic.InterpreteNumber(e.KeyChar >= '0' && e.KeyChar <= '9' ? tb.Text + e.KeyChar : tb.Text);
            var n = Logic.InterpreteNumber(tbNumN.Text);

            if (e.KeyChar == (char)Keys.Enter)
            {
                SelectNextControl((Control)sender, true, true, false, true);
                return;
            }

            if (!Logic.CheckNumberB(b, n))
            {
                e.KeyChar = '\0';
                tb.Text = (n - 1).ToString();
                Logic.ShowWarning($"b должно быть меньше n = {n}");
                return;
            }
            

            if (e.KeyChar != '\b' && e.KeyChar != 26 && e.KeyChar != 3 && (e.KeyChar - OFFSET < 0 || e.KeyChar - OFFSET > 9))
            {
                e.KeyChar = '\0';
            }
            else if (e.KeyChar != 3) tbResult.Text = "";
        }

        private async void btn_Execute(object sender, EventArgs e)
        {
            BigInteger p = Logic.InterpreteNumber(tbNumP.Text);
            BigInteger q = Logic.InterpreteNumber(tbNumQ.Text);
            BigInteger b = Logic.InterpreteNumber(tbNumB.Text);

            CryptoSystem rabin = new CryptoSystem(p, q, b);

            OutputData = Action == ENCRYPT ? rabin.EncryptFile(InputFile) : rabin.DecryptFile(InputFile);
            tbResult.Text = "";
            await Logic.ShowBytes(tbResult, OutputData);
        }

        private void cb_ChangeIndex(object sender, EventArgs e)
        {
            Action = (byte)comboBox.SelectedIndex;
        }

        private async void openFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = FILTER;
            tbInitial.Text = "";

            byte[] bytes;
            var dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    bytes = File.ReadAllBytes(openFileDialog1.FileName);
                    await Logic.ShowBytes(tbInitial, bytes);
                    InputFile = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    Logic.ShowWarning($"Ошибка при открытии файла:\n{ex.Message}");
                    InputFile = "";
                }
                finally
                {
                    bytes = null;
                    tbResult.Text = "";
                }
            }
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            if (tbResult.Text == "")
            {
                Logic.ShowWarning("Нет результата для сохранения!");
                return;
            }

            saveFileDialog1.Filter = FILTER;

            var dialogResult = saveFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    File.WriteAllBytes(saveFileDialog1.FileName, OutputData);
                }
                catch (Exception ex)
                {
                    Logic.ShowWarning($"Ошибка при сохранении файла:\n{ex.Message}");
                }
            }
        }
    }
}
