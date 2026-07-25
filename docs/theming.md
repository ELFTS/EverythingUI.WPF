# 主题样式

## 颜色系统概述

EverythingUI.WPF 使用统一的渐变颜色系统，所有控件支持垂直三色渐变效果：

```
起始色(Offset 0) → 中间色(Offset 0.5) → 起始色(Offset 1)
```

配合全局统一的白色光泽层（`GlossBrush`），实现玻璃拟物化立体效果。

## 全局主题资源

所有控件的渐变颜色都引用 4 个全局动态资源，由 `ThemeManager` 在运行时统一注入并动态切换：

| 资源键 | 类型 | 说明 |
|-------|------|------|
| `GlobalGradientStartColor` | Color | 渐变起始色（Offset 0 与 1 使用） |
| `GlobalGradientEndColor` | Color | 渐变中间色（Offset 0.5 使用） |
| `GlobalTrackColor` | Color | 轨道底色 |
| `GlobalTrackBrush` | SolidColorBrush | 轨道画刷（绑定 `GlobalTrackColor`） |

默认值在 [Styles/GradientColors.xaml](../EverythingUI.WPF/Styles/GradientColors.xaml) 中以蓝色预设，应用启动时通过 `ThemeManager.Initialize` 用当前主题色覆盖。模板通过 `{DynamicResource …}` 引用，主题色变更时所有控件自动响应。

> `GlobalColorName` 也会同步写入，作为只读的当前颜色枚举值观测用途。

## 统一白色光泽层 (GlossBrush)

所有支持光泽的控件共享同一个光泽层画刷资源，定义在 [Styles/GradientColors.xaml](../EverythingUI.WPF/Styles/GradientColors.xaml)：

```xml
<LinearGradientBrush x:Key="GlossBrush" StartPoint="0,0" EndPoint="0,1">
    <GradientStop Offset="0" Color="#CCFFFFFF"/>   <!-- 80% 白色 -->
    <GradientStop Offset="1" Color="#33FFFFFF"/>   <!-- 20% 白色 -->
</LinearGradientBrush>
```

### 光泽层规格

| 属性 | 值 |
|------|-----|
| 渐变方向 | 垂直（Top → Bottom） |
| 渐变类型 | 二段式 |
| 起始色 | `#CCFFFFFF`（80% 白色） |
| 结束色 | `#33FFFFFF`（20% 白色） |
| 高度 | 容器高度的 50%（通过 `HalfHeightConverter`） |
| 对齐方式 | `VerticalAlignment="Top"` |
| 引用方式 | `{DynamicResource GlossBrush}` |

### 支持光泽层的控件

| 控件 | 说明 |
|------|------|
| EverythingButton | 按钮表面半高光泽 |
| EverythingCheckBox | 选中状态时显示光泽 |
| EverythingRadioButton | 选中状态时显示光泽 |
| EverythingComboBox | 下拉按钮表面光泽 + 下拉选中项光泽（Opacity=0.6） |
| EverythingToggleSwitch | 开启状态轨道光泽 |
| EverythingSideBar | 选中滑动指示器光泽（跟随动画） |
| EverythingToolBar | 工具栏选中浮动指示器光泽，固定半透明显示（Opacity=0.6） |
| EverythingSlider | 轨道 + 滑块两处光泽 |
| EverythingProgressBar | 进度填充区域顶部光泽 + 扫光效果 + 阻力感宽度动画 |

### 不使用统一光泽的控件

- **EverythingTextBox** — 文本框使用无光泽层的内阴影样式
- **EverythingCircularProgressBar** — 圆弧进度条不需要光泽增强，使用渐变圆弧与阻力感进度动画
- **EverythingCard** — 卡片容器不需要光泽增强
- **EverythingIconListBox** — 图标列表项使用浮动指示器内联光泽
- **EverythingScrollBar** — 滚动条有独立的拟物化样式体系
- **EverythingOverlayDialog** — 全屏遮罩对话框使用固定黑色遮罩、动画与毛玻璃效果

## ThemeManager 主题管理器

`ThemeManager` 提供全局主题颜色切换功能，作为所有控件主题色的唯一来源。应用启动时必须调用 `Initialize` 注入初始主题色。

核心 API：
```csharp
// 初始化主题色（必须，应用启动时调用一次）
ThemeManager.Initialize();                  // 使用库默认颜色（ColorHelper.DefaultColorName）
ThemeManager.Initialize(ColorName.Cyan);    // 自定义默认颜色

// 切换全局主题颜色（所有控件自动响应）
ThemeManager.ChangeColor(ColorName.Red);

// 获取当前颜色名称
var current = ThemeManager.CurrentColorName;

// 订阅颜色变更事件
ThemeManager.ColorChanged += (sender, colorName) =>
{
    Console.WriteLine($"主题已切换为: {colorName}");
};
```

