# ColorName 颜色名称属性

EverythingUI.WPF 使用 `ColorName` 属性来设置控件颜色，这是唯一推荐的方式。

## 使用方式

```xml
<!-- 只需一个属性，直观明了 -->
<everything:EverythingButton Content="红色按钮" ColorName="Red"/>
```

## 支持 ColorName 的控件

以下控件都支持 `ColorName` 属性：

| 控件 | 默认颜色 | 说明 |
|-----|---------|-----|
| EverythingButton | Blue | 按钮控件 |
| EverythingCheckBox | Blue | 复选框控件 |
| EverythingRadioButton | Blue | 单选框控件 |
| EverythingToggleSwitch | Blue | 开关控件 |
| EverythingSlider | Blue | 滑块控件 |
| EverythingProgressBar | Blue | 进度条控件 |
| EverythingCircularProgressBar | Blue | 圆形进度条控件 |
| EverythingComboBox | Gray | 组合框控件（默认灰色） |
| EverythingSideBar | Blue | 侧边栏控件 |
| EverythingToolBar | Blue | 工具栏控件 |

## 可用的颜色名称

### 基础颜色（11种）

| 颜色名称 | 示例 |
|---------|-----|
| `White` | 白色 |
| `Black` | 黑色 |
| `Gray` | 灰色 |
| `Red` | 红色 |
| `Orange` | 橙色 |
| `Yellow` | 黄色 |
| `Green` | 绿色 |
| `Cyan` | 青色 |
| `Blue` | 蓝色（默认） |
| `Purple` | 紫色 |
| `Pink` | 粉色 |

### 扩展颜色（8种）

| 颜色名称 | 示例 |
|---------|-----|
| `Indigo` | 靛蓝 |
| `Sky` | 天蓝 |
| `Emerald` | 翠绿 |
| `Rose` | 玫瑰 |
| `Amber` | 琥珀 |
| `Violet` | 蓝紫 |
| `Coral` | 珊瑚 |
| `Mint` | 薄荷 |

## 使用示例

### 按钮

```xml
<everything:EverythingButton Content="保存" ColorName="Green"/>
<everything:EverythingButton Content="删除" ColorName="Red"/>
<everything:EverythingButton Content="警告" ColorName="Orange"/>
<everything:EverythingButton Content="信息" ColorName="Blue"/>
```

### 复选框

```xml
<everything:EverythingCheckBox Content="同意条款" ColorName="Green" IsChecked="True"/>
<everything:EverythingCheckBox Content="重要通知" ColorName="Red" IsChecked="True"/>
```

### 进度条

```xml
<everything:EverythingProgressBar Value="75" ColorName="Green"/>
<everything:EverythingProgressBar Value="45" ColorName="Orange"/>
<everything:EverythingProgressBar Value="90" ColorName="Red"/>
```

### 开关

```xml
<everything:EverythingToggleSwitch IsChecked="True" ColorName="Green"/>
<everything:EverythingToggleSwitch IsChecked="True" ColorName="Blue"/>
```

## 颜色使用建议

| 场景 | 推荐颜色 |
|-----|---------|
| 主要操作/默认 | Blue |
| 成功/确认/保存 | Green |
| 警告/注意 | Orange、Yellow、Amber |
| 危险/删除/错误 | Red、Rose |
| 次要操作 | Gray、Black |
| 特殊功能 | Purple、Pink、Violet |
| 清新/信息 | Cyan、Sky、Mint |
| 自然/环保 | Emerald、Green |

## 动态切换颜色

可以在代码中动态切换颜色：

```csharp
// 在 ViewModel 中
public ColorName CurrentColor { get; set; } = ColorName.Blue;

// 切换颜色
CurrentColor = ColorName.Green;
OnPropertyChanged(nameof(CurrentColor));
```

```xml
<!-- 绑定到 ViewModel -->
<everything:EverythingButton Content="动态颜色" 
    ColorName="{Binding CurrentColor}"/>
```

## 总结

`ColorName` 属性让颜色设置变得：

- ✅ **简单** - 只需一个属性
- ✅ **直观** - 使用英文颜色名
- ✅ **易维护** - 统一的颜色管理

使用 `ColorName` 属性来设置控件颜色！
