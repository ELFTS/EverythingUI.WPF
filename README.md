# EverythingUI.WPF

一个拟物化、漂亮的 WPF UI 组件库，提供丰富的控件和主题样式。

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey.svg)

## 特性

- **现代化设计** - 采用当下流行的设计语言，界面简洁美观
- **丰富的控件** - 提供 Button、TextBox、Card、ToggleSwitch、ComboBox 等多种常用控件
- **垂直三色渐变** - 按钮、开关、组合框支持自定义垂直三色渐变效果
- **流畅动画** - 控件带有平滑的过渡动画
- **自定义滚动条** - 圆角滑块设计，支持悬停和拖动效果
- **侧边栏控件** - 支持自定义渐变
- **预设颜色方案** - 提供11种精心调配的渐变颜色方案
- **易于定制** - 通过依赖属性轻松自定义控件外观
- **完全开源** - MIT 协议，可自由使用和修改

## 安装

### 本地引用（开发中）

当前项目正在开发中，尚未发布到 NuGet。你可以通过以下方式使用：

#### 方式一：直接引用项目

1. 将 `EverythingUI.WPF` 项目添加到你的解决方案中
2. 在你的项目中添加对 `EverythingUI.WPF` 的项目引用

#### 方式二：编译后引用 DLL

```bash
# 编译项目
dotnet build EverythingUI.WPF.sln

# 引用生成的 DLL
# 路径：EverythingUI.WPF/bin/Debug/net8.0-windows/EverythingUI.WPF.dll
```

## 快速开始

### 1. 添加命名空间

```xml
xmlns:everything="clr-namespace:EverythingUI.WPF.Controls;assembly=EverythingUI.WPF"
```

### 2. 引入主题资源

在 App.xaml 中添加：

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/EverythingUI.WPF;component/Themes/Generic.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

### 3. 使用控件

```xml
<!-- 默认蓝色渐变按钮 -->
<everything:EverythingButton Content="默认按钮"/>

<!-- 自定义渐变颜色 -->
<everything:EverythingButton 
    Content="自定义颜色" 
    GradientStartColor="#FF5833" 
    GradientEndColor="#D43030"/>
```

## 控件列表

### EverythingButton

多功能按钮控件，支持垂直三色渐变、自定义颜色和流畅动画。

| 属性 | 类型 | 描述 |
|------|------|------|
| CornerRadius | CornerRadius | 圆角半径 |
| Icon | object | 图标内容 |
| IconPlacement | Dock | 图标位置 |
| GradientStartColor | Color | 渐变起始颜色（上下位置） |
| GradientEndColor | Color | 渐变中间颜色 |

**动画效果：**

- **悬停动画**：按钮轻微放大（1.02倍）并显示外阴影
- **按下动画**：按钮缩小（0.98倍）并显示内阴影（顶部和左右）
- **过渡时间**：悬停0.2秒，按下0.1秒，释放0.15秒

**示例：**

```xml
<!-- 默认蓝色渐变 -->
<everything:EverythingButton Content="默认按钮"/>

<!-- 黑色渐变 -->
<everything:EverythingButton 
    Content="黑色按钮" 
    GradientStartColor="#808080" 
    GradientEndColor="#383838"/>

<!-- 红色渐变 -->
<everything:EverythingButton 
    Content="红色按钮" 
    GradientStartColor="#FF5833" 
    GradientEndColor="#D43030"/>

<!-- 绿色渐变 -->
<everything:EverythingButton 
    Content="绿色按钮" 
    GradientStartColor="#A0D605" 
    GradientEndColor="#19A654"/>
```

### EverythingSideBar

侧边栏导航控件，支持自定义渐变。侧边栏背景和菜单项背景独立区分，选中项显示渐变效果。

| 属性 | 类型 | 描述 |
|------|------|------|
| SideBarWidth | double | 侧边栏宽度（默认250） |
| GradientStartColor | Color | 渐变起始颜色 |
| GradientEndColor | Color | 渐变中间颜色 |
| CornerRadius | CornerRadius | 圆角半径 |
| ItemsSource | object | 菜单项数据源 |
| ItemTemplate | DataTemplate | 菜单项模板 |
| SelectedItem | object | 当前选中项 |

**视觉样式：**

- **侧边栏背景**：使用卡片背景色（CardBackgroundBrush），带阴影效果，可通过 `Background` 属性自定义
- **菜单项默认状态**：透明背景
- **菜单项悬停状态**：浅灰色背景（Gray200Brush）
- **菜单项选中状态**：垂直三色渐变背景，白色文字，阴影效果

