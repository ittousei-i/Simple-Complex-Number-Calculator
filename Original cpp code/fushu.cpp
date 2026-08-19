#include <iostream>
#include <string>
#include <cmath>
#include <sstream>
#include <cctype>

using namespace std;

const double PI = acos(-1.0);
const double EPS = 1e-9;

// ======================================================
// 去除字符串首尾空格
// ======================================================
string trim(string s)
{
    size_t start = s.find_first_not_of(" \t\r\n");

    if (start == string::npos)
        return "";

    size_t end = s.find_last_not_of(" \t\r\n");

    return s.substr(start, end - start + 1);
}

// ======================================================
// 判断数字是否符合要求
//
// 最多允许两位小数
// ======================================================
bool isNumber(const string& s)
{
    if (s.empty())
        return false;

    int start = 0;

    // 第一位允许正负号
    if (s[0] == '+' || s[0] == '-')
    {
        start = 1;

        if (s.length() == 1)
            return false;
    }

    bool hasDigit = false;
    bool hasDot = false;
    int decimalDigits = 0;

    for (int i = start; i < (int)s.length(); i++)
    {
        char c = s[i];

        if (isdigit((unsigned char)c))
        {
            hasDigit = true;

            if (hasDot)
            {
                decimalDigits++;

                // 最多两位小数
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

// ======================================================
// 格式化数字
// ======================================================
string formatNumber(double x)
{
    // 消除浮点误差
    if (fabs(x) < EPS)
        x = 0;

    // 接近整数
    if (fabs(x - round(x)) < EPS)
    {
        long long n = (long long)round(x);
        return to_string(n);
    }

    stringstream ss;

    ss.setf(ios::fixed);
    ss.precision(2);
    ss << x;

    string result = ss.str();

    // 删除末尾多余的 0
    while (!result.empty() && result.back() == '0')
        result.pop_back();

    // 删除小数点
    if (!result.empty() && result.back() == '.')
        result.pop_back();

    return result;
}

// ======================================================
// 解析直角坐标
//
// 例如：
// 3+4i
// 3-4i
// -3+4i
// 3+i
// 3-i
// ======================================================
bool parseRectangular(string input, double& a, double& b)
{
    input = trim(input);

    // 必须以 i 结尾
    if (input.empty() || input.back() != 'i')
        return false;

    // 删除 i
    input.pop_back();

    if (input.empty())
        return false;

    // 查找实部和虚部之间的 + 或 -
    int splitPos = -1;

    for (int i = 1; i < (int)input.length(); i++)
    {
        if (input[i] == '+' || input[i] == '-')
        {
            splitPos = i;
            break;
        }
    }

    if (splitPos == -1)
        return false;

    string realPart = input.substr(0, splitPos);
    string imagPart = input.substr(splitPos);

    // 实部
    if (!isNumber(realPart))
        return false;

    a = stod(realPart);

    // +i
    if (imagPart == "+")
    {
        b = 1;
        return true;
    }

    // -i
    if (imagPart == "-")
    {
        b = -1;
        return true;
    }

    // 虚部
    if (!isNumber(imagPart))
        return false;

    b = stod(imagPart);

    return true;
}

// ======================================================
// 解析极坐标
//
// 例如：
// 5∠60
// 2.5∠-30
// ======================================================
bool parsePolar(string input, double& r, double& theta)
{
    input = trim(input);

    string angleSymbol = "∠";

    size_t pos = input.find(angleSymbol);

    if (pos == string::npos)
        return false;

    // 只能出现一个 ∠
    if (input.find(angleSymbol, pos + angleSymbol.length())
        != string::npos)
    {
        return false;
    }

    string radiusPart = input.substr(0, pos);

    string anglePart =
        input.substr(pos + angleSymbol.length());

    if (radiusPart.empty() || anglePart.empty())
        return false;

    if (!isNumber(radiusPart))
        return false;

    if (!isNumber(anglePart))
        return false;

    r = stod(radiusPart);
    theta = stod(anglePart);

    // 模不能为负
    if (r < 0)
        return false;

    return true;
}

// ======================================================
// 极坐标 → 直角坐标
//
// a = r cosθ
// b = r sinθ
// ======================================================
void polarToRectangular(double r, double theta,
    double& a, double& b)
{
    double radian = theta * PI / 180.0;

    a = r * cos(radian);
    b = r * sin(radian);

    // 消除浮点误差
    if (fabs(a) < EPS)
        a = 0;

    if (fabs(b) < EPS)
        b = 0;
}

// ======================================================
// 直角坐标 → 极坐标
//
// r = sqrt(a²+b²)
//
// theta = atan(b/a)
//
// 根据象限修正，使角度范围：
// -180° < theta <= 180°
// ======================================================
void rectangularToPolar(double a, double b,
    double& r, double& theta)
{
    r = sqrt(a * a + b * b);

    // 原点
    if (fabs(a) < EPS && fabs(b) < EPS)
    {
        theta = 0;
        return;
    }

    // 实部为 0
    if (fabs(a) < EPS)
    {
        if (b > 0)
            theta = 90.0;
        else
            theta = -90.0;
    }

    // 第一、第四象限
    else if (a > 0)
    {
        theta = atan(b / a) * 180.0 / PI;
    }

    // 第二、第三象限
    else
    {
        theta = atan(b / a) * 180.0 / PI;

        if (b > 0)
            theta += 180.0;
        else
            theta -= 180.0;
    }

    // 消除极小浮点误差
    if (fabs(theta) < EPS)
        theta = 0;
}

// ======================================================
// 输出直角坐标
// ======================================================
void printRectangular(double a, double b)
{
    if (fabs(a) < EPS)
        a = 0;

    if (fabs(b) < EPS)
        b = 0;

    cout << formatNumber(a);

    if (b >= 0)
        cout << "+" << formatNumber(b) << "i";
    else
        cout << formatNumber(b) << "i";

    cout << endl;
}

// ======================================================
// 输出极坐标
// ======================================================
void printPolar(double r, double theta)
{
    if (fabs(r) < EPS)
        r = 0;

    if (fabs(theta) < EPS)
        theta = 0;

    cout << formatNumber(r)
        << "∠"
        << formatNumber(theta)
        << endl;
}

// ======================================================
// 坐标转换功能
// ======================================================
void convertComplex()
{
    while (true)
    {
        cout << "\n请输入复数（输入 back 返回主菜单）：";

        string input;
        getline(cin, input);

        input = trim(input);

        if (input == "back")
            return;

        if (input.empty())
        {
            cout << "输入不能为空，请重新输入。" << endl;
            continue;
        }

        double a, b;
        double r, theta;

        // 极坐标
        if (input.find("∠") != string::npos)
        {
            if (parsePolar(input, r, theta))
            {
                polarToRectangular(r, theta, a, b);

                cout << "输出：";
                printRectangular(a, b);
            }
            else
            {
                cout << "输入格式错误！" << endl;
                cout << "正确格式例如：2∠60、2.5∠-30" << endl;
            }
        }

        // 直角坐标
        else
        {
            if (parseRectangular(input, a, b))
            {
                rectangularToPolar(a, b, r, theta);

                cout << "输出：";
                printPolar(r, theta);
            }
            else
            {
                cout << "输入格式错误！" << endl;
                cout << "正确格式例如：3+4i、3-4i" << endl;
            }
        }
    }
}

// ======================================================
// 复数加减运算
// ======================================================
void calculateComplexAddSub()
{
    while (true)
    {
        cout << "\n请输入第一个复数（输入 back 返回主菜单）：";

        string input1;
        getline(cin, input1);

        input1 = trim(input1);

        if (input1 == "back")
            return;

        if (input1.empty())
        {
            cout << "输入不能为空，请重新输入。" << endl;
            continue;
        }

        cout << "请输入运算符（+ 或 -）：";

        string op;
        getline(cin, op);

        op = trim(op);

        if (op != "+" && op != "-")
        {
            cout << "运算符输入错误，只支持 + 和 -。" << endl;
            continue;
        }

        cout << "请输入第二个复数：";

        string input2;
        getline(cin, input2);

        input2 = trim(input2);

        if (input2.empty())
        {
            cout << "输入不能为空，请重新输入。" << endl;
            continue;
        }

        bool firstPolar =
            (input1.find("∠") != string::npos);

        bool secondPolar =
            (input2.find("∠") != string::npos);

        // 不允许混合
        if (firstPolar != secondPolar)
        {
            cout << "不支持直角坐标与极坐标形式混合运算！"
                << endl;
            continue;
        }

        double a1, b1;
        double a2, b2;

        // ==================================================
        // 直角坐标
        // ==================================================
        if (!firstPolar)
        {
            if (!parseRectangular(input1, a1, b1))
            {
                cout << "第一个复数格式错误！" << endl;
                continue;
            }

            if (!parseRectangular(input2, a2, b2))
            {
                cout << "第二个复数格式错误！" << endl;
                continue;
            }

            double realResult;
            double imagResult;

            if (op == "+")
            {
                realResult = a1 + a2;
                imagResult = b1 + b2;
            }
            else
            {
                realResult = a1 - a2;
                imagResult = b1 - b2;
            }

            cout << "输出：";
            printRectangular(realResult, imagResult);
        }

        // ==================================================
        // 极坐标
        // ==================================================
        else
        {
            double r1, theta1;
            double r2, theta2;

            if (!parsePolar(input1, r1, theta1))
            {
                cout << "第一个复数格式错误！" << endl;
                continue;
            }

            if (!parsePolar(input2, r2, theta2))
            {
                cout << "第二个复数格式错误！" << endl;
                continue;
            }

            // 极坐标 → 直角坐标
            polarToRectangular(r1, theta1, a1, b1);
            polarToRectangular(r2, theta2, a2, b2);

            double realResult;
            double imagResult;

            if (op == "+")
            {
                realResult = a1 + a2;
                imagResult = b1 + b2;
            }
            else
            {
                realResult = a1 - a2;
                imagResult = b1 - b2;
            }

            // 直角坐标 → 极坐标
            double resultR;
            double resultTheta;

            rectangularToPolar(
                realResult,
                imagResult,
                resultR,
                resultTheta
            );

            cout << "输出：";
            printPolar(resultR, resultTheta);
        }
    }
}

// ======================================================
// 复数乘除运算
// ======================================================
void calculateComplexMulDiv()
{
    while (true)
    {
        cout << "\n请输入第一个复数（输入 back 返回主菜单）：";

        string input1;
        getline(cin, input1);

        input1 = trim(input1);

        if (input1 == "back")
            return;

        if (input1.empty())
        {
            cout << "输入不能为空，请重新输入。" << endl;
            continue;
        }

        cout << "请输入运算符（* 或 /）：";

        string op;
        getline(cin, op);

        op = trim(op);

        if (op != "*" && op != "/")
        {
            cout << "运算符输入错误，只支持 * 和 /。" << endl;
            continue;
        }

        cout << "请输入第二个复数：";

        string input2;
        getline(cin, input2);

        input2 = trim(input2);

        if (input2.empty())
        {
            cout << "输入不能为空，请重新输入。" << endl;
            continue;
        }

        // ==================================================
        // 判断坐标形式
        // ==================================================
        bool firstPolar =
            (input1.find("∠") != string::npos);

        bool secondPolar =
            (input2.find("∠") != string::npos);

        // 不允许混合
        if (firstPolar != secondPolar)
        {
            cout << "不支持直角坐标与极坐标形式混合运算！"
                << endl;
            continue;
        }

        // ==================================================
        // 直角坐标乘除
        // ==================================================
        if (!firstPolar)
        {
            double a, b;
            double c, d;

            if (!parseRectangular(input1, a, b))
            {
                cout << "第一个复数格式错误！" << endl;
                continue;
            }

            if (!parseRectangular(input2, c, d))
            {
                cout << "第二个复数格式错误！" << endl;
                continue;
            }

            // ==================================================
            // 乘法
            //
            // (a+bi)(c+di)
            // = (ac-bd)+(ad+bc)i
            // ==================================================
            if (op == "*")
            {
                double realResult =
                    a * c - b * d;

                double imagResult =
                    a * d + b * c;

                cout << "输出：";

                printRectangular(
                    realResult,
                    imagResult
                );
            }

            // ==================================================
            // 除法
            //
            // (a+bi)/(c+di)
            //
            // = [(ac+bd)+(bc-ad)i]
            //   ------------------
            //       c²+d²
            // ==================================================
            else
            {
                double denominator =
                    c * c + d * d;

                // 除数为 0
                if (fabs(denominator) < EPS)
                {
                    cout << "错误：除数不能为 0！"
                        << endl;
                    continue;
                }

                double realResult =
                    (a * c + b * d)
                    / denominator;

                double imagResult =
                    (b * c - a * d)
                    / denominator;

                cout << "输出：";

                printRectangular(
                    realResult,
                    imagResult
                );
            }
        }

        // ======================================================
        // 极坐标乘除
        //
        // 乘法：
        // A∠α × B∠β = AB∠(α+β)
        //
        // 除法：
        // A∠α ÷ B∠β = (A/B)∠(α-β)
        //
        // 无论两个模是否相等，都直接使用极坐标公式。
        // ======================================================
        else
        {
            double r1, theta1;
            double r2, theta2;

            if (!parsePolar(input1, r1, theta1))
            {
                cout << "第一个复数格式错误！" << endl;
                continue;
            }

            if (!parsePolar(input2, r2, theta2))
            {
                cout << "第二个复数格式错误！" << endl;
                continue;
            }

            // ==================================================
            // 极坐标乘法
            //
            // A∠α × B∠β
            // = AB∠(α+β)
            // ==================================================
            if (op == "*")
            {
                double resultR = r1 * r2;

                double resultTheta =
                    theta1 + theta2;

                // 将角度限制在
                // -180° < θ <= 180°
                while (resultTheta > 180.0)
                    resultTheta -= 360.0;

                while (resultTheta <= -180.0)
                    resultTheta += 360.0;

                cout << "输出：";

                printPolar(
                    resultR,
                    resultTheta
                );
            }

            // ==================================================
            // 极坐标除法
            //
            // A∠α ÷ B∠β
            // = (A/B)∠(α-β)
            // ==================================================
            else
            {
                // 除数不能为 0
                if (fabs(r2) < EPS)
                {
                    cout << "错误：除数不能为 0！"
                        << endl;
                    continue;
                }

                double resultR =
                    r1 / r2;

                double resultTheta =
                    theta1 - theta2;

                // 将角度限制在
                // -180° < θ <= 180°
                while (resultTheta > 180.0)
                    resultTheta -= 360.0;

                while (resultTheta <= -180.0)
                    resultTheta += 360.0;

                cout << "输出：";

                printPolar(
                    resultR,
                    resultTheta
                );
            }
        }
    }
}

// ======================================================
// 主函数
// ======================================================
int main()
{
    cout << "========================================" << endl;
    cout << "          简易复数计算器" << endl;
    cout << "========================================" << endl;

    while (true)
    {
        cout << "\n请选择功能：" << endl;
        cout << "1. 复数坐标形式转换" << endl;
        cout << "2. 复数加减运算" << endl;
        cout << "3. 复数乘除运算" << endl;
        cout << "4. 退出程序" << endl;

        cout << "请输入选项：";

        string choice;
        getline(cin, choice);

        // ==================================================
        // 坐标转换
        // ==================================================
        if (choice == "1")
        {
            convertComplex();
        }

        // ==================================================
        // 加减
        // ==================================================
        else if (choice == "2")
        {
            calculateComplexAddSub();
        }

        // ==================================================
        // 乘除
        // ==================================================
        else if (choice == "3")
        {
            calculateComplexMulDiv();
        }

        // ==================================================
        // 退出
        // ==================================================
        else if (choice == "4" ||
            choice == "exit")
        {
            cout << "程序已退出。" << endl;
            break;
        }

        else
        {
            cout << "选项输入错误，请输入 1、2、3 或 4。"
                << endl;
        }
    }

    return 0;
}