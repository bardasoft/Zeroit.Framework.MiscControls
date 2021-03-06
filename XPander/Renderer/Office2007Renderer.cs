﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MiscControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-19-2018
// ***********************************************************************
// <copyright file="Office2007Renderer.cs" company="Zeroit Dev Technologies">
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
    #region Office2007Renderer
    /// <summary>
    /// Draw ToolStrips using the Office 2007 themed appearance.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.ToolStripProfessionalRenderer" />
    public class Office2007Renderer : ToolStripProfessionalRenderer
    {
        #region FieldsPrivate

        /// <summary>
        /// The margin inset
        /// </summary>
        private static int MarginInset;
        /// <summary>
        /// The status strip blend
        /// </summary>
        private static Blend StatusStripBlend;

        #endregion

        #region MethodsPublic
        /// <summary>
        /// Initializes static members of the <see cref="Office2007Renderer"/> class.
        /// </summary>
        static Office2007Renderer()
        {
            MarginInset = 2;
            // One time creation of the blend for the status strip gradient brush
            StatusStripBlend = new Blend();
            StatusStripBlend.Positions = new float[] { 0.0F, 0.2F, 0.3F, 0.4F, 0.8F, 1.0F };
            StatusStripBlend.Factors = new float[] { 0.3F, 0.4F, 0.5F, 1.0F, 0.8F, 0.7F };
        }
        /// <summary>
        /// Initialize a new instance of the Office2007Renderer class.
        /// </summary>
		public Office2007Renderer()
            : base(new Office2007BlueColorTable())
        {
            this.ColorTable.UseSystemColors = false;
        }
        /// <summary>
        /// Initializes a new instance of the Office2007Renderer class.
        /// </summary>
        /// <param name="professionalColorTable">A <see cref="ProfessionalColorTable" /> to be used for painting.</param>
        public Office2007Renderer(ProfessionalColorTable professionalColorTable)
            : base(professionalColorTable)
        {
        }
        #endregion

        #region MethodsProtected
        /// <summary>
        /// Raises the RenderArrow event.
        /// </summary>
        /// <param name="e">A ToolStripArrowRenderEventArgs that contains the event data.</param>
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (ColorTable.UseSystemColors == false)
            {
                ProfessionalColorTable colorTable = ColorTable as ProfessionalColorTable;
                if (colorTable != null)
                {
                    if ((e.Item.Owner.GetType() == typeof(MenuStrip)) && (e.Item.Selected == false) && e.Item.Pressed == false)
                    {
                        if (colorTable.MenuItemText != Color.Empty)
                        {
                            e.ArrowColor = colorTable.MenuItemText;
                        }
                    }
                    if ((e.Item.Owner.GetType() == typeof(StatusStrip)) && (e.Item.Selected == false) && e.Item.Pressed == false)
                    {
                        if (colorTable.StatusStripText != Color.Empty)
                        {
                            e.ArrowColor = colorTable.StatusStripText;
                        }
                    }
                }
            }
            base.OnRenderArrow(e);
        }
        /// <summary>
        /// Raises the RenderItemText event.
        /// </summary>
        /// <param name="e">A ToolStripItemTextRenderEventArgs that contains the event data.</param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (ColorTable.UseSystemColors == false)
            {
                ProfessionalColorTable colorTable = ColorTable as ProfessionalColorTable;
                if (colorTable != null)
                {
                    if ((e.ToolStrip is MenuStrip) && (e.Item.Selected == false) && e.Item.Pressed == false)
                    {
                        if (colorTable.MenuItemText != Color.Empty)
                        {
                            e.TextColor = colorTable.MenuItemText;
                        }
                    }
                    if ((e.ToolStrip is StatusStrip) && (e.Item.Selected == false) && e.Item.Pressed == false)
                    {
                        if (colorTable.StatusStripText != Color.Empty)
                        {
                            e.TextColor = colorTable.StatusStripText;
                        }
                    }
                }
            }
            base.OnRenderItemText(e);
        }
        /// <summary>
        /// Raises the RenderToolStripContentPanelBackground event.
        /// </summary>
        /// <param name="e">An ToolStripContentPanelRenderEventArgs containing the event data.</param>
        protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
        {
            // Must call base class, otherwise the subsequent drawing does not appear!
            base.OnRenderToolStripContentPanelBackground(e);
            if (ColorTable.UseSystemColors == false)
            {
                // Cannot paint a zero sized area
                if ((e.ToolStripContentPanel.Width > 0) &&
                    (e.ToolStripContentPanel.Height > 0))
                {
                    using (LinearGradientBrush backBrush = new LinearGradientBrush(e.ToolStripContentPanel.ClientRectangle,
                                                                                   ColorTable.ToolStripContentPanelGradientBegin,
                                                                                   ColorTable.ToolStripContentPanelGradientEnd,
                                                                                   LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(backBrush, e.ToolStripContentPanel.ClientRectangle);
                    }
                }
            }
        }
        /// <summary>
        /// Raises the RenderSeparator event.
        /// </summary>
        /// <param name="e">An ToolStripSeparatorRenderEventArgs containing the event data.</param>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            if (ColorTable.UseSystemColors == false)
            {
                e.Item.ForeColor = ColorTable.RaftingContainerGradientBegin;
            }
            base.OnRenderSeparator(e);
        }
        /// <summary>
        /// Raises the RenderToolStripBackground event.
        /// </summary>
        /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (ColorTable.UseSystemColors == true)
            {
                base.OnRenderToolStripBackground(e);
            }
            else
            {
                if (e.ToolStrip is StatusStrip)
                {
                    // We do not paint the top two pixel lines, so are drawn by the status strip border render method
                    //RectangleF backRectangle = new RectangleF(0, 1.5f, e.ToolStrip.Width, e.ToolStrip.Height - 2);
                    RectangleF backRectangle = new RectangleF(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);

                    // Cannot paint a zero sized area
                    if ((backRectangle.Width > 0) && (backRectangle.Height > 0))
                    {
                        using (LinearGradientBrush backBrush = new LinearGradientBrush(backRectangle,
                                                                                       ColorTable.StatusStripGradientBegin,
                                                                                       ColorTable.StatusStripGradientEnd,
                                                                                       LinearGradientMode.Vertical))
                        {
                            backBrush.Blend = StatusStripBlend;
                            e.Graphics.FillRectangle(backBrush, backRectangle);
                        }
                    }
                }
                else
                {
                    base.OnRenderToolStripBackground(e);
                }
            }
        }
        /// <summary>
        /// Raises the RenderImageMargin event.
        /// </summary>
        /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            if (ColorTable.UseSystemColors == true)
            {
                base.OnRenderToolStripBackground(e);
            }
            else
            {
                if ((e.ToolStrip is ContextMenuStrip) ||
                    (e.ToolStrip is ToolStripDropDownMenu))
                {
                    // Start with the total margin area
                    Rectangle marginRectangle = e.AffectedBounds;

                    // Do we need to draw with separator on the opposite edge?
                    bool bIsRightToLeft = (e.ToolStrip.RightToLeft == RightToLeft.Yes);

                    marginRectangle.Y += MarginInset;
                    marginRectangle.Height -= MarginInset * 2;

                    // Reduce so it is inside the border
                    if (bIsRightToLeft == false)
                    {
                        marginRectangle.X += MarginInset;
                    }
                    else
                    {
                        marginRectangle.X += MarginInset / 2;
                    }

                    // Draw the entire margine area in a solid color
                    using (SolidBrush backBrush = new SolidBrush(
                        ColorTable.ImageMarginGradientBegin))
                        e.Graphics.FillRectangle(backBrush, marginRectangle);
                }
                else
                {
                    base.OnRenderImageMargin(e);
                }
            }
        }

        #endregion

        #region MethodsPrivate
        #endregion
    }
    #endregion
}