**自定义背景色：**

```xml
<!-- 使用内置资源 -->
<everything:EverythingSideBar Background="{DynamicResource Gray100Brush}"/>

<!-- 自定义颜色 -->
<everything:EverythingSideBar Background="#F5F5F5"/>

<!-- 使用画刷 -->
<everything:EverythingSideBar>
    <everything:EverythingSideBar.Background>
        <LinearGradientBrush StartPoint="0,0" EndPoint="1,0">
            <GradientStop Offset="0" Color="#E3F2FD"/>
            <GradientStop Offset="1" Color="#BBDEFB"/>
        </LinearGradientBrush>
    </everything:EverythingSideBar.Background>
</everything:EverythingSideBar>
```

**使用示例：**

```xml
<!-- 基础侧边栏 -->
<everything:EverythingSideBar SideBarWidth="220">
    <everything:EverythingSideBar.ItemsSource>
        <x:Array Type="everything:EverythingSideBarItem">
            <everything:EverythingSideBarItem Text="首页"/>
            <everything:EverythingSideBarItem Text="产品"/>
            <everything:EverythingSideBarItem Text="订单"/>
        </x:Array>
    </everything:EverythingSideBar.ItemsSource>
    <everything:EverythingSideBar.ItemTemplate>
        <DataTemplate>
            <TextBlock Text="{Binding Text}"/>
        </DataTemplate>
    </everything:EverythingSideBar.ItemTemplate>
</everything:EverythingSideBar>
```

```csharp
using EverythingUI.WPF.Controls;

// 在代码中创建菜单项
private void InitializeSideBarItems()
{
    var items = new[]
    {
        new EverythingSideBarItem { Text = "首页" },
        new EverythingSideBarItem { Text = "设置" },
        new EverythingSideBarItem { Text = "用户" }
    };
    
    sideBar.ItemsSource = items;
}

// 监听选中项变化
sideBar.SelectedItem = items[0];
```

**依赖属性变更通知：**

所有依赖属性都支持变更通知，可通过以下方式监听：

```csharp
// 方式一：使用 DependencyPropertyDescriptor
DependencyPropertyDescriptor.FromProperty(
    EverythingSideBar.SelectedItemProperty, 
    typeof(EverythingSideBar))
    .AddValueChanged(sideBar, (s, e) =>
    {
        // 处理选中项变化
        var selectedItem = sideBar.SelectedItem as EverythingSideBarItem;
        Console.WriteLine($"选中项: {selectedItem?.Text}");
    });

// 方式二：在 XAML 中绑定并监听 PropertyChanged
<everything:EverythingSideBar 
    SelectedItem="{Binding SelectedCategory, Mode=TwoWay}"/>

// 方式三：注册属性变更回调
private static readonly DependencyProperty SelectedItemProperty =
    DependencyProperty.Register(
        nameof(SelectedItem), 
        typeof(object), 
        typeof(EverythingSideBar),
        new FrameworkPropertyMetadata(
            null, 
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            OnSelectedItemChanged));

private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
{
    if (d is EverythingSideBar sideBar)
    {
        // 属性变更处理逻辑
    }
}
```

**渐变效果说明：**

按钮采用垂直三色渐变：**起始色 → 中间色 → 起始色**（对称渐变）

- Offset 0 (顶部): `GradientStartColor`
- Offset 0.5 (中间): `GradientEndColor`  
- Offset 1 (底部): `GradientStartColor`

**预设颜色方案：**

| 颜色 | 起始色 | 中间色 | 代码示例 |
|------|--------|--------|----------|
| 黑色 | #808080 | #383838 | `GradientStartColor="#808080" GradientEndColor="#383838"` |
| 灰色 | #E5E5E5 | #A6A6A6 | `GradientStartColor="#E5E5E5" GradientEndColor="#A6A6A6"` |
| 红色 | #FF5833 | #D43030 | `GradientStartColor="#FF5833" GradientEndColor="#D43030"` |
| 橘色 | #FFC300 | #FF8D1A | `GradientStartColor="#FFC300" GradientEndColor="#FF8D1A"` |
| 黄色 | #FFEB3B | #FFC400 | `GradientStartColor="#FFEB3B" GradientEndColor="#FFC400"` |
| 绿色 | #A0D605 | #19A654 | `GradientStartColor="#A0D605" GradientEndColor="#19A654"` |
| 青色 | #00BAAD | #00998F | `GradientStartColor="#00BAAD" GradientEndColor="#00998F"` |
| 蓝色 | #00ACF0 | #0078D4 | `GradientStartColor="#00ACF0" GradientEndColor="#0078D4"` |
| 紫色 | #AC33C1 | #8D2C9E | `GradientStartColor="#AC33C1" GradientEndColor="#8D2C9E"` |
| 粉色 | #F7D7EC | #FF9CDB | `GradientStartColor="#F7D7EC" GradientEndColor="#FF9CDB"` |

