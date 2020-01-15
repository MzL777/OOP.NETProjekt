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
    public class LinearGradientGray : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        => new LinearGradientBrush
        {
            StartPoint = new Point(0, 0.2),
            EndPoint = new Point(1, 0.8),
            GradientStops =
                {
                    new GradientStop { Color = Colors.Gray, Offset = 0 },
                    new GradientStop { Color = Colors.White, Offset = 1 }
                }
        };

    }
}
