# EverythingToolBar - 工具栏控件

水平排列的工具栏控件，支持多种显示模式、自定义尺寸、流畅动画和阴影效果。

## 属性

| 属性                 | 类型                     | 描述                                         |
| ------------------ | ---------------------- | ------------------------------------------ |
| ToolBarHeight      | double                 | 工具栏高度（默认48）                                |
| ItemHeight         | double                 | 工具栏项高度（默认36）                               |
| GradientStartColor | Color                  | 渐变起始颜色（默认蓝色 #00ACF0）                       |
| GradientEndColor   | Color                  | 渐变中间颜色（默认深蓝 #0078D4）                       |
| ItemsSource        | object                 | 工具栏项数据源                                    |
| SelectedItem       | object                 | 当前选中项                                      |
| ItemDisplayMode    | ToolBarItemDisplayMode | 显示模式：TextOnly, IconOnly, IconLeft, IconTop |

## EverythingToolBarItem 属性

| 属性         | 类型          | 描述                    |
| ---------- | ----------- | --------------------- |
| Text       | string      | 显示文本                  |
| Icon       | ImageSource | 图标源，支持 ImageSource 类型 |
| IconWidth  | double      | 图标宽度（默认22）            |
| IconHeight | double      | 图标高度（默认22）            |

## 显示模式

| 模式                | 描述        | 适用场景        |
| ----------------- | --------- | ----------- |
| **TextOnly** (默认) | 纯文本显示     | 最常用的场景，无需图标 |
| **IconOnly**      | 仅显示图标     | 图标按钮组       |
| **IconLeft**      | 图标在左，文字在右 | 需要图标的导航工具栏  |
| **IconTop**       | 图标在上，文字在下 | 垂直布局的工具栏    |

## 使用颜色资源

```xml
<!-- 默认蓝色工具栏 -->
<everything:EverythingToolBar>
    <!-- 工具栏项... -->
</everything:EverythingToolBar>

<!-- 使用预设颜色资源 -->
<everything:EverythingToolBar
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}">
    <!-- 工具栏项... -->
</everything:EverythingToolBar>

<everything:EverythingToolBar
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}">
    <!-- 工具栏项... -->
</everything:EverythingToolBar>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色资源。

## 使用示例

```xml
<!-- 基础工具栏（纯文本，默认模式） -->
<everything:EverythingToolBar>
    <everything:EverythingToolBar.ItemsSource>
        <x:Array Type="everything:EverythingToolBarItem">
            <everything:EverythingToolBarItem Text="首页"/>
            <everything:EverythingToolBarItem Text="产品"/>
            <everything:EverythingToolBarItem Text="订单"/>
            <everything:EverythingToolBarItem Text="设置"/>
        </x:Array>
    </everything:EverythingToolBar.ItemsSource>
</everything:EverythingToolBar>

<!-- 图标在左文字在右 -->
<everything:EverythingToolBar ItemDisplayMode="IconLeft" ItemHeight="44">
    <everything:EverythingToolBar.ItemsSource>
        <x:Array Type="everything:EverythingToolBarItem">
            <everything:EverythingToolBarItem Text="首页">
                <everything:EverythingToolBarItem.Icon>
                    <BitmapImage UriSource="pack://application:,,,/YourAssembly;component/Assets/home.png"/>
                </everything:EverythingToolBarItem.Icon>
            </everything:EverythingToolBarItem>
        </x:Array>
    </everything:EverythingToolBar.ItemsSource>
</everything:EverythingToolBar>
```
