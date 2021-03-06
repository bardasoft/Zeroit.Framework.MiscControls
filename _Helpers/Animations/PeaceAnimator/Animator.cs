﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MiscControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 07-06-2018
// ***********************************************************************
// <copyright file="Animator.cs" company="Zeroit Dev Technologies">
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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Zeroit.Framework.MiscControls.HelperControls.AnimationHelpers.WinFormAnimation
{
    /// <summary>
    /// The one dimensional animator class, useful for animating raw values
    /// </summary>
    /// <seealso cref="Zeroit.Framework.MiscControls.HelperControls.AnimationHelpers.WinFormAnimation.IAnimator" />
    public class Animator : IAnimator
    {
        /// <summary>
        /// The known one dimensional properties of WinForm controls
        /// </summary>
        public enum KnownProperties
        {
            /// <summary>
            /// The property named 'Value' of the object
            /// </summary>
            Value,

            ///// <summary>
            /////     The property named 'Text' of the object
            ///// </summary>
            //Text,

            ///// <summary>
            /////     The property named 'Caption' of the object
            ///// </summary>
            //Caption,

            ///// <summary>
            /////     The property named 'BackColor' of the object
            ///// </summary>
            //BackColor,

            ///// <summary>
            /////     The property named 'ForeColor' of the object
            ///// </summary>
            //ForeColor,

            /// <summary>
            /// The property named 'Opacity' of the object
            /// </summary>
            Opacity,

            /// <summary>
            /// The property with an Int or Floting point value other than Color or String value of the object
            /// </summary>
            Custom,

            /// <summary>
            /// The property with an Int or Floting point value other than Color or String value of the object
            /// </summary>
            Width,

            /// <summary>
            /// The property with an Int or Floting point value other than Color or String value of the object
            /// </summary>
            Height,

            ///// <summary>
            /////     The property with an Int or Floting point value other than Color or String value of the object
            ///// </summary>
            //Horizontal,

            ///// <summary>
            /////     The property with an Int or Floting point value other than Color or String value of the object
            ///// </summary>
            //Vertical,
        }

        /// <summary>
        /// The paths
        /// </summary>
        private readonly List<Path> _paths = new List<Path>();

        /// <summary>
        /// The temporary paths
        /// </summary>
        private readonly List<Path> _tempPaths = new List<Path>();

        /// <summary>
        /// The timer
        /// </summary>
        private readonly Timer _timer;

        /// <summary>
        /// The temporary reverse repeat
        /// </summary>
        private bool _tempReverseRepeat;

        /// <summary>
        /// The callback to get invoked at the end of the animation
        /// </summary>
        protected SafeInvoker EndCallback;

        /// <summary>
        /// The callback to get invoked at each frame
        /// </summary>
        protected SafeInvoker<float> FrameCallback;

        /// <summary>
        /// The callback to get invoked at each frame
        /// </summary>
        protected SafeInvoker<int> FrameCallbackInt;

        /// <summary>
        /// The callback to get invoked at each frame
        /// </summary>
        protected SafeInvoker<double> FrameCallbackDouble;

        /// <summary>
        /// The target object to change the property of
        /// </summary>
        protected object TargetObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="Animator" /> class.
        /// </summary>
        public Animator()
            : this(new Path[] {})
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animator" /> class.
        /// </summary>
        /// <param name="fpsLimiter">Limits the maximum frames per seconds</param>
        public Animator(FPSLimiterKnownValues fpsLimiter)
            : this(new Path[] {}, fpsLimiter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animator" /> class.
        /// </summary>
        /// <param name="path">The path of the animation</param>
        public Animator(Path path)
            : this(new[] {path})
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animator" /> class.
        /// </summary>
        /// <param name="path">The path of the animation</param>
        /// <param name="fpsLimiter">Limits the maximum frames per seconds</param>
        public Animator(Path path, FPSLimiterKnownValues fpsLimiter)
            : this(new[] {path}, fpsLimiter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animator" /> class.
        /// </summary>
        /// <param name="paths">An array containing the list of paths of the animation</param>
        public Animator(Path[] paths) : this(paths, FPSLimiterKnownValues.LimitThirty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animator" /> class.
        /// </summary>
        /// <param name="paths">An array containing the list of paths of the animation</param>
        /// <param name="fpsLimiter">Limits the maximum frames per seconds</param>
        public Animator(Path[] paths, FPSLimiterKnownValues fpsLimiter)
        {
            CurrentStatus = AnimatorStatus.Stopped;
            _timer = new Timer(Elapsed, fpsLimiter);
            Paths = paths;
        }

        /// <summary>
        /// Gets or sets an array containing the list of paths of the animation
        /// </summary>
        /// <value>The paths.</value>
        /// <exception cref="InvalidOperationException">Animation is running</exception>
        public Path[] Paths
        {
            get { return _paths.ToArray(); }
            set
            {
                if (CurrentStatus == AnimatorStatus.Stopped)
                {
                    _paths.Clear();
                    _paths.AddRange(value);
                }
                else
                {
                    throw new InvalidOperationException("Animation is running.");
                }
            }
        }

        /// <summary>
        /// Gets the currently active path.
        /// </summary>
        /// <value>The active path.</value>
        public Path ActivePath { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether animator should repeat the animation after its ending
        /// </summary>
        /// <value><c>true</c> if repeat; otherwise, <c>false</c>.</value>
        public virtual bool Repeat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether animator should repeat the animation in reverse after its ending.
        /// </summary>
        /// <value><c>true</c> if [reverse repeat]; otherwise, <c>false</c>.</value>
        public virtual bool ReverseRepeat { get; set; }

        /// <summary>
        /// Gets the current status of the animation
        /// </summary>
        /// <value>The current status.</value>
        public virtual AnimatorStatus CurrentStatus { get; private set; }

        /// <summary>
        /// Pause the animation
        /// </summary>
        public virtual void Pause()
        {
            if (CurrentStatus != AnimatorStatus.OnHold && CurrentStatus != AnimatorStatus.Playing)
                return;
            _timer.Stop();
            CurrentStatus = AnimatorStatus.Paused;
        }

        /// <summary>
        /// Starts the playing of the animation
        /// </summary>
        /// <param name="targetObject">The target object to change the property</param>
        /// <param name="propertyName">The name of the property to change</param>
        public virtual void Play(object targetObject, string propertyName)
        {
            Play(targetObject, propertyName, null);
        }

        /// <summary>
        /// Starts the playing of the animation
        /// </summary>
        /// <param name="targetObject">The target object to change the property</param>
        /// <param name="propertyName">The name of the property to change</param>
        /// <param name="endCallback">The callback to get invoked at the end of the animation</param>
        public virtual void Play(object targetObject, string propertyName, SafeInvoker endCallback)
        {
            TargetObject = targetObject;
            var prop = TargetObject.GetType()
                .GetProperty(
                    propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance |
                    BindingFlags.SetProperty);
            if (prop == null) return;
            Play(
                new SafeInvoker<float>(
                    value => prop.SetValue(TargetObject, Convert.ChangeType(value, prop.PropertyType), null),
                    TargetObject),
                endCallback);
        }

        /// <summary>
        /// Starts the playing of the animation
        /// </summary>
        /// <typeparam name="T">Any object containing a property</typeparam>
        /// <param name="targetObject">The target object to change the property</param>
        /// <param name="propertySetter">The expression that represents the property of the target object</param>
        public virtual void Play<T>(T targetObject, Expression<Func<T, object>> propertySetter)
        {
            Play(targetObject, propertySetter, null);
        }


        /// <summary>
        /// Starts the playing of the animation
        /// </summary>
        /// <typeparam name="T">Any object containing a property</typeparam>
        /// <param name="targetObject">The target object to change the property</param>
        /// <param name="propertySetter">The expression that represents the property of the target object</param>
        /// <param name="endCallback">The callback to get invoked at the end of the animation</param>
        /// <exception cref="ArgumentException">propertySetter</exception>
        public virtual void Play<T>(T targetObject, Expression<Func<T, object>> propertySetter, SafeInvoker endCallback)
        {
            if (propertySetter == null)
                return;
            TargetObject = targetObject;

            var property =
                ((propertySetter.Body as MemberExpression) ??
                 (((UnaryExpression) propertySetter.Body).Operand as MemberExpression))?.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException(nameof(propertySetter));
            }

            Play(
                new SafeInvoker<float>(
                    value => property.SetValue(TargetObject, Convert.ChangeType(value, property.PropertyType), null),
                    TargetObject),
                endCallback);
        }

        /// <summary>
        /// Resume the animation from where it paused
        /// </summary>
        public virtual void Resume()
        {
            if (CurrentStatus == AnimatorStatus.Paused)
            {
                _timer.Resume();
            }
        }

        /// <summary>
        /// Stops the animation and resets its status, resume is no longer possible
        /// </summary>
        public virtual void Stop()
        {
            _timer.Stop();
            lock (_tempPaths)
            {
                _tempPaths.Clear();
            }
            ActivePath = null;
            CurrentStatus = AnimatorStatus.Stopped;
            _tempReverseRepeat = false;
        }

        /// <summary>
        /// Starts the playing of the animation
        /// </summary>
        /// <param name="targetObject">The target object to change the property</param>
        /// <param name="property">The property to change</param>
        public virtual void Play(object targetObject, KnownProperties property)
        {
            Play(targetObject, property, null);
        }


        /// <summary>
        /// Starts the playing of the animation
        /// </summary>
        /// <param name="targetObject">The target object to change the property</param>
        /// <param name="property">The property to change</param>
        /// <param name="endCallback">The callback to get invoked at the end of the animation</param>
        public virtual void Play(object targetObject, KnownProperties property, SafeInvoker endCallback)
        {
            Play(targetObject, property.ToString(), endCallback);
        }

        /// <summary>
        /// Starts the playing of the animation
        /// </summary>
        /// <param name="frameCallback">The callback to get invoked at each frame</param>
        public virtual void Play(SafeInvoker<float> frameCallback)
        {
            Play(frameCallback, (SafeInvoker<float>) null);
        }


        /// <summary>
        /// Starts the playing of the animation
        /// </summary>
        /// <param name="frameCallback">The callback to get invoked at each frame</param>
        /// <param name="endCallback">The callback to get invoked at the end of the animation</param>
        public virtual void Play(SafeInvoker<float> frameCallback, SafeInvoker endCallback)
        {
            Stop();
            FrameCallback = frameCallback;
            EndCallback = endCallback;
            _timer.ResetClock();
            lock (_tempPaths)
            {
                _tempPaths.AddRange(_paths);
            }
            _timer.Start();
        }

        /// <summary>
        /// Elapseds the specified mill since beginning.
        /// </summary>
        /// <param name="millSinceBeginning">The mill since beginning.</param>
        private void Elapsed(ulong millSinceBeginning = 0)
        {
            while (true)
            {
                lock (_tempPaths)
                {
                    if (_tempPaths != null && ActivePath == null && _tempPaths.Count > 0)
                    {
                        while (ActivePath == null)
                        {
                            if (_tempReverseRepeat)
                            {
                                ActivePath = _tempPaths.LastOrDefault();
                                _tempPaths.RemoveAt(_tempPaths.Count - 1);
                            }
                            else
                            {
                                ActivePath = _tempPaths.FirstOrDefault();
                                _tempPaths.RemoveAt(0);
                            }
                            _timer.ResetClock();
                            millSinceBeginning = 0;
                        }
                    }
                    var ended = ActivePath == null;
                    if (ActivePath != null)
                    {
                        if (!_tempReverseRepeat && millSinceBeginning < ActivePath.Delay)
                        {
                            CurrentStatus = AnimatorStatus.OnHold;
                            return;
                        }
                        if (millSinceBeginning - (!_tempReverseRepeat ? ActivePath.Delay : 0) <= ActivePath.Duration)
                        {
                            if (CurrentStatus != AnimatorStatus.Playing)
                            {
                                CurrentStatus = AnimatorStatus.Playing;
                            }
                            var value = ActivePath.Function(_tempReverseRepeat ? ActivePath.Duration - millSinceBeginning : millSinceBeginning - ActivePath.Delay, ActivePath.Start, ActivePath.Change, ActivePath.Duration);
                            FrameCallback.Invoke(value);
                            return;
                        }
                        if (CurrentStatus == AnimatorStatus.Playing)
                        {
                            if (_tempPaths.Count == 0)
                            {
                                // For the last path, we make sure that control is in end point
                                FrameCallback.Invoke(_tempReverseRepeat ? ActivePath.Start : ActivePath.End);
                                ended = true;
                            }
                            else
                            {
                                if ((_tempReverseRepeat && ActivePath.Delay > 0) || !_tempReverseRepeat && _tempPaths.FirstOrDefault()?.Delay > 0)
                                {
                                    // Or if the next path or this one in revese order has a delay
                                    FrameCallback.Invoke(_tempReverseRepeat ? ActivePath.Start : ActivePath.End);
                                }
                            }
                        }
                        if (_tempReverseRepeat && (millSinceBeginning - ActivePath.Duration) < ActivePath.Delay)
                        {
                            CurrentStatus = AnimatorStatus.OnHold;
                            return;
                        }
                        ActivePath = null;
                    }
                    if (!ended)
                    {
                        return;
                    }
                }
                if (Repeat)
                {
                    lock (_tempPaths)
                    {
                        _tempPaths.AddRange(_paths);
                        _tempReverseRepeat = ReverseRepeat && !_tempReverseRepeat;
                    }
                    millSinceBeginning = 0;
                    continue;
                }
                Stop();
                EndCallback?.Invoke();
                break;
            }
        }

        #region Employing Int Values


        /// <summary>
        /// Initializes a new instance of the <see cref="Animator" /> class.
        /// </summary>
        /// <param name="paths">An array containing the list of paths of the animation</param>
        /// <param name="fpsLimiter">Limits the maximum frames per seconds</param>
        /// <param name="type">Redundant value. Enter 1</param>
        public Animator(Path[] paths, FPSLimiterKnownValues fpsLimiter, int type)
        {
            CurrentStatus = AnimatorStatus.Stopped;
            _timer = new Timer(ElapsedInt, fpsLimiter);
            Paths = paths;
        }

        /// <summary>
        /// Starts the playing of the animation
        /// </summary>
        /// <param name="targetObject">The target object to change the property</param>
        /// <param name="propertyName">The name of the property to change</param>
        /// <param name="endCallback">The callback to get invoked at the end of the animation</param>
        /// <param name="type">Redundant value. Enter 1</param>
        public virtual void Play(object targetObject, string propertyName, SafeInvoker endCallback, int type)
        {
            TargetObject = targetObject;
            var prop = TargetObject.GetType()
                .GetProperty(
                    propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance |
                    BindingFlags.SetProperty);
            if (prop == null) return;
            Play(
                new SafeInvoker<int>(
                    value => prop.SetValue(TargetObject, Convert.ChangeType(value, prop.PropertyType), null),
                    TargetObject),
                endCallback);
        }


        /// <summary>
        /// Starts the playing of the animation
        /// </summary>
        /// <typeparam name="T">Any object containing a property</typeparam>
        /// <param name="targetObject">The target object to change the property</param>
        /// <param name="propertySetter">The expression that represents the property of the target object</param>
        /// <param name="endCallback">The callback to get invoked at the end of the animation</param>
        /// <param name="type">Redundant value. Enter 1</param>
        /// <exception cref="ArgumentException">propertySetter</exception>
        public virtual void Play<T>(T targetObject, Expression<Func<T, object>> propertySetter, SafeInvoker endCallback, int type)
        {
            if (propertySetter == null)
                return;
            TargetObject = targetObject;

            var property =
                ((propertySetter.Body as MemberExpression) ??
                 (((UnaryExpression)propertySetter.Body).Operand as MemberExpression))?.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException(nameof(propertySetter));
            }

            Play(
                new SafeInvoker<int>(
                    value => property.SetValue(TargetObject, Convert.ChangeType(value, property.PropertyType), null),
                    TargetObject),
                endCallback);
        }


        /// <summary>
        /// Starts the playing of the animation
        /// </summary>
        /// <param name="frameCallback">The callback to get invoked at each frame</param>
        public virtual void Play(SafeInvoker<int> frameCallback)
        {
            Play(frameCallback, (SafeInvoker<int>)null);
        }

        /// <summary>
        /// Starts the playing of the animation
        /// </summary>
        /// <param name="frameCallback">The callback to get invoked at each frame</param>
        /// <param name="endCallback">The callback to get invoked at the end of the animation</param>
        public virtual void Play(SafeInvoker<int> frameCallback, SafeInvoker endCallback)
        {
            Stop();
            FrameCallbackInt = frameCallback;
            EndCallback = endCallback;
            _timer.ResetClock();
            lock (_tempPaths)
            {
                _tempPaths.AddRange(_paths);
            }
            _timer.Start();
        }

        /// <summary>
        /// Elapseds the int.
        /// </summary>
        /// <param name="millSinceBeginning">The mill since beginning.</param>
        private void ElapsedInt(ulong millSinceBeginning = 0)
        {
            while (true)
            {
                lock (_tempPaths)
                {
                    if (_tempPaths != null && ActivePath == null && _tempPaths.Count > 0)
                    {
                        while (ActivePath == null)
                        {
                            if (_tempReverseRepeat)
                            {
                                ActivePath = _tempPaths.LastOrDefault();
                                _tempPaths.RemoveAt(_tempPaths.Count - 1);
                            }
                            else
                            {
                                ActivePath = _tempPaths.FirstOrDefault();
                                _tempPaths.RemoveAt(0);
                            }
                            _timer.ResetClock();
                            millSinceBeginning = 0;
                        }
                    }
                    var ended = ActivePath == null;
                    if (ActivePath != null)
                    {
                        if (!_tempReverseRepeat && millSinceBeginning < ActivePath.Delay)
                        {
                            CurrentStatus = AnimatorStatus.OnHold;
                            return;
                        }
                        if (millSinceBeginning - (!_tempReverseRepeat ? ActivePath.Delay : 0) <= ActivePath.Duration)
                        {
                            if (CurrentStatus != AnimatorStatus.Playing)
                            {
                                CurrentStatus = AnimatorStatus.Playing;
                            }
                            var value = ActivePath.Function(_tempReverseRepeat ? ActivePath.Duration - millSinceBeginning : millSinceBeginning - ActivePath.Delay, ActivePath.Start, ActivePath.Change, ActivePath.Duration);
                            FrameCallbackInt.Invoke((int)value);
                            return;
                        }
                        if (CurrentStatus == AnimatorStatus.Playing)
                        {
                            if (_tempPaths.Count == 0)
                            {
                                // For the last path, we make sure that control is in end point
                                FrameCallbackInt.Invoke(_tempReverseRepeat ? (int)ActivePath.Start : (int)ActivePath.End);
                                ended = true;
                            }
                            else
                            {
                                if ((_tempReverseRepeat && ActivePath.Delay > 0) || !_tempReverseRepeat && _tempPaths.FirstOrDefault()?.Delay > 0)
                                {
                                    // Or if the next path or this one in revese order has a delay
                                    FrameCallbackInt.Invoke(_tempReverseRepeat ? (int)ActivePath.Start : (int)ActivePath.End);
                                }
                            }
                        }
                        if (_tempReverseRepeat && (millSinceBeginning - ActivePath.Duration) < ActivePath.Delay)
                        {
                            CurrentStatus = AnimatorStatus.OnHold;
                            return;
                        }
                        ActivePath = null;
                    }
                    if (!ended)
                    {
                        return;
                    }
                }
                if (Repeat)
                {
                    lock (_tempPaths)
                    {
                        _tempPaths.AddRange(_paths);
                        _tempReverseRepeat = ReverseRepeat && !_tempReverseRepeat;
                    }
                    millSinceBeginning = 0;
                    continue;
                }
                Stop();
                EndCallback?.Invoke();
                break;
            }
        }
        #endregion


        



    }
}