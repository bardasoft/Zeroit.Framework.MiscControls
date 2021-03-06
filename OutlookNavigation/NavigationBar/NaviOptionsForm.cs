﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MiscControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-19-2018
// ***********************************************************************
// <copyright file="NaviOptionsForm.cs" company="Zeroit Dev Technologies">
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

namespace Zeroit.Framework.MiscControls
{
    /// <summary>
    /// Class NaviOptionsForm.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class NaviOptionsForm : System.Windows.Forms.Form
   {
        /// <summary>
        /// The bar
        /// </summary>
        ZeroitNaviBar bar;

        /// <summary>
        /// Initializes a new instance of the <see cref="NaviOptionsForm"/> class.
        /// </summary>
        public NaviOptionsForm()
      {
         InitializeComponent();
      }

        /// <summary>
        /// Initializes the specified bar.
        /// </summary>
        /// <param name="bar">The bar.</param>
        public void Initialize(ZeroitNaviBar bar)
      {
         this.bar = bar;
         checkedListBox1.Items.Clear();
         foreach (NaviBand band in bar.Bands)
         {
            checkedListBox1.Items.Add(band.Text, band.Visible);
         }
      }

        /// <summary>
        /// Handles the Click event of the buttonOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonOk_Click(object sender, EventArgs e)
      {
         // Set the new order
         for (int i = 0; i < bar.Bands.Count; i++)
         {
            int loc = checkedListBox1.Items.IndexOf(bar.Bands[i].Text);
            bar.Bands[i].Visible = checkedListBox1.CheckedItems.Contains(bar.Bands[i].Text);
            bar.Bands[i].Order = loc;
         }

         // And sort the list based on the new order
         bar.Bands.Sort(new NaviBandOrderComparer());
      }

        /// <summary>
        /// Handles the Click event of the buttonMoveUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonMoveUp_Click(object sender, EventArgs e)
      {
         if (checkedListBox1.SelectedIndex != 0)
         {
            object oldItem = checkedListBox1.Items[checkedListBox1.SelectedIndex - 1];
            checkedListBox1.Items[checkedListBox1.SelectedIndex - 1] =
               checkedListBox1.SelectedItem;
            checkedListBox1.Items[checkedListBox1.SelectedIndex] = oldItem;
            checkedListBox1.SelectedIndex -= 1;
         }
      }

        /// <summary>
        /// Handles the Click event of the buttonMoveDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonMoveDown_Click(object sender, EventArgs e)
      {
         if (checkedListBox1.SelectedIndex < checkedListBox1.Items.Count - 1)
         {
            object oldItem = checkedListBox1.Items[checkedListBox1.SelectedIndex + 1];
            checkedListBox1.Items[checkedListBox1.SelectedIndex + 1] =
               checkedListBox1.SelectedItem;
            checkedListBox1.Items[checkedListBox1.SelectedIndex] = oldItem;
            checkedListBox1.SelectedIndex += 1;
         }
      }

        /// <summary>
        /// Handles the Click event of the buttonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonReset_Click(object sender, EventArgs e)
      {
         // Sort list based on original order
         bar.Bands.Sort(new NaviBandOrgOrderComparer());
         Initialize(bar);
      }

        /// <summary>
        /// Handles the Click event of the buttonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
      {
         // Reset ordering posibly caused by reset button
         bar.Bands.Sort(new NaviBandOrderComparer());
      }
   }
}
