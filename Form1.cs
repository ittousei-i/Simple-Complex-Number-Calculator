namespace ComplexCalculator;

public partial class Form1 : Form
{
    /// <summary>当前获得焦点的输入框，计算器按钮将向其输入。</summary>
    private TextBox? _activeInput;

    /// <summary>历史记录最大条数。</summary>
    private const int MaxHistory = 50;

    public Form1()
    {
        InitializeComponent();
        CreateCalculatorButtons();
        _activeInput = txtConvertInput;
    }

    #region 计算器按钮

    /// <summary>
    /// 动态创建计算器按钮（已移除 ×、÷、±），占面板左侧约一半。
    /// </summary>
    private void CreateCalculatorButtons()
    {
        // 按钮定义：(行, 列, 文本, 跨列数)
        // 布局：
        //   7  8  9  ⌫  C
        //   4  5  6  +  −
        //   1  2  3  i  ∠
        //   [ 0   ]  .
        var buttons = new (int row, int col, string text, int span)[]
        {
            (0, 0, "7", 1), (0, 1, "8", 1), (0, 2, "9", 1), (0, 3, "⌫", 1), (0, 4, "C", 1),
            (1, 0, "4", 1), (1, 1, "5", 1), (1, 2, "6", 1), (1, 3, "+", 1), (1, 4, "−", 1),
            (2, 0, "1", 1), (2, 1, "2", 1), (2, 2, "3", 1), (2, 3, "i", 1), (2, 4, "∠", 1),
            (3, 0, "0", 2), (3, 2, ".", 1),
        };

        // 按钮整体放大 1.5 倍：42→63, 40→60, 间距同步放大
        // 5 列总宽 = 5×63 + 4×8 = 347；4 行总高 = 4×60 + 3×9 = 267
        // 起始 y=35，底部 y=302，与历史列表上下边对齐
        const int btnW = 63;
        const int btnH = 60;
        const int hGap = 8;
        const int vGap = 9;
        int startX = 15;
        int startY = 35;

        foreach (var (row, col, text, span) in buttons)
        {
            int width = span * btnW + (span - 1) * hGap;
            int x = startX + col * (btnW + hGap);
            int y = startY + row * (btnH + vGap);

            Button btn = new()
            {
                Text = text,
                Tag = text,
                Location = new Point(x, y),
                Size = new Size(width, btnH),
                Font = new Font("微软雅黑", 15F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;

            // 按功能着色
            if (text is "⌫" or "C")
            {
                btn.BackColor = Color.FromArgb(255, 152, 0);
                btn.ForeColor = Color.White;
            }
            else if (text is "+" or "−")
            {
                btn.BackColor = Color.FromArgb(52, 168, 83);
                btn.ForeColor = Color.White;
            }
            else if (text is "∠" or "i" or ".")
            {
                btn.BackColor = Color.FromArgb(230, 230, 230);
                btn.ForeColor = Color.Black;
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
            }

            btn.Click += CalcButton_Click;
            calcPanel.Controls.Add(btn);
        }
    }

    /// <summary>计算器按钮统一点击处理。</summary>
    private void CalcButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn) return;
        string tag = btn.Tag?.ToString() ?? "";

        switch (tag)
        {
            case "⌫":
                Backspace();
                return;
            case "C":
                ClearActive();
                return;
            case "+":
                InsertText("+");
                return;
            case "−":
                InsertText("-");
                return;
            default:
                InsertText(tag);
                return;
        }
    }

