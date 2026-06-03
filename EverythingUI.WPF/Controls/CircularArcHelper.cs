using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace EverythingUI.WPF.Controls;

public static class CircularArcHelper
{
    public static readonly IMultiValueConverter GeometryConverter = new GeometryConverterImpl();

    private sealed class GeometryConverterImpl : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 4) return CreateEmptyArc();

            if (values is not [double value, double minimum, double maximum, double width])
                return CreateEmptyArc();

            if (width <= 0 || double.IsNaN(width) || double.IsInfinity(width))
                return CreateEmptyArc();

            double progress = CalculateProgress(value, minimum, maximum);
            if (progress < 0.001)
                return CreateEmptyArc();

            double centerX = width / 2;
            double centerY = width / 2;
            double radius = Math.Max(1, (width - 8) / 2);

            try
            {
                if (progress >= 0.999)
                    return CreateFullCircle(centerX, centerY, radius);

                double angleInRadians = (progress * 360 - 90) * Math.PI / 180;
                double endX = centerX + radius * Math.Cos(angleInRadians);
                double endY = centerY + radius * Math.Sin(angleInRadians);

                var geometry = new PathGeometry
                {
                    Figures =
                    [
                        new PathFigure
                        {
                            StartPoint = new Point(centerX, centerY - radius),
                            IsClosed = false,
                            Segments =
                            [
                                new ArcSegment
                                {
                                    Point = new Point(endX, endY),
                                    Size = new Size(radius, radius),
                                    RotationAngle = 0,
                                    IsLargeArc = progress > 0.5,
                                    SweepDirection = SweepDirection.Clockwise
                                }
                            ]
                        }
                    ]
                };

                geometry.Freeze();
                return geometry;
            }
            catch (Exception)
            {
                return CreateEmptyArc();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    private static Geometry CreateEmptyArc()
    {
        var geometry = new PathGeometry
        {
            Figures =
            [
                new PathFigure
                {
                    StartPoint = new Point(50, 50),
                    IsClosed = false,
                    Segments = [new LineSegment { Point = new Point(50.01, 50) }]
                }
            ]
        };
        geometry.Freeze();
        return geometry;
    }

    private static Geometry CreateFullCircle(double centerX, double centerY, double radius)
    {
        var ellipseGeometry = new EllipseGeometry
        {
            Center = new Point(centerX, centerY),
            RadiusX = radius,
            RadiusY = radius
        };
        ellipseGeometry.Freeze();
        return ellipseGeometry;
    }

    private static double CalculateProgress(double value, double minimum, double maximum)
    {
        if (maximum <= minimum) return 0;

        if (double.IsNaN(value) || double.IsInfinity(value) ||
            double.IsNaN(minimum) || double.IsInfinity(minimum) ||
            double.IsNaN(maximum) || double.IsInfinity(maximum))
            return 0;

        double progress = (value - minimum) / (maximum - minimum);
        return Math.Max(0, Math.Min(1, progress));
    }
}
