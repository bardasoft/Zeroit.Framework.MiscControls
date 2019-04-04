﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MiscControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-19-2018
// ***********************************************************************
// <copyright file="CaptionControl.cs" company="Zeroit Dev Technologies">
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
using System.Windows.Forms;

namespace Zeroit.Framework.MiscControls
{
    #region CaptionControl

    /// <summary>
    /// Delegate for the dragging event.By dragging the caption you tell the panel to reposition itself
    /// </summary>
    /// <param name="sender">the object delegating the process, an instance of a CaptionCtrl</param>
    /// <param name="e">instance of an object holding the event data</param>
    public delegate void CaptionDraggingEvent(object sender, CaptionDraggingEventArgs e);

    /// <summary>
    /// This defines the caption bar used for the ExtendedPanel
    /// </summary>
    /// <seealso cref="Zeroit.Framework.MiscControls.CornerCtrl" />
    internal class CaptionCtrl : CornerCtrl
    {
        #region Members

        /// <summary>
        /// Boolean value specifing if the mouse button is down
        /// </summary>
        private bool mouseDown = false;

        /// <summary>
        /// The mouse x
        /// </summary>
        private int mouseX = 0;
        /// <summary>
        /// The mouse y
        /// </summary>
        private int mouseY = 0;

        /// <summary>
        /// Instance of the string being drawn
        /// </summary>
        private string text = "Caption";

        /// <summary>
        /// The brush type
        /// </summary>
        private BrushType brushType = BrushType.Gradient;

        /// <summary>
        /// The color one
        /// </summary>
        private Color colorOne = Color.White;

        /// <summary>
        /// The color two
        /// </summary>
        private Color colorTwo = Color.FromArgb(155, Color.Orange);

        /// <summary>
        /// The text color
        /// </summary>
        private Color textColor = Color.Black;
        /// <summary>
        /// Instance of the image to be displayed near the text of the text
        /// </summary>
        private Icon captionIcon = null;

        /// <summary>
        /// The direction control
        /// </summary>
        private DirectionCtrl directionCtrl = null;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// The brush
        /// </summary>
        private Brush brush = null;


