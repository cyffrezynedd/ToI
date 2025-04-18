namespace lab3
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tbNumB = new System.Windows.Forms.TextBox();
            this.tbNumP = new System.Windows.Forms.TextBox();
            this.tbNumQ = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.tbInitial = new System.Windows.Forms.TextBox();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.lbNumP = new System.Windows.Forms.Label();
            this.lbNumB = new System.Windows.Forms.Label();
            this.lbNumQ = new System.Windows.Forms.Label();
            this.lbInitial = new System.Windows.Forms.Label();
            this.lbResult = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbNumN = new System.Windows.Forms.TextBox();
            this.lbNumN = new System.Windows.Forms.Label();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lbCountP = new System.Windows.Forms.Label();
            this.lbCountQ = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbNumB
            // 
            this.tbNumB.Enabled = false;
            this.tbNumB.Location = new System.Drawing.Point(436, 244);
            this.tbNumB.Margin = new System.Windows.Forms.Padding(5);
            this.tbNumB.Name = "tbNumB";
            this.tbNumB.Size = new System.Drawing.Size(164, 29);
            this.tbNumB.TabIndex = 2;
            this.tbNumB.TextChanged += new System.EventHandler(this.tb_TextChanged);
            this.tbNumB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_NumChangeB);
            // 
            // tbNumP
            // 
            this.tbNumP.Location = new System.Drawing.Point(436, 55);
            this.tbNumP.Margin = new System.Windows.Forms.Padding(5);
            this.tbNumP.Name = "tbNumP";
            this.tbNumP.Size = new System.Drawing.Size(164, 29);
            this.tbNumP.TabIndex = 0;
            this.tbNumP.TextChanged += new System.EventHandler(this.tb_TextChanged);
            this.tbNumP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_NumChangePQ);
            this.tbNumP.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // tbNumQ
            // 
            this.tbNumQ.Location = new System.Drawing.Point(436, 117);
            this.tbNumQ.Margin = new System.Windows.Forms.Padding(5);
            this.tbNumQ.Name = "tbNumQ";
            this.tbNumQ.Size = new System.Drawing.Size(164, 29);
            this.tbNumQ.TabIndex = 1;
            this.tbNumQ.TextChanged += new System.EventHandler(this.tb_TextChanged);
            this.tbNumQ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_NumChangePQ);
            this.tbNumQ.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // btnExecute
            // 
            this.btnExecute.Enabled = false;
            this.btnExecute.Location = new System.Drawing.Point(436, 387);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(5);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(164, 37);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "Исполнить";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btn_Execute);
            // 
            // tbInitial
            // 
            this.tbInitial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbInitial.Location = new System.Drawing.Point(0, 30);
            this.tbInitial.Margin = new System.Windows.Forms.Padding(5);
            this.tbInitial.Multiline = true;
            this.tbInitial.Name = "tbInitial";
            this.tbInitial.ReadOnly = true;
            this.tbInitial.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInitial.Size = new System.Drawing.Size(421, 331);
            this.tbInitial.TabIndex = 4;
            this.tbInitial.TabStop = false;
            this.tbInitial.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // tbResult
            // 
            this.tbResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbResult.Location = new System.Drawing.Point(613, 30);
            this.tbResult.Margin = new System.Windows.Forms.Padding(5);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ReadOnly = true;
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbResult.Size = new System.Drawing.Size(421, 331);
            this.tbResult.TabIndex = 5;
            this.tbResult.TabStop = false;
            // 
            // lbNumP
            // 
            this.lbNumP.AutoSize = true;
            this.lbNumP.Location = new System.Drawing.Point(481, 33);
            this.lbNumP.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbNumP.Name = "lbNumP";
            this.lbNumP.Size = new System.Drawing.Size(76, 21);
            this.lbNumP.TabIndex = 6;
            this.lbNumP.Text = "Число P";
            // 
            // lbNumB
            // 
            this.lbNumB.AutoSize = true;
            this.lbNumB.Location = new System.Drawing.Point(479, 219);
            this.lbNumB.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbNumB.Name = "lbNumB";
            this.lbNumB.Size = new System.Drawing.Size(77, 21);
            this.lbNumB.TabIndex = 7;
            this.lbNumB.Text = "Число B";
            // 
            // lbNumQ
            // 
            this.lbNumQ.AutoSize = true;
            this.lbNumQ.Location = new System.Drawing.Point(479, 94);
            this.lbNumQ.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbNumQ.Name = "lbNumQ";
            this.lbNumQ.Size = new System.Drawing.Size(79, 21);
            this.lbNumQ.TabIndex = 8;
            this.lbNumQ.Text = "Число Q";
            // 
            // lbInitial
            // 
            this.lbInitial.AutoSize = true;
            this.lbInitial.Location = new System.Drawing.Point(128, 395);
            this.lbInitial.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbInitial.Name = "lbInitial";
            this.lbInitial.Size = new System.Drawing.Size(139, 21);
            this.lbInitial.TabIndex = 9;
            this.lbInitial.Text = "Исходный текст";
            // 
            // lbResult
            // 
            this.lbResult.AutoSize = true;
            this.lbResult.Location = new System.Drawing.Point(768, 395);
            this.lbResult.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(112, 21);
            this.lbResult.TabIndex = 10;
            this.lbResult.Text = "Шифротекст";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1034, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.загрузитьToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "Работа с файлом";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.saveFile_Click);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.openFile_Click);
            // 
            // tbNumN
            // 
            this.tbNumN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbNumN.Location = new System.Drawing.Point(436, 180);
            this.tbNumN.Margin = new System.Windows.Forms.Padding(5);
            this.tbNumN.Name = "tbNumN";
            this.tbNumN.ReadOnly = true;
            this.tbNumN.Size = new System.Drawing.Size(164, 29);
            this.tbNumN.TabIndex = 12;
            this.tbNumN.TabStop = false;
            // 
            // lbNumN
            // 
            this.lbNumN.AutoSize = true;
            this.lbNumN.Location = new System.Drawing.Point(480, 158);
            this.lbNumN.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbNumN.Name = "lbNumN";
            this.lbNumN.Size = new System.Drawing.Size(78, 21);
            this.lbNumN.TabIndex = 13;
            this.lbNumN.Text = "Число N";
            // 
            // comboBox
            // 
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "Зашифровать",
            "Расшифровать"});
            this.comboBox.Location = new System.Drawing.Point(436, 303);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(164, 29);
            this.comboBox.TabIndex = 14;
            this.comboBox.TabStop = false;
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.cb_ChangeIndex);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lbCountP
            // 
            this.lbCountP.AutoSize = true;
            this.lbCountP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCountP.Location = new System.Drawing.Point(561, 32);
            this.lbCountP.Name = "lbCountP";
            this.lbCountP.Size = new System.Drawing.Size(21, 23);
            this.lbCountP.TabIndex = 15;
            this.lbCountP.Text = "0";
            // 
            // lbCountQ
            // 
            this.lbCountQ.AutoSize = true;
            this.lbCountQ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCountQ.Location = new System.Drawing.Point(561, 94);
            this.lbCountQ.Name = "lbCountQ";
            this.lbCountQ.Size = new System.Drawing.Size(21, 23);
            this.lbCountQ.TabIndex = 16;
            this.lbCountQ.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 461);
            this.Controls.Add(this.lbCountQ);
            this.Controls.Add(this.lbCountP);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.lbNumN);
            this.Controls.Add(this.tbNumN);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.lbInitial);
            this.Controls.Add(this.lbNumQ);
            this.Controls.Add(this.lbNumB);
            this.Controls.Add(this.lbNumP);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.tbInitial);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.tbNumQ);
            this.Controls.Add(this.tbNumP);
            this.Controls.Add(this.tbNumB);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lab3";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNumB;
        private System.Windows.Forms.TextBox tbNumP;
        private System.Windows.Forms.TextBox tbNumQ;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.TextBox tbInitial;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Label lbNumP;
        private System.Windows.Forms.Label lbNumB;
        private System.Windows.Forms.Label lbNumQ;
        private System.Windows.Forms.Label lbInitial;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.TextBox tbNumN;
        private System.Windows.Forms.Label lbNumN;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lbCountP;
        private System.Windows.Forms.Label lbCountQ;
    }
}

