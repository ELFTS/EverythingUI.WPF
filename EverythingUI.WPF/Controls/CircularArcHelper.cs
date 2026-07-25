using System;
using System.Windows;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public static class CircularArcHelper
{
    public static Geometry CreateGeometry(double value, double minimum, double maximum, double diameter, double strokeThickness)
    {
        if (!IsValidSize(diameter))
            return Geometry.Empty;

        double progress = CalculateProgress(value, minimum, maximum);
        if (progress <= 0)
            return Geometry.Empty;

        double center = diameter / 2;
        double radius = Math.Max(0, (diameter - strokeThickness) / 2);
        if (radius <= 0)
            return Geometry.Empty;

        if (progress >= 1)
        {
            // 完整圆：复用 Geometry.Empty 和创建 EllipseGeometry，不需要更新单例
            return new EllipseGeometry(new Point(center, center), radius, radius);
        }

        return CreateArc(center, radius, progress);
    }

    private static Geometry CreateArc(double center, double radius, double progress)
    {
        double angle = progress * 360 - 90;
        double radians = angle * Math.PI / 180;
        var endPoint = new Point(
            center + radius * Math.Cos(radians),
            center + radius * Math.Sin(radians));

        var figure = new PathFigure
        {
            StartPoint = new Point(center, center - radius),
            IsClosed = false
        };
        var segment = new ArcSegment
        {
            Point = endPoint,
            Size = new Size(radius, radius),
            SweepDirection = SweepDirection.Clockwise,
            IsLargeArc = progress > 0.5
        };
        figure.Segments.Add(segment);

        var geometry = new PathGeometry();
        geometry.Figures.Add(figure);
        geometry.Freeze();
        return geometry;
    }

    private static double CalculateProgress(double value, double minimum, double maximum)
    {
        if (maximum <= minimum || !IsFinite(value) || !IsFinite(minimum) || !IsFinite(maximum))
            return 0;

        double progress = (value - minimum) / (maximum - minimum);
        return Math.Clamp(progress, 0, 1);
    }

    private static bool IsValidSize(double value) => value > 0 && IsFinite(value);

    private static bool IsFinite(double value) => !double.IsNaN(value) && !double.IsInfinity(value);
}
