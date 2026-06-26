# EverythingListView - 列表视图控件

支持多列详情视图与简单列表的列表控件，支持列头、交替行、渐变选中高亮和多种交互事件。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| ItemsSource | object | null | 数据源 |
| SelectedItem | object | null | 当前选中项 |
| SelectedIndex | int | -1 | 当前选中索引 |
| Columns | ObservableCollection&lt;EverythingListViewColumn&gt; | 空 | 列定义集合（为空时使用简单列表模式） |
| ItemHeight | double | 44 | 列表项高度 |
| ShowHeader | bool | true | 是否显示列头（仅多列模式生效） |
| ShowGridLines | bool | false | 是否显示网格线 |
| IconSize | double | 20 | 简单列表模式下的图标大小 |
| TextFontSize | double | 13 | 文字字体大小 |
| HeaderBackground | Brush | null | 列头背景 |

## EverythingListViewColumn 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Header | string | "" | 列头文本 |
| FieldName | string | "" | 绑定的属性名 |
| Width | double | 140 | 列宽（像素） |
| HorizontalContentAlignment | HorizontalAlignment | Left | 单元格内容水平对齐 |
| CellTemplate | DataTemplate | null | 自定义单元格模板（为空时按 FieldName 自动生成文本绑定） |

## EverythingListViewItem 属性（简单列表模式）

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Text | string | null | 显示文本 |
| Icon | ImageSource | null | 图标源 |
| IconSize | double | 20 | 图标大小 |
| Tag | object | null | 自定义数据 |
| IsEnabled | bool | true | 是否启用 |

## 交互事件

| 事件 | 描述 |
|------|------|
| ItemClick | 单击列表项时触发（延迟区分双击） |
| ItemDoubleClick | 双击列表项时触发 |
| ItemRightClick | 右键单击列表项时触发 |

## 视觉样式

- **多列模式**：当 `Columns` 不为空时，使用 GridView 布局，显示列头与多列单元格
- **简单模式**：当 `Columns` 为空时，显示图标 + 文字的单列列表
- **默认状态**：透明背景
- **交替行**：偶数行使用浅灰背景（Gray100）增强可读性
- **悬停状态**：浅灰背景（Gray200）+ 淡入阴影（BlurRadius: 8, Opacity: 0.12）
- **选中状态**：渐变背景（全局主题色）+ 顶部光泽层（GlossBrush, Opacity=0.6）+ 阴影（BlurRadius: 12, Opacity: 0.25），白色文字，带淡入动画
- **列头**：浅灰背景，次级文字颜色，悬停高亮，底部分隔线
- **内置滚动条**：集成 EverythingScrollBar 样式
- **主题响应**：选中渐变使用动态全局渐变资源，实时响应 `ThemeManager.ColorChanged`

## 动画效果

- **选中淡入动画**：选中时渐变背景与光泽层平滑淡入（DoubleAnimation, 0.18s, CubicEase EaseOut）
- **主题响应**：选中渐变实时跟随全局主题色变化

## 使用示例

### 多列详情视图

```xml
<everything:EverythingListView ItemsSource="{Binding Files}">
    <everything:EverythingListView.Columns>
        <everything:EverythingListViewColumn Header="名称" FieldName="Name" Width="240"/>
        <everything:EverythingListViewColumn Header="大小" FieldName="Size" Width="120"
                                              HorizontalContentAlignment="Right"/>
    </everything:EverythingListView.Columns>
</everything:EverythingListView>
```

### 简单列表（图标 + 文字）

```xml
<everything:EverythingListView>
    <everything:EverythingListView.ItemsSource>
        <x:Array Type="everything:EverythingListViewItem">
            <everything:EverythingListViewItem Text="收件箱">
                <everything:EverythingListViewItem.Icon>
                    <BitmapImage UriSource="pack://application:,,,/YourAssembly;component/Assets/inbox.png"/>
                </everything:EverythingListViewItem.Icon>
            </everything:EverythingListViewItem>
        </x:Array>
    </everything:EverythingListView.ItemsSource>
</everything:EverythingListView>
```

### 隐藏列头

```xml
<everything:EverythingListView ShowHeader="False" ItemsSource="{Binding Files}">
    <everything:EverythingListView.Columns>
        <everything:EverythingListViewColumn Header="名称" FieldName="Name" Width="280"/>
    </everything:EverythingListView.Columns>
</everything:EverythingListView>
```

### 事件处理

```csharp
private void ListView_ItemDoubleClick(object sender, EverythingListViewItemEventArgs e)
{
    if (e.ClickedItem is FileItem file)
        MessageBox.Show($"双击了: {file.Name}");
}
```

查看 [主题样式文档](../theming.md) 了解所有可用的样式资源。
