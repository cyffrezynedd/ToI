using System;
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

namespace lab1
{
    public partial class Lab1 : Form
    {
        Column columnMethod = new Column();
        Vigenera vigeneraMethod = new Vigenera();

        private string Method { get; set; }

        private string Action { get; set; }

        private bool isInputClear { get => getStatus(textBox1.Text); }
        private bool isKeyMainClear { get => getStatus(keyFieldMain.Text); }
        private bool isKeyExtraClear { get => getStatus(keyFieldExtra.Text); }

        private bool getStatus(string text)
        {   
            return text == "";
        }

        private bool haveNormalLength()
        {
            return Method == "Column" ? textBox1.Text.Length >= keyFieldMain.Text.Length && textBox1.Text.Length >= keyFieldExtra.Text.Length
                && keyFieldMain.Text.Length > 1 && keyFieldExtra.Text.Length > 1 :
                textBox1.Text.Length >= keyFieldMain.Text.Length;
        }

        private bool canExecute()
        {
            return Method == "Column" ? !isInputClear && !isKeyMainClear && !isKeyExtraClear : !isInputClear && !isKeyMainClear;
        }

        private string setAction(Byte index)
        {
            textBox2.Text = "";
            keyFieldMain.Text = "";
            keyFieldExtra.Text = "";
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

        private string setMethod(Byte index)
        {
            textBox2.Text = "";
            keyFieldMain.Text = "";
            keyFieldExtra.Text = "";
            comboBox1.SelectedIndex = 0;
            Action = setAction(0);

            switch (index)
            {
                case 0:
                    lbKeyMain.Text = "Ключ 1";
                    lbKeyExtra.Visible = true;
                    keyFieldExtra.Visible = true;
                    this.Text = "Lab1 - Столбцовый метод";
                    return "Column";
                case 1:
                    lbKeyMain.Text = "Ключ";
                    keyFieldExtra.Visible = false;
                    lbKeyExtra.Visible = false;
                    this.Text = "Lab1 - Шифр Виженера";
                    return "Vigenera";
            }
            return "Undefined";
        }

        public Lab1()
        {
            InitializeComponent();
            this.CenterToScreen();
            button1.Enabled = false;
            comboBox1.SelectedIndex = 0;
            Method = "Column";
            this.Text = "Lab1 - Столбцовый метод";
        }

        private void btn_Execute(object sender, EventArgs e)
        {
            switch (Method)
            {
                case "Column":
                    columnMethod.keyMain = getCorrectStr(keyFieldMain.Text);
                    keyFieldMain.Text = columnMethod.keyMain;

                    columnMethod.keyExtra = getCorrectStr(keyFieldExtra.Text);
                    keyFieldExtra.Text = columnMethod.keyExtra;

                    columnMethod.inputText = getCorrectStr(textBox1.Text);
                    textBox1.Text = columnMethod.inputText;

                    if (!canExecute())
                    {
                        MessageBox.Show("Поля должны содержать хотя бы одну русскую букву!",
                                "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!haveNormalLength())
                    {
                        if (keyFieldMain.Text.Length < 2 || keyFieldExtra.Text.Length < 2)
                            MessageBox.Show("Размер ключа не должен быть меньше 2!",
                                "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("Ключ не должен превышать размер текста!",
                                "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    switch (Action)
                    {
                        case "Encrypt":
                            columnMethod.outputText = columnMethod.encryptText(columnMethod.inputText, columnMethod.keyMain);
                            textBox2.Text = columnMethod.encryptText(columnMethod.outputText, columnMethod.keyExtra);
                            return;
                        case "Decrypt":;
                            columnMethod.outputText = columnMethod.decryptText(columnMethod.inputText, columnMethod.keyExtra);
                            textBox2.Text = columnMethod.decryptText(columnMethod.outputText, columnMethod.keyMain);
                            return;
                    }
                    return;

                case "Vigenera":
                    vigeneraMethod.key = getCorrectStr(keyFieldMain.Text);
                    keyFieldMain.Text = vigeneraMethod.key;

                    vigeneraMethod.inputText = getCorrectStr(textBox1.Text);
                    textBox1.Text = vigeneraMethod.inputText;

                    if (!canExecute())
                    {
                        MessageBox.Show("Поля должны содержать хотя бы одну русскую букву!",
                                "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!haveNormalLength())
                    {
                        MessageBox.Show("Ключ превышает размер текста!\nНе все символы ключа были задействованы!",
                            "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    switch (Action)
                    {
                        case "Encrypt":
                            textBox2.Text = vigeneraMethod.encryptText();
                            return;
                        case "Decrypt":
                            textBox2.Text = vigeneraMethod.decryptText();
                            return;
                    }
                    return;
            }

        }

        private void cb_changeIndex(object sender, EventArgs e)
        {
            Action = setAction((Byte)comboBox1.SelectedIndex);
        }

        private void tsmi_setColumn(object sender, EventArgs e)
        {
            Method = setMethod((Byte)0);
        }

        private void tsmi_setVijenera(object sender, EventArgs e)
        {
            Method = setMethod((Byte)1);
        }

        private void tb_textChange(object sender, EventArgs e)
        {
            textBox2.Text = "";

            button1.Enabled = canExecute();
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt";
            var dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                StringBuilder sb = new StringBuilder();

                string str = sr.ReadToEnd();

                sb.Append(str);
                textBox1.Text = sb.ToString();
            }
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Нет результата для сохранения!",
                                "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            saveFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt";
            saveFileDialog1.DefaultExt = "txt";

            var dialogResult = saveFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog1.FileName, textBox2.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла:\n{ex.Message}",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public string getCorrectStr(string str)
        {
            string result = "";

            str = str.ToUpper();

            foreach (char symbol in str)
            {
                if (symbol >= 'А' && symbol <= 'Я' || symbol == 'Ё')
                {
                    result += symbol;
                }
            }

            return result;
        }
    }
}
