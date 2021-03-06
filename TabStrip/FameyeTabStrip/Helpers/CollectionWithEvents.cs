// ***********************************************************************
// Assembly         : Zeroit.Framework.MiscControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 05-12-2018
// ***********************************************************************
// <copyright file="CollectionWithEvents.cs" company="Zeroit Dev Technologies">
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
using System.Collections;
using System.ComponentModel;

namespace Zeroit.Framework.MiscControls
{
    /// <summary>
    /// Represents the method that will handle the event that has no data.
    /// </summary>
    public delegate void CollectionClear();

    /// <summary>
    /// Represents the method that will handle the event that has item data.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="value">The value.</param>
    public delegate void CollectionChange(int index, object value);

    /// <summary>
    /// Extend collection base class by generating change events.
    /// </summary>
    /// <seealso cref="System.Collections.CollectionBase" />
    public abstract class CollectionWithEvents : CollectionBase
    {
        // Instance fields
        /// <summary>
        /// The suspend count
        /// </summary>
        private int _suspendCount;

        /// <summary>
        /// Occurs just before the collection contents are cleared.
        /// </summary>
        [Browsable(false)]
        public event CollectionClear Clearing;

        /// <summary>
        /// Occurs just after the collection contents are cleared.
        /// </summary>
        [Browsable(false)]
        public event CollectionClear Cleared;

        /// <summary>
        /// Occurs just before an item is added to the collection.
        /// </summary>
        [Browsable(false)]
        public event CollectionChange Inserting;

        /// <summary>
        /// Occurs just after an item has been added to the collection.
        /// </summary>
        [Browsable(false)]
        public event CollectionChange Inserted;

        /// <summary>
        /// Occurs just before an item is removed from the collection.
        /// </summary>
        [Browsable(false)]
        public event CollectionChange Removing;

        /// <summary>
        /// Occurs just after an item has been removed from the collection.
        /// </summary>
        [Browsable(false)]
        public event CollectionChange Removed;

        /// <summary>
        /// Initializes DrawTab new instance of the CollectionWithEvents class.
        /// </summary>
        public CollectionWithEvents()
        {
            // Default to not suspended
            _suspendCount = 0;
        }

        /// <summary>
        /// Do not generate change events until resumed.
        /// </summary>
        public void SuspendEvents()
        {
            _suspendCount++;
        }

        /// <summary>
        /// Safe to resume change events.
        /// </summary>
        public void ResumeEvents()
        {
            --_suspendCount;
        }

        /// <summary>
        /// Gets DrawTab value indicating if events are currently suspended.
        /// </summary>
        /// <value><c>true</c> if this instance is suspended; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool IsSuspended
        {
            get { return (_suspendCount > 0); }
        }

        /// <summary>
        /// Raises the Clearing event when not suspended.
        /// </summary>
        protected override void OnClear()
        {
            if (!IsSuspended)
            {
                // Any attached event handlers?
                if (Clearing != null)
                    Clearing();
            }
        }

        /// <summary>
        /// Raises the Cleared event when not suspended.
        /// </summary>
        protected override void OnClearComplete()
        {
            if (!IsSuspended)
            {
                // Any attached event handlers?
                if (Cleared != null)
                    Cleared();
            }
        }

        /// <summary>
        /// Raises the Inserting event when not suspended.
        /// </summary>
        /// <param name="index">Index of object being inserted.</param>
        /// <param name="value">The object that is being inserted.</param>
        protected override void OnInsert(int index, object value)
        {
            if (!IsSuspended)
            {
                // Any attached event handlers?
                if (Inserting != null)
                    Inserting(index, value);
            }
        }

        /// <summary>
        /// Raises the Inserted event when not suspended.
        /// </summary>
        /// <param name="index">Index of inserted object.</param>
        /// <param name="value">The object that has been inserted.</param>
        protected override void OnInsertComplete(int index, object value)
        {
            if (!IsSuspended)
            {
                // Any attached event handlers?
                if (Inserted != null)
                    Inserted(index, value);
            }
        }

        /// <summary>
        /// Raises the Removing event when not suspended.
        /// </summary>
        /// <param name="index">Index of object being removed.</param>
        /// <param name="value">The object that is being removed.</param>
        protected override void OnRemove(int index, object value)
        {
            if (!IsSuspended)
            {
                // Any attached event handlers?
                if (Removing != null)
                    Removing(index, value);
            }
        }

        /// <summary>
        /// Raises the Removed event when not suspended.
        /// </summary>
        /// <param name="index">Index of removed object.</param>
        /// <param name="value">The object that has been removed.</param>
        protected override void OnRemoveComplete(int index, object value)
        {
            if (!IsSuspended)
            {
                // Any attached event handlers?
                if (Removed != null)
                    Removed(index, value);
            }
        }

        /// <summary>
        /// Returns the index of the first occurrence of DrawTab value.
        /// </summary>
        /// <param name="value">The object to locate.</param>
        /// <returns>Index of object; otherwise -1</returns>
        protected int IndexOf(object value)
        {
            // Find the 0 based index of the requested entry
            return List.IndexOf(value);
        }
    }
}