        /// <summary>
        /// Occurs when [dragging].
        /// </summary>
        public event CaptionDraggingEvent Dragging = null;

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="CaptionCtrl"/> class.
        /// </summary>
        public CaptionCtrl()
        {
            InitializeComponent();

            //set up the mouse move event in case the style is to move the parent control
            this.MouseMove += new MouseEventHandler(OnMouseMoveEvent);

            //set up the mouse down event
            this.MouseDown += new MouseEventHandler(OnMouseDownEvent);

            //set up the mouse up event
            this.MouseUp += new MouseEventHandler(OnMouseUpEvent);

            //set up the brush
            InitializeBrush();


        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the color of the direction control.
        /// </summary>
        /// <value>The color of the direction control.</value>
        [Browsable(false)]
        public Color DirectionCtrlColor
        {
            get
            {
                return directionCtrl.Color;
            }
            set
            {
                directionCtrl.Color = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the direction control hover.
        /// </summary>
        /// <value>The color of the direction control hover.</value>
        [Browsable(false)]
        public Color DirectionCtrlHoverColor
        {
            get
            {
                return directionCtrl.HoverColor;
            }
            set
            {
                directionCtrl.HoverColor = value;
            }
        }


        /// <summary>
        /// Gets or sets the caption icon.
        /// </summary>
        /// <value>The caption icon.</value>
        [Category("Appearance")]
        [Description("Get or set the image to be displayed in the caption")]
        public Icon CaptionIcon
        {
            get
            {
                return captionIcon;
            }

            set
            {
                captionIcon = value;
                Update();
            }
        }

        /// <summary>
        /// Gets or sets the caption text.
        /// </summary>
        /// <value>The caption text.</value>
        [Category("Appearance")]
        [Description("Get/Set the text to be displayed")]
        public string CaptionText
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                Update();
            }
        }

        /// <summary>
        /// Gets or sets the caption font.
        /// </summary>
        /// <value>The caption font.</value>
        [Category("Appearance")]
        [Description("The font for this control also used to draw the text in the caption")]
        public Font CaptionFont
        {
            get
            {
                return this.Font;
            }
            set
            {
                this.Font = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the color one.
        /// </summary>
        /// <value>The color one.</value>
        [Category("Appearance")]
        [Description("The starting color for the gradient brush")]
        public Color ColorOne
        {
            get
            {
                return colorOne;
            }
            set
            {
                colorOne = value;
                InitializeBrush();
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the color two.
        /// </summary>
        /// <value>The color two.</value>
        [Category("Appearance")]
        [Description("The end color for the gradient brush ")]
        public Color ColorTwo
        {
            get
            {
                return colorTwo;
            }
            set
            {
                colorTwo = value;
                InitializeBrush();
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        [Category("Appearance")]
        [Description("The color used for drawing caption text ")]
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the type of the brush.
        /// </summary>
        /// <value>The type of the brush.</value>
        [Category("Appearance")]
        [Description("The brush used in painting the caption")]
        public BrushType BrushType
        {
            get
            {
                return this.brushType;
            }
            set
            {
                if (value != this.brushType)
                {
                    this.brushType = value;
                    InitializeBrush();
                    Refresh();
                }
            }
        }
        #endregion

        #region Protected
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

        /// <summary>
        /// Set up the graphic path used for drawing the borders
        /// </summary>
        protected override void InitializeGraphicPath()
        {
            cornerSquare = (int)(Height > Width ? Height * 0.05f : Width * 0.05f);// Width * 0.25f;
            base.InitializeGraphicPath();
        }
        #endregion

        #region Private

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            this.directionCtrl = new DirectionCtrl();
            this.SuspendLayout();

            this.Name = "CaptionCtrl";


            this.directionCtrl.Width = (int)(this.Height);
            this.directionCtrl.Height = this.Height;
            //System.Windows.Forms.MessageBox.Show((this.Width-this.Height).ToString());
            this.directionCtrl.Location = new Point((int)(this.Width - this.Height), 0);
            this.directionCtrl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.directionCtrl.Name = "directionCtrl";

            this.Controls.Add(directionCtrl);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Set up the brush used in filling this control
        /// </summary>
        private void InitializeBrush()
        {
            if (null != brush)
            {
                brush.Dispose();
            }
            if (brushType == BrushType.Solid)
            {
                brush = new SolidBrush(colorOne);
            }
            else
            {
                int width = this.Width;
                int height = this.Height;
                if (width == 0)
                {
                    width = 1;
                }
                if (height == 0)
                {
                    height = 1;
                }
                if (this.directionCtrl.DirectionStyle == DirectionStyle.Up || this.directionCtrl.DirectionStyle == DirectionStyle.Down)
                {
                    brush = new LinearGradientBrush(new Rectangle(0, 0, width, height), colorOne, colorTwo, LinearGradientMode.Vertical);
                }
                else
                {
                    brush = new LinearGradientBrush(new Rectangle(0, 0, width, height), colorOne, colorTwo, LinearGradientMode.Horizontal);
                }
            }
        }
        #endregion

        #region Public

        /// <summary>
        /// Set the handler for the event raised by the "click" control once it is pressed
        /// </summary>
        /// <param name="handler">instance of an handler for the event</param>
        public void SetStyleChangedHandler(DirectionCtrlStyleChangedEvent handler)
        {
            directionCtrl.handlerStyleChange += handler;
        }

        /// <summary>
        /// Checks to see if dragging is enabled.Used by ExtendedPanel to add/remove the handler
        /// </summary>
        /// <returns><c>true</c> if [is dragging enabled]; otherwise, <c>false</c>.</returns>
        public bool IsDraggingEnabled()
        {
            return (Dragging != null);
        }

        /// <summary>
        /// Set the direction style for the "click" control
        /// </summary>
        /// <param name="style">the new direction style</param>
        public void SetDirectionStyle(DirectionStyle style)
        {
            this.directionCtrl.DirectionStyle = style;

        }
        #endregion

        #region WM_PAINT

        /// <summary>
        /// Method for handling the WM_PAINT message
        /// </summary>
        /// <param name="e">instance of the object holding the event data</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //set up some flags
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //check to see if the path has been initialized
            if (graphicPath == null)
            {
                InitializeGraphicPath();
            }
            //draw the border
            e.Graphics.DrawPath(new Pen(borderColor), graphicPath);

            //paint the background
            e.Graphics.FillPath(brush, graphicPath);

            //measure text length
            StringFormat stringFormat = null;
            float xAxis = 0;
            float yAxis = 0;
            if (Width >= Height)
            {
                stringFormat = new StringFormat();
                SizeF size = e.Graphics.MeasureString(text, this.Font, new PointF(0, 0), stringFormat);
                yAxis = (Height - size.Height) * 0.5f;
            }
            else
            {
                stringFormat = new StringFormat(StringFormatFlags.DirectionVertical);
                SizeF size = e.Graphics.MeasureString(text, this.Font, new PointF(0, 0), stringFormat);
                xAxis = (Width - size.Width) * 0.5f;
            }

            //draw the image if it has been set up
            if (captionIcon != null)
            {
                if (Width >= Height)
                {
                    e.Graphics.DrawIcon(captionIcon, (int)cornerSquare / 6, (int)cornerSquare / 6);//, cornerSquare, cornerSquare);
                    //draw the text
                    e.Graphics.DrawString(text, this.Font, new SolidBrush(textColor), new PointF(xAxis + (int)cornerSquare / 6 + captionIcon.Width, yAxis + (int)cornerSquare / 6), stringFormat);
                }
                else
                {
                    e.Graphics.DrawIcon(captionIcon, (int)cornerSquare / 6, (int)cornerSquare / 6);
                    //draw the text
                    e.Graphics.DrawString(text, this.Font, new SolidBrush(textColor), new PointF(xAxis + (int)cornerSquare / 6, yAxis + (int)cornerSquare / 6 + +captionIcon.Height), stringFormat);
                }
            }
            else
            {
                //draw the text
                e.Graphics.DrawString(text, this.Font, new SolidBrush(textColor), new PointF(xAxis + (int)cornerSquare / 6, yAxis + (int)cornerSquare / 6), stringFormat);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the mouse is moved
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        void OnMouseMoveEvent(object sender, MouseEventArgs e)
        {
            //is the mouse down
            if (mouseDown && Dragging != null)
            {
                if (mouseX != e.X || mouseY != e.Y)
                {
                    int width = mouseX - e.X;
                    int height = mouseY - e.Y;
                    //raise the event
                    Dragging(this, new CaptionDraggingEventArgs(width, height));
                }
            }
        }

        /// <summary>
        /// Event raised when one of the mouse buttons is pressed
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        void OnMouseDownEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
                //save mouse coordinates
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        /// <summary>
        /// Event raised when the mouse button being pressed is released
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        void OnMouseUpEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = false;
                //reset mouse X,Y coordinates
                mouseX = 0;
                mouseY = 0;
            }
        }

        #endregion

        #region Override

        /// <summary>
        /// Override the OnResize because it needs to set up the location of the "direction" control and the size of that control
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //need to recreate the graphicpath for the border
            InitializeGraphicPath();

            if (this.Width >= Height)
            {
                this.directionCtrl.Location = new Point((int)(this.Width - this.Height), 0);
                this.directionCtrl.Width = (int)(this.Height);
                this.directionCtrl.Height = this.Height;
            }
            else
            {
                this.directionCtrl.Location = new Point(0, (int)(this.Height - this.Width));
                this.directionCtrl.Width = this.Width;
                this.directionCtrl.Height = this.Width;
            }
            //set the brush
            InitializeBrush();
            this.Region = new Region(regionPath);
        }

        #endregion

    }

    #endregion
}
