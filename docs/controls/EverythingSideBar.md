# EverythingSideBar - 侧边栏导航控件

支持自定义渐变、高度调整、按钮高度调整、外部滚动条和多种图标显示模式。侧边栏背景和菜单项背景独立区分，选中项显示渐变效果。

## 属性

| 属性                 | 类型                     | 描述                                         |
| ------------------ | ---------------------- | ------------------------------------------ |
| SideBarWidth       | double                 | 侧边栏宽度（默认250）                               |
| SideBarHeight      | double                 | 侧边栏最大高度（默认NaN，自动高度）                        |
| ItemHeight         | double                 | 菜单项/按钮高度（默认44）                             |
| GradientStartColor | Color                  | 渐变起始颜色                                     |
| GradientEndColor   | Color                  | 渐变中间颜色                                     |
| CornerRadius       | CornerRadius           | 圆角半径                                       |
| ItemsSource        | object                 | 菜单项数据源                                     |
| SelectedItem       | object                 | 当前选中项                                      |
| ItemDisplayMode    | SideBarItemDisplayMode | 显示模式：TextOnly, IconOnly, IconLeft, IconTop |

## EverythingSideBarItem 属性

| 属性         | 类型          | 描述                    |
| ---------- | ----------- | --------------------- |
| Text       | string      | 显示文本                  |
| Icon       | ImageSource | 图标源，支持 ImageSource 类型 |
| IconWidth  | double      | 图标宽度（默认22）            |
| IconHeight | double      | 图标高度（默认22）            |
| Tag        | object      | 关联数据对象                |

## 视觉样式

- **侧边栏背景**：使用卡片背景色（CardBackgroundBrush），带阴影效果，可通过 `Background` 属性自定义
- **菜单项默认状态**：透明背景
- **菜单项悬停状态**：浅灰色背景（Gray200Brush）
- **菜单项选中状态**：垂直三色渐变背景，白色文字，阴影效果
- **滚动条位置**：滚动条显示在侧边栏按钮区域外面（右侧）
- **动画效果**：背景淡入淡出，无横向移动动画

## 使用颜色资源

```xml
<!-- 默认蓝色侧边栏 -->
<everything:EverythingSideBar SideBarWidth="220">
    <!-- 菜单项... -->
</everything:EverythingSideBar>

<!-- 使用预设颜色资源 -->
<everything:EverythingSideBar SideBarWidth="220"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}">
    <!-- 菜单项... -->
</everything:EverythingSideBar>

<everything:EverythingSideBar SideBarWidth="220"
    GradientStartColor="{StaticResource GradientGreenStart}"
    GradientEndColor="{StaticResource GradientGreenEnd}">
    <!-- 菜单项... -->
</everything:EverythingSideBar>
```

查看 [主题样式文档](../theming.md) 了解所有可用的颜色资源。

## 自定义背景色

```xml
<!-- 使用内置资源 -->
<everything:EverythingSideBar Background="{DynamicResource Gray100Brush}"/>

<!-- 自定义颜色 -->
<everything:EverythingSideBar Background="#F5F5F5"/>

<!-- 使用画刷 -->
<everything:EverythingSideBar>
    <everything:EverythingSideBar.Background>
        <LinearGradientBrush StartPoint="0,0" EndPoint="1,0">
            <GradientStop Offset="0" Color="#E3F2FD"/>
            <GradientStop Offset="1" Color="#BBDEFB"/>
        </LinearGradientBrush>
    </everything:EverythingSideBar.Background>
</everything:EverythingSideBar>
```

## 使用示例

```xml
<!-- 基础侧边栏（仅文字） -->
<everything:EverythingSideBar SideBarWidth="220">
    <everything:EverythingSideBar.ItemsSource>
        <x:Array Type="everything:EverythingSideBarItem">
            <everything:EverythingSideBarItem Text="首页"/>
            <everything:EverythingSideBarItem Text="产品"/>
            <everything:EverythingSideBarItem Text="订单"/>
        </x:Array>
    </everything:EverythingSideBar.ItemsSource>
</everything:EverythingSideBar>

<!-- 图标在上文字在下 -->
<everything:EverythingSideBar SideBarWidth="100" ItemHeight="72" ItemDisplayMode="IconTop">
    <everything:EverythingSideBar.ItemsSource>
        <x:Array Type="everything:EverythingSideBarItem">
            <everything:EverythingSideBarItem Text="首页">
                <everything:EverythingSideBarItem.Icon>
                    <BitmapImage UriSource="pack://application:,,,/YourAssembly;component/Assets/home.png"/>
                </everything:EverythingSideBarItem.Icon>
            </everything:EverythingSideBarItem>
        </x:Array>
    </everything:EverythingSideBar.ItemsSource>
</everything:EverythingSideBar>
```
