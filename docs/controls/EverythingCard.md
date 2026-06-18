# EverythingCard - 卡片控件

带阴影和圆角的卡片容器控件，支持多种变体样式和底部区域自定义。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| CardVariant | CardVariant | Default | 卡片变体样式 |
| CornerRadius | CornerRadius | 8 | 圆角半径 |
| ShadowDepth | double | 4.0 | 阴影深度 |
| Footer | object | null | 底部区域内容 |
| FooterTemplate | DataTemplate | null | 底部区域模板 |
| FooterPadding | Thickness | 16,0,16,16 | 底部区域内边距 |

## CardVariant 枚举

| 值 | 描述 |
|----|------|
| Default | 默认样式 |
| Elevated | 浮起样式 |
| Outlined | 描边样式 |

## 视觉样式

- **圆角设计**：默认圆角 8px，视觉柔和
- **阴影效果**：多层阴影，支持通过 `ShadowDepth` 自定义深度
- **无光泽层**：卡片容器控件不使用白色光泽层效果（作为内容承载容器无需光泽增强）
- **底部区域**：支持通过 `Footer` 和 `FooterTemplate` 自定义卡片底部内容

## 使用示例

```xml
<!-- 默认卡片 -->
<everything:EverythingCard>
    <TextBlock Text="卡片内容"/>
</everything:EverythingCard>

<!-- 带底部的卡片 -->
<everything:EverythingCard CardVariant="Elevated" ShadowDepth="6">
    <TextBlock Text="卡片主体内容"/>
    <everything:EverythingCard.Footer>
        <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
            <Button Content="取消" Margin="0,0,8,0"/>
            <Button Content="确定"/>
        </StackPanel>
    </everything:EverythingCard.Footer>
</everything:EverythingCard>

<!-- 使用渐变背景 -->
<everything:EverythingCard Background="{StaticResource GradientBlueBrush}">
    <TextBlock Text="渐变背景卡片" Foreground="White"/>
</everything:EverythingCard>
```

### C# 代码示例

```csharp
// 创建带底部区域的卡片
var card = new EverythingCard
{
    CardVariant = CardVariant.Elevated,
    ShadowDepth = 6.0,
    CornerRadius = new CornerRadius(12),
    FooterPadding = new Thickness(20, 0, 20, 20)
};

// 设置底部内容
card.Footer = new StackPanel
{
    Orientation = Orientation.Horizontal,
    HorizontalAlignment = HorizontalAlignment.Right,
    Children =
    {
        new Button { Content = "取消", Margin = new Thickness(0, 0, 8, 0) },
        new Button { Content = "确定" }
    }
};
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
