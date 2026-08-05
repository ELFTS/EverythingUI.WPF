# EverythingListView - 列表视图控件

支持多列详情视图与简单列表的列表控件，支持列头、交替行、渐变选中高亮和多种交互事件。

## 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| ItemsSource | object | null | 数据源 |
| SelectedItem | object | null | 当前选中项，支持双向绑定 |
| SelectedIndex | int | -1 | 当前选中索引，支持双向绑定 |
| Columns | ObservableCollection&lt;EverythingListViewColumn&gt; | 空 | 列定义集合（为空时使用简单列表模式） |
| ItemHeight | double | 44 | 列表项高度 |
| ShowHeader | bool | true | 是否显示列头（仅多列模式生效） |
| ShowGridLines | bool | false | 是否显示网格线（模板未绑定） |
| IconSize | double | 20 | 简单列表模式下的图标大小 |
| TextFontSize | double | 13 | 文字字体大小（同时影响简单模式与默认单元格模板） |
| HeaderBackground | Brush | null | 列头背景（模板未绑定，列头实际使用 `Gray100Brush`） |

> 注：`SelectedItem`/`SelectedIndex` 与内部 `ListView` 双向同步；`Loaded` 时自动选中第一项。

## EverythingListViewColumn 属性

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Header | string | "" | 列头文本 |
| FieldName | string | "" | 绑定的属性名（为空时需提供 CellTemplate） |
| Width | double | 140 | 列宽（像素） |
| HorizontalContentAlignment | HorizontalAlignment | Left | 单元格内容水平对齐 |
| CellTemplate | DataTemplate | null | 自定义单元格模板（为空时按 FieldName 自动生成文本绑定模板） |

## EverythingListViewItem 属性（简单列表模式）

| 属性 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Text | string | null | 显示文本 |
| Icon | ImageSource | null | 图标源 |
| Tag | object | null | 自定义数据 |
| IsEnabled | bool | true | 是否启用 |

> 简单列表模式下，默认 `ItemTemplate` 为水平 `StackPanel`，包含图标 + 文字（超长自动省略）。

## 事件

事件参数 `EverythingListViewItemEventArgs` 包含 `ClickedItem`（object）与 `MouseEventArgs`（`MouseButtonEventArgs?`，仅 `ItemRightClick` 有值）。

| 事件 | 描述 |
|------|------|
| ItemClick | 单击列表项时触发（延迟以区分双击） |
| ItemDoubleClick | 双击列表项时触发 |
| ItemRightClick | 右键单击列表项时触发（携带原始 `MouseButtonEventArgs`） |

## 视觉样式

- **外层容器**：圆角边框 + 表面背景
- **多列模式**：`Columns` 不为空时使用 `GridView` 布局，显示列头与多列单元格
- **简单模式**：`Columns` 为空时使用默认 `ItemTemplate` 显示图标 + 文字
- **默认状态**：透明背景，手型光标
- **交替行**：偶数行使用浅灰背景
- **悬停**：浅灰背景 + 阴影
- **选中**：主题色渐变 + 顶部光泽层 + 白色文字，带淡入动画
- **列头**：浅灰背景、次要文字色、半粗字体，底部带分隔线，悬停时背景加深，右侧可拖拽调整列宽
- **隐藏列头**：`ShowHeader=False` 时列头折叠隐藏
- **内置滚动条**：集成 `EverythingVerticalScrollBar` / `EverythingHorizontalScrollBar`
- **主题响应**：选中渐变实时跟随全局主题色变化

## 动画效果

选中状态带淡入/淡出动画，主题色变化时选中渐变实时更新。

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
