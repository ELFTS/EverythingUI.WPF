# 主题样式

## 颜色系统概述

EverythingUI.WPF 使用统一的渐变颜色系统，所有控件支持垂直三色渐变效果：

```
起始色(Offset 0) → 中间色(Offset 0.5) → 起始色(Offset 1)
```

配合顶部半透明白色光泽层（`#60FFFFFF → #20FFFFFF → #00FFFFFF`），实现玻璃拟物化立体效果。

## 使用颜色资源

### 方式一：直接使用颜色资源（推荐）

```xml
<everything:EverythingButton 
    Content="红色按钮"
    GradientStartColor="{StaticResource GradientRedStart}"
    GradientEndColor="{StaticResource GradientRedEnd}"/>
```

### 方式二：使用渐变刷资源

```xml
<Border Background="{StaticResource GradientBlueBrush}"/>
```

### 方式三：自定义颜色

```xml
<everything:EverythingButton 
    Content="自定义"
    GradientStartColor="#FF5833"
    GradientEndColor="#D43030"/>
```

## 预设颜色资源

### 主要颜色（11种）

| 颜色名称 | 起始色 | 中间色 | 资源键 |
|---------|--------|--------|--------|
| **蓝色**（默认） | `#00ACF0` | `#0078D4` | `GradientBlueStart` / `GradientBlueEnd` |
| **红色** | `#FF5833` | `#D43030` | `GradientRedStart` / `GradientRedEnd` |
| **绿色** | `#A0D605` | `#19A654` | `GradientGreenStart` / `GradientGreenEnd` |
| **橙色** | `#FFC300` | `#FF8D1A` | `GradientOrangeStart` / `GradientOrangeEnd` |
| **黑色** | `#808080` | `#383838` | `GradientBlackStart` / `GradientBlackEnd` |
| **灰色** | `#E5E5E5` | `#A6A6A6` | `GradientGrayStart` / `GradientGrayEnd` |
| **白色** | `#FFFFFF` | `#E6E6E6` | `GradientWhiteStart` / `GradientWhiteEnd` |
| **黄色** | `#FFEB3B` | `#FFC400` | `GradientYellowStart` / `GradientYellowEnd` |
| **青色** | `#00BAAD` | `#00998F` | `GradientCyanStart` / `GradientCyanEnd` |
| **紫色** | `#AC33C1` | `#8D2C9E` | `GradientPurpleStart` / `GradientPurpleEnd` |
| **粉色** | `#F7D7EC` | `#FF9CDB` | `GradientPinkStart` / `GradientPinkEnd` |

### 扩展颜色（8种）

| 颜色名称 | 起始色 | 中间色 | 资源键 |
|---------|--------|--------|--------|
| **靛蓝** | `#5B7FFF` | `#4A6BE5` | `GradientIndigoStart` / `GradientIndigoEnd` |
| **天蓝** | `#38BDF8` | `#0EA5E9` | `GradientSkyStart` / `GradientSkyEnd` |
| **翠绿** | `#34D399` | `#10B981` | `GradientEmeraldStart` / `GradientEmeraldEnd` |
| **玫瑰** | `#FB7185` | `#E11D48` | `GradientRoseStart` / `GradientRoseEnd` |
| **琥珀** | `#FBBF24` | `#D97706` | `GradientAmberStart` / `GradientAmberEnd` |
| **蓝紫** | `#8B5CF6` | `#7C3AED` | `GradientVioletStart` / `GradientVioletEnd` |
| **珊瑚** | `#FF7F7F` | `#FF5252` | `GradientCoralStart` / `GradientCoralEnd` |
| **薄荷** | `#6EE7B7` | `#34D399` | `GradientMintStart` / `GradientMintEnd` |

## 渐变刷资源

每个颜色都提供对应的 `LinearGradientBrush` 资源：

```xml
<!-- 使用渐变刷 -->
<Border Background="{StaticResource GradientBlueBrush}"/>
<Border Background="{StaticResource GradientRedBrush}"/>
<Border Background="{StaticResource GradientGreenBrush}"/>
```

可用的渐变刷资源：
- `GradientWhiteBrush`
- `GradientBlackBrush`
- `GradientGrayBrush`
- `GradientRedBrush`
- `GradientOrangeBrush`
- `GradientYellowBrush`
- `GradientGreenBrush`
- `GradientCyanBrush`
- `GradientBlueBrush`
- `GradientPurpleBrush`
- `GradientPinkBrush`
- `GradientIndigoBrush`
- `GradientSkyBrush`
- `GradientEmeraldBrush`
- `GradientRoseBrush`
- `GradientAmberBrush`
- `GradientVioletBrush`
- `GradientCoralBrush`
- `GradientMintBrush`

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
