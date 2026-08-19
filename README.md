# 简易复数计算器

基于 C# WinForms (.NET 9) 的复数计算器，支持直角坐标与极坐标两种表示形式的转换及四则运算。

## 功能特性

- **坐标转换**：直角坐标 (a+bi) ↔ 极坐标 (r∠θ) 双向自动转换
- **四则运算**：加 (+)、减 (−)、乘 (×)、除 (÷)，支持直角坐标和极坐标两种形式
- **计算器键盘**：内置可视化按钮键盘，支持数字、小数点、虚数单位 i、角度符号 ∠、退格、清空
- **计算历史**：自动记录最近 50 条计算记录，支持一键清空
- **结果复制**：转换/运算结果可一键复制到剪贴板
- **输入校验**：不规范输入弹出提示，数字最多两位小数

## 输入格式

| 坐标形式 | 格式 | 示例 |
|---------|------|------|
| 直角坐标 | `a+bi` / `a-bi` | `3+4i`、`-3-4i`、`3-i` |
| 极坐标 | `r∠θ` | `5∠60`、`2.5∠-30` |

> 注意：四则运算中两个复数必须使用同一种坐标形式，不支持混合运算。

## 界面预览

程序包含两个功能标签页：
- **坐标转换**：输入一个复数，自动识别格式并转换为另一种形式
- **四则运算**：输入两个复数，选择运算符后点击 `=` 计算

底部为计算器键盘（左侧）和计算历史列表（右侧）。

## 技术栈

- **语言**：C# 13
- **框架**：.NET 9 (Windows Desktop)
- **UI**：Windows Forms
- **项目类型**：WinExe (单文件可发布)

## 项目结构

```
ComplexCalculator/
├── ComplexCalculator.csproj   # 项目文件
├── Program.cs                 # 程序入口
├── ComplexNumber.cs           # 复数核心类（解析、转换、运算）
├── Form1.cs                   # 主窗体逻辑（事件处理、计算器按钮）
├── Form1.Designer.cs          # 主窗体 UI 布局
└── Form1.resx                 # 窗体资源
```

## 构建与运行

### 环境要求

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) 或更高版本
- Windows 操作系统（WinForms 仅限 Windows）

### 编译运行

```bash
# 还原依赖并编译
dotnet build

# 直接运行
dotnet run

# 发布为单文件可执行程序
dotnet publish -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true
```

发布后的可执行文件位于：
`bin/Release/net9.0-windows/win-x64/publish/ComplexCalculator.exe`

## 数学原理

- **极坐标 → 直角坐标**：a = r·cosθ，b = r·sinθ
- **直角坐标 → 极坐标**：r = √(a²+b²)，θ = atan2(b, a)
- **乘法（直角）**：(a+bi)(c+di) = (ac−bd)+(ad+bc)i
- **除法（直角）**：(a+bi)/(c+di) = [(ac+bd)+(bc−ad)i] / (c²+d²)
- **乘法（极坐标）**：A∠α × B∠β = AB∠(α+β)
- **除法（极坐标）**：A∠α ÷ B∠β = (A/B)∠(α−β)
- 角度范围统一归一化至 −180° < θ ≤ 180°

## License

[MIT](LICENSE)
