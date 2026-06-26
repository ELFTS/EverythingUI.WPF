# EverythingButton - 多功能渐变按钮控件

支持垂直三色渐变、统一白色光泽层（GlossBrush）、胶囊按钮、长按触发和流畅动画。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Text | string | null | 按钮文本内容 |
| IsCapsule | bool | false | 是否为胶囊按钮（完全圆角） |
| IsLongPressEnabled | bool | false | 是否启用长按触发模式 |
| LongPressDuration | TimeSpan | 0:0:0.7 | 长按触发时长 |
| Icon | object | null | 图标内容 |
| IconPlacement | Dock | Left | 图标位置 |

## 事件

| 事件 | 委托类型 | 描述 |
|------|----------|------|
| Click | MouseButtonEventHandler | 点击事件（MouseButtonEventArgs） |
| LongPress | MouseButtonEventHandler | 长按触发事件（仅在 `IsLongPressEnabled=True` 时触发） |

### Click 事件

`Click` 事件是 EverythingButton 的重写事件，使用 `MouseButtonEventHandler` 委托，提供完整的鼠标信息：

- **事件类型**：冒泡路由事件（`RoutingStrategy.Bubble`）
- **触发时机**：在基类 Click 事件之后触发
- **可用信息**：
  - 点击位置（`GetPosition()`）
  - 鼠标按键（`ChangedButton`）
  - 点击次数（`ClickCount`）
  - 设备状态（`Mouse.PrimaryDevice`）

#### 使用方式

**XAML:**
```xml
<everything:EverythingButton Text="点击我" Click="OnButtonClick"/>
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

### LongPress 事件

当 `IsLongPressEnabled=True` 时，按钮按下保持指定时长后会同时触发 `Click` 和 `LongPress` 事件：

- **触发条件**：按下后保持 `LongPressDuration` 时长不释放
- **触发顺序**：先触发 `Click`，再触发 `LongPress`
- **离开/释放**：提前释放或移出按钮区域则取消触发
- **进度视觉**：按下时按钮表面显示一层白色半透明填充，在 `LongPressDuration` 时长内从左到右覆盖整个按钮，释放或取消时回退并淡出

```xml
<everything:EverythingButton Text="长按我"
    IsLongPressEnabled="True"
    LongPressDuration="0:0:0.8"
    LongPress="OnLongPress"/>
```

## 视觉样式

- **固定圆角**：默认圆角 6px，不支持自定义圆角
- **胶囊按钮**：`IsCapsule=True` 时圆角根据按钮实际高度自动计算（高度的一半），保证光泽层、内阴影与主体圆角完全一致
- **垂直三色渐变**：起始色 → 中间色 → 起始色
- **统一白色光泽层（GlossBrush）**：全局统一的白色光泽层资源（定义在 `Styles/GradientColors.xaml`），增强立体感
- **阴影效果**：外阴影（悬停）和内阴影（按下），按下时外阴影隐藏

## 动画效果

- **悬停动画**：按钮轻微放大（1.02倍）并显示外阴影，内容同步放大（启用 ClearTypeHint 保持文字清晰）
- **按下动画**：按钮缩小（0.98倍）并隐藏外阴影、显示内阴影（顶部和左右）、隐藏光泽层，内容同步缩小
- **过渡时间**：悬停0.2秒，按下0.1秒，释放0.15秒

## 使用示例

```xml
<!-- 默认按钮 -->
<everything:EverythingButton Text="默认按钮"/>

<!-- 胶囊按钮 -->
<everything:EverythingButton Text="胶囊按钮" IsCapsule="True" Padding="24,8"/>

<!-- 长按触发按钮 -->
<everything:EverythingButton Text="长按触发"
    IsLongPressEnabled="True"
    LongPressDuration="0:0:0.8"
    LongPress="OnLongPress"/>
```

### C# 代码示例

```csharp
// 使用 Text 属性设置按钮文本
var button = new EverythingButton
{
    Text = "点击我"
};

// 也可以在运行时动态修改 Text 属性
button.Text = "新的文本";
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
