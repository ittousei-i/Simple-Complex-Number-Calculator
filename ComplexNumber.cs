using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ComplexCalculator;

/// <summary>
/// 复数类，支持直角坐标(a+bi)和极坐标(r∠θ)两种表示，
/// 提供解析、坐标转换和四则运算功能。
/// </summary>
public class ComplexNumber
{
    private const double PI = Math.PI;
    private const double EPS = 1e-9;

    /// <summary>实部（直角坐标）</summary>
    public double Real { get; private set; }

    /// <summary>虚部（直角坐标）</summary>
    public double Imag { get; private set; }

    /// <summary>模（极坐标）</summary>
    public double Magnitude => Math.Sqrt(Real * Real + Imag * Imag);

    /// <summary>辐角（极坐标，角度制，范围 -180 &lt; θ ≤ 180）</summary>
    public double Angle
    {
        get
        {
            if (Math.Abs(Real) < EPS && Math.Abs(Imag) < EPS)
                return 0;

            double theta;
            if (Math.Abs(Real) < EPS)
            {
                theta = Imag > 0 ? 90.0 : -90.0;
            }
            else if (Real > 0)
            {
                theta = Math.Atan(Imag / Real) * 180.0 / PI;
            }
            else
            {
                theta = Math.Atan(Imag / Real) * 180.0 / PI;
                if (Imag > 0)
                    theta += 180.0;
                else
                    theta -= 180.0;
            }

            if (Math.Abs(theta) < EPS)
                theta = 0;
            return theta;
        }
    }

    private ComplexNumber(double real, double imag)
    {
        Real = Math.Abs(real) < EPS ? 0 : real;
        Imag = Math.Abs(imag) < EPS ? 0 : imag;
    }

    #region 解析

    /// <summary>
    /// 自动识别输入格式并解析复数。
    /// 包含 ∠ 视为极坐标，否则视为直角坐标。
    /// </summary>
    public static ComplexNumber Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("输入不能为空。");

        input = input.Trim();

