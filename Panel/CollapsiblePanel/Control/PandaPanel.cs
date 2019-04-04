﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MiscControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-19-2018
// ***********************************************************************
// <copyright file="XPPanel.cs" company="Zeroit Dev Technologies">
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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Zeroit.Framework.MiscControls
{
    #region ZeroitPandaPanel


    #region ZeroitPandaPanel enumerations
    /// <summary>
    /// Enumeration describing the panel state for <c><see cref="ZeroitPandaPanel" /></c>.
    /// </summary>
    /// <remarks>The panel can be in an inbetween state. <see cref="ZeroitPandaPanelDrawState" /></remarks>
    public enum ZeroitPandaPanelState
    {
        /// <summary>
        /// The <c>ZeroitPandaPanel</c> is expanded
        /// </summary>
        Expanded,

        /// <summary>
        /// The <c>ZeroitPandaPanel</c> is collapsed
        /// </summary>
        Collapsed
    }

    /// <summary>
    /// Primarily for internal use. Provides more information for <c><see cref="ZeroitPandaPanel" /></c>.
    /// regarding the <see cref="ZeroitPandaPanelState" />
    /// </summary>
    public enum ZeroitPandaPanelDrawState
    {
        /// <summary>
        /// <c>ZeroitPandaPanel</c> is not actively changing
        /// </summary>
        Normal,

        /// <summary>
        /// <c>ZeroitPandaPanel</c> is actively expanding
        /// </summary>
        Expanding,

        /// <summary>
        /// <c>ZeroitPandaPanel</c> is actively collapsing
        /// </summary>
        Collapsing,

        /// <summary>
        /// <c>ZeroitPandaPanel</c> is in an internal state
        /// </summary>
        Internal
    }

    /// <summary>
    /// Enumeration of all <c><see cref="ZeroitPandaPanel" /></c> properties that trigger
    /// <see cref="ZeroitPandaPanel.PropertyChange" /> events
    /// </summary>
    /// <remarks>When a property changes, a <see cref="ZeroitPandaPanelPropertyChangeEventArgs" /> event is
    /// fired to listeners. The <see cref="EventArgs" /> will contain one of the following
    /// values allowing listeners to react to specific property changes</remarks>
    public enum ZeroitPandaPanelProperties
    {
        /// <summary>
        /// <see cref="ZeroitPandaPanel.Caption" />
        /// </summary>
        CaptionProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.CaptionGradient" />
        /// </summary>
        CaptionGradientProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.CaptionGradientMode" />
        /// </summary>
        CaptionGradientModeProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.CaptionCornerType" />
        /// </summary>
        CaptionCornerTypeProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.GradientOffset" />
        /// </summary>
        GradientOffsetProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.CaptionUnderline" />
        /// </summary>
        CaptionUnderlineProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.HorzAlignment" />
        /// </summary>
        HorzAlignmentProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.VertAlignment" />
        /// </summary>
        VertAlignmentProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.Font" />
        /// </summary>
        FontProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.TextColors" />
        /// </summary>
        TextColorsProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.TextHighlightColors" />
        /// </summary>
        TextHighlightColorsProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.CurveRadius" />
        /// </summary>
        CurveRadiusProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.ImageItems" />
        /// </summary>
        ImageItemsProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.ExpandedGlyphs" />
        /// </summary>
        ExpandedGlyphsProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.CollapsedGlyphs" />
        /// </summary>
        CollapsedGlyphsProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.Spacing" />
        /// </summary>
        SpacingProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.FitToImage" />
        /// </summary>
        FitToImageProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.PanelState" />
        /// </summary>
        PanelStateProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.IsFixedHeight" />
        /// </summary>
        IsFixedHeightProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.PanelGradient" />
        /// </summary>
        PanelGradientProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.PanelGradientMode" />
        /// </summary>
        PanelGradientModeProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.OutlineColor" />
        /// </summary>
        PanelOutlineColorProperty,
        /// <summary>
        /// <see cref="Control.Enabled" />
        /// </summary>
        EnabledProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.AnimationRate" />
        /// </summary>
        AnimationRateProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.DisabledOpacity" />
        /// </summary>
        DisabledOpacityProperty,
        /// <summary>
        /// <see cref="ZeroitPandaPanel.ZeroitPandaPanelStyle" />
        /// </summary>
        ZeroitPandaPanelStyleProperty,
        /// <summary>
        /// Minimum panel height (when expanded)
        /// </summary>
        PanelHeightProperty
    }

    /// <summary>
    /// Enumeration defining visual style of <c><see cref="ZeroitPandaPanel" /></c>.
    /// </summary>
    public enum ZeroitPandaPanelStyle
    {
        /// <summary>
        /// User defined style
        /// </summary>
        Custom,

        /// <summary>
        /// Emulation of WindowsXP Collapsible panel
        /// </summary>
        WindowsXP
    }
    #endregion ZeroitPandaPanel enumerations

    /// <summary>
    /// <c>ZeroitPandaPanel</c> - Collapsible Panel ala Windows XP
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Panel" />
    /// <remarks>An <c>ZeroitPandaPanel</c> is a control/container based on <see cref="System.Windows.Forms.Panel" />.
    /// It can be associated with an <see cref="ZeroitPandaPanelGroup" /> for automatic grouping/management, or
    /// it can be placed directly on a <see cref="System.Windows.Forms.Form" /><para><c>ZeroitPandaPanel</c> uses a <see cref="TypeConverter" /> to allow designer code generation to use the constructor that
    /// specifies the <see cref="ExpandedHeight" /> property.
    /// </para><para><c>ZeroitPandaPanel</c> has the following primary properties:
    /// <list type="table"><listheader><term>Property</term><description>Purpose</description></listheader><item><term><see cref="Caption" /></term><description>Text displayed in the caption of the <c>ZeroitPandaPanel</c></description></item><item><term><see cref="CaptionGradient" /></term><description><see cref="GradientColor" /> describing the start/end colors for the <see cref="Caption" />
    /// gradient
    /// </description></item><item><term><see cref="CurveRadius" /></term><description>The radius of the curver for the rounded corners of the <see cref="Caption" /></description></item><item><term><see cref="CaptionUnderline" /></term><description><see cref="Color" /> for the <see cref="Caption" /> underline, or <see cref="Color.Empty" />
    /// for no <see cref="Caption" /> underline</description></item><item><term><see cref="Font" /></term><description>Font for the <see cref="Caption" /> text</description></item><item><term><see cref="TextColors" /></term><description><see cref="XPanelColorPair" /> describing the <i>normal</i> foreground/background colors for
    /// the <see cref="Caption" /> text
    /// </description></item><item><term><see cref="TextHighlightColors" /></term><description><see cref="XPanelColorPair" /> describing the foreground/background colors for the
    /// <see cref="Caption" /> text when it is <i>hot/highlighted</i> (mouse over)
    /// </description></item><item><term><see cref="HorzAlignment" /></term><description>Horizontal alignment of <see cref="Caption" /> text (see <see cref="StringAlignment" />)</description></item><item><term><see cref="VertAlignment" /></term><description>Vertical alignment of <see cref="Caption" /> text (see <see cref="StringAlignment" />)</description></item><item><term><see cref="ImageItems" /></term><description><see cref="StateImageItems" /> describing the <see cref="ImageSet" /> and index mappings for
    /// the <c>Normal</c>, <c>Highlight</c>, <c>Pressed</c>, and <c>Disabled</c> images that appear on the
    /// left of the <see cref="Caption" /></description></item><item><term><see cref="CollapsedGlyphs" /></term><description><see cref="StateImageItems" /> describing the <see cref="ImageSet" /> and index mappings for
    /// the <c>Normal</c>, <c>Highlight</c>, <c>Pressed</c>, and <c>Disabled</c> images that appear on the
    /// right of the <see cref="Caption" /> when the <see cref="PanelState" /> is <see cref="ZeroitPandaPanelState.Collapsed" /></description></item><item><term><see cref="ExpandedGlyphs" /></term><description><see cref="StateImageItems" /> describing the <see cref="ImageSet" /> and index mappings for
    /// the <c>Normal</c>, <c>Highlight</c>, <c>Pressed</c>, and <c>Disabled</c> images that appear on the
    /// right of the <see cref="Caption" /> when the <see cref="PanelState" /> is <see cref="ZeroitPandaPanelState.Expanded" /></description></item><item><term><see cref="PanelState" /></term><description>Current state of the <c>ZeroitPandaPanel</c>. <see cref="ZeroitPandaPanelState.Expanded" /> or <see cref="ZeroitPandaPanelState.Collapsed" /></description></item><item><term><see cref="IsFixedHeight" /></term><description><see langword="true" /> if the <c>ZeroitPandaPanel</c> cannot be expanded/collapsed</description></item><item><term><see cref="PanelGradient" /></term><description><see cref="GradientColor" /> describing the start/end colors for the panel gradient</description></item><item><term><see cref="OutlineColor" /></term><description><see cref="Color" /> of the panel outline, or <see cref="Color.Empty" /> for no outline</description></item><item><term><see cref="AnimationRate" /></term><description>The animation rate in milliseconds (or 0 for no animation)</description></item><item><term><see cref="ZeroitPandaPanelStyle" /></term><description>Set the style of the <c>ZeroitPandaPanel</c> - <see cref="UIComponents.ZeroitPandaPanelStyle.Custom" /> or
    /// <see cref="UIComponents.ZeroitPandaPanelStyle.WindowsXP" /></description></item></list></para><para><c>ZeroitPandaPanelGroup</c> has the following primary events:
    /// <list type="table"><listheader><term>Event</term><description>Purpose</description></listheader><item><term><see cref="PropertyChange" /></term><description>Triggered when a primary property changes. See <see cref="ZeroitPandaPanelProperties" /></description></item><item><term><see cref="PanelStateChange" /></term><description>Triggered when the <c>ZeroitPandaPanel</c> expands/collapses</description></item></list></para></remarks>
    [TypeConverter(typeof(ZeroitPandaPanelTypeConverter))]
    [ToolboxItem(true)]
    public class ZeroitPandaPanel : System.Windows.Forms.Panel
    {
        #region Constants
        /// <summary>
        /// Reasonable minimum height for the caption
        /// </summary>
        private const int MinCaptionHeight = 24;

        /// <summary>
        /// Default margins/spacing for the <c>ZeroitPandaPanel</c> caption
        /// </summary>
        private static readonly Point DefaultSpacing = new Point(8, 4);

        /// <summary>
        /// Default gradient start <see cref="Color" /> for the <see cref="Caption" />
        /// </summary>
        private static readonly Color DefaultCaptionGradientStart = Color.White;

        /// <summary>
        /// Default gradient end <see cref="Color" /> for the <see cref="Caption" />
        /// </summary>
        private static readonly Color DefaultCaptionGradientEnd = Color.FromArgb(200, 213, 247);

        /// <summary>
        /// GradientColor describing the default caption gradient
        /// </summary>
        private static readonly GradientColor DefaultCaptionGradient =
            new GradientColor(DefaultCaptionGradientStart, DefaultCaptionGradientEnd);

        /// <summary>
        /// Default gradient start <see cref="Color" /> for the panel
        /// </summary>
        private static readonly Color DefaultPanelGradientStart = Color.FromArgb(214, 223, 247);

        /// <summary>
        /// Default gradient end <see cref="Color" /> for the panel
        /// </summary>
        private static readonly Color DefaultPanelGradientEnd = Color.FromArgb(214, 223, 247);

        /// <summary>
        /// <see cref="GradientColor" /> describing the default panel gradient
        /// </summary>
        private static readonly GradientColor DefaultPanelGradient =
            new GradientColor(DefaultPanelGradientStart, DefaultPanelGradientEnd);

        /// <summary>
        /// Default foreground <see cref="Color" /> for <see cref="Caption" /> text in the <i>normal</i> state
        /// </summary>
        private static readonly Color DefaultTextColor = Color.FromArgb(33, 93, 198);

        /// <summary>
        /// <see cref="XPanelColorPair" /> describing the default foreground/background <see cref="Color" /> for <i>normal</i><see cref="Caption" /> text
        /// </summary>
        private static readonly XPanelColorPair DefaultTextColors = new XPanelColorPair(DefaultTextColor, Color.Empty);

        /// <summary>
        /// Default foreground <see cref="Color" /> for <see cref="Caption" /> text in the <i>hot/highlight</i> state
        /// </summary>
        private static readonly Color DefaultTextHighlightColor = Color.FromArgb(66, 142, 255);

        /// <summary>
        /// <see cref="XPanelColorPair" /> describing the default foreground/background <see cref="Color" /> for <i>hot/highlight</i><see cref="Caption" /> text
        /// </summary>
        private static readonly XPanelColorPair DefaultTextHighlightColors = new XPanelColorPair(DefaultTextHighlightColor, Color.Empty);

        /// <summary>
        /// Default <see cref="Color" /> of the <see cref="Caption" /> underline
        /// </summary>
        private static readonly Color DefaultCaptionUnderlineColor = Color.White;

        /// <summary>
        /// Default <see cref="Color" /> of the panel outline
        /// </summary>
        private static readonly Color DefaultOutlineColor = Color.DarkGray;
        #endregion Constants

        #region Static Members
        /// <summary>
        /// Standard gray-scale conversion for images
        /// </summary>
        private static System.Drawing.Imaging.ImageAttributes grayScaleAttributes;

        /// <summary>
        /// Windows XP style expand glyphs state image mapping
        /// </summary>
        private static StateImageItems xpStyleExpandGlyphs = null;

        /// <summary>
        /// Windows XP style collapse glyphs state image mapping
        /// </summary>
        private static StateImageItems xpStyleCollapseGlyphs = null;
        #endregion Static Members

        #region Static Methods
        /// <summary>
        /// Returns the standard grayscale matrix for image transformations
        /// </summary>
        /// <value>The gray scale attributes.</value>
        [Browsable(false)]
        public static ImageAttributes GrayScaleAttributes
        {
            get
            {
                // prevent two of these from being created concurrently by locking the class
                lock (typeof(ZeroitPandaPanel))
                {
                    if (grayScaleAttributes == null)
                    {
                        ColorMatrix matrix = new ColorMatrix();
                        matrix.Matrix00 = 1 / 3f;
                        matrix.Matrix01 = 1 / 3f;
                        matrix.Matrix02 = 1 / 3f;
                        matrix.Matrix10 = 1 / 3f;
                        matrix.Matrix11 = 1 / 3f;
                        matrix.Matrix12 = 1 / 3f;
                        matrix.Matrix20 = 1 / 3f;
                        matrix.Matrix21 = 1 / 3f;
                        matrix.Matrix22 = 1 / 3f;

                        grayScaleAttributes = new ImageAttributes();

                        grayScaleAttributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    }
                }

                return grayScaleAttributes;
            }
        }

        /// <summary>
        /// Returns a <see cref="StateImageItems" /> for the expand glyphs when using
        /// <see cref="UIComponents.ZeroitPandaPanelStyle.WindowsXP" />
        /// </summary>
        /// <value>The xp style expand glyphs.</value>
        public static StateImageItems XPStyleExpandGlyphs
        {
            get
            {
                lock (typeof(ZeroitPandaPanel))
                {
                    if (xpStyleExpandGlyphs == null)
                    {
                        xpStyleExpandGlyphs = new StateImageItems();
                        try
                        {
                            xpStyleExpandGlyphs.ImageSet = new ImageSet(LoadBitmap("Resources.xp.style.glyphs.png"), 4);
                            xpStyleExpandGlyphs.Normal = 1;
                            xpStyleExpandGlyphs.Highlight = 0;
                            xpStyleExpandGlyphs.Pressed = 0;

                            // ingore exception. Images wont be available...
                        }
                        catch { }
                    }

                    return xpStyleExpandGlyphs;
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="StateImageItems" /> for the collapse glyphs when using
        /// <see cref="UIComponents.ZeroitPandaPanelStyle.WindowsXP" />
        /// </summary>
        /// <value>The xp style collapse glyphs.</value>
        public static StateImageItems XPStyleCollapseGlyphs
        {
            get
            {
                lock (typeof(ZeroitPandaPanel))
                {
                    if (xpStyleCollapseGlyphs == null)
                    {
                        xpStyleCollapseGlyphs = new StateImageItems();
                        try
                        {
                            xpStyleCollapseGlyphs.ImageSet = new ImageSet(LoadBitmap("Resources.xp.style.glyphs.png"), 4);
                            xpStyleCollapseGlyphs.Normal = 3;
                            xpStyleCollapseGlyphs.Highlight = 2;
                            xpStyleCollapseGlyphs.Pressed = 2;

                            // ingore exception. Images wont be available...
                        }
                        catch { }
                    }

                    return xpStyleCollapseGlyphs;
                }
            }
        }

        /// <summary>
        /// Used to load bitmap resources from the <c>ZeroitPandaPanel</c> assembly
        /// </summary>
        /// <param name="resource">The name of the resource</param>
        /// <returns>The <see cref="Bitmap" /> resource</returns>
        private static Bitmap LoadBitmap(String resource)
        {
            System.Reflection.Assembly xpPanel = System.Reflection.Assembly.GetAssembly(typeof(ZeroitPandaPanel));
            return new Bitmap(xpPanel.GetManifestResourceStream(resource));
        }
        #endregion Static Methods

        #region Fields
        /// <summary>
        /// Standard Windows Forms container
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Timer used for expand/collapse animation
        /// </summary>
        private System.Windows.Forms.Timer animationTimer;

        /// <summary>
        /// Tracks the current <see cref="ZeroitPandaPanelState" />of the panel
        /// </summary>
        private ZeroitPandaPanelState panelState = ZeroitPandaPanelState.Expanded;

        /// <summary>
        /// The text for the <see cref="Caption" /> of the ZeroitPandaPanel
        /// </summary>
        private String text = "";

        /// <summary>
        /// Horizontal alignment of <see cref="Caption" /> text
        /// </summary>
        private StringAlignment horzAlignment = StringAlignment.Near;

        /// <summary>
        /// Vertical alignment of <see cref="Caption" /> text
        /// </summary>
        private StringAlignment vertAlignment = StringAlignment.Center;

        /// <summary>
        /// Font used to draw the <see cref="Caption" />
        /// </summary>
        private Font font = new Font("Microsoft Verdana", 8, FontStyle.Bold);

        /// <summary>
        /// Normal <see cref="XPanelColorPair" /> of the <see cref="Caption" /> text
        /// </summary>
        private XPanelColorPair textColors = new XPanelColorPair(DefaultTextColors);

        /// <summary>
        /// Highlight <see cref="XPanelColorPair" /> of the <see cref="Caption" /> text (foreground and background)
        /// </summary>
        private XPanelColorPair textHighlightColors = new XPanelColorPair(DefaultTextHighlightColors);

        /// <summary>
        /// Encapsulates the start and end colors for the caption gradient
        /// </summary>
        private GradientColor captionGradient = new GradientColor(DefaultCaptionGradient);

        /// <summary>
        /// <see cref="Color " /> of the accent line beneath the caption
        /// </summary>
        private Color captionUnderline = Color.FromArgb(DefaultCaptionUnderlineColor.R, DefaultCaptionUnderlineColor.G, DefaultCaptionUnderlineColor.B);

        /// <summary>
        /// Curve Radius for ZeroitPandaPanel Caption
        /// </summary>
        private int captionCurveRadius = 7;

        /// <summary>
        /// Where the caption corner's are rounded
        /// </summary>
        private XPanelCornerType captionCornerType = XPanelCornerType.Top;

        /// <summary>
        /// Direction of the linear gradient of the Caption
        /// </summary>
        private LinearGradientMode captionGradientMode = LinearGradientMode.Horizontal;

        /// <summary>
        /// Defines the image index mappings for each Image state
        /// (normal,highlight,pressed,disabled)
        /// </summary>
        private StateImageItems imageItems = new StateImageItems();

        /// <summary>
        /// Initial animation rate in milliseconds for expanding/collapsing
        /// </summary>
        private int animationRate = 50;

        /// <summary>
        /// Defines the image index mappings for each glyph image state
        /// (normal,highlight,pressed,disabled) when <see cref="ZeroitPandaPanelState.Expanded" />
        /// </summary>
        private StateImageItems glyphImageItemsExpanded = new StateImageItems();

        /// <summary>
        /// Defines the image index mappings for each glyph image state
        /// (normal,highlight,pressed,disabled) when <see cref="ZeroitPandaPanelState.Collapsed" />
        /// </summary>
        private StateImageItems glyphImageItemsCollapsed = new StateImageItems();

        /// <summary>
        /// Defines the X and Y margins for the left/right and top/bottom
        /// </summary>
        private Point spacing = DefaultSpacing;

        /// <summary>
        /// <see langword="true" /> if we want to fit the caption to the size of the image
        /// </summary>
        private bool isFitToImage = false;

        /// <summary>
        /// <see langword="true" /> if the panel cannot be collapsed
        /// </summary>
        private bool isFixedHeight = false;

        /// <summary>
        /// Offset for the start of the caption gradient expressed
        /// in terms of %.
        /// </summary>
        private double gradientOffset = 0.5d;

        /// <summary>
        /// <see cref="GradientColor" /> used for graident background of the panel
        /// </summary>
        private GradientColor panelGradient = new GradientColor(DefaultPanelGradient);

        /// <summary>
        /// Direction of the linear gradient of the Panel
        /// </summary>
        private LinearGradientMode panelGradientMode = LinearGradientMode.Horizontal;

        /// <summary>
        /// <see cref="Color" /> of the panel outline
        /// </summary>
        private Color panelOutlineColor = System.Drawing.Color.DarkGray;

        /// <summary>
        /// Changes the transparency of an <c>ZeroitPandaPanel</c> when in the disabled state
        /// </summary>
        private byte disabledOpacity = byte.MaxValue;

        /// <summary>
        /// Style of the ZeroitPandaPanel
        /// </summary>
        private ZeroitPandaPanelStyle xpPanelStyle = ZeroitPandaPanelStyle.Custom;

        #region Events
        /// <summary>
        /// <see cref="PropertyChange" /> event listeners
        /// </summary>
        private PanelPropertyChangeHandler propertyChangeListeners = null;

        /// <summary>
        /// <see cref="PanelStateChange" /> event listeners
        /// </summary>
        private EventHandler panelStateChangeListeners = null;

        /// <summary>
        /// See <see cref="Expanded" /> event
        /// </summary>
        private EventHandler panelExpandedListeners = null;

        /// <summary>
        /// See <see cref="Expanding" /> event
        /// </summary>
        private EventHandler panelExpandingListeners = null;

        /// <summary>
        /// See <see cref="Collapsed" /> event
        /// </summary>
        private EventHandler panelCollapsedListeners = null;

        /// <summary>
        /// See <see cref="Collapsing" /> event
        /// </summary>
        private EventHandler panelCollapsingListeners = null;
        #endregion Events

        #region Cached Drawing Objects
        /// <summary>
        /// <see cref="Font" /> used to draw caption text when <c>ZeroitPandaPanel</c> is disabled
        /// </summary>
        private Font disabledFont = null;

        /// <summary>
        /// <see cref="LinearGradientBrush" /> for the full caption area of <c>ZeroitPandaPanel</c>
        /// </summary>
        private LinearGradientBrush captionBrush = null;

        /// <summary>
        /// Cached <see cref="GraphicsPath" /> for the full caption area of <c>ZeroitPandaPanel</c>
        /// </summary>
        private GraphicsPath captionPath = null;

        /// <summary>
        /// <see cref="SolidBrush" /> used to draw the caption text in the selected text foreground color
        /// </summary>
        private SolidBrush fontBrush = null;

        /// <summary>
        /// <see cref="SolidBrush" /> used to draw the caption text in the selected text highlight foreground color
        /// </summary>
        private SolidBrush fontHighlightBrush = null;

        /// <summary>
        /// <see cref="Pen" /> used to draw the caption underline
        /// </summary>
        private Pen captionUnderlinePen = null;

        /// <summary>
        /// <see cref="StringFormat" /> used to measure caption text extents
        /// </summary>
        private StringFormat measureTextFormat = null;

        /// <summary>
        /// <see cref="StringFormat" /> used to draw caption text
        /// </summary>
        private StringFormat drawTextFormat = null;

        /// <summary>
        /// <see cref="LinearGradientBrush" /> used to paint the panel area
        /// </summary>
        private LinearGradientBrush panelBrush = null;
        #endregion Cached Drawing Objects

        #region Layout Objects
        /// <summary>
        /// <see langword="true" /> if a property change has invalidated the
        /// cached layout of the <c>ZeroitPandaPanel</c>
        /// </summary>
        private bool isLayoutDirty = true;

        /// <summary>
        /// Internal to track the height of the panel when expanded. We need to remember this because
        /// we whack the height when the panel is collapsed.
        /// </summary>
        private int expandedHeight = 100;

        /// <summary>
        /// Minimum size of panel when expanded
        /// </summary>
        private int panelHeight = 0;

        /// <summary>
        /// The <see cref="Rectangle" /> describing the image on the left side of the caption
        /// </summary>
        private Rectangle imageRect = new Rectangle(0, 0, 0, 0);

        /// <summary>
        /// The <see cref="Rectangle" /> describing the image on the right side of the caption
        /// </summary>
        private Rectangle glyphRect = new Rectangle(0, 0, 0, 0);

        /// <summary>
        /// The <see cref="Rectangle" /> describing the image on the left side of the caption
        /// </summary>
        private Rectangle textRect = new Rectangle(0, 0, 0, 0);

        /// <summary>
        /// The <see cref="Rectangle" /> describing the entire caption area of the <c>ZeroitPandaPanel</c>
        /// </summary>
        private Rectangle xpCaptionRect = new Rectangle(0, 0, 0, 0);


        /// <summary>
        /// The <see cref="Rectangle" /> describing the entire panel area (excluding the caption)
        /// </summary>
        private Rectangle xpPanelRect = new Rectangle(0, 0, 0, 0);
        #endregion Layout Objects

        #region Dynamic State Fields
        /// <summary>
        /// <see langword="true" /> if the mouse is over the caption
        /// </summary>
        private bool isCaptionHot = false;

        /// <summary>
        /// <see langword="true" /> if the mouse is *down* over the caption
        /// </summary>
        private bool isCaptionPressed = false;

        /// <summary>
        /// Current <see cref="ZeroitPandaPanelDrawState" />
        /// </summary>
        private ZeroitPandaPanelDrawState panelDrawState = ZeroitPandaPanelDrawState.Normal;
        #endregion Dynamic State Fields
        #endregion Fields

        #region Constructor(s)
        /// <summary>
        /// Construct an <c>ZeroitPandaPanel</c> with the specified <see cref="ExpandedHeight" />
        /// </summary>
        /// <param name="expandedHeight">Height of the expanded.</param>
        public ZeroitPandaPanel(int expandedHeight)
        {
            // save this value before anything else happens as it can/will have an
            // effect on logic in InitalizeComponent
            this.ExpandedHeight = expandedHeight;

            InitializeComponent();

            // Set the following control styles so that we get more optimized/flicker-free drawing,
            // and the use of transparent back color
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ContainerControl, true);

            // These are properties we override so that the user cannot set them
            // to values we dont like. They are not available in the designer
            this.BorderStyle = BorderStyle.None;
            this.BackColor = Color.Transparent;
            this.ForeColor = SystemColors.WindowText;       // not really relevant, just cuz we override it...
        }

        /// <summary>
        /// Construct an <c>ZeroitPandaPanel</c> with a default <see cref="ExpandedHeight" /> of 100 pixels
        /// </summary>
        public ZeroitPandaPanel() : this(100) { }
        #endregion Constructor

        #region Dispose (and related)
        /// <summary>
        /// Sets all cached GDI+ drawing object proeprties to null which
        /// causes their current value (if any) to be disposed
        /// </summary>
        protected void DisposeCachedObjects()
        {
            CaptionBrush = null;
            CaptionUnderlinePen = null;
            CaptionPath = null;
            Font = null;
            DisabledFont = null;
            FontBrush = null;
            FontHighlightBrush = null;
            PanelBrush = null;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeCachedObjects();

                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion Dispose (and related)

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            // 
            // animationTimer
            // 
            this.animationTimer.Interval = 50;
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);

        }
        #endregion

        #region Properties
        #region Base Properties        
        /// <summary>
        /// Gets or sets the caption gradient mode.
        /// </summary>
        /// <value>The caption gradient mode.</value>
        [Category("Caption")]
        [Description("Direction of the Caption Gradient")]
        [DefaultValue("Horizontal")]
        public LinearGradientMode CaptionGradientMode
        {
            get
            {
                return captionGradientMode;
            }

            set
            {
                if (captionGradientMode != value)
                {
                    captionGradientMode = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.CaptionGradientModeProperty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the caption corner.
        /// </summary>
        /// <value>The type of the caption corner.</value>
        [Category("Caption")]
        [Description("Caption Corner Type")]
        [DefaultValue("Top")]
        public XPanelCornerType CaptionCornerType
        {
            get
            {
                return captionCornerType;
            }

            set
            {
                if (captionCornerType != value)
                {
                    captionCornerType = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.CaptionCornerTypeProperty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the panel gradient mode.
        /// </summary>
        /// <value>The panel gradient mode.</value>
        [Category("Panel")]
        [Description("Direction of the Panel Gradient")]
        [DefaultValue("Horizontal")]
        public LinearGradientMode PanelGradientMode
        {
            get
            {
                return panelGradientMode;
            }

            set
            {
                if (panelGradientMode != value)
                {
                    panelGradientMode = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.PanelGradientModeProperty);
                }
            }
        }

        /// <summary>
        /// Get/Set the style for the ZeroitPandaPanel
        /// </summary>
        /// <value>The zeroit panda panel style.</value>
        /// <remarks><see cref="UIComponents.ZeroitPandaPanelStyle.Custom" /> uses all user-defined settings, while
        /// <see cref="UIComponents.ZeroitPandaPanelStyle.WindowsXP" /> provides overrides to certain properties
        /// to provide a <c>WindowsXP</c> look and feel.
        /// <para>
        /// Currently, <see cref="UIComponents.ZeroitPandaPanelStyle.WindowsXP" />, overrides user-specified values
        /// for <see cref="ExpandedGlyphs" /> and <see cref="CollapsedGlyphs" /></para></remarks>
        [Category("Behavior"),
        Description("ZeroitPandaPanel Style"),
        DefaultValue(typeof(ZeroitPandaPanelStyle), "Custom")]
        public ZeroitPandaPanelStyle ZeroitPandaPanelStyle
        {
            get
            {
                return xpPanelStyle;
            }

            set
            {
                if (xpPanelStyle != value)
                {
                    xpPanelStyle = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.ZeroitPandaPanelStyleProperty);
                }
            }
        }

        /// <summary>
        /// Get/Set the initial animation rate for expand/collapse
        /// </summary>
        /// <value>The animation rate.</value>
        /// <remarks>The animation rate increases on each interval to provide an acceleration effect
        /// <para>Set to zero (0) for no animation</para><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.AnimationRateProperty" /> argument</para></remarks>
        [Category("Behavior"),
        Description("Rate of animation in milliseconds (or 0 for no animation)"),
        DefaultValue(50)]
        public int AnimationRate
        {
            get
            {
                return animationRate;
            }

            set
            {
                if (animationRate != value)
                {
                    animationRate = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.AnimationRateProperty);
                }
            }
        }

        /// <summary>
        /// Get/Set the transparency of the <c>ZeroitPandaPanel</c> when disabled
        /// </summary>
        /// <value>The disabled opacity.</value>
        /// <remarks>The default value is 255 (no transparency). Decrease towards zero to
        /// cause transparency effect
        /// <para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.DisabledOpacityProperty" /> argument</para></remarks>
        [Category("Behavior"),
        Description("Level of transparency when ZeroitPandaPanel is disabled"),
        DefaultValue(byte.MaxValue)]
        public byte DisabledOpacity
        {
            get
            {
                return disabledOpacity;
            }

            set
            {
                if (disabledOpacity != value)
                {
                    disabledOpacity = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.DisabledOpacityProperty);
                }
            }
        }

        /// <summary>
        /// Sets the height.
        /// </summary>
        /// <param name="height">The height.</param>
        protected void SetHeight(int height)
        {
            // isLayoutDirty = true ;
            Height = height;
        }

        /// <summary>
        /// Get/Set the panel state
        /// </summary>
        /// <value>The state of the panel.</value>
        /// <remarks><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.PanelStateProperty" /> argument</para>
        /// <para>Fires a PanelStateChange event w/ <see cref="System.EventArgs.Empty" /> arguments</para></remarks>
        [Category("Behavior"),
        Description("The state of the ZeroitPandaPanel"),
        DefaultValue(ZeroitPandaPanelState.Expanded)]
        public ZeroitPandaPanelState PanelState
        {
            get
            {
                return panelState;
            }

            set
            {
                if (panelState != value)
                {
                    if (value == ZeroitPandaPanelState.Collapsed)
                    {
                        // We must be early in the init cycle...
                        if (xpCaptionRect.IsEmpty)
                        {
                            using (Graphics graphics = CreateGraphics())
                            {
                                UpdatePanelLayout(graphics);
                            }
                        }

                        // set an internal draw state to keep ExpandedHeight from being effected
                        PanelDrawState = ZeroitPandaPanelDrawState.Internal;
                        SetHeight(xpCaptionRect.Top + xpCaptionRect.Height);
                        PanelDrawState = ZeroitPandaPanelDrawState.Normal;
                        panelState = value;
                    }
                    else
                    {
                        panelDrawState = ZeroitPandaPanelDrawState.Expanding;
                        panelState = value;
                        // restore expanded height
                        SetHeight(expandedHeight);
                        panelDrawState = ZeroitPandaPanelDrawState.Normal;
                    }

                    // Send panel state change event
                    OnPanelStateChange();
                    OnPropertyChange(ZeroitPandaPanelProperties.PanelStateProperty);

                    if (panelState == ZeroitPandaPanelState.Expanded)
                        OnPanelExpanded();
                    else
                        OnPanelCollapsed();
                }
            }
        }

        /// <summary>
        /// <see langword="true" /> if the panel is always expanded and
        /// cannot be collapsed
        /// </summary>
        /// <value><c>true</c> if this instance is fixed height; otherwise, <c>false</c>.</value>
        /// <remarks>Glyph images are <b>not</b> drawn when the <c>ZeroitPandaPanel</c> is fixed
        /// <para>The default value for this property is <see langword="false" /></para><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.IsFixedHeightProperty" /> argument</para></remarks>
        [Category("Behavior"),
        Description("True if the panel is always expanded and cannot be collapsed"),
        DefaultValue(false)]
        public bool IsFixedHeight
        {
            get
            {
                return isFixedHeight;
            }

            set
            {
                if (isFixedHeight != value)
                {
                    isFixedHeight = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.IsFixedHeightProperty);
                }
            }
        }

        /// <summary>
        /// The caption text for the <c>ZeroitPandaPanel</c>
        /// </summary>
        /// <value>The caption.</value>
        /// <remarks>Similar to the <see cref="Font" /> property, we could use the base class text property.
        /// <para>The default value for this property is <see cref="String.Empty" /></para><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.CaptionProperty" /> argument</para></remarks>
        [Category("Caption"),
        Description("The text displayed in the caption of the ZeroitPandaPanel"), DefaultValue("")]
        public String Caption
        {
            get
            {
                return text;
            }

            set
            {
                if (value != text)
                {
                    if (value == null) value = String.Empty;
                    text = (value != String.Empty) ? value : Name;
                    OnPropertyChange(ZeroitPandaPanelProperties.CaptionProperty);
                }
            }
        }

        /// <summary>
        /// Determine if the property needs to be serialized by the designer during code generation
        /// </summary>
        /// <returns><see langword="true" /> if the property has a non-default value</returns>
        /// <remarks>Called by the IDE</remarks>
        protected bool ShouldSerializeCaption()
        {
            return text != String.Empty;
        }

        /// <summary>
        /// Resets the property value back to its default
        /// </summary>
        /// <remarks>Called by the IDE</remarks>
        protected void ResetCaption()
        {
            Caption = String.Empty;
        }

        /// <summary>
        /// The <see cref="Font" /> for the caption text
        /// </summary>
        /// <value>The font.</value>
        /// <remarks>There is no real reason to override this Property. It is an artifact of my
        /// initial thinking. If removed adjustments would need to be made to
        /// detect font changes so the property change event can be fired. As an alternative,
        /// override property but dont define a <see cref="Font" /> field and just use <c>base.Font</c><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.FontProperty" /> argument</para></remarks>
        [Category("Caption"),
        Description("The font for the caption of the ZeroitPandaPanel"), DefaultValue("")]
        public override Font Font
        {
            get
            {
                return font;
            }

            set
            {
                if (font != value)
                {
                    if (font != null)
                    {
                        font.Dispose();
                    }

                    font = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.FontProperty);
                }
            }
        }

        /// <summary>
        /// <see cref="XPanelColorPair" /> describing the foreground/background colors for the
        /// caption text in a <i>normal</i> state
        /// </summary>
        /// <value>The text colors.</value>
        /// <remarks>Currently, the background color value is ignored (i.e., never used) and always
        /// <see cref="Color.Empty" />.
        /// Also, we do not map the text foreground color to the base.Foreground property
        /// <para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.TextColorsProperty" /> argument</para></remarks>
        [Category("Caption"), Description("The normal fore/background colors of the ZeroitPandaPanel caption text")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public XPanelColorPair TextColors
        {
            get
            {
                return textColors;
            }

            set
            {
                if (textColors != value)
                {
                    textColors = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.TextColorsProperty);
                }
            }
        }

        /// <summary>
        /// Determine if the property needs to be serialized by the designer during code generation
        /// </summary>
        /// <returns><see langword="true" /> if the property has a non-default value</returns>
        /// <remarks>Called by the IDE</remarks>
        protected bool ShouldSerializeTextColors()
        {
            return textColors != DefaultTextColors;
        }

        /// <summary>
        /// Resets the property value back to its default
        /// </summary>
        /// <remarks>Called by the IDE</remarks>
        protected void ResetTextColors()
        {
            TextColors = DefaultTextColors;
        }

        /// <summary>
        /// <see cref="XPanelColorPair" /> describing the foreground/background colors for the
        /// caption text when in a <i>hot/highlight</i> state (mouse over)
        /// </summary>
        /// <value>The text highlight colors.</value>
        /// <remarks>Currently, the background color value is ignored (i.e., never used) and always
        /// <see cref="Color.Empty" /><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.TextHighlightColorsProperty" /> argument</para></remarks>
        [Category("Caption"), Description("The highlight colors (fore/back) of the ZeroitPandaPanel Caption text when highlighted")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public XPanelColorPair TextHighlightColors
        {
            get
            {
                return textHighlightColors;
            }

            set
            {
                if (textHighlightColors != value)
                {
                    textHighlightColors = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.TextHighlightColorsProperty);
                }
            }
        }

        /// <summary>
        /// Determine if the property needs to be serialized by the designer during code generation
        /// </summary>
        /// <returns><see langword="true" /> if the property has a non-default value</returns>
        /// <remarks>Called by the IDE</remarks>
        protected bool ShouldSerializeTextHighlightColors()
        {
            return textHighlightColors != DefaultTextHighlightColors;
        }

        /// <summary>
        /// Resets the property value back to its default
        /// </summary>
        /// <remarks>Called by the IDE</remarks>
        protected void ResetTextHighlightColors()
        {
            TextHighlightColors = DefaultTextHighlightColors;
        }

        /// <summary>
        /// <see cref="GradientColor" /> describing the gradient colors of the
        /// caption
        /// </summary>
        /// <value>The caption gradient.</value>
        /// <remarks>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.CaptionGradientProperty" /> argument</remarks>
        [Category("Caption"), Description("The gradient colors for the caption of the ZeroitPandaPanel")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GradientColor CaptionGradient
        {
            get
            {
                return captionGradient;
            }

            set
            {
                if (captionGradient != value)
                {
                    captionGradient = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.CaptionGradientProperty);
                }
            }
        }

        /// <summary>
        /// Determine if the property needs to be serialized by the designer during code generation
        /// </summary>
        /// <returns><see langword="true" /> if the property has a non-default value</returns>
        /// <remarks>Called by the IDE</remarks>
        protected bool ShouldSerializeCaptionGradient()
        {
            return captionGradient != DefaultCaptionGradient;
        }

        /// <summary>
        /// Resets the property value back to its default
        /// </summary>
        /// <remarks>Called by the IDE</remarks>
        protected void ResetCaptionGradient()
        {
            CaptionGradient = DefaultCaptionGradient;
        }

        /// <summary>
        /// The <see cref="Color" /> used to draw the caption underline
        /// </summary>
        /// <value>The caption underline.</value>
        /// <remarks>Set to <see cref="Color.Empty" /> for no underline
        /// <para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.CaptionUnderlineProperty" /> argument</para></remarks>
        [Category("Caption"),
        Description("Color for underlining the caption of the ZeroitPandaPanel")]
        public Color CaptionUnderline
        {
            get
            {
                return captionUnderline;
            }

            set
            {
                if (captionUnderline != value)
                {
                    captionUnderline = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.CaptionUnderlineProperty);
                }
            }
        }

        /// <summary>
        /// Determine if the property needs to be serialized by the designer during code generation
        /// </summary>
        /// <returns><see langword="true" /> if the property has a non-default value</returns>
        /// <remarks>Called by the IDE</remarks>
        protected bool ShouldSerializeCaptionUnderline()
        {
            return CaptionUnderline != DefaultCaptionUnderlineColor;
        }

        /// <summary>
        /// Resets the property value back to its default
        /// </summary>
        /// <remarks>Called by the IDE</remarks>
        protected void ResetCaptionUnderline()
        {
            CaptionUnderline = DefaultCaptionUnderlineColor;
        }

        /// <summary>
        /// The horizontal alignment of the caption text
        /// </summary>
        /// <value>The horz alignment.</value>
        /// <remarks>Uses the standard <see cref="StringAlignment" /> values that abstract right-to-left
        /// left-to-right semnatics
        /// <para>Default value is <see cref="StringAlignment.Near" /></para><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.HorzAlignmentProperty" /> argument</para></remarks>
        [Category("Caption"),
        Description("The horizontal alignment of the ZeroitPandaPanel caption text"),
        DefaultValue("Near")]
        public StringAlignment HorzAlignment
        {
            get
            {
                return horzAlignment;
            }

            set
            {
                if (horzAlignment != value)
                {
                    horzAlignment = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.HorzAlignmentProperty);
                }
            }
        }

        /// <summary>
        /// The vertical alignment of the caption text
        /// </summary>
        /// <value>The vert alignment.</value>
        /// <remarks>Uses the standard <see cref="StringAlignment" /> values that abstract right-to-left
        /// left-to-right semnatics
        /// <para>Default value is <see cref="StringAlignment.Near" /></para><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.VertAlignmentProperty" /> argument</para></remarks>
        [Category("Caption"),
        Description("The vertial alignment of the ZeroitPandaPanel caption text"),
        DefaultValue("Near")]
        public StringAlignment VertAlignment
        {
            get
            {
                return vertAlignment;
            }

            set
            {
                if (vertAlignment != value)
                {
                    vertAlignment = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.VertAlignmentProperty);
                }
            }
        }

        /// <summary>
        /// Describes images that appear on the left side of the caption
        /// </summary>
        /// <value>The image items.</value>
        /// <remarks><see cref="StateImageItems" /> which encapsulates an <see cref="ImageSet" /> and
        /// image mappings for the states <c>Normal</c>, <c>Highlight</c>, <c>Pressed</c>, and
        /// <c>Disabled</c><para>It is important that the <see cref="DesignerSerializationVisibility" /> attribute be
        /// set to <see cref="DesignerSerializationVisibility.Content" /> for proper code generation
        /// in InitializeComponent()</para><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.ImageItemsProperty" /> argument</para></remarks>
        [Category("Caption"),
        Description("Caption image indices by state")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StateImageItems ImageItems
        {
            get
            {
                return imageItems;
            }

            set
            {
                if (imageItems != value)
                {
                    imageItems = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.ImageItemsProperty);
                }
            }
        }

        /// <summary>
        /// Determine if the property needs to be serialized by the designer during code generation
        /// </summary>
        /// <returns><see langword="true" /> if the property has a non-default value</returns>
        /// <remarks>Called by the IDE</remarks>
        protected bool ShouldSerializeImageStates()
        {
            return !imageItems.IsEmpty || (imageItems.ImageSet != null);
        }

        /// <summary>
        /// Resets the property value back to its default
        /// </summary>
        /// <remarks>Called by the IDE</remarks>
        protected void ResetImageStates()
        {
            ImageItems = new StateImageItems();
        }

        /// <summary>
        /// Describes images that appear on the right side of the caption when the
        /// <c>ZeroitPandaPanel</c> is <see cref="ZeroitPandaPanelState.Expanded" />
        /// </summary>
        /// <value>The expanded glyphs.</value>
        /// <remarks><see cref="StateImageItems" /> which encapsulates an <see cref="ImageSet" /> and
        /// image mappings for the states <c>Normal</c>, <c>Highlight</c>, <c>Pressed</c>, and
        /// <c>Disabled</c><para>It is important that the <see cref="DesignerSerializationVisibility" /> attribute be
        /// set to <see cref="DesignerSerializationVisibility.Content" /> for proper code generation
        /// in InitializeComponent()</para><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.ExpandedGlyphsProperty" /> argument</para></remarks>
        [Category("Caption"),
        Description("Glyph state image map when the ZeroitPandaPanel is Expanded")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StateImageItems ExpandedGlyphs
        {
            get
            {
                return glyphImageItemsExpanded;
            }

            set
            {
                if (glyphImageItemsExpanded != value)
                {
                    glyphImageItemsExpanded = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.ExpandedGlyphsProperty);
                }
            }
        }

        /// <summary>
        /// Determine if the property needs to be serialized by the designer during code generation
        /// </summary>
        /// <returns><see langword="true" /> if the property has a non-default value</returns>
        /// <remarks>Called by the IDE</remarks>
        protected bool ShouldSerializeExpandedGlyphs()
        {
            return (!glyphImageItemsExpanded.IsEmpty || (glyphImageItemsExpanded.ImageSet != null));
        }

        /// <summary>
        /// Resets the property value back to its default
        /// </summary>
        /// <remarks>Called by the IDE</remarks>
        protected void ResetExpandedGlyphs()
        {
            ExpandedGlyphs = new StateImageItems();
        }

        /// <summary>
        /// Describes images that appear on the right side of the caption when the
        /// <c>ZeroitPandaPanel</c> is <see cref="ZeroitPandaPanelState.Collapsed" />
        /// </summary>
        /// <value>The collapsed glyphs.</value>
        /// <remarks><see cref="StateImageItems" /> which encapsulates an <see cref="ImageSet" /> and
        /// image mappings for the states <c>Normal</c>, <c>Highlight</c>, <c>Pressed</c>, and
        /// <c>Disabled</c><para>It is important that the <see cref="DesignerSerializationVisibility" /> attribute be
        /// set to <see cref="DesignerSerializationVisibility.Content" /> for proper code generation
        /// in InitializeComponent()</para><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.CollapsedGlyphsProperty" /> argument</para></remarks>
        [Category("Caption"),
        Description("Glyph state image map when the ZeroitPandaPanel is Collapsed")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StateImageItems CollapsedGlyphs
        {
            get
            {
                return glyphImageItemsCollapsed;
            }

            set
            {
                if (glyphImageItemsCollapsed != value)
                {
                    glyphImageItemsCollapsed = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.CollapsedGlyphsProperty);
                }
            }
        }

        /// <summary>
        /// Determine if the property needs to be serialized by the designer during code generation
        /// </summary>
        /// <returns><see langword="true" /> if the property has a non-default value</returns>
        /// <remarks>Called by the IDE</remarks>
        protected bool ShouldSerializeCollapsedGlyphs()
        {
            return (!glyphImageItemsCollapsed.IsEmpty || (imageItems.ImageSet != null));
        }

        /// <summary>
        /// Resets the property value back to its default
        /// </summary>
        /// <remarks>Called by the IDE</remarks>
        protected void ResetCollapsedGlyphs()
        {
            CollapsedGlyphs = new StateImageItems();
        }

        /// <summary>
        /// Defines the readius of the rounded corners on the caption
        /// </summary>
        /// <value>The curve radius.</value>
        /// <remarks>Default value is 7
        /// <para>Set to zero (0) for square corners</para><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.CurveRadiusProperty" /> argument</para></remarks>
        [Category("Caption"),
        Description("Curve radius for the caption of the ZeroitPandaPanel"),
        DefaultValue(7)]
        public int CurveRadius
        {
            get
            {
                return captionCurveRadius;
            }

            set
            {
                if (captionCurveRadius != value)
                {
                    captionCurveRadius = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.CurveRadiusProperty);
                }
            }
        }

        /// <summary>
        /// Describes the starting point (as a percentage) for the gradient of the caption
        /// </summary>
        /// <value>The gradient offset.</value>
        /// <remarks>When set to a value &gt; 0.0 and &lt; 1.0 the gradient does not
        /// begin until the appropriate relative position. The initial part of
        /// the caption is drawn using (a <see cref="SolidBrush" />) with the start
        /// <see cref="Color" /> of the <see cref="GradientColor" /> property.
        /// <para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.GradientOffsetProperty" /> argument</para></remarks>
        [Category("Caption"),
        Description("Gradient offset (start of gradient) for caption of the ZeroitPandaPanel"),
        DefaultValue(0.5d)]
        public double GradientOffset
        {
            get
            {
                return gradientOffset;
            }

            set
            {
                if (gradientOffset != value)
                {
                    gradientOffset = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.GradientOffsetProperty);
                }
            }
        }


        /// <summary>
        /// <see cref="GradientColor" /> describing the gradient colors of the panel
        /// </summary>
        /// <value>The panel gradient.</value>
        /// <remarks>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.PanelGradientProperty" /> argument</remarks>
        [Category("Panel"), Description("The gradient colors for the panel of the ZeroitPandaPanel")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GradientColor PanelGradient
        {
            get
            {
                return panelGradient;
            }

            set
            {
                if (panelGradient != value)
                {
                    panelGradient = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.PanelGradientProperty);
                }
            }
        }

        /// <summary>
        /// Determine if the property needs to be serialized by the designer during code generation
        /// </summary>
        /// <returns><see langword="true" /> if the property has a non-default value</returns>
        /// <remarks>Called by the IDE</remarks>
        protected bool ShouldSerializePanelGradient()
        {
            return panelGradient != DefaultPanelGradient;
        }

        /// <summary>
        /// Resets the property value back to its default
        /// </summary>
        /// <remarks>Called by the IDE</remarks>
        protected void ResetPanelGradient()
        {
            PanelGradient = DefaultPanelGradient;
        }


        /// <summary>
        /// The <see cref="Color" /> used to draw the panel outline
        /// </summary>
        /// <value>The color of the outline.</value>
        /// <remarks>Set to <see cref="Color.Empty" /> for no panel outline
        /// <para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.PanelOutlineColorProperty" /> argument</para></remarks>
        [Category("Panel"),
        Description("The color of the outline for the panel of the ZeroitPandaPanel")]
        public Color OutlineColor
        {
            get
            {
                return panelOutlineColor;
            }

            set
            {
                if (panelOutlineColor != value)
                {
                    panelOutlineColor = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.PanelOutlineColorProperty);
                }
            }
        }

        /// <summary>
        /// Determine if the property needs to be serialized by the designer during code generation
        /// </summary>
        /// <returns><see langword="true" /> if the property has a non-default value</returns>
        /// <remarks>Called by the IDE</remarks>
        protected bool ShouldSerializeOutlineColor()
        {
            return OutlineColor != DefaultOutlineColor;
        }

        /// <summary>
        /// Resets the property value back to its default
        /// </summary>
        /// <remarks>Called by the IDE</remarks>
        protected void ResetOutlineColor()
        {
            OutlineColor = DefaultOutlineColor;
        }


        /// <summary>
        /// The <see cref="Control.Height" /> of the <c>ZeroitPandaPanel</c> when expanded
        /// </summary>
        /// <value>The height of the expanded.</value>
        /// <remarks>This value needs to be remembered because we <b>whack</b> the height when
        /// it is collapsed. When to remember it is the <i>tricky</i> part
        /// <para>At various points we try to force the ExpandedHeight to be
        /// serialized so we have to protect ourselves from infinite recursion</para><para>We dont want the designer to serialize this property because
        /// the <c>ZeroitPandaPanel</c> uses a <see cref="TypeConverter" /> to create
        /// code that initializes this value through a constructor</para><para>Note that this property does not play the OnPropertyChange game</para></remarks>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ExpandedHeight
        {
            get
            {
                return expandedHeight;
            }

            set
            {
                if (expandedHeight != value)
                {
                    expandedHeight = value;
                }
            }
        }

        /// <summary>
        /// Get/Set the fixed height of the panel area
        /// </summary>
        /// <value>The height of the panel.</value>
        /// <remarks>Setting this property to value &gt; 0 causes the height of the panel area to be
        /// be a contstant value when expanded. This is useful for dynamic content which
        /// needs to control the available panel area regardless of the portion taken
        /// by the caption. This value can be set in the designer, in which case the panel will
        /// not resize (at all!)
        /// <para>
        /// If the layout of the <c>ZeroitPandaPanel</c> is not up-to-date it is updated
        /// </para><para>
        /// If the <c>ZeroitPandaPanel</c> is not animating, and currently expanded the height is adjusted
        /// immediately, otherwise the height change is reflected via <see cref="ExpandedHeight" />
        /// and will take effect at an appropriate future time
        /// </para><para>
        /// This method fires the <see cref="PropertyChange" /> event with an argument
        /// of <see cref="ZeroitPandaPanelProperties.PanelHeightProperty" /></para></remarks>
        [Category("Panel"), Description("Fixed Height of Panel area")]
        [DefaultValue(0)]
        public int PanelHeight
        {
            get
            {
                return panelHeight;
            }

            set
            {
                if (panelHeight != value)
                {
                    panelHeight = value;

                    if (isLayoutDirty)
                    {
                        using (Graphics graphics = CreateGraphics())
                        {
                            UpdatePanelLayout(graphics);
                        }
                    }

                    if (Height != (xpCaptionRect.Bottom + panelHeight))
                    {
                        if (!IsActive && IsExpanded)
                        {
                            SetHeight(xpCaptionRect.Bottom + panelHeight);
                        }
                        else
                        {
                            ExpandedHeight = xpCaptionRect.Bottom + panelHeight;
                        }
                    }

                    OnPropertyChange(ZeroitPandaPanelProperties.PanelHeightProperty);
                }
            }
        }

        /// <summary>
        /// Returns the current dimensions of the Panel
        /// </summary>
        /// <value>The panel rect.</value>
        /// <remarks>If the Layout of the <c>ZeroitPandaPanel</c> is not upto date, it is updated</remarks>
        [Browsable(false)]
        public Rectangle PanelRect
        {
            get
            {
                if (isLayoutDirty)
                {
                    using (Graphics graphics = CreateGraphics())
                    {
                        UpdatePanelLayout(graphics);
                    }
                }

                return xpPanelRect;
            }
        }

        /// <summary>
        /// Get/Set the margin/spacing values for items in the caption
        /// </summary>
        /// <value>The spacing.</value>
        /// <remarks>The X value defines the left/right margin from the caption borders, as
        /// well as the inter-item spacing. The Y value provides the
        /// margins from the top/bottom of the caption borders but has no effect
        /// on inter-item spacing
        /// <para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.SpacingProperty" /> argument</para></remarks>
        public Point Spacing
        {
            get
            {
                return spacing;
            }

            set
            {
                if (spacing != value)
                {
                    spacing = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.SpacingProperty);
                }
            }
        }

        /// <summary>
        /// Determine if the property needs to be serialized by the designer during code generation
        /// </summary>
        /// <returns><see langword="true" /> if the property has a non-default value</returns>
        /// <remarks>Called by the IDE</remarks>
        protected bool ShouldSerializeSpacing()
        {
            return (spacing != DefaultSpacing);
        }

        /// <summary>
        /// Resets the property value back to its default
        /// </summary>
        /// <remarks>Called by the IDE</remarks>
        protected void ResetSpacing()
        {
            Spacing = DefaultSpacing;
        }

        /// <summary>
        /// Forces the height of the caption to be &gt;= the height of the <see cref="ImageItems" />
        /// </summary>
        /// <value><c>true</c> if [fit to image]; otherwise, <c>false</c>.</value>
        /// <remarks>The default value is <see langword="false" /><para>
        /// This property can be used to prevent an image from extending above the caption header when
        /// the image is taller than best caption height. Otherwise it has no effect
        /// </para><para>Fires a PropertyChange event w/ <see cref="ZeroitPandaPanelProperties.FitToImageProperty" /> argument</para></remarks>
        [Category("Caption"), Description("True to force the caption height >= the image height"), DefaultValue(false)]
        public bool FitToImage
        {
            get
            {
                return isFitToImage;
            }

            set
            {
                if (isFitToImage != value)
                {
                    isFitToImage = value;
                    OnPropertyChange(ZeroitPandaPanelProperties.FitToImageProperty);
                }
            }
        }
        #endregion Base Properties

        #region Overrides
        /// <summary>
        /// Overridden to supress the user/designer from changing the border style
        /// </summary>
        /// <value>The border style.</value>
        /// <remarks>Do not display this property in the browse as it should be a constant
        /// value (<see cref="System.Windows.Forms.BorderStyle.None" />)</remarks>
        [Browsable(false)]
        public new BorderStyle BorderStyle
        {
            get
            {
                return BorderStyle.None;
            }

            set
            {
                if (value == BorderStyle.None)
                {
                    base.BorderStyle = value;
                }
            }
        }

        /// <summary>
        /// Overridden to supress the user/designer from changing the background color
        /// </summary>
        /// <value>The color of the back.</value>
        /// <remarks>Do not display this property in the browse as it should be a constant
        /// value (<see cref="Color.Transparent" />)</remarks>
        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                if (value == Color.Transparent)
                {
                    base.BackColor = value;
                }
            }
        }

        /// <summary>
        /// Overridden to supress the user/designer from changing the foreground color
        /// </summary>
        /// <value>The color of the fore.</value>
        /// <remarks>Do not display this property in the browser as it has no meaning
        /// See <see cref="TextColors" /> for setting the color of the caption text</remarks>
        [Browsable(false)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
        #endregion Overrides

        #region Cached Object Properties
        /// <summary>
        /// Get the full catpion rect which is the entire area of the
        /// caption, not just the <see cref="xpCaptionRect" />
        /// </summary>
        /// <value>The full caption rect.</value>
        /// <remarks>This rectangle includes any portion of the caption that extends
        /// above the actual caption area (such as the top of an image)</remarks>
        protected Rectangle FullCaptionRect
        {
            get
            {
                return new Rectangle(0, 0, xpCaptionRect.Width, xpCaptionRect.Top + xpCaptionRect.Height);
            }
        }
        /// <summary>
        /// <see cref="LinearGradientBrush" /> used to draw the caption background
        /// </summary>
        /// <value>The caption brush.</value>
        protected LinearGradientBrush CaptionBrush
        {
            get
            {
                if (captionBrush == null)
                {
                    if (Enabled)
                    {
                        captionBrush = new LinearGradientBrush(xpCaptionRect, CaptionGradient.Start, CaptionGradient.End, CaptionGradientMode);
                    }
                    else
                    {
                        ExtendedColor grayStart = new ExtendedColor(CaptionGradient.Start, 0.0d);
                        ExtendedColor grayEnd = new ExtendedColor(CaptionGradient.End, 0.0d);
                        grayStart.Alpha = DisabledOpacity;
                        grayEnd.Alpha = DisabledOpacity;

                        captionBrush = new LinearGradientBrush(
                            xpCaptionRect, grayStart, grayEnd,
                            CaptionGradientMode);
                    }

                    if ((GradientOffset > 0.0) && (GradientOffset < 1.0))
                    {
                        ColorBlend colorBlend = new ColorBlend();
                        colorBlend.Colors = new Color[] { captionBrush.LinearColors[0], captionBrush.LinearColors[0], captionBrush.LinearColors[1] };
                        colorBlend.Positions = new float[] { 0.0f, (float)GradientOffset, 1.0f };
                        captionBrush.InterpolationColors = colorBlend;
                    }
                    else if ((GradientOffset > -1.0) && (GradientOffset < 0.0))
                    {
                        ColorBlend colorBlend = new ColorBlend();
                        colorBlend.Colors = new Color[] { captionBrush.LinearColors[0], captionBrush.LinearColors[1], captionBrush.LinearColors[1] };
                        colorBlend.Positions = new float[] { 0.0f, (float)Math.Abs(GradientOffset), 1.0f };
                        captionBrush.InterpolationColors = colorBlend;
                    }
                }

                return captionBrush;
            }

            set
            {
                if (value != captionBrush)
                {
                    if (captionBrush != null)
                    {
                        captionBrush.Dispose();
                    }

                    captionBrush = value;
                }
            }
        }

        /// <summary>
        /// <see cref="Pen" /> used to draw the caption underline
        /// </summary>
        /// <value>The caption underline pen.</value>
        protected Pen CaptionUnderlinePen
        {
            get
            {
                if (captionUnderlinePen == null)
                {
                    if (Enabled)
                    {
                        captionUnderlinePen = new Pen(CaptionUnderline);
                    }
                    else
                    {
                        // Create a gray scale color by whacking the saturation of the underline colro
                        captionUnderlinePen = new Pen(new ExtendedColor(CaptionUnderline, 0.0d));
                    }
                }

                return captionUnderlinePen;
            }

            set
            {
                if (captionUnderlinePen != value)
                {
                    if (captionUnderlinePen != null)
                    {
                        captionUnderlinePen.Dispose();
                    }
                    captionUnderlinePen = value;
                }
            }
        }

        /// <summary>
        /// <see cref="LinearGradientBrush" /> used to draw the panel background
        /// </summary>
        /// <value>The panel brush.</value>
        protected LinearGradientBrush PanelBrush
        {
            get
            {
                if (panelBrush == null)
                {
                    if (Enabled)
                    {
                        panelBrush = new LinearGradientBrush(
                            xpPanelRect,
                            PanelGradient.Start,
                            PanelGradient.End,
                            PanelGradientMode
                            );
                    }
                    else
                    {
                        // Translate colors to gray scale by whacking the saturation level
                        ExtendedColor grayStart = new ExtendedColor(PanelGradient.Start, 0.0d);
                        ExtendedColor grayEnd = new ExtendedColor(PanelGradient.End, 0.0d);
                        // set alpha for the color = to the DisabledOpacity value
                        grayStart.Alpha = DisabledOpacity;
                        grayEnd.Alpha = DisabledOpacity;

                        panelBrush = new LinearGradientBrush(
                            xpPanelRect, grayStart, grayEnd,
                            PanelGradientMode);
                    }
                }

                return panelBrush;
            }

            set
            {
                if (value != panelBrush)
                {
                    if (panelBrush != null)
                    {
                        panelBrush.Dispose();
                    }

                    panelBrush = value;
                }
            }
        }

        /// <summary>
        /// Defines the outline of the caption area when <see cref="GradientOffset" /> is 0.0 or 1.0
        /// </summary>
        /// <value>The caption path.</value>
        protected GraphicsPath CaptionPath
        {
            get
            {
                if (captionPath == null)
                {
                    /*int diameter = 2 * captionCurveRadius ;

					captionPath = new GraphicsPath() ;
					captionPath.AddLine(xpCaptionRect.Left + captionCurveRadius, xpCaptionRect.Top, xpCaptionRect.Right - diameter - 1, xpCaptionRect.Top) ;
					captionPath.AddArc(xpCaptionRect.Right - diameter - 1, xpCaptionRect.Top, diameter, diameter, 270, 90) ;
					captionPath.AddLine(xpCaptionRect.Right, xpCaptionRect.Top + captionCurveRadius, xpCaptionRect.Right, xpCaptionRect.Bottom) ;
					captionPath.AddLine(xpCaptionRect.Right, xpCaptionRect.Bottom, xpCaptionRect.Left - 1, xpCaptionRect.Bottom) ;
					captionPath.AddArc(xpCaptionRect.Left, xpCaptionRect.Top, diameter, diameter, 180, 90) ;*/

                    captionPath = RoundedRect.CreatePath(xpCaptionRect, captionCurveRadius, 0, CaptionCornerType);
                }

                return captionPath;
            }

            set
            {
                if (captionPath != null)
                {
                    captionPath.Dispose();
                }

                captionPath = value;
            }
        }

        /// <summary>
        /// <see cref="Font" /> used to draw the <see cref="Caption" /> when the <c>ZeroitPandaPanel</c> is disabled
        /// </summary>
        /// <value>The disabled font.</value>
        /// <remarks>Normal caption font is <b>FontStyle.Bold</b>. Disabled font is <c>FontStyle.Regular</c></remarks>
        protected Font DisabledFont
        {
            get
            {
                if (disabledFont == null)
                {
                    disabledFont = new Font(Font, FontStyle.Regular);
                }

                return disabledFont;
            }

            set
            {
                if (disabledFont != value)
                {
                    if (disabledFont != null)
                    {
                        disabledFont.Dispose();
                    }
                    disabledFont = value;
                }
            }
        }

        /// <summary>
        /// <see cref="SolidBrush" /> used to draw the caption text when <i>normal</i>
        /// </summary>
        /// <value>The font brush.</value>
        protected SolidBrush FontBrush
        {
            get
            {
                if (fontBrush == null)
                {
                    fontBrush = new SolidBrush(TextColors.Foreground);
                }

                return fontBrush;
            }

            set
            {
                if (fontBrush != value)
                {
                    if (fontBrush != null)
                    {
                        fontBrush.Dispose();
                    }

                    fontBrush = value;
                }
            }
        }

        /// <summary>
        /// <see cref="SolidBrush" /> used to draw the caption text when <i>highlighted</i> (mouse over)
        /// </summary>
        /// <value>The font highlight brush.</value>
        protected SolidBrush FontHighlightBrush
        {
            get
            {
                if (fontHighlightBrush == null)
                {
                    Color color = TextHighlightColors.Foreground;
                    if (color == Color.Empty)
                    {
                        color = TextColors.Foreground;
                    }
                    fontHighlightBrush = new SolidBrush(color);
                }

                return fontHighlightBrush;
            }

            set
            {
                if (fontHighlightBrush != value)
                {
                    if (fontHighlightBrush != null)
                    {
                        fontHighlightBrush.Dispose();
                    }

                    fontHighlightBrush = value;
                }
            }
        }

        /// <summary>
        /// <see cref="StringFormat" /> used to measure the Y extent of the caption text
        /// </summary>
        /// <value>The measure text format.</value>
        /// <remarks>Respects the user specified horizontal/vertical alignments and
        /// trims to "..." on word boundaries</remarks>
        protected StringFormat MeasureTextFormat
        {
            get
            {
                if (measureTextFormat == null)
                {
                    measureTextFormat = new StringFormat();
                    measureTextFormat.Trimming = StringTrimming.Word;
                    measureTextFormat.Alignment = horzAlignment;
                    measureTextFormat.LineAlignment = vertAlignment;
                }

                return measureTextFormat;
            }

            set
            {
                measureTextFormat = value;
            }
        }

        /// <summary>
        /// <see cref="StringFormat" /> used to draw caption text
        /// </summary>
        /// <value>The draw text format.</value>
        /// <remarks>Respects the user specified horizontal/vertical alignments and
        /// trims to "..." on word boundaries
        /// <para>Use of <see cref="StringFormatFlags.LineLimit" /> prevents
        /// a partial line from being displayed (vertically)</para></remarks>
        protected StringFormat DrawTextFormat
        {
            get
            {
                if (drawTextFormat == null)
                {
                    drawTextFormat = new StringFormat(StringFormatFlags.LineLimit);
                    drawTextFormat.Alignment = horzAlignment;
                    drawTextFormat.LineAlignment = vertAlignment;
                    drawTextFormat.Trimming = StringTrimming.EllipsisWord;
                }

                return drawTextFormat;
            }

            set
            {
                drawTextFormat = value;
            }
        }
        #endregion Cached Object Properties

        #region PanelState Helpers
        /// <summary>
        /// <see true="true" /> if some action is occurring on the panel
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool IsActive
        {
            get
            {
                return PanelDrawState != ZeroitPandaPanelDrawState.Normal;
            }
        }

        /// <summary>
        /// <see true="true" /> if the panel is expanding
        /// </summary>
        /// <value><c>true</c> if this instance is expanding; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool IsExpanding
        {
            get
            {
                return PanelDrawState == ZeroitPandaPanelDrawState.Expanding;
            }
        }

        /// <summary>
        /// <see true="true" /> if the panel is collapsing
        /// </summary>
        /// <value><c>true</c> if this instance is collapsing; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool IsCollapsing
        {
            get
            {
                return PanelDrawState == ZeroitPandaPanelDrawState.Collapsing;
            }
        }

        /// <summary>
        /// <see true="true" /> if <see cref="ZeroitPandaPanelState.Expanded" />
        /// </summary>
        /// <value><c>true</c> if this instance is expanded; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool IsExpanded
        {
            get
            {
                return panelState == ZeroitPandaPanelState.Expanded;
            }
        }

        /// <summary>
        /// <see true="true" /> if <see cref="ZeroitPandaPanelState.Collapsed" />
        /// </summary>
        /// <value><c>true</c> if this instance is collapsed; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool IsCollapsed
        {
            get
            {
                return panelState == ZeroitPandaPanelState.Collapsed;
            }
        }
        #endregion PanelState Helpers

        #region Events
        /// <summary>
        /// Register/Remove a PropertyChange listener
        /// </summary>
        public event PanelPropertyChangeHandler PropertyChange
        {
            add
            {
                propertyChangeListeners += value;
            }

            remove
            {
                propertyChangeListeners -= value;
            }
        }

        /// <summary>
        /// Register/Remove a PanelStateChange listener
        /// </summary>
        public event EventHandler PanelStateChange
        {
            add
            {
                panelStateChangeListeners += value;
            }

            remove
            {
                panelStateChangeListeners -= value;
            }
        }

        /// <summary>
        /// Register/Remove a Collapsed event listener
        /// </summary>
        public event EventHandler Collapsed
        {
            add
            {
                panelCollapsedListeners += value;
            }

            remove
            {
                panelCollapsedListeners -= value;
            }
        }

        /// <summary>
        /// Register/Remove a Collapsing event listener
        /// </summary>
        public event EventHandler Collapsing
        {
            add
            {
                panelCollapsingListeners += value;
            }

            remove
            {
                panelCollapsingListeners -= value;
            }
        }

        /// <summary>
        /// Register/Remove a Expanding event listener
        /// </summary>
        public event EventHandler Expanding
        {
            add
            {
                panelExpandingListeners += value;
            }

            remove
            {
                panelExpandingListeners -= value;
            }
        }

        /// <summary>
        /// Register/Remove a Expanded event listener
        /// </summary>
        public event EventHandler Expanded
        {
            add
            {
                panelExpandedListeners += value;
            }

            remove
            {
                panelExpandedListeners -= value;
            }
        }
        #endregion Events
        #endregion Properties

        #region Methods
        /// <summary>
        /// Routine to toggle the state of <c>ZeroitPandaPanel</c> from Expanded to Collapsed
        /// (or visa-versa)
        /// </summary>
        /// <remarks>This routine provides a way to trigger the visual animation of
        /// expanding/collapsing a <c>ZeroitPandaPanel</c></remarks>
        public virtual void TogglePanelState()
        {
            // If we are animating, start it now...
            if (animationRate != 0)
            {
                if (IsExpanded)
                {
                    PanelDrawState = ZeroitPandaPanelDrawState.Collapsing;
                }
                else
                {
                    PanelDrawState = ZeroitPandaPanelDrawState.Expanding;
                }

                StartAnimation();
            }
            else
            {
                // No animation. Just expand/collapse immediately
                // (note: set the DrawState to internal so that expandedHeight does not
                // get mucked up)
                if (IsExpanded)
                {
                    PanelDrawState = ZeroitPandaPanelDrawState.Internal;
                    PanelState = ZeroitPandaPanelState.Collapsed;
                }
                else
                {
                    PanelDrawState = ZeroitPandaPanelDrawState.Internal;
                    PanelState = ZeroitPandaPanelState.Expanded;
                }

                // we are back to normal
                PanelDrawState = ZeroitPandaPanelDrawState.Normal;
            }
        }
        #endregion Methods

        #region Implementation
        /// <summary>
        /// Send a <see cref="PanelStateChange" /> to listeners
        /// </summary>
        protected virtual void OnPanelStateChange()
        {
            if (panelStateChangeListeners != null)
            {
                panelStateChangeListeners(this, System.EventArgs.Empty);
            }
        }

        /// <summary>
        /// Preprocessor for <see cref="PropertyChange" /> events
        /// </summary>
        /// <param name="xpPanelProperty">The property that changed</param>
        /// <remarks>Look at each property change and invalidate cached GDI+ objects
        /// as necessary.</remarks>
        protected virtual void OnPropertyChange(ZeroitPandaPanelProperties xpPanelProperty)
        {
            switch (xpPanelProperty)
            {
                case ZeroitPandaPanelProperties.HorzAlignmentProperty:
                case ZeroitPandaPanelProperties.VertAlignmentProperty:
                    MeasureTextFormat = null;
                    DrawTextFormat = null;
                    break;

                case ZeroitPandaPanelProperties.TextColorsProperty:
                    FontBrush = null;
                    break;

                case ZeroitPandaPanelProperties.TextHighlightColorsProperty:
                    FontHighlightBrush = null;
                    break;

                case ZeroitPandaPanelProperties.CaptionGradientProperty:
                case ZeroitPandaPanelProperties.CaptionGradientModeProperty:
                case ZeroitPandaPanelProperties.GradientOffsetProperty:
                case ZeroitPandaPanelProperties.CaptionCornerTypeProperty:
                    CaptionBrush = null;
                    break;

                case ZeroitPandaPanelProperties.FontProperty:
                    DisabledFont = null;
                    isLayoutDirty = true;
                    break;

                case ZeroitPandaPanelProperties.CaptionProperty:
                case ZeroitPandaPanelProperties.SpacingProperty:
                case ZeroitPandaPanelProperties.CurveRadiusProperty:
                case ZeroitPandaPanelProperties.FitToImageProperty:
                case ZeroitPandaPanelProperties.ZeroitPandaPanelStyleProperty:
                case ZeroitPandaPanelProperties.PanelHeightProperty:
                    isLayoutDirty = true;
                    break;

                case ZeroitPandaPanelProperties.CaptionUnderlineProperty:
                    CaptionUnderlinePen = null;
                    // this may not be necessary, but its hard to tell without more logic
                    isLayoutDirty = true;
                    break;

                case ZeroitPandaPanelProperties.PanelGradientProperty:
                case ZeroitPandaPanelProperties.PanelGradientModeProperty:
                    PanelBrush = null;
                    break;

                case ZeroitPandaPanelProperties.IsFixedHeightProperty:
                    if (IsFixedHeight)
                    {
                        PanelState = ZeroitPandaPanelState.Expanded;
                    }
                    break;

                case ZeroitPandaPanelProperties.PanelStateProperty:
                case ZeroitPandaPanelProperties.ExpandedGlyphsProperty:
                case ZeroitPandaPanelProperties.CollapsedGlyphsProperty:
                    break;

                case ZeroitPandaPanelProperties.EnabledProperty:
                    CaptionBrush = null;
                    PanelBrush = null;
                    CaptionUnderlinePen = null;
                    break;

                case ZeroitPandaPanelProperties.DisabledOpacityProperty:
                    if (!Enabled)
                    {
                        Invalidate();
                    }
                    break;
            }

            // forward the event to listeners
            if (propertyChangeListeners != null)
            {
                propertyChangeListeners(this, new ZeroitPandaPanelPropertyChangeEventArgs(xpPanelProperty));
            }

            // invalidate the control
            Invalidate();
        }

        /// <summary>
        /// Shim for the <see cref="Expanding" /> event
        /// </summary>
        protected virtual void OnPanelExpanding()
        {
            if (panelExpandingListeners != null)
            {
                panelExpandingListeners(this, System.EventArgs.Empty);
            }
        }

        /// <summary>
        /// Shim for the <see cref="Expanded" /> event
        /// </summary>
        protected virtual void OnPanelExpanded()
        {
            if (panelExpandedListeners != null)
            {
                panelExpandedListeners(this, System.EventArgs.Empty);
            }
        }

        /// <summary>
        /// Shim for the <see cref="Collapsing" /> event
        /// </summary>
        protected virtual void OnPanelCollapsing()
        {
            if (panelCollapsingListeners != null)
            {
                panelCollapsingListeners(this, System.EventArgs.Empty);
            }
        }

        /// <summary>
        /// Shim for the <see cref="Collapsed" /> event
        /// </summary>
        protected virtual void OnPanelCollapsed()
        {
            if (panelCollapsedListeners != null)
            {
                panelCollapsedListeners(this, System.EventArgs.Empty);
            }
        }

        /// <summary>
        /// Get the appropriate image for the current state as defined by the
        /// specified <see cref="StateImageItems" />
        /// </summary>
        /// <param name="imageItems">Images and image state map</param>
        /// <returns>The appropriate image for the state, or the <c>Normal</c> image</returns>
        private Image GetStatefulImage(StateImageItems imageItems)
        {
            Image image = null;

            if (imageItems != null)
            {
                if (Enabled)
                {
                    // mouse down
                    if (isCaptionPressed)
                    {
                        image = imageItems.PressedImage;
                    }

                    // isCaptionHot and isCaptionPressed can be true at the same time
                    // so if we dont have a pressed image, try the hot image, otherwise...
                    if (isCaptionHot && (image == null))
                    {
                        image = imageItems.HighlightImage;
                    }
                }
                else
                {
                    image = imageItems.DisabledImage;
                }

                // fall back
                if (image == null)
                {
                    image = imageItems.NormalImage;
                }
            }

            return image;
        }

        /// <summary>
        /// Select the appropriate <see cref="StateImageItems" /> based on the current
        /// state of the <c>ZeroitPandaPanel</c>
        /// </summary>
        /// <returns>The <see cref="StateImageItems" /> containing the appropriate glyph images
        /// and state mappings</returns>
        private StateImageItems GetGlyphStateImageItems()
        {
            if (ZeroitPandaPanelStyle == ZeroitPandaPanelStyle.WindowsXP)
            {
                if (IsActive)
                {
                    return IsExpanding ? XPStyleCollapseGlyphs : XPStyleExpandGlyphs;
                }

                return IsExpanded ? XPStyleCollapseGlyphs : XPStyleExpandGlyphs;
            }

            if (IsActive)
            {
                return IsExpanding ? glyphImageItemsExpanded : glyphImageItemsCollapsed;
            }

            return IsExpanded ? glyphImageItemsExpanded : glyphImageItemsCollapsed;
        }

        /// <summary>
        /// Select the approprTiate <see cref="ImageSet" /> based on the current
        /// state of the <c>ZeroitPandaPanel</c>
        /// </summary>
        /// <returns>The <see cref="ImageSet" /> containing the appropriate glyph images</returns>
        private ImageSet GetGlyphImages()
        {
            return GetGlyphStateImageItems().ImageSet;
        }


        /// <summary>
        /// Gets or sets the state of the panel draw.
        /// </summary>
        /// <value>The state of the panel draw.</value>
        private ZeroitPandaPanelDrawState PanelDrawState
        {
            get
            {
                return panelDrawState;
            }

            set
            {
                if (panelDrawState != value)
                {
                    panelDrawState = value;

                    if (panelDrawState == ZeroitPandaPanelDrawState.Collapsing)
                    {
                        OnPanelCollapsing();
                    }
                    else if (panelDrawState == ZeroitPandaPanelDrawState.Expanding)
                    {
                        OnPanelExpanding();
                    }
                }
            }
        }
        #endregion Implementation

        #region Paint Code
        /// <summary>
        /// Measure the caption text to establish the <c>vertical extents</c> of its
        /// bounding box. This way we can use multi-line captions
        /// </summary>
        /// <param name="g">The <see cref="Graphics" /> object being used</param>
        /// <param name="width">The horizontal extents/limits</param>
        /// <returns>Size.</returns>
        protected Size GetTextSize(Graphics g, int width)
        {
            return g.MeasureString(text, Font, width, MeasureTextFormat).ToSize();
        }

        /// <summary>
        /// Calculate the various rectangles for the caption and caption items
        /// </summary>
        /// <param name="graphics">The <see cref="Graphics" /> instance to use. May be <see langword="null" /></param>
        /// <remarks>At initialization the <see cref="Graphics" /> instance may be null since we need to force an inital
        /// panel layout. In this case we do <b>not</b> clear the dirty flag
        /// <para>
        /// Images (on the left) are clipped to 64x64. Glyphs (on the right) are clipped to 32x32. These
        /// restrictions are arbitrary. Any image that is larger will be <i>scaled</i> to the max width/height
        /// </para></remarks>
        protected void UpdatePanelLayout(Graphics graphics)
        {
            // left/right margin (2 units of margin)
            int totalXSpacing = Spacing.X << 1;

            // Calculate the Image on the left if necessary. Clip to 64x64
            if (ImageItems.ImageSet != null)
            {
                imageRect = new Rectangle(0, 0, Math.Min(ImageItems.ImageSet.Size.Width, 64), Math.Min(ImageItems.ImageSet.Size.Height, 64));
                // 1 unit of margin (border to image)
                totalXSpacing += Spacing.X;
            }

            ImageSet glyphImages = GetGlyphImages();

            // Calculate the Glyph rectangle (expand and collapse) on the right. Clip to 32x32
            // dont draw glyphs for fixed height panels
            if ((glyphImages != null) && !IsFixedHeight)
            {
                glyphRect = new Rectangle(
                    0,
                    0,
                    Math.Min(glyphImages.Size.Width, 32),
                    Math.Min(glyphImages.Size.Height, 32)
                    );
                // another unit of margin
                totalXSpacing += Spacing.X;
            }

            Size textSize = new Size(16, 16);

            // Calculate the size of the caption text rect based on the space between the Image and Glyph
            // taking into account the appropriate margin/spacing. We are looking for vertical extents
            if (graphics != null)
            {
                textSize = GetTextSize(graphics, this.Width - totalXSpacing - glyphRect.Width - imageRect.Width);
            }

            // Calculate best height for caption without regard for any image
            int bestHeight = Math.Max(MinCaptionHeight, Math.Max(textSize.Height, glyphRect.Height));

            // If we want to fit the caption to the image then 
            if (isFitToImage)
            {
                bestHeight = Math.Max(bestHeight, imageRect.Height);
            }

            // need an extra line for this...
            if (CaptionUnderline != Color.Empty)
            {
                bestHeight++;
            }

            // clip the whole thing to 64 (does this make sense?)
            bestHeight = Math.Min(bestHeight, 64);

            int totalYSpacing = (Spacing.Y << 1);

            // this rect defines the entire caption including Y margins (top and bottom)
            xpCaptionRect = new Rectangle(0, 0, this.Width, bestHeight + totalYSpacing);

            // If the image height exceeds the caption height then the caption needs
            // to be offset in the Y direction appropriately
            if (ImageItems.ImageSet != null)
            {
                if (ImageItems.ImageSet.Size.Height > xpCaptionRect.Height)
                {
                    // we have an extra Spacing.Y so we need to take it out
                    xpCaptionRect.Offset(0, (imageRect.Height - xpCaptionRect.Height) + Spacing.Y);
                }
                else
                {
                    // Calculate offset in such a way as to insure that the bottom margin for the 
                    // image is always = Spacing.Y
                    imageRect.Offset(0, Math.Min(Spacing.Y, xpCaptionRect.Height - imageRect.Height));
                }

                imageRect.Offset(Spacing.X, 0);
            }

            // Calculate the *real* text rect (make as large as possible given the image/glyph)
            // this is very important for horz/vert alignment. Remember to add the Y margins (top and bottom)
            textRect = new Rectangle(
                imageRect.Right + Spacing.X,
                xpCaptionRect.Top + Spacing.Y,
                this.Width - totalXSpacing - glyphRect.Width - imageRect.Width,
                xpCaptionRect.Height - totalYSpacing
                );

            // Offset glyph rect. Right align and Center in caption
            if (!glyphRect.IsEmpty)
            {
                glyphRect.Offset(
                    xpCaptionRect.Width - Spacing.X - glyphRect.Width,
                    xpCaptionRect.Top + ((xpCaptionRect.Height - glyphRect.Height) >> 1)
                    );
            }

            // calculate the left over area (non-caption) as the panel rect
            xpPanelRect = new Rectangle(
                                xpCaptionRect.Left,
                                xpCaptionRect.Bottom,
                                xpCaptionRect.Width,
                                //panelHeight
                                Height - xpCaptionRect.Bottom
                                );

            // adjust for the caption underline
            if (CaptionUnderline != Color.Empty)
            {
                xpPanelRect.Inflate(0, -1);
            }

            // Account for border when drawing panel. Note that the
            // border is not applied to the top since there is no
            // "outline" border there
            if (OutlineColor != Color.Empty)
            {
                xpPanelRect.Inflate(-1, 0);
                xpPanelRect.Height -= 1;
            }

            //	--------------------------------------------------------------
            //	Dispose of cached GDI+ objects that are now invalid
            //	--------------------------------------------------------------
            CaptionBrush = null;
            CaptionPath = null;
            PanelBrush = null;

            if (graphics != null)
            {
                // layout is now up-to-date
                isLayoutDirty = false;
            }

#if DEBUG_DRAWING
			System.Diagnostics.Debug.WriteLine("--- ZeroitPandaPanel "+Caption+" ---") ;
			System.Diagnostics.Debug.WriteLine("("+Name+")"+"Caption Rect="+xpCaptionRect) ;
			System.Diagnostics.Debug.WriteLine("("+Name+")"+"Image Rect="+imageRect) ;
			System.Diagnostics.Debug.WriteLine("("+Name+")"+"Text Rect="+textRect) ;
			System.Diagnostics.Debug.WriteLine("("+Name+")"+"Glyph Rect="+glyphRect) ;
			System.Diagnostics.Debug.WriteLine("("+Name+")"+"Panel Rect="+xpPanelRect) ;
#endif
        }

        /// <summary>
        /// Draw the caption area (sans items)
        /// </summary>
        /// <param name="graphics">The <see cref="Graphics" /> object to use</param>
        protected void DrawCaptionBackground(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            graphics.FillPath(CaptionBrush, CaptionPath);

            if (CaptionUnderline != Color.Empty)
            {
                graphics.DrawLine(
                    CaptionUnderlinePen,
                    xpCaptionRect.Left,
                    xpCaptionRect.Bottom,
                    xpCaptionRect.Right,
                    xpCaptionRect.Bottom
                    );
            }
        }

        /// <summary>
        /// Draw the caption area of the <c>ZeroitPandaPanel</c>
        /// </summary>
        /// <param name="graphics"><see cref="Graphics" /> for drawing</param>
        protected void DrawCaption(Graphics graphics)
        {
            // if the layout is dirty recalculate
            if (isLayoutDirty)
            {
                UpdatePanelLayout(graphics);
            }

            // draw the caption area (sans items)
            DrawCaptionBackground(graphics);

            // Draw the image on the left side of the caption (if applicable)
            if (!imageRect.IsEmpty)
            {
                // get the image for this state
                Image image = GetStatefulImage(imageItems);

                // is it defined?
                if (image != null)
                {
#if DEBUG_DRAWING
					graphics.DrawRectangle(new Pen(System.Drawing.Color.Red,1),imageRect) ;
#else
                    // Draw the image. If disabled use the gray scale image attributes to perform a color transformation
                    // (note this transfrom is even applied to the disabled image when specified)
                    // If the source image is larger/smaller than the dest rect it will be stretched
                    graphics.DrawImage(image, imageRect,
                        0, 0, image.Width, image.Height,
                        GraphicsUnit.Pixel,
                        Enabled ? null : GrayScaleAttributes
                        );
#endif
                }
            }

            RectangleF textRectF = new RectangleF(
                    textRect.Left,
                    textRect.Top,
                    textRect.Width,
                    textRect.Height
                );

            // Draw tha caption text
            if (Enabled)
            {
#if DEBUG_DRAWING
				graphics.DrawRectangle(new Pen(System.Drawing.Color.Red,1),textRectF.Left,textRectF.Top,textRectF.Width,textRectF.Height) ;
#endif
                // Draw the caption text using these modes to emulate Windows XP look
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                // pick the correct brush
                graphics.DrawString(
                                Caption,
                                Font,
                                isCaptionHot ? FontHighlightBrush : FontBrush,
                                textRectF,
                                DrawTextFormat
                            );
            }
            else
            {
#if DEBUG_DRAWING
				graphics.DrawRectangle(new Pen(System.Drawing.Color.Red,1),textRectF.Left,textRectF.Top,textRectF.Width,textRectF.Height) ;
#endif
                // draw the text disabled using the DisabledFont (using a method provided by the framework)
                ControlPaint.DrawStringDisabled(
                                graphics,
                                Caption,
                                DisabledFont,
                                SystemColors.GrayText,
                                textRectF,
                                DrawTextFormat
                                );
            }

            // Lets do the Glyph
            ImageSet glyphImages = GetGlyphImages();

            // Draw the expand/collapse glyph (if applicable)
            if (!glyphRect.IsEmpty)
            {
                Image image = GetStatefulImage(GetGlyphStateImageItems());

                if (image != null)
                {
#if DEBUG_DRAWING
					graphics.DrawRectangle(new Pen(System.Drawing.Color.Red,1),glyphRect) ;
#else
                    // use gray scale image attributes if disabled
                    graphics.DrawImage(image, glyphRect,
                        0, 0, image.Width, image.Height,
                        GraphicsUnit.Pixel,
                        Enabled ? null : GrayScaleAttributes
                        );
#endif
                }
            }
        }

        /// <summary>
        /// Paint the panel portion of the <c>ZeroitPandaPanel</c>
        /// </summary>
        /// <param name="graphics"><see cref="Graphics" /> object for drawing</param>
        protected void DrawPanel(Graphics graphics)
        {
            // Draw Panel if visible
            if (xpPanelRect.Height > 1)
            {
                // Draw the panel outline if necessary. Note that xpPanelRect has
                // already been modified to take the outline into account so
                // we need to make minor adjustments to the coordinates when
                // we draw the outline
                if (OutlineColor != Color.Empty)
                {
                    // this is the only GDI+ object we *dont* cache
                    using (Pen p = new Pen(OutlineColor))
                    {
                        // Draw the panel outline w/o anti-aliasing (looks bad...)
                        graphics.SmoothingMode = SmoothingMode.None;

                        // Left Side
                        graphics.DrawLine(p,
                            xpPanelRect.Left - 1,
                            xpPanelRect.Top,
                            xpPanelRect.Left - 1,
                            xpPanelRect.Bottom
                            );
                        // Right Side
                        graphics.DrawLine(p,
                            xpPanelRect.Right,
                            xpPanelRect.Top,
                            xpPanelRect.Right,
                            xpPanelRect.Bottom
                            );
                        // Bottom
                        graphics.DrawLine(p,
                            xpPanelRect.Left - 1,
                            xpPanelRect.Bottom,
                            xpPanelRect.Right,
                            xpPanelRect.Bottom
                            );
                    }
                }

#if DEBUG_DRAWING
				using(Pen pen = new Pen(Color.White)) {
					graphics.DrawRectangle(pen,xpPanelRect) ;
				}
#else
                if (!PanelGradient.IsTransparent)
                {
                    // fill the panel with the gradient
                    graphics.FillRectangle(PanelBrush, xpPanelRect);
                }
#endif
            }
        }

        /// <summary>
        /// Paint the <c>ZeroitPandaPanel</c>
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs" /></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw the caption
            DrawCaption(e.Graphics);

            // Draw the panel area 
            DrawPanel(e.Graphics);
        }
        #endregion Paint Code

        #region Overrides
        /// <summary>
        /// Override this so that Docking of child controls will work
        /// </summary>
        /// <value>The display rectangle.</value>
        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle dispRect = PanelRect;
                dispRect.Inflate(-1, -1);
                return dispRect;
            }
        }

        /// <summary>
        /// Send an <see cref="PanelStateChange" /> when the visibility of the
        /// <c>ZeroitPandaPanel</c> changes
        /// </summary>
        /// <param name="e">The event args</param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
        }

        /// <summary>
        /// Prevent the panel from being inappropriately resized.
        /// This logic applies when the panel is collapsed (cuz we dont want to allow collapsed panel
        /// to be resized by/in the designer) as well as for fixed height panel areas (which are required
        /// to be a specific height and no other.)
        /// </summary>
        /// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
        /// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
        /// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
        /// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
        /// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if ((specified & BoundsSpecified.Height) != 0)
            {
                // Dont allow resize when collapsed and not animated
                if ((panelState == ZeroitPandaPanelState.Collapsed) && (PanelDrawState == ZeroitPandaPanelDrawState.Normal))
                    return;
            }

            int bestHeight = height;

            // Is it a fixed height panel?
            if (PanelHeight != 0)
            {
                // long as we are expanded an not animating then enforce this relationship
                if (!IsActive && IsExpanded)
                {
                    bestHeight = xpCaptionRect.Bottom + PanelHeight;
                }
            }

            isLayoutDirty = true;

            // Let the base class do the rest of the work
            base.SetBoundsCore(x, y, width, bestHeight, specified);
        }

        /// <summary>
        /// Track the mouse movement so we can draw the caption hot/normal
        /// </summary>
        /// <param name="e">MouseMove args</param>
        /// <remarks>If the <c>ZeroitPandaPanel</c><see cref="IsFixedHeight" /> property is
        /// <see langword="true" /> then panel is <b>never</b> hot</remarks>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // is it a caption hit?
            if ((e.Y < xpPanelRect.Top) && !IsFixedHeight)
            {
                // we are going hot
                if (!isCaptionHot)
                {
                    this.Cursor = Cursors.Hand;
                    isCaptionHot = true;
                    // redraw with highlight
                    Invalidate(FullCaptionRect);
                }
            }
            else
            {
                // its a miss, if we were hot, now we are not
                if (isCaptionHot)
                {
                    this.Cursor = Cursors.Default;
                    isCaptionHot = false;
                    // redraw w/o highlight
                    Invalidate(FullCaptionRect);
                }
            }
        }

        /// <summary>
        /// Mouse is out of the control
        /// </summary>
        /// <param name="e">MouseLeave args</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            // If we were hot, now we are not
            if (isCaptionHot)
            {
                this.Cursor = Cursors.Default;
                isCaptionHot = false;
                // redraw w/o highlight
                Invalidate(FullCaptionRect);
            }
        }

        /// <summary>
        /// Override base.OnSizeChanged
        /// </summary>
        /// <param name="e">The OnSizeChanged event args</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            // recalculate everything
            isLayoutDirty = true;

            // we are normal and expanded then this must be a
            // legitimate request to change the actual expanded height
            if (!IsActive && IsExpanded)
            {
                ExpandedHeight = Height;
                OnPanelStateChange();
            }

            Invalidate();
        }

        /// <summary>
        /// Handle mouse down event
        /// </summary>
        /// <param name="e">MouseDown event args</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // do nothing if any of these are true
            if ((e.Y >= xpPanelRect.Top) || this.IsActive || IsFixedHeight || (e.Button != MouseButtons.Left))
            {
                return;
            }

            // its a caption hit
            this.isCaptionPressed = true;

            // Call routine to change expand/collapse state of panel
            TogglePanelState();
        }

        /// <summary>
        /// Handle mouse up event
        /// </summary>
        /// <param name="e">MouseUp args</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            // force this to false
            this.isCaptionPressed = false;
            Invalidate(FullCaptionRect);
        }


        /// <summary>
        /// Handles enable/disable of <c>ZeroitPandaPanel</c>
        /// </summary>
        /// <param name="e">Enable event args</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            // send a property change so we can invalidate/redraw
            OnPropertyChange(ZeroitPandaPanelProperties.EnabledProperty);
        }
        #endregion Overrides

        #region Animation Code
        //	------------------------------------------------------------------
        //	The original source of this animation technique was written by
        //	Daren May for his Collapsible Panel implementation which can
        //	be found here:
        //		http://www.codeproject.com/cs/miscctrl/xpgroupbox.asp
        //
        //	Although I have changed things quite a bit, nothing is 
        //	fundamentally different from his original work, so I give
        //	many thanks to him for solving this problem so that I could
        //	solve others
        //	------------------------------------------------------------------

        // degree to adjust the height of the panel when animating
        /// <summary>
        /// The animation height adjustment
        /// </summary>
        private int animationHeightAdjustment = 0;
        // current opacity level
        /// <summary>
        /// The animation opacity
        /// </summary>
        private int animationOpacity = 0;
        // remember the base visibility of each control
        /// <summary>
        /// The animation visibility
        /// </summary>
        private Hashtable animationVisibility = new Hashtable();

        /// <summary>
        /// Initialize animation values and start the timer
        /// </summary>
        private void StartAnimation()
        {
            RememberControlsVisibility();
            animationHeightAdjustment = 1;
            animationOpacity = 5;
            animationTimer.Interval = animationRate;
            animationTimer.Enabled = true;
        }


        /// <summary>
        /// Set the alpha value (transparency) for a <see cref="Color" />
        /// </summary>
        /// <param name="alpha">The alpha value</param>
        /// <param name="c">The <see cref="Color" /></param>
        /// <returns>The color with the alpha modification</returns>
        private Color SetColorAlpha(int alpha, Color c)
        {
            if ((c != Color.Empty) && (c != Color.Transparent))
                c = Color.FromArgb(Math.Max(Math.Min(byte.MaxValue, alpha), 0), c);

            return c;
        }

        /// <summary>
        /// Handles the Tick event of the animationTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void animationTimer_Tick(object sender, System.EventArgs e)
        {
            //	---------------------------------------------------------------
            //	Gradually reduce the interval between timer events so that the
            //	animation begins slowly and eventually accelerates to completion
            //	---------------------------------------------------------------
            if (animationTimer.Interval > 10)
            {
                animationTimer.Interval -= 10;
            }
            else
            {
                animationHeightAdjustment += 2;
            }

            // Increase transparency as we collapse
            if ((animationOpacity + 5) < byte.MaxValue)
            {
                animationOpacity += 5;
            }

            int currOpacity = animationOpacity;

            switch (PanelDrawState)
            {
                case ZeroitPandaPanelDrawState.Expanding:
                    // still room to expand?
                    if ((Height + animationHeightAdjustment) < ExpandedHeight)
                    {
                        Height += animationHeightAdjustment;
                        // notify panel state change listeners so they can react
                        OnPanelStateChange();
                    }
                    else
                    {
                        // we are done so we dont want any transparency
                        currOpacity = byte.MaxValue;
                        PanelState = ZeroitPandaPanelState.Expanded;
                        PanelDrawState = ZeroitPandaPanelDrawState.Normal;
                    }
                    break;

                case ZeroitPandaPanelDrawState.Collapsing:
                    // still something to collapse
                    if ((Height - animationHeightAdjustment) > xpPanelRect.Top)
                    {
                        Height -= animationHeightAdjustment;
                        // continue decreasing opacity
                        currOpacity = byte.MaxValue - animationOpacity;
                        // notify panel state change listeners so they can react
                        OnPanelStateChange();
                    }
                    else
                    {
                        // we are done so we dont want any transparency
                        currOpacity = byte.MaxValue;
                        PanelState = ZeroitPandaPanelState.Collapsed;
                        PanelDrawState = ZeroitPandaPanelDrawState.Normal;
                    }
                    break;

                default:
                    return;
            }

            // set the opacity for all the controls on the ZeroitPandaPanel
            SetControlsOpacity(currOpacity);
            // make panel colors more/less transparent
            panelGradient.Start = SetColorAlpha(currOpacity, panelGradient.Start);
            panelGradient.End = SetColorAlpha(currOpacity, panelGradient.End);
            PanelBrush = null;
            panelOutlineColor = SetColorAlpha(currOpacity, panelOutlineColor);
            // hide/show controls based on the height of the ZeroitPandaPanel (as it shrinks/grows)
            SetControlsVisible();

            // are we done?
            if (!IsActive)
            {
                animationTimer.Enabled = false;
            }

            Invalidate();
        }

        /// <summary>
        /// Remembers the controls visibility.
        /// </summary>
        private void RememberControlsVisibility()
        {
            animationVisibility.Clear();
            foreach (Control c in this.Controls)
            {
                animationVisibility[c] = c.Visible;
            }
        }

        /// <summary>
        /// Changes the visibility of controls based upon the height of the ZeroitPandaPanel
        /// </summary>
        /// <remarks>Only used during animation</remarks>
        private void SetControlsVisible()
        {
            foreach (Control c in animationVisibility.Keys)
            {
                if (((bool)animationVisibility[c]) == true)
                {
                    c.Visible = (c.Bottom >= xpPanelRect.Top);
                }
            }
        }

        /// <summary>
        /// Changes the transparency of controls based upon the height of the ZeroitPandaPanel
        /// </summary>
        /// <param name="opacity">The opacity.</param>
        /// <remarks>Only used during animation</remarks>
        private void SetControlsOpacity(int opacity)
        {
            foreach (Control c in this.Controls)
            {
                if (c.Visible)
                {
                    try
                    {
                        if (c.BackColor != Color.Transparent)
                        {
                            c.BackColor = Color.FromArgb(opacity, c.BackColor);
                        }
                        // ignore exception from controls that do not support transparent background color
                    }
                    catch
                    {
                    }
                    c.ForeColor = Color.FromArgb(opacity, c.ForeColor);
                }
            }
        }
        #endregion Animation Code
    }

    #region class ZeroitPandaPanelPropertyChangeEventArgs (and related)
    /// <summary>
    /// Delegate signature for <see cref="ZeroitPandaPanel.PropertyChange" /> events
    /// </summary>
    /// <param name="xpPanel">The xp panel.</param>
    /// <param name="e">The <see cref="ZeroitPandaPanelPropertyChangeEventArgs"/> instance containing the event data.</param>
    public delegate void PanelPropertyChangeHandler(ZeroitPandaPanel xpPanel, ZeroitPandaPanelPropertyChangeEventArgs e);

    /// <summary>
    /// EventArgs for <see cref="ZeroitPandaPanel.PropertyChange" /> events
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ZeroitPandaPanelPropertyChangeEventArgs : System.EventArgs
    {
        /// <summary>
        /// The property that changed
        /// </summary>
        private readonly ZeroitPandaPanelProperties xpPanelProperty;

        /// <summary>
        /// Create a new <c>ZeroitPandaPanelPropertyChangeArgs</c> with the specified
        /// property enumeration value
        /// </summary>
        /// <param name="property">The property.</param>
        public ZeroitPandaPanelPropertyChangeEventArgs(ZeroitPandaPanelProperties property)
        {
            this.xpPanelProperty = property;
        }

        /// <summary>
        /// Get the <see cref="ZeroitPandaPanelProperties" /> property that changed
        /// </summary>
        /// <value>The zeroit panda panel property.</value>
        public ZeroitPandaPanelProperties ZeroitPandaPanelProperty
        {
            get
            {
                return this.xpPanelProperty;
            }
        }
    }
    #endregion class ZeroitPandaPanelPropertyChangeEventArgs
    #endregion
}
