# EverythingRadioButton - 单选框控件

带渐变效果的单选框控件，支持自定义颜色、统一白色光泽层（GlossBrush）和分组。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| GroupName | string | null | 分组名称 |
| IsChecked | bool | false | 是否选中 |
| BoxSize | double | 22 | 单选框大小 |
| DotBrush | Brush | 白色 | 圆点标记颜色 |

## 视觉样式

- **选中状态**：主题色垂直渐变（`PrimaryVerticalBrush`）+ 顶部半高白色光泽层（`GlossBrush`），白色圆点居中
- **未选中状态**：白灰渐变背景 + 内阴影层，灰色边框
- **光泽层（统一）**：使用 `GlossBrush`，顶部半高显示
- **圆形外观**：基于 `BoxSize` 实现正圆
- **阴影**：外阴影保持立体外观，选中/未选中均显示
- **按下状态**：边框变深，内阴影背景显示主题色预览效果

## 动画效果

- **选中动画**：圆点由缩放淡入显示
- **取消选中动画**：圆点缩放淡出
- **背景过渡**：选中背景淡入/淡出，内阴影反向交叉

## 使用示例

```xml
<!-- 默认蓝色 -->
<everything:EverythingRadioButton Content="选项 A" GroupName="Group1" IsChecked="True"/>
```

### 单选组示例

```xml
<StackPanel>
    <TextBlock Text="请选择：" Margin="0,0,0,10"/>
    <everything:EverythingRadioButton Content="选项 A" GroupName="Group1" IsChecked="True"/>
    <everything:EverythingRadioButton Content="选项 B" GroupName="Group1"/>
    <everything:EverythingRadioButton Content="选项 C" GroupName="Group1"/>
</StackPanel>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
