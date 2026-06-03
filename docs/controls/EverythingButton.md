# EverythingButton - 多功能渐变按钮控件

支持垂直三色渐变、统一白色光泽层（GlossBrush）、自定义颜色和流畅动画。

## 属性

| 属性                 | 类型           | 描述           |
| ------------------ | ------------ | ------------ |
| Text               | string       | 按钮文本内容         |
| CornerRadius       | CornerRadius | 圆角半径         |
| Icon               | object       | 图标内容         |
| IconPlacement      | Dock         | 图标位置         |
| ColorName          | ColorName    | 颜色名称（默认 Blue） |

> **ColorManager 说明**：`ColorName` 属性由 `ColorManager` 静态类管理，通过附加属性实现颜色同步。

## 事件

| 事件                 | 委托类型                  | 描述           |
| ------------------ | ------------------- | ------------ |
| Click              | MouseButtonEventHandler | 点击事件（ MouseButtonEventArgs ） |

### Click 事件

`Click` 事件是 EverythingButton 的重写事件，使用 `MouseButtonEventHandler` 委托，提供完整的鼠标信息：

- **事件类型**：冒泡路由事件（ `RoutingStrategy.Bubble` ）
- **触发时机**：在基类 Click 事件之后触发
- **可用信息**：
  - 点击位置（ `GetPosition()` ）
  - 鼠标按键（ `ChangedButton` ）
  - 点击次数（ `ClickCount` ）
  - 设备状态（ `Mouse.PrimaryDevice` ）

#### 使用方式

**XAML:**
```xml
<everything:EverythingButton Text="点击我"
                             Click="OnButtonClick"/>
```

**C#:**
```csharp
private void OnButtonClick(object sender, MouseButtonEventArgs e)
{
    var button = sender as EverythingButton;
    
    // 获取点击位置（相对于按钮）
    var position = e.GetPosition(button);
    Console.WriteLine($"点击位置: X={position.X}, Y={position.Y}");
    
    // 获取按键信息
    Console.WriteLine($"按键: {e.ChangedButton}, 点击次数: {e.ClickCount}");
}
```

## 视觉效果

- **垂直三色渐变**：起始色 → 中间色 → 起始色
- **统一白色光泽层（GlossBrush）**：全局统一的白色光泽层资源，增强立体感
- **阴影效果**：外阴影（悬停）和内阴影（按下）

## 统一光泽层

本控件使用全局统一的白色光泽层资源 `GlossBrush`（定义在 `Styles/GradientColors.xaml`）：

- **渐变规格**：二段式半高，顶部 `#CCFFFFFF`(80%白) → 底部 `#33FFFFFF`(20%白)
- **实现方式**：通过 `{DynamicResource GlossBrush}` 引用，配合 `HalfHeightConverter` 实现高度减半
- **定位**：`VerticalAlignment="Top"` 顶部对齐
- **裁剪**：`ClipToBounds="True"` 防止溢出

## 动画效果

- **悬停动画**：按钮轻微放大（1.02倍）并显示外阴影，内容同步放大
- **按下动画**：按钮缩小（0.98倍）并显示内阴影（顶部和左右），内容同步缩小
- **过渡时间**：悬停0.2秒，按下0.1秒，释放0.15秒
- **缓动函数**：CubicEase (EaseOut)

## 使用示例

```xml
<!-- 默认蓝色按钮（使用 Text 属性） -->
<everything:EverythingButton Text="默认按钮"/>

<!-- 使用颜色名称 -->
<everything:EverythingButton Text="红色按钮" ColorName="Red"/>
<everything:EverythingButton Text="绿色按钮" ColorName="Green"/>
<everything:EverythingButton Text="橙色按钮" ColorName="Orange"/>
<everything:EverythingButton Text="紫色按钮" ColorName="Purple"/>
<everything:EverythingButton Text="粉色按钮" ColorName="Pink"/>
```

### C# 代码示例

```csharp
// 使用 Text 属性设置按钮文本
var button = new EverythingButton
{
    Text = "点击我",
    ColorName = ColorName.Blue,
    CornerRadius = new CornerRadius(8)
};

// 也可以在运行时动态修改 Text 属性
button.Text = "新的文本";
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色。
