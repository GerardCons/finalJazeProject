namespace CTHardware_EmployeeManagement
{
    partial class updateAttendance
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
            this.label9 = new System.Windows.Forms.Label();
            this.rtbt_comment = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_status = new System.Windows.Forms.ComboBox();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtp_timeOut = new System.Windows.Forms.DateTimePicker();
            this.dtp_timeIn = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbt_employeeId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbt_position = new System.Windows.Forms.TextBox();
            this.tbt_lastName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tbt_firstName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.lbl_timeIn = new System.Windows.Forms.Label();
            this.lbl_timeOut = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(283, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 44);
            this.label1.TabIndex = 167;
            this.label1.Text = "Attendance";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(74, 386);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 18);
            this.label9.TabIndex = 186;
            this.label9.Text = "Comment:";
            // 
            // rtbt_comment
            // 
            this.rtbt_comment.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbt_comment.Location = new System.Drawing.Point(166, 386);
            this.rtbt_comment.Name = "rtbt_comment";
            this.rtbt_comment.Size = new System.Drawing.Size(200, 96);
            this.rtbt_comment.TabIndex = 185;
            this.rtbt_comment.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(98, 329);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 18);
            this.label8.TabIndex = 184;
            this.label8.Text = "Status:";
            // 
            // cb_status
            // 
            this.cb_status.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cb_status.FormattingEnabled = true;
            this.cb_status.Items.AddRange(new object[] {
            "Late",
            "Present",
            "Absent",
            "Leave"});
            this.cb_status.Location = new System.Drawing.Point(166, 327);
            this.cb_status.Name = "cb_status";
            this.cb_status.Size = new System.Drawing.Size(200, 25);
            this.cb_status.TabIndex = 183;
            // 
            // dtp_date
            // 
            this.dtp_date.Enabled = false;
            this.dtp_date.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtp_date.Location = new System.Drawing.Point(166, 204);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(200, 25);
            this.dtp_date.TabIndex = 182;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(101)))), ((int)(((byte)(51)))));
            this.button3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(520, 422);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(114, 44);
            this.button3.TabIndex = 181;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(101)))), ((int)(((byte)(51)))));
            this.button4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(520, 360);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(114, 44);
            this.button4.TabIndex = 180;
            this.button4.Text = "Update";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(101)))), ((int)(((byte)(51)))));
            this.panel2.Controls.Add(this.dtp_timeOut);
            this.panel2.Controls.Add(this.dtp_timeIn);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(57, 248);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(651, 47);
            this.panel2.TabIndex = 179;
            // 
            // dtp_timeOut
            // 
            this.dtp_timeOut.CalendarFont = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtp_timeOut.Location = new System.Drawing.Point(443, 10);
            this.dtp_timeOut.Name = "dtp_timeOut";
            this.dtp_timeOut.ShowUpDown = true;
            this.dtp_timeOut.Size = new System.Drawing.Size(200, 23);
            this.dtp_timeOut.TabIndex = 167;
            // 
            // dtp_timeIn
            // 
            this.dtp_timeIn.CalendarFont = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtp_timeIn.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtp_timeIn.Location = new System.Drawing.Point(109, 11);
            this.dtp_timeIn.Name = "dtp_timeIn";
            this.dtp_timeIn.ShowUpDown = true;
            this.dtp_timeIn.Size = new System.Drawing.Size(200, 23);
            this.dtp_timeIn.TabIndex = 119;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(341, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 19);
            this.label6.TabIndex = 117;
            this.label6.Text = "Time Out";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(33, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 19);
            this.label7.TabIndex = 6;
            this.label7.Text = "Time In";
            // 
            // tbt_employeeId
            // 
            this.tbt_employeeId.Enabled = false;
            this.tbt_employeeId.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbt_employeeId.Location = new System.Drawing.Point(166, 134);
            this.tbt_employeeId.Name = "tbt_employeeId";
            this.tbt_employeeId.Size = new System.Drawing.Size(200, 25);
            this.tbt_employeeId.TabIndex = 177;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(57, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 18);
            this.label5.TabIndex = 176;
            this.label5.Text = "Employee ID:";
            // 
            // tbt_position
            // 
            this.tbt_position.Enabled = false;
            this.tbt_position.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbt_position.Location = new System.Drawing.Point(500, 204);
            this.tbt_position.Name = "tbt_position";
            this.tbt_position.Size = new System.Drawing.Size(200, 25);
            this.tbt_position.TabIndex = 175;
            // 
            // tbt_lastName
            // 
            this.tbt_lastName.Enabled = false;
            this.tbt_lastName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbt_lastName.Location = new System.Drawing.Point(500, 165);
            this.tbt_lastName.Name = "tbt_lastName";
            this.tbt_lastName.Size = new System.Drawing.Size(200, 25);
            this.tbt_lastName.TabIndex = 174;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(406, 167);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(88, 18);
            this.label15.TabIndex = 173;
            this.label15.Text = "Last Name:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label16.Location = new System.Drawing.Point(425, 204);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(69, 18);
            this.label16.TabIndex = 172;
            this.label16.Text = "Position:";
            // 
            // tbt_firstName
            // 
            this.tbt_firstName.Enabled = false;
            this.tbt_firstName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbt_firstName.Location = new System.Drawing.Point(166, 167);
            this.tbt_firstName.Name = "tbt_firstName";
            this.tbt_firstName.Size = new System.Drawing.Size(200, 25);
            this.tbt_firstName.TabIndex = 171;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(72, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 18);
            this.label4.TabIndex = 170;
            this.label4.Text = "First Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(108, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 18);
            this.label3.TabIndex = 169;
            this.label3.Text = "Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(57, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 22);
            this.label2.TabIndex = 168;
            this.label2.Text = "Personal Details";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(101)))), ((int)(((byte)(51)))));
            this.panel1.Controls.Add(this.label10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(764, 74);
            this.panel1.TabIndex = 187;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(226, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(359, 44);
            this.label10.TabIndex = 0;
            this.label10.Text = "Update Attendance";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(438, 91);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(0, 15);
            this.lblId.TabIndex = 188;
            this.lblId.Visible = false;
            // 
            // lbl_timeIn
            // 
            this.lbl_timeIn.AutoSize = true;
            this.lbl_timeIn.Location = new System.Drawing.Point(406, 134);
            this.lbl_timeIn.Name = "lbl_timeIn";
            this.lbl_timeIn.Size = new System.Drawing.Size(0, 15);
            this.lbl_timeIn.TabIndex = 189;
            this.lbl_timeIn.Visible = false;
            // 
            // lbl_timeOut
            // 
            this.lbl_timeOut.AutoSize = true;
            this.lbl_timeOut.Location = new System.Drawing.Point(549, 134);
            this.lbl_timeOut.Name = "lbl_timeOut";
            this.lbl_timeOut.Size = new System.Drawing.Size(0, 15);
            this.lbl_timeOut.TabIndex = 190;
            this.lbl_timeOut.Visible = false;
            // 
            // updateAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 504);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_timeOut);
            this.Controls.Add(this.lbl_timeIn);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.rtbt_comment);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cb_status);
            this.Controls.Add(this.dtp_date);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tbt_employeeId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbt_position);
            this.Controls.Add(this.tbt_lastName);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.tbt_firstName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "updateAttendance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.updateAttendance_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label9;
        private Label label8;
        private Button button3;
        private Button button4;
        private Panel panel2;
        private Label label6;
        private Label label7;
        private Label label5;
        private Label label15;
        private Label label16;
        private Label label4;
        private Label label3;
        private Label label2;
        private Panel panel1;
        private Label label10;
        public DateTimePicker dtp_date;
        public TextBox tbt_employeeId;
        public TextBox tbt_position;
        public TextBox tbt_lastName;
        public TextBox tbt_firstName;
        public Label lblId;
        public RichTextBox rtbt_comment;
        public ComboBox cb_status;
        public DateTimePicker dtp_timeOut;
        public DateTimePicker dtp_timeIn;
        public Label lbl_timeIn;
        public Label lbl_timeOut;
    }
}