namespace TesteDaMariana.WinAPP.ModuloTeste
{
    partial class TelaCriacaoTesteForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxQuestoes = new System.Windows.Forms.ListBox();
            this.textBoxTitulo = new System.Windows.Forms.TextBox();
            this.comboBoxDisciplina = new System.Windows.Forms.ComboBox();
            this.comboBoxMateria = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxProvão = new System.Windows.Forms.CheckBox();
            this.buttonSortearQuestoes = new System.Windows.Forms.Button();
            this.buttonGravar = new System.Windows.Forms.Button();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.txtData = new System.Windows.Forms.DateTimePicker();
            this.maskedTextBoxQuestoes = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 0;
            this.label1.Tag = " ";
            this.label1.Text = "Titulo :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 1;
            this.label2.Tag = " ";
            this.label2.Text = "Disciplina :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 2;
            this.label3.Tag = " ";
            this.label3.Text = "Materia :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 40);
            this.label4.TabIndex = 3;
            this.label4.Tag = " ";
            this.label4.Text = "Quantidade \r\nde questões :";
            // 
            // listBoxQuestoes
            // 
            this.listBoxQuestoes.FormattingEnabled = true;
            this.listBoxQuestoes.ItemHeight = 20;
            this.listBoxQuestoes.Location = new System.Drawing.Point(12, 255);
            this.listBoxQuestoes.Name = "listBoxQuestoes";
            this.listBoxQuestoes.Size = new System.Drawing.Size(396, 124);
            this.listBoxQuestoes.TabIndex = 4;
            // 
            // textBoxTitulo
            // 
            this.textBoxTitulo.Location = new System.Drawing.Point(72, 31);
            this.textBoxTitulo.Name = "textBoxTitulo";
            this.textBoxTitulo.Size = new System.Drawing.Size(178, 27);
            this.textBoxTitulo.TabIndex = 5;
            // 
            // comboBoxDisciplina
            // 
            this.comboBoxDisciplina.FormattingEnabled = true;
            this.comboBoxDisciplina.Location = new System.Drawing.Point(99, 84);
            this.comboBoxDisciplina.Name = "comboBoxDisciplina";
            this.comboBoxDisciplina.Size = new System.Drawing.Size(151, 28);
            this.comboBoxDisciplina.TabIndex = 6;
            this.comboBoxDisciplina.SelectedIndexChanged += new System.EventHandler(this.comboBoxDisciplina_SelectedIndexChanged);
            // 
            // comboBoxMateria
            // 
            this.comboBoxMateria.FormattingEnabled = true;
            this.comboBoxMateria.Location = new System.Drawing.Point(99, 140);
            this.comboBoxMateria.Name = "comboBoxMateria";
            this.comboBoxMateria.Size = new System.Drawing.Size(151, 28);
            this.comboBoxMateria.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(273, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 10;
            this.label5.Tag = " ";
            this.label5.Text = "Data :";
            // 
            // checkBoxProvão
            // 
            this.checkBoxProvão.AutoSize = true;
            this.checkBoxProvão.Location = new System.Drawing.Point(273, 139);
            this.checkBoxProvão.Name = "checkBoxProvão";
            this.checkBoxProvão.Size = new System.Drawing.Size(84, 24);
            this.checkBoxProvão.TabIndex = 11;
            this.checkBoxProvão.Text = "Provão?";
            this.checkBoxProvão.UseVisualStyleBackColor = true;
            // 
            // buttonSortearQuestoes
            // 
            this.buttonSortearQuestoes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonSortearQuestoes.Location = new System.Drawing.Point(12, 398);
            this.buttonSortearQuestoes.Name = "buttonSortearQuestoes";
            this.buttonSortearQuestoes.Size = new System.Drawing.Size(138, 40);
            this.buttonSortearQuestoes.TabIndex = 12;
            this.buttonSortearQuestoes.Text = "Sortear Questões";
            this.buttonSortearQuestoes.UseVisualStyleBackColor = true;
            this.buttonSortearQuestoes.Click += new System.EventHandler(this.buttonSortearQuestoes_Click);
            // 
            // buttonGravar
            // 
            this.buttonGravar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonGravar.Location = new System.Drawing.Point(154, 398);
            this.buttonGravar.Name = "buttonGravar";
            this.buttonGravar.Size = new System.Drawing.Size(138, 40);
            this.buttonGravar.TabIndex = 13;
            this.buttonGravar.Text = "Gravar";
            this.buttonGravar.UseVisualStyleBackColor = true;
            this.buttonGravar.Click += new System.EventHandler(this.buttonGravar_Click);
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancelar.Location = new System.Drawing.Point(296, 398);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(138, 40);
            this.buttonCancelar.TabIndex = 14;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(327, 84);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(107, 27);
            this.txtData.TabIndex = 15;
            // 
            // maskedTextBoxQuestoes
            // 
            this.maskedTextBoxQuestoes.Location = new System.Drawing.Point(125, 199);
            this.maskedTextBoxQuestoes.Mask = "00000";
            this.maskedTextBoxQuestoes.Name = "maskedTextBoxQuestoes";
            this.maskedTextBoxQuestoes.Size = new System.Drawing.Size(125, 27);
            this.maskedTextBoxQuestoes.TabIndex = 16;
            this.maskedTextBoxQuestoes.ValidatingType = typeof(int);
            // 
            // TelaCriacaoTesteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 450);
            this.Controls.Add(this.maskedTextBoxQuestoes);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonGravar);
            this.Controls.Add(this.buttonSortearQuestoes);
            this.Controls.Add(this.checkBoxProvão);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxMateria);
            this.Controls.Add(this.comboBoxDisciplina);
            this.Controls.Add(this.textBoxTitulo);
            this.Controls.Add(this.listBoxQuestoes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TelaCriacaoTesteForm";
            this.ShowIcon = false;
            this.Text = "Criação De Teste";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBoxQuestoes;
        private System.Windows.Forms.TextBox textBoxTitulo;
        private System.Windows.Forms.ComboBox comboBoxDisciplina;
        private System.Windows.Forms.ComboBox comboBoxMateria;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxProvão;
        private System.Windows.Forms.Button buttonSortearQuestoes;
        private System.Windows.Forms.Button buttonGravar;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.DateTimePicker txtData;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxQuestoes;
    }
}