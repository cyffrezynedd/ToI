using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            btnExecute.Enabled = false;
            btnGen.Enabled = false;
            comboBox.SelectedIndex = 0;
        }

        private string LastExtension { get; set; }
        private string Action { get; set; }
        private Encoding FileEncoding { get; set; }

        private bool isInitTextClear { get => getStatus(tbInitText.Text); }
        private bool isResultKeyClear { get => getStatus(tbResultKey.Text); }
        private bool isResultTextClear { get => getStatus(tbResultText.Text); }

        private bool getStatus(string value)
        {   
            return value == "";
        }

        private bool canExecute()
        {
            return !isInitTextClear && !isResultKeyClear;
        }

        private bool canGenerate()
        {
            return !isInitTextClear && lbCount.Text == "35";
        }

        private string setAction(Byte index)
        {
            tbInitText.Text = "";
            tbInitKey.Text = "";
            tbResultText.Text = "";
            tbResultKey.Text = "";

            switch (index)
            {
                case 0:
                    label1.Text = "Исходный текст";
                    label2.Text = "Шифротекст";
                    return "Encrypt";
                case 1:
                    label2.Text = "Исходный текст";
                    label1.Text = "Шифротекст";
                    return "Decrypt";
            }
            return "Undefined";
        }

        private async void openFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter =
                "Текстовые файлы (*.txt)|*.txt|" +
                "Файлы PDF (*.pdf)|*.pdf|" +
                "Аудио MP3 (*.mp3)|*.mp3|" +
                "Видео MP4 (*.mp4)|*.mp4|" +
                "Изображения (*.png; *.jpg)|*.png;*.jpg";

            var dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                tbInitText.Text = "";
                tbResultKey.Text = "";
                byte[] fileBytes;
                string buffer = Path.GetExtension(openFileDialog1.FileName);
                LastExtension = buffer.Substring(1,buffer.Length - 1);

                fileBytes = File.ReadAllBytes(openFileDialog1.FileName);

                Logic.InitialText = (byte[])fileBytes.Clone();

                try
                {
                    await Logic.showBits(tbInitText, new BitArray(fileBytes));
                }
                catch (Exception ex)
                {
                    Logic.handleError($"Ошибка при открытии файла:\n{ex.Message}");
                }
            }
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            if (isResultTextClear)
            {
                Logic.handleError("Нет результата для сохранения!");
                return;
            }

            saveFileDialog1.Filter =
                "Текстовые файлы (*.txt)|*.txt|" +
                "Файлы PDF (*.pdf)|*.pdf|" +
                "Аудио MP3 (*.mp3)|*.mp3|" +
                "Видео MP4 (*.mp4)|*.mp4|" +
                "Изображения (*.png; *.jpg)|*.png;*.jpg";

            saveFileDialog1.DefaultExt = LastExtension;

            var dialogResult = saveFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    File.WriteAllBytes(saveFileDialog1.FileName, Logic.ResultText);
                }
                catch (Exception ex)
                {
                    Logic.handleError($"Ошибка при сохранении файла:\n{ex.Message}");
                }
            }
        }

        private void cb_ChangeIndex(object sender, EventArgs e)
        {
            Action = setAction((Byte)comboBox.SelectedIndex);
        }

        private async void btn_Execute(object sender, EventArgs e)
        {
            Logic.generateResult();
            await Logic.showBits(tbResultText, new BitArray(Logic.ResultText));
        }

        private async void btn_GenKey(object sender, EventArgs e)
        {
            Logic.generateKey(tbInitKey.Text);
            await Logic.showBits(tbResultKey, Logic.Key);

            btnExecute.Enabled = canExecute();
        }

        private void tb_TextChange(object sender, EventArgs e)
        {
            tbResultText.Text = "";

            btnGen.Enabled = canGenerate();
            btnExecute.Enabled = canExecute();
        }

        private void tb_KeyChanged(object sender, EventArgs e)
        {
            short count = 0;

            for(short i = 0; i < tbInitKey.Text.Length; i++)
            {
                if (tbInitKey.Text[i] == '0' || tbInitKey.Text[i] == '1')
                {
                    count++;
                }
            }

            lbCount.Text = count.ToString();
            btnGen.Enabled = canGenerate();
        }

        private void tb_KeyInput(object sender, KeyPressEventArgs e)
        {
            if (lbCount.Text == "35" && e.KeyChar != '\b' && e.KeyChar != 26 && e.KeyChar != 3
                || e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '\b' && e.KeyChar != 26 && e.KeyChar != 3)
            {
                e.KeyChar = '\0';
            }
            if (e.KeyChar != 3)
            {
                tbResultKey.Text = "";
            }
        }
    }
}
