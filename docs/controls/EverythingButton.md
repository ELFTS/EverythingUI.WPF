# EverythingButton - 多功能渐变按钮控件

支持垂直三色渐变、统一白色光泽层（GlossBrush）、胶囊按钮、长按触发和流畅动画。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Text | string | "" | 按钮文本内容 |
| IsCapsule | bool | false | 是否为胶囊按钮（完全圆角） |
| IsLongPressEnabled | bool | false | 是否启用长按触发模式 |
| LongPressDuration | TimeSpan | 0:0:0.7 | 长按触发时长 |
| Icon | object | null | 图标内容 |

## 事件

| 事件 | 描述 |
|------|------|
| Click | 点击事件，提供点击位置、鼠标按键、点击次数等完整鼠标信息 |

### 长按触发

当 `IsLongPressEnabled=True` 时，按下保持指定时长后会触发 `Click` 事件：

- 按下后保持 `LongPressDuration` 时长不释放即触发
- 提前释放或移出按钮区域则取消触发
- 按下时按钮表面显示一层从左到右覆盖的白色半透明进度填充，释放或取消时回退并淡出

## 视觉样式

- **默认**：垂直三色渐变背景 + 顶部白色光泽层（GlossBrush），固定圆角
- **胶囊按钮**：`IsCapsule=True` 时圆角根据按钮高度自动计算
- **悬停**：轻微放大 + 外阴影
- **按下**：缩小 + 隐藏外阴影，叠加顶部和左右内阴影，隐藏光泽层

## 动画效果

- 悬停时轻微放大并显示外阴影，按下时缩小并隐藏光泽层与外阴影、显示内阴影
- 内容随按钮同步缩放（保持文字清晰）

## 使用示例

**XAML:**
```xml
<!-- 默认按钮 -->
<everything:EverythingButton Text="默认按钮"/>

<!-- 胶囊按钮 -->
<everything:EverythingButton Text="胶囊按钮" IsCapsule="True" Padding="24,8"/>

<!-- 长按触发按钮 -->
<everything:EverythingButton Text="长按触发"
    IsLongPressEnabled="True"
    LongPressDuration="0:0:0.8"
    Click="OnButtonClick"/>
```

**C# 事件处理:**
```csharp
private void OnButtonClick(object sender, MouseButtonEventArgs e)
{
    var button = sender as EverythingButton;
    // 获取点击位置（相对于按钮）
    var position = e.GetPosition(button);
    Console.WriteLine($"点击位置: X={position.X}, Y={position.Y}");
    Console.WriteLine($"按键: {e.ChangedButton}, 点击次数: {e.ClickCount}");
}
```

**C# 创建按钮:**
```csharp
var button = new EverythingButton
{
    Text = "点击我"
};
// 也可在运行时动态修改 Text 属性
button.Text = "新的文本";
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