    /// <summary>向当前活跃输入框插入文本。</summary>
    private void InsertText(string text)
    {
        if (_activeInput == null)
        {
            MessageBox.Show("请先点击一个输入框。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        int pos = _activeInput.SelectionStart;
        _activeInput.Text = _activeInput.Text.Insert(pos, text);
        _activeInput.SelectionStart = pos + text.Length;
        _activeInput.Focus();
    }

    /// <summary>退格：删除当前活跃输入框光标前一个字符。</summary>
    private void Backspace()
    {
        if (_activeInput == null || _activeInput.Text.Length == 0) return;
        int pos = _activeInput.SelectionStart;
        if (pos > 0)
        {
            _activeInput.Text = _activeInput.Text.Remove(pos - 1, 1);
            _activeInput.SelectionStart = pos - 1;
        }
        _activeInput.Focus();
    }

    /// <summary>清空当前活跃输入框。</summary>
    private void ClearActive()
    {
        if (_activeInput == null) return;
        _activeInput.Text = "";
        _activeInput.Focus();
    }

    /// <summary>输入框获得焦点时记录为当前活跃输入框。</summary>
    private void Input_Enter(object? sender, EventArgs e)
    {
        if (sender is TextBox tb)
            _activeInput = tb;
    }

    /// <summary>切换 Tab 时更新默认活跃输入框。</summary>
    private void tabControl_SelectedIndexChanged(object? sender, EventArgs e)
    {
        _activeInput = tabControl.SelectedTab == tabConvert
            ? txtConvertInput
            : txtArith1;
    }

    #endregion

    #region 坐标转换

    private void btnConvert_Click(object? sender, EventArgs e)
    {
        string input = txtConvertInput.Text.Trim();

        if (!ValidateNonEmpty(input, "输入复数")) return;
        if (!ValidateComplex(input, "输入复数")) return;

        try
        {
            ComplexNumber num = ComplexNumber.Parse(input);
            bool isPolarInput = input.Contains('∠');

            string result = isPolarInput
                ? num.ToRectangularString()
                : num.ToPolarString();

            lblConvertOutput.Text = result;

            // 加入历史
            AddHistory($"{input}  →  {result}");
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
            lblConvertOutput.Text = "";
        }
    }

    #endregion

    #region 四则运算

    private void btnArithCalc_Click(object? sender, EventArgs e)
    {
        string input1 = txtArith1.Text.Trim();
        string input2 = txtArith2.Text.Trim();
        string op = cboArithOp.SelectedItem?.ToString() ?? "+";

        if (!ValidateNonEmpty(input1, "第一个复数")) return;
        if (!ValidateNonEmpty(input2, "第二个复数")) return;
        if (!ValidateComplex(input1, "第一个复数")) return;
        if (!ValidateComplex(input2, "第二个复数")) return;

        // 不允许混合坐标形式
        bool firstPolar = input1.Contains('∠');
        bool secondPolar = input2.Contains('∠');
        if (firstPolar != secondPolar)
        {
            ShowError("不支持直角坐标与极坐标形式混合运算！\n请确保两个复数使用同一种坐标形式。");
            return;
        }

        try
        {
            ComplexNumber num1 = ComplexNumber.Parse(input1);
            ComplexNumber num2 = ComplexNumber.Parse(input2);
            ComplexNumber result = op switch
            {
                "+" => num1 + num2,
                "−" => num1 - num2,
                "×" => num1 * num2,
                "÷" => num1 / num2,
                _ => num1 + num2
            };

            string output = firstPolar
                ? result.ToPolarString()
                : result.ToRectangularString();

            lblArithOutput.Text = output;

            // 加入历史
            AddHistory($"({input1}) {op} ({input2}) = {output}");
        }
        catch (DivideByZeroException ex)
        {
            ShowError(ex.Message);
            lblArithOutput.Text = "";
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
            lblArithOutput.Text = "";
        }
    }

    #endregion

    #region 计算历史

    /// <summary>向历史列表添加一条记录，超出上限则移除最旧的。</summary>
    private void AddHistory(string record)
    {
        lstHistory.Items.Insert(0, record);
        if (lstHistory.Items.Count > MaxHistory)
            lstHistory.Items.RemoveAt(lstHistory.Items.Count - 1);
    }

    /// <summary>清空历史记录。</summary>
    private void btnClearHistory_Click(object? sender, EventArgs e)
    {
        if (lstHistory.Items.Count == 0) return;
        if (MessageBox.Show("确定要清空所有计算历史吗？", "确认",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            lstHistory.Items.Clear();
        }
    }

    #endregion

    #region 复制结果

    private void btnCopyConvert_Click(object? sender, EventArgs e)
    {
        CopyResult(lblConvertOutput.Text);
    }

    private void btnCopyArith_Click(object? sender, EventArgs e)
    {
        CopyResult(lblArithOutput.Text);
    }

    /// <summary>将结果复制到剪贴板并给出提示。</summary>
    private static void CopyResult(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            MessageBox.Show("没有可复制的结果。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        Clipboard.SetText(text);
        MessageBox.Show($"已复制：{text}", "复制成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    #endregion

    #region 输入校验

    /// <summary>校验非空，失败弹出 MessageBox。</summary>
    private static bool ValidateNonEmpty(string input, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            MessageBox.Show(
                $"{fieldName}不能为空！",
                "输入错误",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

    /// <summary>校验复数格式，失败弹出 MessageBox 并给出正确示例。</summary>
    private static bool ValidateComplex(string input, string fieldName)
    {
        if (!ComplexNumber.TryParse(input, out _))
        {
            MessageBox.Show(
                $"{fieldName}格式不规范：\"{input}\"\n\n" +
                "正确格式示例：\n" +
                "  直角坐标：3+4i、-3-4i、3-i\n" +
                "  极坐标：5∠60、2.5∠-30\n\n" +
                "注意：数字最多允许两位小数。",
                "输入格式错误",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

    /// <summary>统一错误弹窗。</summary>
    private static void ShowError(string message)
    {
        MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    #endregion
}
