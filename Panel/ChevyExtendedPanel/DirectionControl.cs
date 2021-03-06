﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MiscControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-19-2018
// ***********************************************************************
// <copyright file="DirectionControl.cs" company="Zeroit Dev Technologies">
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
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Zeroit.Framework.MiscControls
{
    #region DirectionCtrl

    /// <summary>
    /// Raised when the direction pointed by this control changes
    /// </summary>
    /// <param name="sender">instance of a directioncontrol object</param>
    /// <param name="e">instance of an object holding the data for the event</param>
    public delegate void DirectionCtrlStyleChangedEvent(object sender, ChangeStyleEventArgs e);

    /// <summary>
    /// A "direction" control class
    /// </summary>
    /// <seealso cref="Zeroit.Framework.MiscControls.ZeroitBufferPanel" />
    [ToolboxItem(false)]
    public class DirectionCtrl : ZeroitBufferPanel
    {
        #region Members

        /// <summary>
        /// Instance of a bool variable telling wheather the mouse is inside this control or not
        /// </summary>
        private bool mouseInside = false;

        /// <summary>
        /// Color used in drawing the arrow
        /// </summary>
        private Color color = Color.DarkGray;

        /// <summary>
        /// Color used in drawing the arrow while the mouse pointer is inside this control
        /// </summary>
        private Color hoverColor = Color.Orange;

        /// <summary>
        /// Holds the direction pointed by this control
        /// </summary>
        private DirectionStyle directionStyle = DirectionStyle.Up;

        /// <summary>
        /// Instance of the image being painted inside the control
        /// </summary>
        private Image image = null;

        /// <summary>
        /// Instance of the image being painted inside the control while the mouse is inside
        /// </summary>
        private Image imageHover = null;

        /// <summary>
        /// Handler for the change "direction" event
        /// </summary>
        public event DirectionCtrlStyleChangedEvent handlerStyleChange = null;
        #endregion

        #region ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectionCtrl"/> class.
        /// </summary>
        public DirectionCtrl() : base()
        {
            //set up the mouse events
            this.MouseEnter += new EventHandler(OnMouseEnterEvent);
            this.MouseLeave += new EventHandler(OnMouseLeaveEvent);
            this.MouseClick += new MouseEventHandler(OnMouseClickEvent);
            //InitializeGraphicPath();
        }


        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the direction style.
        /// </summary>
        /// <value>The direction style.</value>
        [Category("Apperance")]
        [Description("Get/Set where this control points to")]
        [DefaultValue(DirectionStyle.Up)]
        public DirectionStyle DirectionStyle
        {
            get
            {
                return directionStyle;
            }
            set
            {
                if (directionStyle != value)
                {
                    image.Dispose();
                    image = null;

                }
                directionStyle = value;
            }
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        [Category("Appearance")]
        [Description("Get/Set the color used for the direction control")]
        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                if (value != color)
                {
                    color = value;
                    InitializeImage();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover.
        /// </summary>
        /// <value>The color of the hover.</value>
        [Category("Appearance")]
        [Description("Get/Set the color used for the direction control")]
        public Color HoverColor
        {
            get
            {
                return hoverColor;
            }

            set
            {
                if (value != hoverColor)
                {
                    hoverColor = value;
                    InitializeImage();
                }
            }
        }
        #endregion

        #region Private

        /// <summary>
        /// Creates the two images used in the WM_PAINT handler
        /// </summary>
        private void InitializeImage()
        {
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
            if (imageHover != null)
            {
                imageHover.Dispose();
                imageHover = null;
            }

            Brush brush = new SolidBrush(color);

            image = CreateImage(brush, true);

            brush.Dispose();
            brush = new SolidBrush(hoverColor);

            imageHover = CreateImage(brush, false);
            brush.Dispose();
        }

        /// <summary>
        /// Method used in creating the two images
        /// </summary>
        /// <param name="brush">the brush used to draw the arrow</param>
        /// <param name="flag">true if the mouse is inside the control</param>
        /// <returns>a bitmap image displaying an arrow</returns>
        private Image CreateImage(Brush brush, bool flag)
        {
            string imgText = "»";

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;


            int imageWidth = this.Width;
            int imageHeight = this.Height;

            if (imageWidth == 0)
            {
                imageWidth = 1;
            }
            if (imageHeight == 0)
            {
                imageHeight = 1;
            }
            Image image = new Bitmap(imageWidth, imageHeight, PixelFormat.Format32bppPArgb);
            //create the Graphics object
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.Transparent);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            SizeF sizeString = g.MeasureString(imgText, this.Font);

            Font font = null;
            if (flag)
            {
                font = new Font("Arial", 12, FontStyle.Bold);
            }
            else
            {
                font = new Font("Arial", 15, FontStyle.Bold);
            }
            g.DrawString(imgText, font, brush, new RectangleF(0, 0, this.Width, this.Height), format);
            g.Dispose();
            image.RotateFlip((RotateFlipType)((int)directionStyle));
            return image;
        }

        #endregion

        #region WM_PAINT
        /// <summary>
        /// Handles the paint event
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (null == image)
            {
                InitializeImage();
            }

            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            if (!mouseInside)
            {
                e.Graphics.DrawImage(image, new Point(0, 0));
            }
            else
            {
                e.Graphics.DrawImage(imageHover, new Point(0, 0));
            }
            base.OnPaint(e);
        }
        #endregion

        #region Override
        /// <summary>
        /// If resized the images displaying the arrow have to be recreated
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            InitializeImage();
            Update();
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the <see cref="E:MouseEnterEvent" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void OnMouseEnterEvent(object sender, EventArgs e)
        {

            mouseInside = true;

            Refresh();
        }

        /// <summary>
        /// Handles the <see cref="E:MouseLeaveEvent" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void OnMouseLeaveEvent(object sender, EventArgs e)
        {

            mouseInside = false;

            Refresh();
        }

        /// <summary>
        /// Handles the <see cref="E:MouseClickEvent" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        void OnMouseClickEvent(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DirectionStyle oldStyle = directionStyle;
            switch (directionStyle)
            {
                case DirectionStyle.Up:
                    directionStyle = DirectionStyle.Down;
                    break;

                case DirectionStyle.Down:
                    directionStyle = DirectionStyle.Up;
                    break;

                case DirectionStyle.Left:
                    directionStyle = DirectionStyle.Right;
                    break;

                case DirectionStyle.Right:
                    directionStyle = DirectionStyle.Left;
                    break;
            }
            InitializeImage();
            // Update();
            if (handlerStyleChange != null)
            {
                ChangeStyleEventArgs args = new ChangeStyleEventArgs(oldStyle, directionStyle);
                handlerStyleChange(this, args);
            }
        }
        #endregion

    }

    #endregion
}
