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
    public class RadialGradientRed : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        => new RadialGradientBrush
        {
            Center = new Point(0.5, 0.5),
            RadiusY = 1,
            RadiusX = 0.6,
            GradientStops =
                {
                    new GradientStop { Color = Colors.Red, Offset = 1 },
                    new GradientStop { Color = Colors.Black, Offset = 0 }
                }
        };

    }
}
