using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace WPF.Custom_
{
    public class RadialGradientGreen : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        => new RadialGradientBrush
        {
            Center = new Point(0.5, 0.5),
            RadiusY = 1.7,
            RadiusX = 0.5,
            GradientStops =
                {
                    new GradientStop { Color = Colors.Green, Offset = 0 },
                    new GradientStop { Color = Colors.Black, Offset = 1 }
                }
        };

    }
}
