# 快速开始

## 安装

### 方式一：NuGet 包管理器

```powershell
Install-Package EverythingUI.WPF
```

### 方式二：.NET CLI

```bash
dotnet add package EverythingUI.WPF
```

### 方式三：手动引用

下载源码后，在项目中引用 `EverythingUI.WPF.dll`。

## 基本配置

### 1. 添加命名空间

```xml
xmlns:everything="clr-namespace:EverythingUI.WPF.Controls;assembly=EverythingUI.WPF"
xmlns:controls="clr-namespace:EverythingUI.WPF.Controls;assembly=EverythingUI.WPF"
```

### 2. 引入主题资源

在 `App.xaml` 中添加：

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/EverythingUI.WPF;component/Themes/Generic.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

引入 `Generic.xaml` 后，以下资源自动可用：
- 所有控件样式和模板
- 渐变颜色资源（`Gradient*Start/End`）
- 统一光泽层画刷（`GlossBrush`）
- 中性色资源（`TextPrimaryBrush` 等）

## 使用颜色资源

EverythingUI.WPF 提供了一套完整的颜色资源，支持垂直三色渐变效果。

### 方式一：ColorName 属性（推荐）

最简单的方式，只需一个属性：

```xml
<everything:EverythingButton Content="红色按钮" ColorName="Red"/>
<everything:EverythingToggleSwitch IsChecked="True" ColorName="Green"/>
<everything:EverythingSlider Value="50" ColorName="Orange"/>
```

### 方式二：直接使用颜色资源

```xml
<everything:EverythingButton Content="红色按钮"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}"/>
```

### 方式三：使用渐变刷资源

```xml
<Border Background="{StaticResource GradientBlueBrush}"/>
```

### 方式四：自定义颜色

```xml
<everything:EverythingButton 
    Content="自定义颜色" 
    GradientStartColor="#FF5833" 
    GradientEndColor="#D43030"/>
```

## 使用统一光泽层

光泽层已内置在所有控件模板中，无需额外配置。如需在自定义控件中使用：

```xml
<Border Background="{DynamicResource GlossBrush}"
        VerticalAlignment="Top"
        ClipToBounds="True"
        IsHitTestVisible="False"
        Height="{Binding ActualHeight, RelativeSource={RelativeSource TemplatedParent},
                  Converter={x:Static controls:HalfHeightConverter.Instance}}"/>
```

## 完整示例

```xml
<Window x:Class="YourNamespace.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:everything="clr-namespace:EverythingUI.WPF.Controls;assembly=EverythingUI.WPF"
        Title="EverythingUI Demo" Height="450" Width="800">
    <StackPanel Margin="20" Spacing="10">
        
        <!-- 按钮 -->
        <everything:EverythingButton Content="主要操作"/>
        
        <!-- 红色按钮 -->
        <everything:EverythingButton Content="危险操作" ColorName="Red"/>
        
        <!-- 绿色按钮 -->
        <everything:EverythingButton Content="成功操作" ColorName="Green"/>
        
        <!-- 复选框 -->
        <everything:EverythingCheckBox Content="记住我" IsChecked="True"/>
        
        <!-- 单选框 -->
        <StackPanel Orientation="Horizontal">
            <everything:EverythingRadioButton Content="选项A" GroupName="Group1" IsChecked="True"/>
            <everything:EverythingRadioButton Content="选项B" GroupName="Group1"/>
        </StackPanel>
        
        <!-- 开关 -->
        <everything:EverythingToggleSwitch IsChecked="True"/>
        
        <!-- 组合框 -->
        <everything:EverythingComboBox SelectedIndex="0" ColorName="Purple">
            <ComboBoxItem Content="选项 1"/>
            <ComboBoxItem Content="选项 2"/>
        </everything:EverythingComboBox>
        
        <!-- 滑块 -->
        <everything:EverythingSlider Value="50" Minimum="0" Maximum="100"/>
        
        <!-- 进度条 -->
        <everything:EverythingProgressBar Value="75"/>
        
        <!-- 圆形进度条 -->
        <everything:EverythingCircularProgressBar Value="65" Width="80" Height="80"/>
        
        <!-- 文本框 -->
        <everything:EverythingTextBox Placeholder="请输入内容..."/>
        
        <!-- 卡片 -->
        <everything:EverythingCard Padding="16">
            <TextBlock Text="卡片内容"/>
        </everything:EverythingCard>
        
    </StackPanel>
</Window>
```

## 可用颜色资源

### 主要颜色（11种）

| 颜色 | 起始色 | 中间色 | 资源键 |
|-----|-------|-------|-------|
| 蓝色（默认） | `#00ACF0` | `#0078D4` | `GradientBlueStart/End` / `ColorName.Blue` |
| 红色 | `#FF5833` | `#D43030` | `GradientRedStart/End` / `ColorName.Red` |
| 绿色 | `#A0D605` | `#19A654` | `GradientGreenStart/End` / `ColorName.Green` |
| 橙色 | `#FFC300` | `#FF8D1A` | `GradientOrangeStart/End` / `ColorName.Orange` |
| 紫色 | `#AC33C1` | `#8D2C9E` | `GradientPurpleStart/End` / `ColorName.Purple` |
| 粉色 | `#F7D7EC` | `#FF9CDB` | `GradientPinkStart/End` / `ColorName.Pink` |
| 青色 | `#00BAAD` | `#00998F` | `GradientCyanStart/End` / `ColorName.Cyan` |
| 黄色 | `#FFEB3B` | `#FFC400` | `GradientYellowStart/End` / `ColorName.Yellow` |
| 黑色 | `#808080` | `#383838` | `GradientBlackStart/End` / `ColorName.Black` |
| 灰色 | `#E5E5E5` | `#A6A6A6` | `GradientGrayStart/End` / `ColorName.Gray` |
| 白色 | `#FFFFFF` | `#E6E6E6` | `GradientWhiteStart/End` / `ColorName.White` |

### 扩展颜色（8种）

| 颜色 | 起始色 | 中间色 | 资源键 |
|-----|-------|-------|-------|
| 靛蓝 | `#5B7FFF` | `#4A6BE5` | `GradientIndigoStart/End` / `ColorName.Indigo` |
| 天蓝 | `#38BDF8` | `#0EA5E9` | `GradientSkyStart/End` / `ColorName.Sky` |
| 翠绿 | `#34D399` | `#10B981` | `GradientEmeraldStart/End` / `ColorName.Emerald` |
| 玫瑰 | `#FB7185` | `#E11D48` | `GradientRoseStart/End` / `ColorName.Rose` |
| 琥珀 | `#FBBF24` | `#D97706` | `GradientAmberStart/End` / `ColorName.Amber` |
| 蓝紫 | `#8B5CF6` | `#7C3AED` | `GradientVioletStart/End` / `ColorName.Violet` |
| 珊瑚 | `#FF7F7F` | `#FF5252` | `GradientCoralStart/End` / `ColorName.Coral` |
| 薄荷 | `#6EE7B7` | `#34D399` | `GradientMintStart/End` / `ColorName.Mint` |

## 下一步

- 查看 [主题样式文档](theming.md) 了解更多颜色使用技巧和光泽层详情
- 查看 [ColorName 文档](colorname.md) 了解颜色名称的完整用法
- 查看各控件的详细文档了解高级用法
- 参考演示程序源码学习实际应用
