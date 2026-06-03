# ColorName 颜色名称属性

EverythingUI.WPF 使用 `ColorName` 属性来设置控件颜色，这是最简单直观的方式。该属性由 `ColorManager` 静态类统一管理。

## ColorManager 工作原理

`ColorManager` 是一个静态辅助类，通过**附加属性**（Attached Properties）机制为所有控件提供颜色管理能力，无需基类继承：

```csharp
public static class ColorManager
{
    // 核心附加属性
    public static readonly DependencyProperty ColorNameProperty = ...;
    public static readonly DependencyProperty GradientStartColorProperty = ...;
    public static readonly DependencyProperty GradientEndColorProperty = ...;

    // 统一更新方法：根据 ColorName 查表设置 GradientStart/EndColor
    public static void UpdateColors(DependencyObject obj);
}
```

**优势**：
- 控件可继承任意基类（`Button`、`CheckBox`、`Control` 等）而不受限制
- 颜色逻辑集中在单一位置，易于维护
- 新控件只需调用 `ColorManager.UpdateColors(this)` 即可接入

## 使用方式

```xml
<!-- 只需一个属性，直观明了 -->
<everything:EverythingButton Content="红色按钮" ColorName="Red"/>
```

## 支持 ColorName 的控件

以下控件都支持 `ColorName` 属性：

| 控件 | 默认颜色 | 说明 |
|-----|---------|-----|
| EverythingButton | Blue | 按钮控件 |
| EverythingCheckBox | Blue | 复选框控件 |
| EverythingRadioButton | Blue | 单选框控件 |
| EverythingToggleSwitch | Blue | 开关控件 |
| EverythingSlider | Blue | 滑块控件 |
| EverythingProgressBar | Blue | 进度条控件 |
| EverythingCircularProgressBar | Blue | 圆形进度条控件 |
| EverythingComboBox | Gray | 组合框控件（默认灰色） |
| EverythingSideBar | Blue | 侧边栏控件 |
| EverythingToolBar | Blue | 工具栏控件 |
| EverythingIconListBox | Blue | 图标列表框控件 |

不支持 `ColorName` 的控件：EverythingCard（纯容器）、EverythingScrollBar（独立样式）、EverythingTextBox（焦点驱动颜色）。

## 可用的颜色名称

### 基础颜色（11种）

| 颜色名称 | 示例 | 适用场景 |
|---------|-----|---------|
| `White` | 白色 | 浅色主题 |
| `Black` | 黑色 | 深色主题 |
| `Gray` | 灰色 | 次要操作 |
| `Red` | 红色 | 危险/删除 |
| `Orange` | 橙色 | 警告/注意 |
| `Yellow` | 黄色 | 提示/ caution |
| `Green` | 绿色 | 成功/确认 |
| `Cyan` | 青色 | 信息/清新 |
| `Blue` | 蓝色（默认） | 主要操作 |
| `Purple` | 紫色 | 特殊功能 |
| `Pink` | 粉色 | 强调 |

### 扩展颜色（8种）

| 颜色名称 | 示例 | 适用场景 |
|---------|-----|---------|
| `Indigo` | 靛蓝 | 专业/商务 |
| `Sky` | 天蓝 | 清新/天空 |
| `Emerald` | 翠绿 | 自然/环保 |
| `Rose` | 玫瑰 | 优雅/柔和 |
| `Amber` | 琥珀 | 温暖/警示 |
| `Violet` | 蓝紫 | 创意/设计 |
| `Coral` | 珊瑚 | 活力/热情 |
| `Mint` | 薄荷 | 清爽/健康 |

## 使用示例

### 按钮

```xml
<everything:EverythingButton Content="保存" ColorName="Green"/>
<everything:EverythingButton Content="删除" ColorName="Red"/>
<everything:EverythingButton Content="警告" ColorName="Orange"/>
<everything:EverythingButton Content="信息" ColorName="Blue"/>
```

### 复选框

```xml
<everything:EverythingCheckBox Content="同意条款" ColorName="Green" IsChecked="True"/>
<everything:EverythingCheckBox Content="重要通知" ColorName="Red" IsChecked="True"/>
```

### 进度条

```xml
<everything:EverythingProgressBar Value="75" ColorName="Green"/>
<everything:EverythingProgressBar Value="45" ColorName="Orange"/>
<everything:EverythingProgressBar Value="90" ColorName="Red"/>
```

### 开关

```xml
<everything:EverythingToggleSwitch IsChecked="True" ColorName="Green"/>
<everything:EverythingToggleSwitch IsChecked="True" ColorName="Blue"/>
```

### 侧边栏

```xml
<everything:EverythingSideBar ColorName="Purple">
    <!-- 导航项... -->
</everything:EverythingSideBar>
```

## 动态切换颜色

可以在代码中动态切换颜色：

```csharp
// 在 ViewModel 中
public ColorName CurrentColor { get; set; } = ColorName.Blue;

// 切换颜色
CurrentColor = ColorName.Green;
OnPropertyChanged(nameof(CurrentColor));
```

```xml
<!-- 绑定到 ViewModel -->
<everything:EverythingButton Content="动态颜色" 
    ColorName="{Binding CurrentColor}"/>
```

## 自定义颜色映射

如果需要扩展颜色，可以在 `ColorManager` 的颜色查找表中添加新的映射：

```csharp
// ColorManager 内部结构示意
private static readonly Dictionary<ColorName, (Color start, Color end)> ColorMap = new()
{
    { ColorName.Red,     (#FF5833, #D43030) },
    { ColorName.Green,   (#A0D605, #19A654) },
    // ... 更多颜色
    { ColorName.Custom,  (#YourStart, #YourEnd) },  // 添加自定义颜色
};
```

## 颜色使用建议

| 场景 | 推荐颜色 |
|-----|---------|
| 主要操作/默认 | Blue |
| 成功/确认/保存 | Green |
| 警告/注意 | Orange、Yellow、Amber |
| 危险/删除/错误 | Red、Rose |
| 次要操作 | Gray、Black |
| 特殊功能 | Purple、Pink、Violet |
| 清新/信息 | Cyan、Sky、Mint |
| 自然/环保 | Emerald、Green |
| 专业/商务 | Indigo |

## 总结

`ColorName` 属性让颜色设置变得：

- ✅ **简单** - 只需一个属性
- ✅ **直观** - 使用英文颜色名
- ✅ **易维护** - 通过 ColorManager 统一管理
- ✅ **灵活** - 支持运行时动态切换

使用 `ColorName` 属性来设置控件颜色！
