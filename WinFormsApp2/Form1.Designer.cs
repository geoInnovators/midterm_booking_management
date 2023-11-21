namespace WinFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            Init = new Button();
            textBox3 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            listBox1 = new ListBox();
            Cancelorder = new Button();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Location = new Point(33, 34);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(2262, 1432);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(2344, 34);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(250, 47);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(2344, 119);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(250, 47);
            textBox2.TabIndex = 2;
            // 
            // Init
            // 
            Init.Location = new Point(2614, 23);
            Init.Name = "Init";
            Init.Size = new Size(188, 58);
            Init.TabIndex = 3;
            Init.Text = "Init";
            Init.UseVisualStyleBackColor = true;
            Init.Click += Init_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(2346, 259);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(250, 47);
            textBox3.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(2628, 247);
            button1.Name = "button1";
            button1.Size = new Size(138, 80);
            button1.TabIndex = 5;
            button1.Text = "Scatter";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(2792, 247);
            button2.Name = "button2";
            button2.Size = new Size(138, 84);
            button2.TabIndex = 6;
            button2.Text = "Gather";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 41;
            listBox1.Location = new Point(2315, 566);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(300, 373);
            listBox1.TabIndex = 7;
            // 
            // Cancelorder
            // 
            Cancelorder.Location = new Point(2655, 567);
            Cancelorder.Name = "Cancelorder";
            Cancelorder.Size = new Size(188, 58);
            Cancelorder.TabIndex = 8;
            Cancelorder.Text = "Cancel";
            Cancelorder.UseVisualStyleBackColor = true;
            Cancelorder.Click += Cancelorder_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(3062, 1504);
            Controls.Add(Cancelorder);
            Controls.Add(listBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox3);
            Controls.Add(Init);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button Init;
        private TextBox textBox3;
        private Button button1;
        private Button button2;
        private ListBox listBox1;
        private Button Cancelorder;
    }
}