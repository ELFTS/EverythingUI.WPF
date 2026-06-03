<div align="center">
    <img width="150" src="/logo.png"></img>
</div>
<h1 align="center">万物界面库WPF</h1>
<h4 align="center">一个拟物化、漂亮的 WPF UI 组件库，提供丰富的控件和主题样式。</h4>
<div align="center">

[![GitHub Release](https://img.shields.io/github/v/release/ELFTS/EverythingUI.WPF?label=正式版)](https://github.com/ELFTS/EverythingUI.WPF/releases/latest)
[![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/ELFTS/EverythingUI.WPF/total?label=总下载量)](https://github.com/ELFTS/EverythingUI.WPF/releases)
[![Stars](https://img.shields.io/github/stars/ELFTS/EverythingUI.WPF?style=flat\&label=收藏)](https://github.com/ELFTS/EverythingUI.WPF/stargazers)
[![GitHub Issues or Pull Requests](https://img.shields.io/github/issues/ELFTS/EverythingUI.WPF?label=问题)](https://github.com/ELFTS/EverythingUI.WPF/issues)

</div>

***

## 特性

- **拟物化设计** - 采用拟物化设计语言，还原真实质感，界面简洁美观
- **丰富控件库** - 提供多种常用控件（14种）
- **垂直三色渐变** - 支持自定义垂直三色渐变效果，打造精致视觉层次
- **统一白色光泽层** - 全局统一的二段式半高光泽效果（顶部80%白 → 底部20%白），所有控件共享同一画刷资源
- **流畅动画体验** - 悬停、按下、选中状态均带有平滑过渡动画；侧边栏选中项滑动动画
- **预设颜色方案** - 20种精心调配的渐变配色，即开即用
- **ColorName 属性** - 使用颜色名称即可快速切换主题色，简单直观
- **ColorManager 静态管理器** - 统一的颜色管理方案，通过附加属性实现跨控件颜色同步
- **ThemeManager 主题管理器** - 支持运行时全局切换主题色，所有控件自动响应颜色变更
- **高度可定制** - 通过依赖属性轻松自定义控件外观和行为

## 快速开始

查看 [快速开始指南](docs/quick-start.md) 了解如何安装和使用控件。

### 使用 ColorName 属性

所有控件支持直接使用颜色名称属性：

```xml
<!-- 只需一行代码即可设置颜色 -->
<everything:EverythingButton Content="红色按钮" ColorName="Red"/>
<everything:EverythingCheckBox Content="绿色复选框" ColorName="Green" IsChecked="True"/>
<everything:EverythingProgressBar Value="50" ColorName="Orange"/>
```

支持的颜色名称：`White`, `Black`, `Gray`, `Red`, `Orange`, `Yellow`, `Green`, `Cyan`, `Blue`, `Purple`, `Pink`, `Indigo`, `Sky`, `Emerald`, `Rose`, `Amber`, `Violet`, `Coral`, `Mint`

### 统一光泽层

所有控件使用全局统一的光泽层资源 `GlossBrush`：

```xml
<!-- 在 Styles/GradientColors.xaml 中统一定义 -->
<LinearGradientBrush x:Key="GlossBrush" StartPoint="0,0" EndPoint="0,1">
    <GradientStop Offset="0" Color="#CCFFFFFF"/>   <!-- 80% 白色 -->
    <GradientStop Offset="1" Color="#33FFFFFF"/>   <!-- 20% 白色 -->
</LinearGradientBrush>
```

支持光泽层的控件：Button、TextBox、CheckBox、RadioButton、ComboBox、ToggleSwitch、SideBar、ToolBar、Slider。通过 `{DynamicResource GlossBrush}` 引用，配合 `HalfHeightConverter` 实现半高显示。

## 控件列表

| 控件                                | 描述             | 光泽层 | 文档                                                   |
| ---------------------------------- | --------------- | ------ | ---------------------------------------------------- |
| **EverythingButton**              | 多功能渐变按钮控件  | ✅     | [文档](docs/controls/EverythingButton.md)              |
| **EverythingSideBar**             | 侧边栏导航控件     | ✅     | [文档](docs/controls/EverythingSideBar.md)             |
| **EverythingIconListBox**         | 图标列表框控件     | —      | [文档](docs/controls/EverythingIconListBox.md)         |
| **EverythingToolBar**             | 工具栏控件        | ✅     | [文档](docs/controls/EverythingToolBar.md)             |
| **EverythingComboBox**            | 组合框控件        | ✅     | [文档](docs/controls/EverythingComboBox.md)            |
| **EverythingSlider**              | 滑块控件         | ✅     | [文档](docs/controls/EverythingSlider.md)              |
| **EverythingProgressBar**         | 进度条控件        | —      | [文档](docs/controls/EverythingProgressBar.md)         |
| **EverythingCircularProgressBar** | 圆形进度条控件     | —      | [文档](docs/controls/EverythingCircularProgressBar.md) |
| **EverythingToggleSwitch**        | 开关控件         | ✅     | [文档](docs/controls/EverythingToggleSwitch.md)        |
| **EverythingCheckBox**            | 复选框控件        | ✅     | [文档](docs/controls/EverythingCheckBox.md)            |
| **EverythingRadioButton**         | 单选框控件        | ✅     | [文档](docs/controls/EverythingRadioButton.md)         |
| **EverythingCard**                | 卡片控件         | —      | [文档](docs/controls/EverythingCard.md)                |
| **EverythingTextBox**             | 文本框控件        | ✅     | [文档](docs/controls/EverythingTextBox.md)             |
| **EverythingScrollBar**           | 滚动条控件        | —      | [文档](docs/controls/EverythingScrollBar.md)           |

## 测试程序

项目包含一个完整的测试程序 `EverythingUI.Demo`，展示所有控件的使用方法和效果。

```bash
# 编译解决方案
dotnet build EverythingUI.WPF.sln

# 运行测试程序
.\EverythingUI.Demo\bin\Debug\net8.0-windows\EverythingUI.Demo.exe
```

## 主题样式

查看 [主题样式文档](docs/theming.md) 了解可用的颜色资源和渐变配色方案。

## 依赖项

- .NET 8.0
- Microsoft.Xaml.Behaviors.Wpf (1.1.77)

## 系统要求

- Windows 7 或更高版本
- .NET 8.0 SDK 或运行时

## 贡献

欢迎提交 Issue 和 Pull Request！

## 许可证与赞助

查看 [授权与赞助文档](docs/license.md) 了解详细的授权信息和赞助方式。

***

**Copyright © 2021-2026 万物永存技术工作室**
