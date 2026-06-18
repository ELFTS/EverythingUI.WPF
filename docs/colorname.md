# ColorName 颜色枚举

`ColorName` 是一个颜色枚举类型，用于表示预设的渐变配色方案。它通过 `ThemeManager` 主题管理器来全局切换应用主题色。

## 默认颜色

默认颜色由 `ColorHelper` 统一管理，作为全局主题、控件属性及回退值的唯一来源：

```csharp
// ColorHelper 提供的默认颜色常量与属性
ColorHelper.DefaultColorName           // 默认颜色名称（ColorName.Blue）
ColorHelper.DefaultGradientStartColor  // 默认渐变起始色
ColorHelper.DefaultGradientEndColor    // 默认渐变结束色
ColorHelper.DefaultTrackColor          // 默认轨道色
```

修改 `ColorHelper.DefaultColorName` 即可全局调整默认颜色，无需在各处同步硬编码值。

## 使用方式

```csharp
// 切换全局主题颜色
ThemeManager.ChangeColor(ColorName.Red);
ThemeManager.ChangeColor(ColorName.Green);
ThemeManager.ChangeColor(ColorName.Blue);
```

## 可用的颜色名称

### 基础颜色（11种）

| 颜色名称 | 示例 | 适用场景 |
|---------|-----|---------|
| `White` | 白色 | 浅色主题 |
| `Black` | 黑色 | 深色主题 |
| `Gray` | 灰色 | 次要操作 |
| `Red` | 红色 | 危险/删除 |
| `Orange` | 橙色 | 警告/注意 |
| `Yellow` | 黄色 | 提示 |
| `Green` | 绿色 | 成功/确认 |
| `Cyan` | 青色 | 信息/清新 |
| `Blue` | 蓝色（默认） | 主要操作 |
| `Purple` | 紫色 | 特殊功能 |
| `Pink` | 粉色 | 强调 |

### 扩展颜色（8种）

| 颜色名称 | 示例 | 适用场景 |
|---------|-----|---------|
| `Indigo` | 靛蓝 | 专业/商务 |
| `Sky` | 天蓝 | 清新/天空 |
| `Emerald` | 翠绿 | 自然/环保 |
| `Rose` | 玫瑰 | 优雅/柔和 |
| `Amber` | 琥珀 | 温暖/警示 |
| `Violet` | 蓝紫 | 创意/设计 |
| `Coral` | 珊瑚 | 活力/热情 |
| `Mint` | 薄荷 | 清爽/健康 |

## 动态切换颜色

```csharp
// 切换为红色主题
ThemeManager.ChangeColor(ColorName.Red);

// 切换为绿色主题
ThemeManager.ChangeColor(ColorName.Green);

// 获取当前颜色
var current = ThemeManager.CurrentColorName;
```