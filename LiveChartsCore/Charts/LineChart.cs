﻿//The MIT License(MIT)

//copyright(c) 2016 Alberto Rodriguez

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Collections.Generic;
using LiveCharts.CoreComponents;

namespace LiveCharts
{
    public class LineChart : Chart, ILine
    {
        public LineChart()
        {
            ShapeHoverBehavior = ShapeHoverBehavior.Dot;
            LineSmoothness = 0.8;

            SetCurrentValue(AxisXProperty, new List<Axis> {DefaultAxes.CleanAxis});
            SetCurrentValue(AxisYProperty, new List<Axis> {DefaultAxes.DefaultAxis});
        }

        #region Properties
        public double LineSmoothness { get; set; }

        #endregion

        #region Overriden Methods

        protected override void PrepareAxes()
        {
            if (!HasValidSeriesAndValues) return;

            base.PrepareAxes();

            foreach (var xi in AxisX)
            {
                xi.CalculateSeparator(this, AxisTags.X);
                if (!Invert) continue;
                if (xi.MaxValue == null) xi.MaxLimit = (Math.Round(xi.MaxLimit/xi.S) + 1)*xi.S;
                if (xi.MinValue == null) xi.MinLimit = (Math.Truncate(xi.MinLimit/xi.S) - 1)*xi.S;
            }

            foreach (var yi in AxisY)
            {
                yi.CalculateSeparator(this, AxisTags.Y);
                if (Invert) continue;
                if (yi.MaxValue == null) yi.MaxLimit = (Math.Round(yi.MaxLimit/yi.S) + 1)*yi.S;
                if (yi.MinValue == null) yi.MinLimit = (Math.Truncate(yi.MinLimit/yi.S) - 1)*yi.S;
            }

            CalculateComponentsAndMargin();
        }

        protected override void CalculateComponentsAndMargin()
        {
            if (Invert) ConfigureYAsIndexed();
            else ConfigureXAsIndexed();

            //This calculation should be done by eaxh axis, not by the chart, to keep this clean...

            //Canvas.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //var lastLabelX = Math.Truncate((Max.X - AxisX.MinLimit) / S.X) * S.X;
            //var longestYLabelSize = GetLongestLabelSize(AxisY, AxisTags.Y);
            //var firstXLabelSize = GetLabelSize(AxisX, AxisX.MinLimit);
            //var lastXLabelSize = GetLabelSize(AxisX, lastLabelX);

            //const int padding = 5;

            //var xCorrectionForLabels = AxisX.ShowLabels
            //    ? (longestYLabelSize.X > firstXLabelSize.X * .5
            //        ? longestYLabelSize.X
            //        : firstXLabelSize.X * .5)
            //    : 0;

            //var yCorrectionForLabels = AxisX.ShowLabels
            //    ? longestYLabelSize.Y * .5
            //    : 0;

            //PlotArea.X = padding * 2 + xCorrectionForLabels;

            //PlotArea.Y = yCorrectionForLabels + padding;

            //PlotArea.Height = Math.Max(0, Canvas.DesiredSize.Height - (padding * 2 + firstXLabelSize.Y) - PlotArea.Y);
            //PlotArea.Width = Math.Max(0, Canvas.DesiredSize.Width - PlotArea.X - padding);

            //var distanceToEnd = ToPlotArea(Max.X - lastLabelX, AxisTags.X) - PlotArea.X;
            //var change = lastXLabelSize.X * .5 - distanceToEnd > 0 ? lastXLabelSize.X * .5 - distanceToEnd : 0;
            //if (change <= PlotArea.Width)
            //    PlotArea.Width -= change;

            base.CalculateComponentsAndMargin();
        }

        #endregion
    }
}