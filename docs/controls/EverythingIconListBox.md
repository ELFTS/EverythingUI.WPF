# EverythingIconListBox - 图标列表框控件

网格布局的图标列表控件，支持图标在上文字在下、自动换行、自定义尺寸和多种交互事件。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| ItemWidth | double | 80 | 列表项宽度 |
| ItemHeight | double | 80 | 列表项高度 |
| IconSize | double | 28 | 图标大小 |
| TextFontSize | double | 12 | 文字字体大小 |
| IconTextSpacing | double | 6 | 图标与文字间距 |
| CornerRadius | CornerRadius | 12 | 列表框圆角半径 |
| ItemCornerRadius | CornerRadius | 8 | 列表项圆角半径 |
| ItemsSource | object | null | 数据源 |
| SelectedItem | object | null | 当前选中项 |
| SelectedIndex | int | -1 | 当前选中索引 |

## EverythingIconListBoxItem 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Text | string | null | 显示文本 |
| Icon | ImageSource | null | 图标源 |
| Tag | object | null | 附加数据 |
| IsEnabled | bool | true | 是否启用 |

## 事件

| 事件 | 描述 |
|------|------|
| ItemClick | 单击列表项时触发（延迟区分双击） |
| ItemDoubleClick | 双击列表项时触发 |
| ItemRightClick | 右键单击列表项时触发 |

## 视觉样式

- **布局方式**：网格布局，自动换行，类似应用启动器
- **默认**：透明背景，无阴影
- **悬停**：浅灰背景 + 淡入阴影
- **选中**：白色文字，由浮动指示器提供视觉高亮
- **浮动指示器**：选中项上方覆盖渐变背景 + 顶部光泽层（GlossBrush）+ 阴影，跟随全局主题实时变化
- **焦点边框**：键盘聚焦时显示主色边框
- **文字换行**：长文本自动换行，自适应列表项宽度
- **内置滚动条**：集成 EverythingScrollBar 样式

## 动画效果

- 切换选中项时浮动指示器平滑滑动到新位置，并带透明度过渡
- 悬停时背景色和阴影平滑过渡

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

### 事件处理

```csharp
private void IconListBox_ItemClick(object sender, EverythingIconListBoxItemClickEventArgs e)
{
    var item = e.ClickedItem as EverythingIconListBoxItem;
    MessageBox.Show($"单击了: {item?.Text}");
}
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
