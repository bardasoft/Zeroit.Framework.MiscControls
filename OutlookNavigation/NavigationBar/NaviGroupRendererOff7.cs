﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MiscControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-19-2018
// ***********************************************************************
// <copyright file="NaviGroupRendererOff7.cs" company="Zeroit Dev Technologies">
//    This program is for creating various controls.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************


using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Zeroit.Framework.MiscControls
{
    /// <summary>
    /// Class NaviGroupRendererOff7.
    /// </summary>
    /// <seealso cref="Zeroit.Framework.MiscControls.NaviGroupRenderer" />
    public class NaviGroupRendererOff7 : NaviGroupRenderer   
   {
        #region Fields

        /// <summary>
        /// The color table
        /// </summary>
        NaviColorTableOff7 colorTable;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NaviGroupRendererOff7"/> class.
        /// </summary>
        public NaviGroupRendererOff7()
      {
         colorTable = new NaviColorTableOff7();
      }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the colors to draw the control with
        /// </summary>
        /// <value>The color table.</value>
        public NaviColorTableOff7 ColorTable
      {
         get { return colorTable; }
         set { colorTable = value; }
      }

        #endregion

        #region Methods

        /// <summary>
        /// Draws the background.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="bounds">The bounds.</param>
        public override void DrawBackground(Graphics g, Rectangle bounds)
      {
         using (Brush b = new SolidBrush(colorTable.Background))
         {
            g.FillRectangle(b, bounds);
         }
      }

        /// <summary>
        /// Draws the text.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="font">The font.</param>
        /// <param name="headerText">The header text.</param>
        /// <param name="rightToLeft">if set to <c>true</c> [right to left].</param>
        public override void DrawText(Graphics g, Rectangle bounds, Font font, string headerText, bool rightToLeft)
      {
         using (Brush brush = new SolidBrush(colorTable.Text))
         {
            if (rightToLeft)
            {
               TextRenderer.DrawText(g, headerText, font, bounds, colorTable.Text,
                  TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis |
                  TextFormatFlags.Right | TextFormatFlags.RightToLeft);
            }
            else
            {
               TextRenderer.DrawText(g, headerText, font, bounds, colorTable.Text,
                  TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            }
         }
      }

        /// <summary>
        /// Draws the header.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="state">The state.</param>
        /// <param name="expanded">if set to <c>true</c> [expanded].</param>
        /// <param name="rightToLeft">if set to <c>true</c> [right to left].</param>
        public override void DrawHeader(Graphics g, Rectangle bounds, InputState state, bool expanded, bool rightToLeft)
      {
         Color dark, light;
         bounds.Height--;

         if (state == InputState.Hovered)
         {
            dark = colorTable.GroupBgHoveredDark;
            light = colorTable.GroupBgHoveredLight;
         }
         else
         {
            dark = colorTable.GroupBgDark;
            light = colorTable.GroupBgLight;
         }

         // Background
         Color[] EndColors = { dark, light, dark };
         float[] ColorPositions = { 0.0f, .50f, 1.0f };

         ColorBlend blend = new ColorBlend();

         blend.Colors = EndColors;
         blend.Positions = ColorPositions;

         if (bounds.Width == 0)
            bounds.Width = 1; // its to prevent an out of memory exception

         //Make the linear brush and assign the custom blend to it
         using (LinearGradientBrush brush = new LinearGradientBrush(new Point(0, 0),
                                                           new Point(bounds.Width, 0),
                                                           Color.White,
                                                           Color.Black))
         {
            brush.InterpolationColors = blend;
            g.FillRectangle(brush, bounds);
         }

         using (Pen pen = new Pen(colorTable.DarkBorder))
         {
            // Dark border
            //g.DrawRectangle(pen, bounds);
            g.DrawLine(pen, new Point(0, 0), new Point(bounds.Width, 0));

            // Light line bottom
            pen.Color = colorTable.GroupBorderLight;
            g.DrawLine(pen, new Point(0, bounds.Height),
               new Point(bounds.Width, bounds.Height));

            // Light inner border
            pen.Color = colorTable.GroupInnerBorder;
            g.DrawLine(pen, new Point(0, 1), new Point(bounds.Width, 1));
            g.DrawLine(pen, new Point(0, 1), new Point(0, bounds.Height - 1));

            // Arrows
            pen.Color = colorTable.ShapesFront;

            //width-7
            //(height/2)+1
            // w=7 h=4
            pen.Width = 1.5f;
            float x = 0;
            float y = 0;

            if (bounds.Height != 0)
               y = (bounds.Height / 2) - 3; // + 1px border and - 4 size

            if (rightToLeft)
               x = 7;
            else
               x = bounds.Width - 7 - 7; // 7 px spacing and - 7 width            

            if (expanded)
            {
               PointF[] points = { new PointF(x, y + 3 + 4), 
                               new PointF(x + 3,y + 4), 
                               new PointF(x + 3 + 3, y + 3 + 4) };
               g.DrawLines(pen, points);

               PointF[] points2 = { new PointF(x, y + 3), 
                               new PointF(x + 3,y ), 
                               new PointF(x + 3 + 3, y + 3) };
               g.DrawLines(pen, points2);
            }
            else
            {
               PointF[] points = { new PointF(x, y + 4), 
                               new PointF(x + 3,y + 3 + 4), 
                               new PointF(x + 3 + 3, y + 4) };
               g.DrawLines(pen, points);

               PointF[] points2 = { new PointF(x, y ), 
                               new PointF(x + 3,y + 3 ), 
                               new PointF(x + 3 + 3, y) };
               g.DrawLines(pen, points2);
            }
         }

      }

        /// <summary>
        /// Draws the hatched panel.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="bounds">The bounds.</param>
        public override void DrawHatchedPanel(Graphics g, Rectangle bounds)
      {
         using (Pen pen = new Pen(colorTable.DashedLineColor))
         {
            pen.DashStyle = DashStyle.Dash;
            g.DrawRectangle(pen, bounds);
         }
      }
      #endregion

      #region Event Handling
      #endregion
   }
}