### 滚动条样式

提供现代化的滚动条样式，可应用于 ScrollViewer。

**样式键名：**

| 样式键名 | 用途 |
|---------|------|
| `EverythingScrollViewerStyle` | ScrollViewer 整体样式 |
| `EverythingVerticalScrollBar` | 垂直滚动条样式 |
| `EverythingHorizontalScrollBar` | 水平滚动条样式 |

**使用示例：**

```xml
<!-- 确保在 App.xaml 中引用了主题资源 -->
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/EverythingUI.WPF;component/Themes/Generic.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>

<!-- 应用到 ScrollViewer -->
<ScrollViewer Style="{StaticResource EverythingScrollViewerStyle}">
    <StackPanel>
        <!-- 内容 -->
    </StackPanel>
</ScrollViewer>
```

**样式特点：**

- **滑块**：圆角6px设计，带轻微阴影
- **颜色**：默认灰色，悬停变深，拖动时更深
- **轨道**：透明背景，简洁现代
- **尺寸**：12px 宽度，适合现代 UI

### EverythingCard

卡片容器控件，支持头部、内容和底部区域。

| 属性 | 类型 | 描述 |
|------|------|------|
| Header | object | 头部内容 |
| HeaderTemplate | DataTemplate | 头部模板 |
| Footer | object | 底部内容 |
| FooterTemplate | DataTemplate | 底部模板 |
| CardVariant | CardVariant | 卡片变体：Default, Elevated, Outlined |
| CornerRadius | CornerRadius | 圆角半径 |
| ShadowDepth | double | 阴影深度 |
| Padding | Thickness | 内容区域内边距 |
| HeaderPadding | Thickness | 头部区域内边距 |
| FooterPadding | Thickness | 底部区域内边距 |

**示例：**

```xml
<!-- 基础卡片 -->
<everything:EverythingCard CardVariant="Elevated" CornerRadius="12">
    <everything:EverythingCard.Header>
        <TextBlock Text="卡片标题" FontWeight="Bold"/>
    </everything:EverythingCard.Header>
    <TextBlock Text="这是卡片内容"/>
    <everything:EverythingCard.Footer>
        <everything:EverythingButton Content="确定"/>
    </everything:EverythingCard.Footer>
</everything:EverythingCard>

<!-- 自定义内边距 -->
<everything:EverythingCard 
    Header="标题"
    Padding="24"
    HeaderPadding="0,0,0,16"
    FooterPadding="16,12,0,0"
    CardVariant="Outlined">
    <TextBlock Text="自定义内边距的内容"/>
</everything:EverythingCard>
```

### EverythingTextBox

增强型文本输入框，支持占位符和图标。

| 属性 | 类型 | 描述 |
|------|------|------|
| Placeholder | string | 占位符文本 |
| PlaceholderBrush | Brush | 占位符颜色 |
| Icon | object | 图标内容 |
| ClearButtonVisible | bool | 是否显示清除按钮 |
| TextBoxVariant | TextBoxVariant | 输入框变体：Default, Filled, Outlined |
| CornerRadius | CornerRadius | 圆角半径 |

**示例：**

```xml
<everything:EverythingTextBox 
    Placeholder="请输入用户名"
    TextBoxVariant="Filled"/>
```

### EverythingToggleSwitch

圆角矩形开关控件，支持垂直三色渐变和流畅的切换动画。

| 属性 | 类型 | 描述 |
|------|------|------|
| SwitchWidth | double | 开关宽度（默认50） |
| SwitchHeight | double | 开关高度（默认26） |
| ThumbSize | double | 滑块大小（默认22） |
| CheckedGradientStartColor | Color | 开启状态渐变起始颜色 |
| CheckedGradientEndColor | Color | 开启状态渐变中间颜色 |
| UncheckedBackground | Brush | 关闭状态背景色 |
| ThumbBrush | Brush | 滑块颜色（默认白色） |
| OnText | string | 开启状态显示文本 |
| OffText | string | 关闭状态显示文本 |
| TextPlacement | Dock | 文本位置：Left 或 Right |

