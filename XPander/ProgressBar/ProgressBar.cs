﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MiscControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-19-2018
// ***********************************************************************
// <copyright file="ProgressBar.cs" company="Zeroit Dev Technologies">
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
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace Zeroit.Framework.MiscControls
{
    #region ProgressBar

    #region Control
    /// <summary>
    /// Represents a Windows progress bar control.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    /// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
    /// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
    /// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
    /// PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER
    /// REMAINS UNCHANGED.
    /// </copyright>
    [ToolboxBitmap(typeof(System.Windows.Forms.ProgressBar))]
    public partial class ZeroitProProgressBar : Control
    {
        #region Events
        /// <summary>
        /// Occurs when the value of the BorderColor property changes.
        /// </summary>
        [Description("Occurs when the value of the BorderColor property is changed on the control.")]
        public event EventHandler<EventArgs> BorderColorChanged;
        /// <summary>
        /// Occurs when the value of the BackgroundColor property changes.
        /// </summary>
        [Description("Occurs when the value of the BackgroundColor property is changed on the control.")]
        public event EventHandler<EventArgs> BackgroundColorChanged;
        /// <summary>
        /// Occurs when the value of the ValueColor property changes.
        /// </summary>
        [Description("Occurs when the value of the ValueColor property is changed on the control.")]
        public event EventHandler<EventArgs> ValueColorChanged;
        #endregion

        #region FieldsPrivate
        /// <summary>
        /// The m background color
        /// </summary>
        private Color m_backgroundColor;
        /// <summary>
        /// The m value color
        /// </summary>
        private Color m_valueColor;
        /// <summary>
        /// The m border color
        /// </summary>
        private Color m_borderColor;
        /// <summary>
        /// The m i minimum
        /// </summary>
        private int m_iMinimum;
        /// <summary>
        /// The m i maximum
        /// </summary>
        private int m_iMaximum;
        /// <summary>
        /// The m i value
        /// </summary>
        private int m_iValue;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the color used for the background rectangle of this control.
        /// </summary>
        /// <value>Type: <see cref="System.Drawing.Color" />
        /// A Color used for the background rectangle of this control.</value>
        [Browsable(true)]
        [Description("The color used for the background rectangle of this control.")]
        public Color BackgroundColor
        {
            get { return this.m_backgroundColor; }
            set
            {
                if (this.m_backgroundColor != value)
                {
                    this.m_backgroundColor = value;
                    OnBackgroundColorChanged(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the color used for the value rectangle of this control.
        /// </summary>
        /// <value>Type: <see cref="System.Drawing.Color" />
        /// A Color used for the value rectangle of this control.</value>
        [Browsable(true)]
        [Description("The color used for the value rectangle of this control.")]
        public Color ValueColor
        {
            get { return this.m_valueColor; }
            set
            {
                if (this.m_valueColor != value)
                {
                    this.m_valueColor = value;
                    OnValueColorChanged(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the border color for the control.
        /// </summary>
        /// <value>Type: <see cref="System.Drawing.Color " />
        /// A Color that represents the border color of the control.</value>
        public Color BorderColor
        {
            get { return this.m_borderColor; }
            set
            {
                if (this.m_borderColor != value)
                {
                    this.m_borderColor = value;
                    OnBorderColorChanged(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        /// <value>The color of the back.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }
        /// <summary>
        /// Gets or sets the maximum value of the range of the control.
        /// </summary>
        /// <value>Type: <see cref="System.Int32" />
        /// The maximum value of the range. The default is 100.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Maximum</exception>
        [Browsable(true)]
        [Description("The upper bound of range this ProgressBar is working with.")]
        public int Maximum
        {
            get { return this.m_iMaximum; }
            set
            {
                if (this.m_iMaximum != value)
                {
                    if (value < 0)
                    {
                        object[] args = new object[] { "Maximum", value.ToString(CultureInfo.CurrentCulture), "Maximum" };
                        throw new ArgumentOutOfRangeException("Maximum", string.Format(CultureInfo.InvariantCulture, Properties.Resources.IDS_InvalidLowBoundArgument, args));
                    }
                    if (this.m_iMinimum > value)
                    {
                        this.m_iMinimum = value;
                    }
                    this.m_iMaximum = value;
                    if (this.m_iValue > this.m_iMaximum)
                    {
                        this.m_iValue = this.m_iMaximum;
                    }
                    UpdatePos();
                }
            }
        }
        /// <summary>
        /// Gets or sets the minimum value of the range of the control.
        /// </summary>
        /// <value>Type: <see cref="System.Int32" />
        /// The minimum value of the range. The default is 0.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Minimum</exception>
        [Browsable(true)]
        [Description("The lower bound of range this ProgressBar is working with.")]
        public int Minimum
        {
            get { return this.m_iMinimum; }
            set
            {
                if (this.m_iMinimum != value)
                {
                    if (value < 0)
                    {
                        object[] args = new object[] { "Minimum", value.ToString(CultureInfo.CurrentCulture), "Minimum" };
                        throw new ArgumentOutOfRangeException("Minimum", string.Format(CultureInfo.InvariantCulture, Properties.Resources.IDS_InvalidLowBoundArgument, args));
                    }
                    if (this.m_iMaximum < value)
                    {
                        this.m_iMaximum = value;
                    }
                    this.m_iMinimum = value;
                    if (this.m_iValue < this.m_iMinimum)
                    {
                        this.m_iValue = this.m_iMinimum;
                    }
                    UpdatePos();
                }
            }
        }
        /// <summary>
        /// Gets or sets the current position of the progress bar.
        /// </summary>
        /// <value>Type: <see cref="System.Int32" />
        /// The position within the range of the progress bar. The default is 0.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Value</exception>
        [Browsable(true)]
        [Description("The current value for the ProgressBar, in the range specified by the minimum and maximum properties.")]
        public int Value
        {
            get { return this.m_iValue; }
            set
            {
                if (this.m_iValue != value)
                {
                    if ((value < this.m_iMinimum) || (value > this.m_iMaximum))
                    {
                        throw new ArgumentOutOfRangeException("Value", string.Format(CultureInfo.InvariantCulture, Properties.Resources.IDS_InvalidBoundArgument, new object[] { "Value", value.ToString(CultureInfo.CurrentCulture), "'minimum'", "'maximum'" }));
                    }
                    this.m_iValue = value;
                    UpdatePos();
                }
            }
        }
        #endregion

        #region MethodsPublic
        /// <summary>
        /// Initializes a new instance of the ProgressBar class.
        /// </summary>
        public ZeroitProProgressBar()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();

            this.m_iMaximum = 100;
            this.m_backgroundColor = Color.FromArgb(20, 20, 255);
            this.m_valueColor = Color.FromArgb(255, 0, 255);
            this.m_borderColor = SystemColors.ActiveBorder;
            this.BackColor = Color.Transparent;
        }
        #endregion

        #region MethodsProtected
        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            using (UseAntiAlias antiAlias = new UseAntiAlias(e.Graphics))
            {
                Graphics graphics = e.Graphics;
                DrawProgressBar(
                    graphics,
                    this.ClientRectangle,
                    this.m_backgroundColor,
                    this.m_valueColor,
                    this.m_borderColor,
                    this.RightToLeft,
                    this.Minimum,
                    this.Maximum,
                    this.Value);

                if (string.IsNullOrEmpty(this.Text) == false)
                {
                    using (UseClearTypeGridFit useClearTypeGridFit = new UseClearTypeGridFit(graphics))
                    {
                        using (SolidBrush textBrush = new SolidBrush(this.ForeColor))
                        {
                            using (StringFormat stringFormat = new StringFormat())
                            {
                                stringFormat.FormatFlags = StringFormatFlags.NoWrap;
                                if (this.RightToLeft == RightToLeft.Yes)
                                {
                                    stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                                }
                                stringFormat.Trimming = StringTrimming.EllipsisCharacter;
                                stringFormat.LineAlignment = StringAlignment.Center;
                                stringFormat.Alignment = StringAlignment.Center;

                                Rectangle stringRectangle = this.ClientRectangle;
                                graphics.DrawString(this.Text, this.Font, textBrush, stringRectangle, stringFormat);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Raises the BorderColor changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
		protected virtual void OnBorderColorChanged(object sender, EventArgs e)
        {
            this.Invalidate(true);
            if (this.BorderColorChanged != null)
            {
                this.BorderColorChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the BackgroundColor changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        protected virtual void OnBackgroundColorChanged(object sender, EventArgs e)
        {
            Invalidate();
            if (this.BackgroundColorChanged != null)
            {
                this.BackgroundColorChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the ValueColor changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        protected virtual void OnValueColorChanged(object sender, EventArgs e)
        {
            Invalidate(true);
            if (this.ValueColorChanged != null)
            {
                this.ValueColorChanged(sender, e);
            }
        }
        #endregion

        #region MethodsPrivate
        /// <summary>
        /// Updates the position.
        /// </summary>
        private void UpdatePos()
        {
            this.Invalidate(true);
        }

        /// <summary>
        /// Draws the progress bar.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <param name="colorBackgroundEnd">The color background end.</param>
        /// <param name="colorValueEnd">The color value end.</param>
        /// <param name="borderColor">Color of the border.</param>
        /// <param name="rightToLeft">The right to left.</param>
        /// <param name="iMinimum">The i minimum.</param>
        /// <param name="iMaximum">The i maximum.</param>
        /// <param name="iValue">The i value.</param>
        private static void DrawProgressBar(
            Graphics graphics,
            Rectangle clientRectangle,
            Color colorBackgroundEnd,
            Color colorValueEnd,
            Color borderColor,
            RightToLeft rightToLeft,
            int iMinimum,
            int iMaximum,
            int iValue)
        {

            Rectangle outerRectangle = GetRectangleBackground(clientRectangle);

            using (GraphicsPath outerRectangleGraphicsPath = GetBackgroundPath(outerRectangle, 4))
            {
                if (outerRectangleGraphicsPath != null)
                {
                    using (LinearGradientBrush gradientBrush = GetGradientBackBrush(outerRectangle, colorBackgroundEnd))
                    {
                        if (gradientBrush != null)
                        {
                            graphics.FillPath(gradientBrush, outerRectangleGraphicsPath);
                        }
                    }

                    // Draws the value rectangle
                    if (iValue > 0)
                    {
                        Rectangle valueRectangle = GetRectangleValue(outerRectangle, rightToLeft, iMinimum, iMaximum, iValue);
                        using (GraphicsPath valueGraphicsPath = GetValuePath(valueRectangle, rightToLeft, 5))
                        {
                            using (LinearGradientBrush gradientBrush = GetGradientBackBrush(valueRectangle, colorValueEnd))
                            {
                                if (gradientBrush != null)
                                {
                                    graphics.FillPath(gradientBrush, valueGraphicsPath);
                                }
                            }
                        }
                    }
                    using (Pen borderPen = new Pen(borderColor))
                    {
                        graphics.DrawPath(borderPen, outerRectangleGraphicsPath);
                    }
                }
            }
        }
        /// <summary>
        /// Gets the rectangle background.
        /// </summary>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <returns>Rectangle.</returns>
        private static Rectangle GetRectangleBackground(Rectangle clientRectangle)
        {
            Rectangle rectangleBackground = clientRectangle;
            rectangleBackground.Inflate(-1, -1);
            return rectangleBackground;
        }
        /// <summary>
        /// Gets the rectangle value.
        /// </summary>
        /// <param name="backgroundRectangle">The background rectangle.</param>
        /// <param name="rightToLeft">The right to left.</param>
        /// <param name="iMinimum">The i minimum.</param>
        /// <param name="iMaximum">The i maximum.</param>
        /// <param name="iValue">The i value.</param>
        /// <returns>Rectangle.</returns>
        private static Rectangle GetRectangleValue(Rectangle backgroundRectangle, RightToLeft rightToLeft, int iMinimum, int iMaximum, int iValue)
        {
            Rectangle valueRectangle = backgroundRectangle;
            int iProgressRange = iMaximum - iMinimum;
            int iValueRange = iValue - iMinimum;
            int iRange = (int)((float)iValueRange / (float)iProgressRange * backgroundRectangle.Width);
            valueRectangle.Width = iRange;
            if (rightToLeft == RightToLeft.Yes)
            {
                valueRectangle.X = backgroundRectangle.Width - valueRectangle.Width;
            }
            return valueRectangle;
        }
        /// <summary>
        /// Gets the background path.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        /// <param name="radius">The radius.</param>
        /// <returns>GraphicsPath.</returns>
        private static GraphicsPath GetBackgroundPath(Rectangle bounds, int radius)
        {
            int x = bounds.X;
            int y = bounds.Y;
            int width = bounds.Width;
            int height = bounds.Height;
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(x, y, radius, radius, 180, 90);				                    //Upper left corner
            graphicsPath.AddArc(x + width - radius, y, radius, radius, 270, 90);			    //Upper right corner
            graphicsPath.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);//Lower right corner
            graphicsPath.AddArc(x, y + height - radius, radius, radius, 90, 90);			    //Lower left corner
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        /// <summary>
        /// Gets the value path.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        /// <param name="rightToLeft">The right to left.</param>
        /// <param name="radius">The radius.</param>
        /// <returns>GraphicsPath.</returns>
        private static GraphicsPath GetValuePath(Rectangle bounds, RightToLeft rightToLeft, int radius)
        {
            int x = bounds.X;
            int y = bounds.Y;
            int width = bounds.Width;
            int height = bounds.Height;
            GraphicsPath graphicsPath = new GraphicsPath();
            if (rightToLeft == RightToLeft.No)
            {
                graphicsPath.AddArc(x, y, radius, radius, 180, 90);				                    //Upper left corner
                graphicsPath.AddLine(x + radius, y, x + width, y);                                  //Upper line
                graphicsPath.AddLine(x + width, y, x + width, y + height);                          //Right line
                graphicsPath.AddArc(x, y + height - radius, radius, radius, 90, 90);			    //Lower left corner
            }
            else
            {
                graphicsPath.AddLine(x, y, width - radius, y);                                      //Upper Line
                graphicsPath.AddArc(x + width - radius, y, radius, radius, 270, 90);                // Upper right corner
                graphicsPath.AddLine(x + width, y + radius, x + width, y + radius + height - (2 * radius)); // right line
                graphicsPath.AddArc(x + width - radius, y + radius + height - (2 * radius), radius, radius, 360, 90); // Lower right corner
                graphicsPath.AddLine(x + width - radius, y + height, x, y + height);                // Lower line
            }
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        /// <summary>
        /// Gets the gradient back brush.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        /// <param name="backColor">Color of the back.</param>
        /// <returns>LinearGradientBrush.</returns>
        private static LinearGradientBrush GetGradientBackBrush(Rectangle bounds, Color backColor)
        {
            if (IsZeroWidthOrHeight(bounds))
            {
                return null;
            }
            LinearGradientBrush linearGradientBrush = linearGradientBrush = new LinearGradientBrush(bounds, Color.White, backColor, LinearGradientMode.Vertical);
            if (linearGradientBrush != null)
            {
                Blend blend = new Blend();
                blend.Positions = new float[] { 0.0F, 0.2F, 0.3F, 0.5F, 0.6F, 0.8F, 1.0F };
                blend.Factors = new float[] { 0.3F, 0.4F, 0.5F, 0.8F, 1.0F, 1.0F, 0.9F };
                linearGradientBrush.Blend = blend;
            }
            return linearGradientBrush;
        }
        /// <summary>
        /// Checks if the rectangle width or height is equal to 0.
        /// </summary>
        /// <param name="rectangle">the rectangle to check</param>
        /// <returns>true if the with or height of the rectangle is 0 else false</returns>
        private static bool IsZeroWidthOrHeight(Rectangle rectangle)
        {
            if (rectangle.Width != 0)
            {
                return (rectangle.Height == 0);
            }
            return true;
        }
        #endregion
    }
    #endregion

    #region Designer Generated Code
    partial class ZeroitProProgressBar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.Size = new System.Drawing.Size(100, 23);
            this.Name = "progressBar";
        }

        #endregion
    }
    #endregion

    #endregion
}
