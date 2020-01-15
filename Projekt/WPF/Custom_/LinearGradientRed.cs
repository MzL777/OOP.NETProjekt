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
    public class LinearGradientRed : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        => new LinearGradientBrush
        {
            StartPoint = new Point(0.5, 0),
            EndPoint = new Point(0.5, 1),
            GradientStops =
                {
                    new GradientStop { Color = Colors.Red, Offset = 0 },
                    new GradientStop { Color = Colors.Black, Offset = 1 }
                }
        };

    }
}