**样式特点：**

- **圆角矩形设计**：轨道和滑块均采用圆角矩形（CornerRadius=4）
- **渐变背景**：开启状态显示垂直三色渐变
- **阴影效果**：轨道和滑块带有轻微阴影

**动画效果：**

- **背景过渡**：渐变背景淡入淡出，0.2秒过渡
- **滑块滑动**：滑块平滑移动，0.25秒 CubicEase 缓动
- **文本变化**：文本透明度随状态变化

**示例：**

```xml
<!-- 基础开关 -->
<everything:EverythingToggleSwitch/>

<!-- 带文本的开关 -->
<everything:EverythingToggleSwitch 
    OnText="开启" 
    OffText="关闭" 
    TextPlacement="Right"/>

<!-- 自定义颜色 -->
<everything:EverythingToggleSwitch 
    IsChecked="True"
    CheckedGradientStartColor="#FF5833" 
    CheckedGradientEndColor="#D43030"/>
```

### EverythingComboBox

渐变色组合框控件，支持下拉菜单动画和悬停效果。

| 属性 | 类型 | 描述 |
|------|------|------|
| Placeholder | string | 占位符文本 |
| PlaceholderBrush | Brush | 占位符颜色 |
| Icon | object | 图标内容 |
| CornerRadius | CornerRadius | 圆角半径 |

**样式特点：**

- **渐变背景**：组合框使用水平三色渐变（#00ACF0 → #0078D4 → #00ACF0）
- **白色文字**：默认文字颜色为白色
- **下拉箭头**：白色箭头图标

**动画效果：**

- **打开动画**：下拉菜单从0.9缩放到1，同时淡入（0.2秒）
- **悬停动画**：菜单项轻微放大（1.02倍）并显示浅灰色背景
- **选中效果**：选中项显示垂直渐变背景和白色文字

**示例：**

```xml
<!-- 基础组合框 -->
<everything:EverythingComboBox Placeholder="请选择一个选项...">
    <ComboBoxItem>选项一</ComboBoxItem>
    <ComboBoxItem>选项二</ComboBoxItem>
    <ComboBoxItem>选项三</ComboBoxItem>
</everything:EverythingComboBox>

<!-- 带图标的组合框 -->
<everything:EverythingComboBox Placeholder="选择国家...">
    <everything:EverythingComboBox.Icon>
        <Path Data="M12,2C6.48,2..." Fill="White" Width="18" Height="18"/>
    </everything:EverythingComboBox.Icon>
    <ComboBoxItem>中国</ComboBoxItem>
    <ComboBoxItem>美国</ComboBoxItem>
</everything:EverythingComboBox>
```

## 测试程序

项目包含一个完整的测试程序 `EverythingUI.Demo`，展示所有控件的使用方法和效果。

### 运行测试程序

```bash
# 编译解决方案
dotnet build EverythingUI.WPF.sln

# 运行测试程序
.\EverythingUI.Demo\bin\Debug\net8.0-windows\EverythingUI.Demo.exe
```

### 测试程序功能

测试程序采用多页面设计，左侧导航栏可切换不同页面：

- **按钮** - 默认渐变按钮、11种预设颜色、不同圆角
- **开关** - 基础开关、带文本开关、不同颜色方案、禁用状态
- **输入框** - 基础输入框
- **组合框** - 渐变色组合框、带图标组合框
- **卡片** - 3 种卡片样式（Default、Elevated、Outlined）、带页眉页脚
- **侧边栏** - 多种配色方案
- **综合示例** - 完整的登录表单

### 项目结构

```
EverythingUI.Demo/
├── Views/                      # 页面文件夹
│   ├── ButtonPage.xaml         # 按钮页面
│   ├── ToggleSwitchPage.xaml   # 开关页面
│   ├── TextBoxPage.xaml        # 输入框页面
│   ├── ComboBoxPage.xaml       # 组合框页面
│   ├── CardPage.xaml           # 卡片页面
│   ├── SideBarPage.xaml        # 侧边栏页面
│   └── ExamplesPage.xaml       # 综合示例页面
├── MainWindow.xaml             # 主窗口
└── MainWindow.xaml.cs          # 页面切换逻辑
```

## 主题样式

### 颜色资源

库中定义了一套完整的颜色资源，可在项目中直接使用：

