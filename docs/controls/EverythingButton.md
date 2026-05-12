# EverythingButton - 多功能渐变按钮控件

支持垂直三色渐变、顶部光泽效果、自定义颜色和流畅动画。

## 属性

| 属性                 | 类型           | 描述           |
| ------------------ | ------------ | ------------ |
| Content            | object       | 按钮内容         |
| CornerRadius       | CornerRadius | 圆角半径         |
| Icon               | object       | 图标内容         |
| IconPlacement      | Dock         | 图标位置         |
| ColorName          | ColorName    | 颜色名称（默认 Blue） |

## 视觉效果

- **垂直三色渐变**：起始色 → 中间色 → 起始色
- **顶部光泽效果**：半个透明白色渐变叠加，增强立体感
- **阴影效果**：外阴影（悬停）和内阴影（按下）

## 动画效果

- **悬停动画**：按钮轻微放大（1.02倍）并显示外阴影
- **按下动画**：按钮缩小（0.98倍）并显示内阴影（顶部和左右）
- **过渡时间**：悬停0.2秒，按下0.1秒，释放0.15秒

## 使用示例

```xml
<!-- 默认蓝色按钮 -->
<everything:EverythingButton Content="默认按钮"/>

<!-- 使用颜色名称 -->
<everything:EverythingButton Content="红色按钮" ColorName="Red"/>
<everything:EverythingButton Content="绿色按钮" ColorName="Green"/>
<everything:EverythingButton Content="橙色按钮" ColorName="Orange"/>
<everything:EverythingButton Content="紫色按钮" ColorName="Purple"/>
<everything:EverythingButton Content="粉色按钮" ColorName="Pink"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色。
