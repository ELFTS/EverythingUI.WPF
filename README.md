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
- **丰富控件库** - 提供多种常用控件
- **垂直三色渐变** - 支持自定义垂直三色渐变效果，打造精致视觉层次
- **光泽立体效果** - 顶部透明白色渐变光泽，增强控件立体感
- **流畅动画体验** - 悬停、按下、选中状态均带有平滑过渡动画
- **预设颜色方案** - 11种精心调配的渐变配色，即开即用
- **高度可定制** - 通过依赖属性轻松自定义控件外观和行为

## 快速开始

查看 [快速开始指南](docs/quick-start.md) 了解如何安装和使用控件。

## 控件列表

| 控件                                | 描述        | 文档                                                   |
| --------------------------------- | --------- | ---------------------------------------------------- |
| **EverythingButton**              | 多功能渐变按钮控件 | [文档](docs/controls/EverythingButton.md)              |
| **EverythingSideBar**             | 侧边栏导航控件   | [文档](docs/controls/EverythingSideBar.md)             |
| **EverythingIconListBox**         | 图标列表框控件   | [文档](docs/controls/EverythingIconListBox.md)         |
| **EverythingToolBar**             | 工具栏控件     | [文档](docs/controls/EverythingToolBar.md)             |
| **EverythingComboBox**            | 组合框控件     | [文档](docs/controls/EverythingComboBox.md)            |
| **EverythingSlider**              | 滑块控件      | [文档](docs/controls/EverythingSlider.md)              |
| **EverythingProgressBar**         | 进度条控件     | [文档](docs/controls/EverythingProgressBar.md)         |
| **EverythingCircularProgressBar** | 圆形进度条控件   | [文档](docs/controls/EverythingCircularProgressBar.md) |
| **EverythingToggleSwitch**        | 开关控件      | [文档](docs/controls/EverythingToggleSwitch.md)        |
| **EverythingCheckBox**            | 复选框控件     | [文档](docs/controls/EverythingCheckBox.md)            |
| **EverythingRadioButton**         | 单选框控件     | [文档](docs/controls/EverythingRadioButton.md)         |
| **EverythingCard**                | 卡片控件      | [文档](docs/controls/EverythingCard.md)                |
| **EverythingTextBox**             | 文本框控件     | [文档](docs/controls/EverythingTextBox.md)             |

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
