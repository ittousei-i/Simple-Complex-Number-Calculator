namespace ComplexCalculator;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        lblTitle = new Label();
        tabControl = new TabControl();
        tabConvert = new TabPage();
        lblConvertHint = new Label();
        btnCopyConvert = new Button();
        lblConvertOutput = new Label();
        lblConvertResult = new Label();
        btnConvert = new Button();
        txtConvertInput = new TextBox();
        lblConvertInput = new Label();
        tabArithmetic = new TabPage();
        lblArithHint = new Label();
        btnCopyArith = new Button();
        lblArithOutput = new Label();
        lblArithResult = new Label();
        btnArithCalc = new Button();
        txtArith2 = new TextBox();
        lblArith2 = new Label();
        cboArithOp = new ComboBox();
        txtArith1 = new TextBox();
        lblArith1 = new Label();
        calcPanel = new Panel();
        btnClearHistory = new Button();
        lstHistory = new ListBox();
        lblHistoryTitle = new Label();
        lblCalcTitle = new Label();
        lblArithOp = new Label();
        tabControl.SuspendLayout();
        tabConvert.SuspendLayout();
        tabArithmetic.SuspendLayout();
        calcPanel.SuspendLayout();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.Font = new Font("微软雅黑", 16F, FontStyle.Bold);
        lblTitle.Location = new Point(0, 14);
        lblTitle.Margin = new Padding(5, 0, 5, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(974, 51);
        lblTitle.TabIndex = 2;
        lblTitle.Text = "简易复数计算器";
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // tabControl
        // 
        tabControl.Controls.Add(tabConvert);
        tabControl.Controls.Add(tabArithmetic);
        tabControl.Font = new Font("微软雅黑", 9F);
        tabControl.Location = new Point(19, 73);
        tabControl.Margin = new Padding(5, 4, 5, 4);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(937, 438);
        tabControl.TabIndex = 1;
        tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
        // 
        // tabConvert
        // 
        tabConvert.Controls.Add(lblConvertHint);
        tabConvert.Controls.Add(btnCopyConvert);
        tabConvert.Controls.Add(lblConvertOutput);
        tabConvert.Controls.Add(lblConvertResult);
        tabConvert.Controls.Add(btnConvert);
        tabConvert.Controls.Add(txtConvertInput);
        tabConvert.Controls.Add(lblConvertInput);
        tabConvert.Location = new Point(4, 33);
        tabConvert.Margin = new Padding(5, 4, 5, 4);
        tabConvert.Name = "tabConvert";
        tabConvert.Padding = new Padding(16, 14, 16, 14);
        tabConvert.Size = new Size(929, 401);
        tabConvert.TabIndex = 0;
        tabConvert.Text = "坐标转换";
        // 
        // lblConvertHint
        // 
        lblConvertHint.Font = new Font("微软雅黑", 9F);
        lblConvertHint.ForeColor = Color.Gray;
        lblConvertHint.Location = new Point(31, 233);
        lblConvertHint.Margin = new Padding(5, 0, 5, 0);
        lblConvertHint.Name = "lblConvertHint";
        lblConvertHint.Size = new Size(691, 56);
        lblConvertHint.TabIndex = 0;
        lblConvertHint.Text = "提示：输入直角坐标(如 3+4i)自动转极坐标；输入极坐标(如 5∠60)自动转直角坐标";
        // 
        // btnCopyConvert
        // 
        btnCopyConvert.BackColor = Color.White;
        btnCopyConvert.Cursor = Cursors.Hand;
        btnCopyConvert.FlatStyle = FlatStyle.Flat;
        btnCopyConvert.Font = new Font("微软雅黑", 9F);
        btnCopyConvert.Location = new Point(647, 165);
        btnCopyConvert.Margin = new Padding(5, 4, 5, 4);
        btnCopyConvert.Name = "btnCopyConvert";
        btnCopyConvert.Size = new Size(60, 38);
        btnCopyConvert.TabIndex = 1;
        btnCopyConvert.Text = "复制";
        btnCopyConvert.UseVisualStyleBackColor = false;
        btnCopyConvert.Click += btnCopyConvert_Click;
        // 
        // lblConvertOutput
        // 
        lblConvertOutput.BackColor = Color.FromArgb(245, 245, 245);
        lblConvertOutput.BorderStyle = BorderStyle.FixedSingle;
        lblConvertOutput.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
        lblConvertOutput.ForeColor = Color.FromArgb(66, 133, 244);
        lblConvertOutput.Location = new Point(204, 165);
        lblConvertOutput.Margin = new Padding(5, 0, 5, 0);
        lblConvertOutput.Name = "lblConvertOutput";
        lblConvertOutput.Padding = new Padding(6, 0, 0, 0);
        lblConvertOutput.Size = new Size(436, 37);
        lblConvertOutput.TabIndex = 2;
        lblConvertOutput.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblConvertResult
        // 
        lblConvertResult.Font = new Font("微软雅黑", 10F);
        lblConvertResult.Location = new Point(31, 169);
        lblConvertResult.Margin = new Padding(5, 0, 5, 0);
        lblConvertResult.Name = "lblConvertResult";
        lblConvertResult.Size = new Size(157, 34);
        lblConvertResult.TabIndex = 3;
        lblConvertResult.Text = "转换结果：";
        // 
        // btnConvert
        // 
        btnConvert.BackColor = Color.FromArgb(66, 133, 244);
        btnConvert.Cursor = Cursors.Hand;
        btnConvert.FlatAppearance.BorderSize = 0;
        btnConvert.FlatStyle = FlatStyle.Flat;
        btnConvert.Font = new Font("微软雅黑", 11F, FontStyle.Bold);
        btnConvert.ForeColor = Color.White;
        btnConvert.Location = new Point(283, 92);
        btnConvert.Margin = new Padding(5, 4, 5, 4);
        btnConvert.Name = "btnConvert";
        btnConvert.Size = new Size(189, 51);
        btnConvert.TabIndex = 4;
        btnConvert.Text = "转 换";
        btnConvert.UseVisualStyleBackColor = false;
        btnConvert.Click += btnConvert_Click;
        // 
        // txtConvertInput
        // 
        txtConvertInput.Font = new Font("微软雅黑", 11F);
        txtConvertInput.Location = new Point(204, 31);
        txtConvertInput.Margin = new Padding(5, 4, 5, 4);
        txtConvertInput.Name = "txtConvertInput";
        txtConvertInput.PlaceholderText = "例如：3+4i 或 5∠60";
        txtConvertInput.Size = new Size(501, 37);
        txtConvertInput.TabIndex = 5;
        txtConvertInput.Enter += Input_Enter;
        // 
        // lblConvertInput
        // 
        lblConvertInput.Font = new Font("微软雅黑", 10F);
        lblConvertInput.Location = new Point(31, 35);
        lblConvertInput.Margin = new Padding(5, 0, 5, 0);
        lblConvertInput.Name = "lblConvertInput";
        lblConvertInput.Size = new Size(157, 34);
        lblConvertInput.TabIndex = 6;
        lblConvertInput.Text = "输入复数：";
        // 
        // tabArithmetic
        // 
        tabArithmetic.Controls.Add(lblArithHint);
        tabArithmetic.Controls.Add(btnCopyArith);
        tabArithmetic.Controls.Add(lblArithOutput);
        tabArithmetic.Controls.Add(lblArithResult);
        tabArithmetic.Controls.Add(btnArithCalc);
        tabArithmetic.Controls.Add(txtArith2);
        tabArithmetic.Controls.Add(lblArith2);
        tabArithmetic.Controls.Add(cboArithOp);
        tabArithmetic.Controls.Add(txtArith1);
        tabArithmetic.Controls.Add(lblArith1);
        tabArithmetic.Location = new Point(4, 33);
        tabArithmetic.Margin = new Padding(5, 4, 5, 4);
        tabArithmetic.Name = "tabArithmetic";
        tabArithmetic.Padding = new Padding(16, 14, 16, 14);
        tabArithmetic.Size = new Size(929, 401);
        tabArithmetic.TabIndex = 1;
        tabArithmetic.Text = "四则运算";
        // 
        // lblArithHint
        // 
        lblArithHint.Font = new Font("微软雅黑", 9F);
        lblArithHint.ForeColor = Color.Gray;
        lblArithHint.Location = new Point(31, 311);
        lblArithHint.Margin = new Padding(5, 0, 5, 0);
        lblArithHint.Name = "lblArithHint";
        lblArithHint.Size = new Size(691, 42);
        lblArithHint.TabIndex = 0;
        lblArithHint.Text = "提示：两个复数必须同为直角坐标或同为极坐标；可用下方计算器按钮输入";
        // 
        // btnCopyArith
        // 
        btnCopyArith.BackColor = Color.White;
        btnCopyArith.Cursor = Cursors.Hand;
        btnCopyArith.FlatStyle = FlatStyle.Flat;
        btnCopyArith.Font = new Font("微软雅黑", 9F);
        btnCopyArith.Location = new Point(647, 247);
        btnCopyArith.Margin = new Padding(5, 4, 5, 4);
        btnCopyArith.Name = "btnCopyArith";
        btnCopyArith.Size = new Size(60, 38);
        btnCopyArith.TabIndex = 1;
        btnCopyArith.Text = "复制";
        btnCopyArith.UseVisualStyleBackColor = false;
        btnCopyArith.Click += btnCopyArith_Click;
        // 
        // lblArithOutput
        // 
        lblArithOutput.BackColor = Color.FromArgb(245, 245, 245);
        lblArithOutput.BorderStyle = BorderStyle.FixedSingle;
        lblArithOutput.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
        lblArithOutput.ForeColor = Color.FromArgb(52, 168, 83);
        lblArithOutput.Location = new Point(204, 247);
        lblArithOutput.Margin = new Padding(5, 0, 5, 0);
        lblArithOutput.Name = "lblArithOutput";
        lblArithOutput.Padding = new Padding(6, 0, 0, 0);
        lblArithOutput.Size = new Size(436, 37);
        lblArithOutput.TabIndex = 2;
        lblArithOutput.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblArithResult
        // 
        lblArithResult.Font = new Font("微软雅黑", 10F);
        lblArithResult.Location = new Point(31, 251);
        lblArithResult.Margin = new Padding(5, 0, 5, 0);
        lblArithResult.Name = "lblArithResult";
        lblArithResult.Size = new Size(157, 34);
        lblArithResult.TabIndex = 3;
        lblArithResult.Text = "计算结果：";
        // 
        // btnArithCalc
        // 
        btnArithCalc.BackColor = Color.FromArgb(52, 168, 83);
        btnArithCalc.Cursor = Cursors.Hand;
        btnArithCalc.FlatAppearance.BorderSize = 0;
        btnArithCalc.FlatStyle = FlatStyle.Flat;
        btnArithCalc.Font = new Font("微软雅黑", 14F, FontStyle.Bold);
        btnArithCalc.ForeColor = Color.White;
        btnArithCalc.Location = new Point(283, 172);
        btnArithCalc.Margin = new Padding(5, 4, 5, 4);
        btnArithCalc.Name = "btnArithCalc";
        btnArithCalc.Size = new Size(189, 51);
        btnArithCalc.TabIndex = 4;
        btnArithCalc.Text = "=";
        btnArithCalc.UseVisualStyleBackColor = false;
        btnArithCalc.Click += btnArithCalc_Click;
        // 
        // txtArith2
        // 
        txtArith2.Font = new Font("微软雅黑", 11F);
        txtArith2.Location = new Point(204, 116);
        txtArith2.Margin = new Padding(5, 4, 5, 4);
        txtArith2.Name = "txtArith2";
        txtArith2.PlaceholderText = "例如：1+2i 或 2∠30";
        txtArith2.Size = new Size(501, 37);
        txtArith2.TabIndex = 5;
        txtArith2.Enter += Input_Enter;
        // 
        // lblArith2
        // 
        lblArith2.Font = new Font("微软雅黑", 10F);
        lblArith2.Location = new Point(31, 120);
        lblArith2.Margin = new Padding(5, 0, 5, 0);
        lblArith2.Name = "lblArith2";
        lblArith2.Size = new Size(157, 34);
        lblArith2.TabIndex = 6;
        lblArith2.Text = "第二个复数：";
        // 
        // cboArithOp
        // 
        cboArithOp.DropDownStyle = ComboBoxStyle.DropDownList;
        cboArithOp.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
        cboArithOp.Items.AddRange(new object[] { "+", "−", "×", "÷" });
        cboArithOp.Location = new Point(204, 66);
        cboArithOp.Margin = new Padding(5, 4, 5, 4);
        cboArithOp.Name = "cboArithOp";
        cboArithOp.Size = new Size(123, 39);
        cboArithOp.TabIndex = 7;
        // 
        // txtArith1
        // 
        txtArith1.Font = new Font("微软雅黑", 11F);
        txtArith1.Location = new Point(204, 17);
        txtArith1.Margin = new Padding(5, 4, 5, 4);
        txtArith1.Name = "txtArith1";
        txtArith1.PlaceholderText = "例如：3+4i 或 5∠60";
        txtArith1.Size = new Size(501, 37);
        txtArith1.TabIndex = 8;
        txtArith1.Enter += Input_Enter;
        // 
        // lblArith1
        // 
        lblArith1.Font = new Font("微软雅黑", 10F);
        lblArith1.Location = new Point(31, 21);
        lblArith1.Margin = new Padding(5, 0, 5, 0);
        lblArith1.Name = "lblArith1";
        lblArith1.Size = new Size(157, 34);
        lblArith1.TabIndex = 9;
        lblArith1.Text = "第一个复数：";
        // 
        // calcPanel
        // 
        calcPanel.BackColor = Color.FromArgb(248, 248, 248);
        calcPanel.BorderStyle = BorderStyle.FixedSingle;
        calcPanel.Controls.Add(btnClearHistory);
        calcPanel.Controls.Add(lstHistory);
        calcPanel.Controls.Add(lblHistoryTitle);
        calcPanel.Controls.Add(lblCalcTitle);
        calcPanel.Location = new Point(19, 525);
        calcPanel.Margin = new Padding(5, 4, 5, 4);
        calcPanel.Name = "calcPanel";
        calcPanel.Size = new Size(935, 367);
        calcPanel.TabIndex = 0;
        // 
        // btnClearHistory
        // 
        btnClearHistory.BackColor = Color.White;
        btnClearHistory.Cursor = Cursors.Hand;
        btnClearHistory.FlatStyle = FlatStyle.Flat;
        btnClearHistory.Font = new Font("微软雅黑", 9F);
        btnClearHistory.Location = new Point(784, 314);
        btnClearHistory.Margin = new Padding(5, 4, 5, 4);
        btnClearHistory.Name = "btnClearHistory";
        btnClearHistory.Size = new Size(126, 37);
        btnClearHistory.TabIndex = 0;
        btnClearHistory.Text = "清空历史";
        btnClearHistory.UseVisualStyleBackColor = false;
        btnClearHistory.Click += btnClearHistory_Click;
        // 
        // lstHistory
        // 
        lstHistory.BackColor = Color.White;
        lstHistory.BorderStyle = BorderStyle.FixedSingle;
        lstHistory.Font = new Font("微软雅黑", 9F);
        lstHistory.IntegralHeight = false;
        lstHistory.Location = new Point(436, 49);
        lstHistory.Margin = new Padding(5, 4, 5, 4);
        lstHistory.Name = "lstHistory";
        lstHistory.Size = new Size(474, 257);
        lstHistory.TabIndex = 1;
        // 
        // lblHistoryTitle
        // 
        lblHistoryTitle.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
        lblHistoryTitle.ForeColor = Color.Gray;
        lblHistoryTitle.Location = new Point(436, 11);
        lblHistoryTitle.Margin = new Padding(5, 0, 5, 0);
        lblHistoryTitle.Name = "lblHistoryTitle";
        lblHistoryTitle.Size = new Size(157, 28);
        lblHistoryTitle.TabIndex = 2;
        lblHistoryTitle.Text = "计算历史";
        // 
        // lblCalcTitle
        // 
        lblCalcTitle.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
        lblCalcTitle.ForeColor = Color.Gray;
        lblCalcTitle.Location = new Point(24, 11);
        lblCalcTitle.Margin = new Padding(5, 0, 5, 0);
        lblCalcTitle.Name = "lblCalcTitle";
        lblCalcTitle.Size = new Size(157, 28);
        lblCalcTitle.TabIndex = 3;
        lblCalcTitle.Text = "计算器键盘";
        // 
        // lblArithOp
        // 
        lblArithOp.Font = new Font("微软雅黑", 10F);
        lblArithOp.Location = new Point(20, 50);
        lblArithOp.Name = "lblArithOp";
        lblArithOp.Size = new Size(100, 24);
        lblArithOp.TabIndex = 0;
        lblArithOp.Text = "运算符：";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(974, 910);
        Controls.Add(calcPanel);
        Controls.Add(tabControl);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Margin = new Padding(5, 4, 5, 4);
        MaximizeBox = false;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "简易复数计算器";
        tabControl.ResumeLayout(false);
        tabConvert.ResumeLayout(false);
        tabConvert.PerformLayout();
        tabArithmetic.ResumeLayout(false);
        tabArithmetic.PerformLayout();
        calcPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    // ===== 字段声明 =====
    private Label lblTitle;
    private TabControl tabControl;
    private TabPage tabConvert;
    private TabPage tabArithmetic;
    private Panel calcPanel;

    // 坐标转换
    private Label lblConvertInput;
    private TextBox txtConvertInput;
    private Button btnConvert;
    private Label lblConvertResult;
    private Label lblConvertOutput;
    private Button btnCopyConvert;
    private Label lblConvertHint;

    // 四则运算
    private Label lblArith1;
    private TextBox txtArith1;
    private Label lblArithOp;
    private ComboBox cboArithOp;
    private Label lblArith2;
    private TextBox txtArith2;
    private Button btnArithCalc;
    private Label lblArithResult;
    private Label lblArithOutput;
    private Button btnCopyArith;
    private Label lblArithHint;

    // 计算器面板 / 历史
    private Label lblCalcTitle;
    private Label lblHistoryTitle;
    private ListBox lstHistory;
    private Button btnClearHistory;
}
