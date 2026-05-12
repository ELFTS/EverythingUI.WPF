# 主题样式

## 颜色系统概述

EverythingUI.WPF 使用统一的渐变颜色系统，所有控件支持垂直三色渐变效果：

```
起始色(Offset 0) → 中间色(Offset 0.5) → 起始色(Offset 1)
```

配合顶部半透明白色光泽层（`#60FFFFFF → #20FFFFFF → #00FFFFFF`），实现玻璃拟物化立体效果。

## 使用 ColorName 属性

所有控件支持直接使用颜色名称属性，这是唯一推荐的方式：

```xml
<everything:EverythingButton Content="红色按钮" ColorName="Red"/>
<everything:EverythingCheckBox Content="绿色复选框" ColorName="Green" IsChecked="True"/>
<everything:EverythingProgressBar Value="50" ColorName="Orange"/>
```

**支持的 ColorName 值：**
- `White`, `Black`, `Gray`
- `Red`, `Orange`, `Yellow`, `Green`, `Cyan`, `Blue`, `Purple`, `Pink`
- `Indigo`, `Sky`, `Emerald`, `Rose`, `Amber`, `Violet`, `Coral`, `Mint`

## 预设颜色

### 主要颜色（11种）

| 颜色名称 | 起始色 | 中间色 |
|---------|--------|--------|
| **蓝色**（默认） | `#00ACF0` | `#0078D4` |
| **红色** | `#FF5833` | `#D43030` |
| **绿色** | `#A0D605` | `#19A654` |
| **橙色** | `#FFC300` | `#FF8D1A` |
| **黑色** | `#808080` | `#383838` |
| **灰色** | `#E5E5E5` | `#A6A6A6` |
| **白色** | `#FFFFFF` | `#E6E6E6` |
| **黄色** | `#FFEB3B` | `#FFC400` |
| **青色** | `#00BAAD` | `#00998F` |
| **紫色** | `#AC33C1` | `#8D2C9E` |
| **粉色** | `#F7D7EC` | `#FF9CDB` |

### 扩展颜色（8种）

| 颜色名称 | 起始色 | 中间色 |
|---------|--------|--------|
| **靛蓝** | `#5B7FFF` | `#4A6BE5` |
| **天蓝** | `#38BDF8` | `#0EA5E9` |
| **翠绿** | `#34D399` | `#10B981` |
| **玫瑰** | `#FB7185` | `#E11D48` |
| **琥珀** | `#FBBF24` | `#D97706` |
| **蓝紫** | `#8B5CF6` | `#7C3AED` |
| **珊瑚** | `#FF7F7F` | `#FF5252` |
| **薄荷** | `#6EE7B7` | `#34D399` |

## 中性色资源

```xml
<!-- 文字颜色 -->
<SolidColorBrush x:Key="TextPrimaryBrush" Color="#1F2937"/>
<SolidColorBrush x:Key="TextSecondaryBrush" Color="#6B7280"/>

<!-- 背景颜色 -->
<SolidColorBrush x:Key="CardBackgroundBrush" Color="#FFFFFF"/>
<SolidColorBrush x:Key="Gray100Brush" Color="#F3F4F6"/>
<SolidColorBrush x:Key="Gray200Brush" Color="#E5E7EB"/>
<SolidColorBrush x:Key="Gray300Brush" Color="#D1D5DB"/>
```

## 颜色使用建议

| 场景 | 推荐颜色 |
|------|---------|
| 主要操作 | 蓝色（默认） |
| 成功/确认 | 绿色 |
| 警告/注意 | 橙色、黄色 |
| 危险/删除 | 红色 |
| 次要操作 | 灰色、黑色 |
| 特殊功能 | 紫色、粉色 |
| 清新/信息 | 青色、天蓝 |