`ThemeManager` 通过 `UpdateGlobalResources(ColorName)` 一次性写入 `GlobalColorName`、`GlobalGradientStartColor`、`GlobalGradientEndColor`、`GlobalTrackColor`、`GlobalTrackBrush`，控件模板中的 `{DynamicResource …}` 引用自动随之刷新。

## 默认颜色配置

默认颜色由 `ColorHelper.DefaultColorName` 唯一控制，作为 `ThemeManager.Initialize()` 的默认参数：

```csharp
ColorHelper.DefaultColorName  // 默认颜色名称（ColorName.Blue）
```

主题相关颜色通过 `ColorHelper` 三个方法统一解析，不再提供 `Default*Color` 派生属性，避免分散的硬编码：

```csharp
ColorHelper.GetGradientStartColor(ColorName)   // 取得某颜色的渐变起始色
ColorHelper.GetGradientEndColor(ColorName)     // 取得某颜色的渐变中间色
ColorHelper.GetTrackColor(ColorName)            // 取得某颜色的轨道色
ColorName.GetGradientColors()                  // 同时返回 (start, end)，由 ColorHelper 实现
```

应用启动时通过 `ThemeManager.Initialize` 指定默认主题颜色，所有控件将自动响应：

```csharp
// 使用库默认颜色（ColorHelper.DefaultColorName）
ThemeManager.Initialize();

// 或自定义默认颜色（如青色）
ThemeManager.Initialize(ColorName.Cyan);
```

修改 `ColorHelper.DefaultColorName` 即可全局调整默认颜色，无需在各处同步硬编码值。

## 预设颜色

### 主要颜色（11种）

| 颜色名称 | 起始色 | 中间色 |
|---------|--------|--------|
| **蓝色**（默认） | `#00ACF0` | `#0078D4` |
| **红色** | `#FF5833` | `#D43030` |
| **绿色** | `#A0D605` | `#19A654` |
| **橙色** | `#FFC300` | `#FF8D1A` |
| **黑色** | `#808080` | `#383838` |
| **灰色** | `#E5E5E5` | `#A6A6A6` |
| **白色** | `#FFFFFF` | `#E6E6E6` |
| **黄色** | `#FFEB3B` | `#FFC400` |
| **青色** | `#00BAAD` | `#00998F` |
| **紫色** | `#AC33C1` | `#8D2C9E` |
| **粉色** | `#F7D7EC` | `#FF9CDB` |

### 扩展颜色（8种）

| 颜色名称 | 起始色 | 中间色 |
|---------|--------|--------|
| **靛蓝** | `#5B7FFF` | `#4A6BE5` |
| **天蓝** | `#38BDF8` | `#0EA5E9` |
| **翠绿** | `#34D399` | `#10B981` |
| **玫瑰** | `#FB7185` | `#E11D48` |
| **琥珀** | `#FBBF24` | `#D97706` |
| **蓝紫** | `#8B5CF6` | `#7C3AED` |
| **珊瑚** | `#FF7F7F` | `#FF5252` |
| **薄荷** | `#6EE7B7` | `#34D399` |

## 中性色资源

```xml
<!-- 文字颜色 -->
<SolidColorBrush x:Key="TextPrimaryBrush" Color="#1F2937"/>
<SolidColorBrush x:Key="TextSecondaryBrush" Color="#6B7280"/>

<!-- 背景颜色 -->
<SolidColorBrush x:Key="CardBackgroundBrush" Color="#FFFFFF"/>
<SolidColorBrush x:Key="Gray100Brush" Color="#F3F4F6"/>
<SolidColorBrush x:Key="Gray200Brush" Color="#E5E7EB"/>
<SolidColorBrush x:Key="Gray300Brush" Color="#D1D5DB"/>
```

## 辅助工具

### HalfHeightConverter

值转换器，将绑定的高度值减半，用于光泽层实现半高效果：

```xml
Height="{Binding ActualHeight, RelativeSource={RelativeSource TemplatedParent},
          Converter={x:Static controls:HalfHeightConverter.Instance}}"
```

### CircularArcHelper

圆弧几何生成辅助类，用于圆形进度条控件。模板不再使用转换器绑定，控件代码会在 `AnimatedValue` 变化时调用该辅助类逐帧生成圆弧：

- 输入：当前动画值、最小值、最大值、控件直径、线条粗细
- 输出：`Geometry` 对象（`PathGeometry` 或 `EllipseGeometry`）
- 特殊处理：0% 返回空几何，100% 返回完整椭圆，半径按 `StrokeThickness` 自动内缩

## 颜色使用建议

| 场景 | 推荐颜色 |
|------|---------|
| 主要操作 | 蓝色（默认） |
| 成功/确认 | 绿色 |
| 警告/注意 | 橙色、黄色 |
| 危险/删除 | 红色 |
| 次要操作 | 灰色、黑色 |
| 特殊功能 | 紫色、粉色 |
| 清新/信息 | 青色、天蓝 |