        if (input.Contains('∠'))
            return ParsePolar(input);
        return ParseRectangular(input);
    }

    /// <summary>
    /// 尝试解析，失败返回 false。
    /// </summary>
    public static bool TryParse(string input, out ComplexNumber? result)
    {
        try
        {
            result = Parse(input);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }

    /// <summary>
    /// 解析直角坐标，例如 3+4i、3-4i、-3+4i、3+i、3-i
    /// </summary>
    public static ComplexNumber ParseRectangular(string input)
    {
        input = input.Trim();

        if (string.IsNullOrEmpty(input) || input[^1] != 'i')
            throw new FormatException("直角坐标格式错误，必须以 i 结尾，例如 3+4i");

        input = input[..^1]; // 去掉末尾的 i

        if (string.IsNullOrEmpty(input))
            throw new FormatException("直角坐标格式错误，例如 3+4i");

        // 从第 2 个字符开始查找 + 或 -（分隔实部和虚部）
        int splitPos = -1;
        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == '+' || input[i] == '-')
            {
                splitPos = i;
                break;
            }
        }

        if (splitPos == -1)
            throw new FormatException("直角坐标格式错误，例如 3+4i");

        string realPart = input[..splitPos];
        string imagPart = input[splitPos..];

        if (!IsNumber(realPart))
            throw new FormatException($"实部 \"{realPart}\" 格式错误（最多两位小数）。");

        double a = double.Parse(realPart, CultureInfo.InvariantCulture);
        double b;

        if (imagPart == "+")
            b = 1;
        else if (imagPart == "-")
            b = -1;
        else
        {
            if (!IsNumber(imagPart))
                throw new FormatException($"虚部 \"{imagPart}\" 格式错误（最多两位小数）。");
            b = double.Parse(imagPart, CultureInfo.InvariantCulture);
        }

        return new ComplexNumber(a, b);
    }

    /// <summary>
    /// 解析极坐标，例如 5∠60、2.5∠-30
    /// </summary>
    public static ComplexNumber ParsePolar(string input)
    {
        input = input.Trim();

        int angleCount = input.Count(c => c == '∠');
        if (angleCount != 1)
            throw new FormatException("极坐标格式错误，必须且只能包含一个 ∠，例如 5∠60");

        string[] parts = input.Split('∠');
        if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
            throw new FormatException("极坐标格式错误，例如 5∠60");

        string radiusPart = parts[0].Trim();
        string anglePart = parts[1].Trim();

        if (!IsNumber(radiusPart))
            throw new FormatException($"模 \"{radiusPart}\" 格式错误（最多两位小数）。");
        if (!IsNumber(anglePart))
            throw new FormatException($"角度 \"{anglePart}\" 格式错误（最多两位小数）。");

        double r = double.Parse(radiusPart, CultureInfo.InvariantCulture);
        double theta = double.Parse(anglePart, CultureInfo.InvariantCulture);

        if (r < 0)
            throw new FormatException("模不能为负数。");

        return FromPolar(r, theta);
    }

    /// <summary>
    /// 从极坐标构造复数。
    /// </summary>
    public static ComplexNumber FromPolar(double r, double theta)
    {
        double radian = theta * PI / 180.0;
        double a = r * Math.Cos(radian);
        double b = r * Math.Sin(radian);
        return new ComplexNumber(a, b);
    }

    #endregion

    #region 运算

    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        => new(a.Real + b.Real, a.Imag + b.Imag);

    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        => new(a.Real - b.Real, a.Imag - b.Imag);

    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
    {
        // (a+bi)(c+di) = (ac-bd)+(ad+bc)i
        double real = a.Real * b.Real - a.Imag * b.Imag;
        double imag = a.Real * b.Imag + a.Imag * b.Real;
        return new ComplexNumber(real, imag);
    }

    public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
    {
        double denominator = b.Real * b.Real + b.Imag * b.Imag;
        if (Math.Abs(denominator) < EPS)
            throw new DivideByZeroException("除数不能为 0。");

        // (a+bi)/(c+di) = [(ac+bd)+(bc-ad)i] / (c²+d²)
        double real = (a.Real * b.Real + a.Imag * b.Imag) / denominator;
        double imag = (a.Imag * b.Real - a.Real * b.Imag) / denominator;
        return new ComplexNumber(real, imag);
    }

    #endregion

    #region 格式化输出

    /// <summary>
    /// 格式化为直角坐标字符串，例如 3+4i、3-4i、3+i
    /// </summary>
    public string ToRectangularString()
    {
        double a = Math.Abs(Real) < EPS ? 0 : Real;
        double b = Math.Abs(Imag) < EPS ? 0 : Imag;

        var sb = new StringBuilder();
        sb.Append(FormatNumber(a));

        if (b >= 0)
            sb.Append('+');
        sb.Append(FormatNumber(b));
        sb.Append('i');

        return sb.ToString();
    }

    /// <summary>
    /// 格式化为极坐标字符串，例如 5∠53.13
    /// </summary>
    public string ToPolarString()
    {
        double r = Math.Abs(Magnitude) < EPS ? 0 : Magnitude;
        double theta = Math.Abs(Angle) < EPS ? 0 : Angle;
        return $"{FormatNumber(r)}∠{FormatNumber(theta)}";
    }

    /// <summary>
    /// 格式化数字：消除浮点误差，整数不带小数，最多两位小数并去掉末尾多余 0。
    /// </summary>
    private static string FormatNumber(double x)
    {
        if (Math.Abs(x) < EPS)
            x = 0;

        if (Math.Abs(x - Math.Round(x)) < EPS)
        {
            long n = (long)Math.Round(x);
            return n.ToString(CultureInfo.InvariantCulture);
        }

        string result = x.ToString("F2", CultureInfo.InvariantCulture);

        // 去掉末尾多余的 0
        while (result.Length > 0 && result[^1] == '0')
            result = result[..^1];

        // 去掉小数点
        if (result.Length > 0 && result[^1] == '.')
            result = result[..^1];

        return result;
    }

    #endregion

    #region 输入校验

    /// <summary>
    /// 判断字符串是否为合法数字，最多允许两位小数，允许正负号。
    /// </summary>
    private static bool IsNumber(string s)
    {
        if (string.IsNullOrEmpty(s))
            return false;

        int start = 0;
        if (s[0] == '+' || s[0] == '-')
        {
            start = 1;
            if (s.Length == 1)
                return false;
        }

        bool hasDigit = false;
        bool hasDot = false;
        int decimalDigits = 0;

        for (int i = start; i < s.Length; i++)
        {
            char c = s[i];
            if (char.IsDigit(c))
            {
                hasDigit = true;
                if (hasDot)
                {
                    decimalDigits++;
                    if (decimalDigits > 2)
                        return false;
                }
            }
            else if (c == '.')
            {
                if (hasDot)
                    return false;
                hasDot = true;
            }
            else
            {
                return false;
            }
        }

        return hasDigit;
    }

    #endregion
}
