# EverythingCheckBox - 复选框控件

带渐变效果的复选框控件，支持自定义颜色、统一白色光泽层（GlossBrush）和三种状态。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| IsChecked | bool? | false | 是否选中（支持三态） |
| IsThreeState | bool | false | 是否启用三态 |
| BoxSize | double | 22 | 复选框大小 |
| CornerRadius | CornerRadius | 6 | 圆角半径 |
| CheckMarkBrush | Brush | 白色 | 勾选标记颜色 |

## 视觉样式

- **选中状态**：主题色垂直渐变（`PrimaryVerticalBrush`）+ 顶部半高白色光泽层（`GlossBrush`），白色勾选标记
- **未选中状态**：白灰渐变背景 + 内阴影层，灰色边框
- **不确定状态**：渐变背景 + 光泽层，白色横线
- **光泽层（统一）**：使用 `GlossBrush`，顶部半高显示
- **阴影**：外阴影保持立体外观
- **悬停状态**：边框与内阴影背景变深
- **按下状态**：边框与内阴影背景进一步加深

## 动画效果

- **选中动画**：勾选标记由缩放淡入显示
- **取消选中动画**：勾选标记缩放淡出
- **不确定动画**：横线横向展开并淡入
- **背景过渡**：选中背景淡入/淡出，内阴影反向交叉

## 使用示例

```xml
<!-- 默认蓝色 -->
<everything:EverythingCheckBox Content="默认蓝色" IsChecked="True"/>
```

### 三态复选框

```xml
<everything:EverythingCheckBox
    Content="三态选项"
    IsThreeState="True"
    IsChecked="{x:Null}"/>
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
