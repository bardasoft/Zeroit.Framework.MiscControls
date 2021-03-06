﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MiscControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-19-2018
// ***********************************************************************
// <copyright file="NaviControl.cs" company="Zeroit Dev Technologies">
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
using System.Windows.Forms;

namespace Zeroit.Framework.MiscControls
{
    /// <summary>
    /// Represents a control with customizable drawing and layout styles
    /// </summary>
    /// <seealso cref="System.Windows.Forms.ContainerControl" />
    [ToolboxItem(false)]
   public partial class NaviControl : ContainerControl
   {
        /// <summary>
        /// The layout style
        /// </summary>
        NaviLayoutStyle layoutStyle = NaviLayoutStyle.Office2007Blue;
        /// <summary>
        /// The layout style changed
        /// </summary>
        EventHandler layoutStyleChanged;

        /// <summary>
        /// The thread lock
        /// </summary>
        protected readonly object threadLock = new object();

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the NaviControl
        /// </summary>
        public NaviControl()
      {
         InitializeComponent();
      }

        /// <summary>
        /// Initializes a new instance of the NaviControl
        /// </summary>
        /// <param name="container">The parent container</param>
        public NaviControl(IContainer container)
      {
         container.Add(this);
         InitializeComponent();
      }

        #endregion

        #region Properties

        /// <summary>
        /// Indicates how the control is presented to the user
        /// </summary>
        /// <value>The layout style.</value>
        [DefaultValue(NaviLayoutStyle.Office2007Blue)]
      public NaviLayoutStyle LayoutStyle
      {
         get { return layoutStyle; }
         set
         {
            layoutStyle = value;
            OnLayoutStyleChanged(new EventArgs());
         }
      }

        #endregion

        #region Events

        /// <summary>
        /// Occurs after the layout style has been changed
        /// </summary>
        public event EventHandler LayoutStyleChanged
      {
         add { lock (threadLock) { layoutStyleChanged += value; } }
         remove { lock (threadLock) { layoutStyleChanged -= value; } }
      }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the LayoutStyleChanged event
        /// </summary>
        /// <param name="e">Additional event info</param>
        protected virtual void OnLayoutStyleChanged(EventArgs e)
      {
         EventHandler handler = layoutStyleChanged;
         if (handler != null)
            handler(this, e);
      }

      #endregion
   }
}