# EverythingCard - 卡片控件

带阴影和圆角的卡片容器控件，支持自定义背景和内容。

## 属性

| 属性           | 类型           | 描述       |
| ------------ | ------------ | -------- |
| CornerRadius | CornerRadius | 圆角半径     |
| ShadowDepth  | int          | 阴影深度（0-5）|
| Background   | Brush        | 卡片背景     |
| Padding      | Thickness    | 内边距      |

## 视觉样式

- **圆角设计**：默认圆角，视觉柔和
- **阴影效果**：多层阴影，支持自定义深度
- **背景自定义**：支持任意画刷作为背景

## 使用颜色资源

```xml
<!-- 默认白色卡片 -->
<everything:EverythingCard>
    <TextBlock Text="卡片内容"/>
</everything:EverythingCard>

<!-- 使用渐变背景 -->
<everything:EverythingCard Background="{StaticResource GradientBlueBrush}">
    <TextBlock Text="渐变背景卡片" Foreground="White"/>
</everything:EverythingCard>

<everything:EverythingCard Background="{StaticResource GradientGreenBrush}">
    <TextBlock Text="绿色渐变卡片" Foreground="White"/>
</everything:EverythingCard>

<everything:EverythingCard Background="{StaticResource GradientRedBrush}">
    <TextBlock Text="红色渐变卡片" Foreground="White"/>
</everything:EverythingCard>
```

查看 [主题样式文档](../theming.md) 了解所有可用的渐变刷资源。

## 自定义背景

```xml
<!-- 纯色背景 -->
<everything:EverythingCard Background="#F5F5F5">
    <TextBlock Text="纯色背景卡片"/>
</everything:EverythingCard>

<!-- 自定义渐变 -->
<everything:EverythingCard>
    <everything:EverythingCard.Background>
        <LinearGradientBrush StartPoint="0,0" EndPoint="1,1">
            <GradientStop Offset="0" Color="#FF6B6B"/>
            <GradientStop Offset="1" Color="#4ECDC4"/>
        </LinearGradientBrush>
    </everything:EverythingCard.Background>
    <TextBlock Text="自定义渐变" Foreground="White"/>
</everything:EverythingCard>
```
