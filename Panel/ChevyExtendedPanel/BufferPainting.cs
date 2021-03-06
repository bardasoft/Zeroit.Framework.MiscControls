﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MiscControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-19-2018
// ***********************************************************************
// <copyright file="BufferPainting.cs" company="Zeroit Dev Technologies">
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
using System.Windows.Forms;

namespace Zeroit.Framework.MiscControls
{
    #region ZeroitBufferPanel

    /// <summary>
    /// An extension of the Panel class that enables double buffering(all painting occurs in WM_PAINT
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Panel" />
    public class ZeroitBufferPanel : Panel
    {
        #region ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitBufferPanel"/> class.
        /// </summary>
        protected ZeroitBufferPanel()
        {
            ///set up the control styles so that it support double buffering painting
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.DoubleBuffer, true);

            UpdateStyles();

        }
        #endregion
    }

    #endregion
}
