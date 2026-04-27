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

## 使用颜色资源

EverythingUI.WPF 提供了一套完整的颜色资源，支持垂直三色渐变效果：

### 方式一：直接使用颜色资源（推荐）

```xml
<!-- 默认蓝色渐变按钮 -->
<everything:EverythingButton Content="默认按钮"/>

<!-- 使用预设颜色 -->
<everything:EverythingButton Content="红色按钮"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}"/>

<everything:EverythingButton Content="绿色按钮"
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}"/>
```

### 方式二：使用渐变刷资源

```xml
<Border Background="{StaticResource GradientBlueBrush}"/>
```

### 方式三：自定义颜色

```xml
<everything:EverythingButton 
    Content="自定义颜色" 
    GradientStartColor="#FF5833" 
    GradientEndColor="#D43030"/>
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
        <everything:EverythingButton Content="危险操作"
            GradientStartColor="{StaticResource GradientRedStart}"
            GradientEndColor="{StaticResource GradientRedEnd}"/>
        
        <!-- 绿色按钮 -->
        <everything:EverythingButton Content="成功操作"
            GradientStartColor="{StaticResource GradientGreenStart}"
            GradientEndColor="{StaticResource GradientGreenEnd}"/>
        
        <!-- 复选框 -->
        <everything:EverythingCheckBox Content="记住我" IsChecked="True"/>
        
        <!-- 单选框 -->
        <StackPanel Orientation="Horizontal">
            <everything:EverythingRadioButton Content="选项A" GroupName="Group1" IsChecked="True"/>
            <everything:EverythingRadioButton Content="选项B" GroupName="Group1"/>
        </StackPanel>
        
        <!-- 开关 -->
        <everything:EverythingToggleSwitch IsChecked="True"/>
        
        <!-- 滑块 -->
        <everything:EverythingSlider Value="50" Minimum="0" Maximum="100"/>
        
        <!-- 进度条 -->
        <everything:EverythingProgressBar Value="75"/>
        
    </StackPanel>
</Window>
```

## 可用颜色资源

### 主要颜色（11种）

| 颜色 | 起始色 | 中间色 | 资源键 |
|-----|-------|-------|-------|
| 蓝色（默认） | `#00ACF0` | `#0078D4` | `GradientBlueStart/End` |
| 红色 | `#FF5833` | `#D43030` | `GradientRedStart/End` |
| 绿色 | `#A0D605` | `#19A654` | `GradientGreenStart/End` |
| 橙色 | `#FFC300` | `#FF8D1A` | `GradientOrangeStart/End` |
| 紫色 | `#AC33C1` | `#8D2C9E` | `GradientPurpleStart/End` |
| 粉色 | `#F7D7EC` | `#FF9CDB` | `GradientPinkStart/End` |
| 青色 | `#00BAAD` | `#00998F` | `GradientCyanStart/End` |
| 黄色 | `#FFEB3B` | `#FFC400` | `GradientYellowStart/End` |
| 黑色 | `#808080` | `#383838` | `GradientBlackStart/End` |
| 灰色 | `#E5E5E5` | `#A6A6A6` | `GradientGrayStart/End` |
| 白色 | `#FFFFFF` | `#E6E6E6` | `GradientWhiteStart/End` |

### 扩展颜色（8种）

| 颜色 | 起始色 | 中间色 | 资源键 |
|-----|-------|-------|-------|
| 靛蓝 | `#5B7FFF` | `#4A6BE5` | `GradientIndigoStart/End` |
| 天蓝 | `#38BDF8` | `#0EA5E9` | `GradientSkyStart/End` |
| 翠绿 | `#34D399` | `#10B981` | `GradientEmeraldStart/End` |
| 玫瑰 | `#FB7185` | `#E11D48` | `GradientRoseStart/End` |
| 琥珀 | `#FBBF24` | `#D97706` | `GradientAmberStart/End` |
| 蓝紫 | `#8B5CF6` | `#7C3AED` | `GradientVioletStart/End` |
| 珊瑚 | `#FF7F7F` | `#FF5252` | `GradientCoralStart/End` |
| 薄荷 | `#6EE7B7` | `#34D399` | `GradientMintStart/End` |

## 下一步

- 查看 [主题样式文档](theming.md) 了解更多颜色使用技巧
- 查看各控件的详细文档了解高级用法
- 参考演示程序源码学习实际应用
