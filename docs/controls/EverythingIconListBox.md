# EverythingIconListBox - 图标列表框控件

网格布局的图标列表控件，支持图标在上文字在下、自动换行、自定义尺寸和多种交互事件。

## 属性

| 属性                 | 类型           | 描述               |
| ------------------ | ------------ | ---------------- |
| ItemWidth          | double       | 列表项宽度（默认80）      |
| ItemHeight         | double       | 列表项高度（默认80）      |
| IconSize           | double       | 图标大小（默认28）       |
| TextFontSize       | double       | 文字字体大小（默认12）     |
| IconTextSpacing    | double       | 图标与文字间距（默认6）     |
| CornerRadius       | CornerRadius | 列表框圆角半径          |
| ItemCornerRadius   | CornerRadius | 列表项圆角半径（默认8）     |
| GradientStartColor | Color        | 选中状态渐变起始颜色（默认蓝色） |
| GradientEndColor   | Color        | 选中状态渐变中间颜色（默认深蓝） |
| ItemsSource        | object       | 数据源              |
| SelectedItem       | object       | 当前选中项            |
| SelectedIndex      | int          | 当前选中索引           |

## EverythingIconListBoxItem 属性

| 属性   | 类型          | 描述   |
| ---- | ----------- | ---- |
| Text | string      | 显示文本 |
| Icon | ImageSource | 图标源  |

## 视觉样式

- **布局方式**：网格布局，自动换行，类似应用启动器
- **默认状态**：透明背景
- **悬停状态**：白色渐变背景，轻微放大动画
- **选中状态**：垂直三色渐变背景 + 顶部半透明白色光泽，白色文字，放大动画
- **阴影效果**：列表项带有阴影，悬停和选中时阴影加深
- **文字换行**：长文本自动换行，自适应列表项宽度

## 动画效果

- **悬停动画**：轻微放大（1.03倍），阴影加深
- **选中动画**：放大（1.05倍），阴影进一步增强
- **过渡时间**：0.15-0.2秒，CubicEase 缓动

## 交互事件

| 事件              | 描述                       |
| --------------- | ------------------------ |
| ItemClick       | 单击列表项时触发（延迟300ms，用于区分双击） |
| ItemDoubleClick | 双击列表项时触发                 |
| ItemRightClick  | 右键单击列表项时触发               |

## 使用颜色资源

```xml
<!-- 默认蓝色 -->
<everything:EverythingIconListBox ItemWidth="100" ItemHeight="100">
    <!-- 列表项... -->
</everything:EverythingIconListBox>

<!-- 使用预设颜色资源 -->
<everything:EverythingIconListBox ItemWidth="100" ItemHeight="100"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}">
    <!-- 列表项... -->
</everything:EverythingIconListBox>

<everything:EverythingIconListBox ItemWidth="100" ItemHeight="100"
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}">
    <!-- 列表项... -->
</everything:EverythingIconListBox>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色资源。

## 使用示例

```xml
<everything:EverythingIconListBox 
    ItemWidth="100"
    ItemHeight="100"
    IconSize="32"
    TextFontSize="12">
    <everything:EverythingIconListBox.ItemsSource>
        <x:Array Type="everything:EverythingIconListBoxItem">
            <everything:EverythingIconListBoxItem Text="文档">
                <everything:EverythingIconListBoxItem.Icon>
                    <BitmapImage UriSource="pack://application:,,,/YourAssembly;component/Assets/document.png"/>
                </everything:EverythingIconListBoxItem.Icon>
            </everything:EverythingIconListBoxItem>
        </x:Array>
    </everything:EverythingIconListBox.ItemsSource>
</everything:EverythingIconListBox>
```

## 事件处理

```csharp
private void IconListBox_ItemClick(object sender, EverythingIconListBoxItemClickEventArgs e)
{
    var item = e.ClickedItem as EverythingIconListBoxItem;
    MessageBox.Show($"单击了: {item?.Text}");
}
```