```xml
<!-- 主色调 -->
<SolidColorBrush x:Key="PrimaryBrush" Color="#5B7FFF"/>
<SolidColorBrush x:Key="SecondaryBrush" Color="#FF6B9D"/>

<!-- 功能色 -->
<SolidColorBrush x:Key="SuccessBrush" Color="#52C41A"/>
<SolidColorBrush x:Key="WarningBrush" Color="#FAAD14"/>
<SolidColorBrush x:Key="ErrorBrush" Color="#FF4D4F"/>
<SolidColorBrush x:Key="InfoBrush" Color="#1890FF"/>

<!-- 中性色 -->
<SolidColorBrush x:Key="Gray900Brush" Color="#1F2937"/>
<SolidColorBrush x:Key="Gray600Brush" Color="#6B7280"/>
<SolidColorBrush x:Key="Gray300Brush" Color="#E5E7EB"/>
```

### 样式资源

```xml
<!-- 基础按钮样式 -->
<Style x:Key="BaseButtonStyle" TargetType="Button"/>

<!-- 输入框样式 -->
<Style x:Key="DefaultTextBoxStyle" TargetType="TextBox"/>
<Style x:Key="FilledTextBoxStyle" TargetType="TextBox"/>

<!-- 卡片样式 -->
<Style x:Key="DefaultCardStyle" TargetType="Border"/>
<Style x:Key="ElevatedCardStyle" TargetType="Border"/>
<Style x:Key="OutlinedCardStyle" TargetType="Border"/>
```

## 项目结构

```
WPF/
├── EverythingUI.WPF/           # UI 组件库项目
│   ├── Controls/               # 自定义控件
│   │   ├── EverythingButton.cs
│   │   ├── EverythingCard.cs
│   │   ├── EverythingComboBox.cs
│   │   ├── EverythingSideBar.cs
│   │   ├── EverythingSideBarItem.cs
│   │   ├── EverythingTextBox.cs
│   │   └── EverythingToggleSwitch.cs
│   ├── Themes/                 # 控件主题
│   │   ├── Generic.xaml
│   │   ├── EverythingButton.xaml
│   │   ├── EverythingCard.xaml
│   │   ├── EverythingComboBox.xaml
│   │   ├── EverythingScrollBar.xaml
│   │   ├── EverythingSideBar.xaml
│   │   ├── EverythingTextBox.xaml
│   │   └── EverythingToggleSwitch.xaml
│   ├── Styles/                 # 样式定义
│   │   ├── Colors.xaml
│   │   ├── Fonts.xaml
│   │   ├── ButtonStyles.xaml
│   │   ├── TextBoxStyles.xaml
│   │   └── CardStyles.xaml
│   ├── Converters/             # 值转换器
│   │   ├── BooleanToTextConverter.cs
│   │   ├── NullToVisibilityConverter.cs
│   │   └── IconPlaceholderMarginConverter.cs
│   └── Assets/                 # 资源文件
│
├── EverythingUI.Demo/          # 测试程序项目
│   ├── Views/                  # 页面文件夹
│   │   ├── ButtonPage.xaml
│   │   ├── ToggleSwitchPage.xaml
│   │   ├── TextBoxPage.xaml
│   │   ├── ComboBoxPage.xaml
│   │   ├── CardPage.xaml
│   │   ├── SideBarPage.xaml
│   │   └── ExamplesPage.xaml
│   ├── App.xaml
│   ├── MainWindow.xaml         # 主窗口
│   ├── MainWindow.xaml.cs      # 页面切换逻辑
│   └── EverythingUI.Demo.csproj
│
└── EverythingUI.WPF.sln        # 解决方案文件
```

## 依赖项

- .NET 8.0
- Microsoft.Xaml.Behaviors.Wpf (1.1.77)


## 系统要求

- Windows 10 版本 1809 或更高版本
- .NET 8.0 SDK 或运行时

## 贡献

欢迎提交 Issue 和 Pull Request！

## 许可证

本项目采用 [MIT](LICENSE) 许可证开源。

## 更新日志

### 计划发布
- 初始版本发布
- 添加 EverythingButton 控件，支持垂直三色渐变和11种预设颜色方案
- 添加 EverythingCard 控件，支持 Padding、HeaderPadding、FooterPadding 内边距属性
- 添加 EverythingTextBox 控件
- 添加 EverythingComboBox 控件，支持渐变色和动画效果
- 添加基础主题和样式系统
- 添加测试程序 EverythingUI.Demo
- 添加 EverythingSideBar 侧边栏控件
- 支持自定义渐变颜色
- 添加 EverythingToggleSwitch 开关控件
- 添加自定义滚动条样式

---

**Made with ❤️ by EverythingUI Team**
