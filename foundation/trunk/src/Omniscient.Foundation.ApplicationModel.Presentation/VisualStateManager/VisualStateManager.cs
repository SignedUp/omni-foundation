using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls.Internal;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;

namespace System.Windows
{
    /// <summary>
    /// Silverlight VisualStateManager Implementation for Silverlight
    /// http://blogs.msdn.com/johngossman/archive/2008/08/08/visualstatemanager-for-desktop-wpf.aspx
    /// </summary>
    public class VisualStateManager : DependencyObject
    {
        public static bool GoToState(Control control, string stateName, bool useTransitions)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            if (stateName == null)
                throw new ArgumentNullException("stateName");

            FrameworkElement root = VisualStateManager.GetTemplateRoot(control);
            if (root == null)
                return false; // Ignore state changes if a ControlTemplate hasn't been applied

            IList<VisualStateGroup> groups = VisualStateManager.GetVisualStateGroupsInternal(root);
            if (groups == null)
                return false;

            // System.Diagnostics.Debug.WriteLine("Going to " + stateName);

            VisualState state;
            VisualStateGroup group;
            VisualStateManager.TryGetState(groups, stateName, out group, out state);

            // Look for a custom VSM, and call it if it was found, regardless of whether the state was found or not.
            // This is because we don't know what the custom VSM will want to do. But for our default implementation,
            // we know that if we haven't found the state, we don't actually want to do anything.
            VisualStateManager customVsm = GetCustomVisualStateManager(control);
            if (customVsm != null)
            {
                return customVsm.GoToStateCore(control, root, stateName, group, state, useTransitions);
            }
            else if (state != null)
            {
                return GoToStateInternal(control, root, group, state, useTransitions);
            }


            return false;
        }

        protected virtual bool GoToStateCore(Control control, FrameworkElement templateRoot, string stateName, VisualStateGroup group, VisualState state, bool useTransitions)
        {
            return GoToStateInternal(control, templateRoot, group, state, useTransitions);
        }

        #region CustomVisualStateManager attached DependencyProperty
        public static DependencyProperty CustomVisualStateManagerProperty =
            DependencyProperty.RegisterAttached(
            "CustomVisualStateManager",
            typeof(VisualStateManager),
            typeof(VisualStateManager),
            null);

        public static VisualStateManager GetCustomVisualStateManager(DependencyObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            return obj.GetValue(VisualStateManager.CustomVisualStateManagerProperty) as VisualStateManager;
        }

        public static void SetCustomVisualStateManager(DependencyObject obj, VisualStateManager value)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            obj.SetValue(VisualStateManager.CustomVisualStateManagerProperty, value);
        }
        #endregion

        #region Collection<VisualStateGroup> attached DependencyProperty
        internal static readonly DependencyProperty VisualStateGroupsProperty =
            DependencyProperty.RegisterAttached("InternalVisualStateGroups",
                            typeof(VisualStateGroupCollection),
                            typeof(VisualStateManager),
                            new UIPropertyMetadata(null,
                                    new PropertyChangedCallback(VisualStateManager.VisualStateGroupsProperty_Changed)));

        private static void VisualStateGroupsProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = d as FrameworkElement;
            Control control = element.TemplatedParent as Control;
            System.Diagnostics.Debug.Assert(element == VisualStateManager.GetTemplateRoot(control));
            if (control != null)
            {
                VisualStateBehaviorFactory.AttachBehavior(control);
            }
        }

        internal static VisualStateGroupCollection GetVisualStateGroupsInternal(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            VisualStateGroupCollection groups = obj.GetValue(VisualStateManager.VisualStateGroupsProperty) as VisualStateGroupCollection;
            if (groups == null)
            {
                groups = new VisualStateGroupCollection();
                SetVisualStateGroups(obj, groups);
            }
            return groups;
        }

        public static IList GetVisualStateGroups(DependencyObject obj)
        {
            return VisualStateManager.GetVisualStateGroupsInternal(obj);
        }

        public static void SetVisualStateGroups(DependencyObject obj, IList value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            obj.SetValue(VisualStateManager.VisualStateGroupsProperty, value);
        }

        #endregion VisualStateGroups

        internal static bool TryGetState(IList<VisualStateGroup> groups, string stateName, out VisualStateGroup group, out VisualState state)
        {
            for (int groupIndex = 0; groupIndex < groups.Count; ++groupIndex)
            {
                VisualStateGroup g = groups[groupIndex];
                for (int stateIndex = 0; stateIndex < g.States.Count; ++stateIndex)
                {
                    VisualState s = (VisualState)g.States[stateIndex];
                    if (s.Name == stateName)
                    {
                        group = g;
                        state = s;
                        return true;
                    }
                }
            }

            group = null;
            state = null;
            return false;
        }

        internal static bool GoToStateInternal(Control control, FrameworkElement element, VisualStateGroup group, VisualState state, bool useTransitions)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            if (state == null)
                throw new ArgumentNullException("state");

            if (group == null)
                throw new InvalidOperationException();

            VisualState lastState = group.CurrentState;
            if (lastState == state)
                return true;

            VisualTransition transition = useTransitions ? VisualStateManager.GetTransition(element, group, lastState, state) : null;

            // Generate dynamicTransition Storyboard
            Storyboard dynamicTransition = GenerateDynamicTransitionAnimations(group, state, transition);

            // If the transition is null, then we want to instantly snap. The dynamicTransition will
            // consist of everything that is being moved back to the default state.
            // If the transition.Duration is 0, then we want both the dynamic and state Storyboards
            // to happen in the same tick, so we start them at the same time.
            if (transition == null || transition.Duration == DurationZero)
            {
                // Start new state Storyboard and stop any previously running Storyboards
                // the state.Storyboard is second in the list so that it "wins" and will 
                // override any animation in the dynamicTransition.
                group.StartNewThenStopOld(element, dynamicTransition, state.Storyboard);
            }
            else
            {
                // In this case, we have an interstitial storyboard of duration > 0, so we need 
                // to run it first, and then we'll run the state storyboard.

                // Hook up transition Storyboard's Completed event handler
                dynamicTransition.Completed += delegate(object sender, EventArgs e)
                {
                    group.StartNewThenStopOld(element, state.Storyboard);
                };

                // Start transition and dynamicTransition Storyboards
                // Stop any previously running Storyboards
                group.StartNewThenStopOld(element, transition.Storyboard, dynamicTransition);
            }

            group.CurrentState = state;

            return true;
        }

        private static Storyboard GenerateDynamicTransitionAnimations(VisualStateGroup group, VisualState newState, VisualTransition transition)
        {
            Storyboard transitionStoryboard = new Storyboard();
            if (transition != null && transition.Duration != null)
                transitionStoryboard.Duration = transition.Duration;

            VisualState lastState = group.CurrentState;
            Storyboard lastStoryboard = lastState != null ? lastState.Storyboard : null;

            Storyboard currentStoryboard = newState.Storyboard;

            // foreach property in current storyboard, set a beginning animation
            if (currentStoryboard != null)
            {
                foreach (Timeline timeline in currentStoryboard.Children)
                {
                    Timeline transitionAnimation = VisualStateManager.GetTransitionAnimation(timeline, true, transition);

                    if (transitionAnimation != null)
                    {
                        transitionStoryboard.Children.Add(transitionAnimation);
                    }
                }
            }

            // foreach property in last storyboard but not in current, set an ending animation
            if (lastStoryboard != null)
            {
                foreach (Timeline timeline in lastStoryboard.Children)
                {
                    Timeline matchingTimeline = GetMatchingTimeline(timeline, currentStoryboard);

                    if (matchingTimeline == null)
                    {
                        Timeline transitionAnimation = VisualStateManager.GetTransitionAnimation(timeline, false, transition);

                        if (transitionAnimation != null)
                        {
                            transitionStoryboard.Children.Add(transitionAnimation);
                        }
                    }
                }
            }

            return transitionStoryboard;
        }

        private static Timeline GetTransitionAnimation(Timeline stateTimeline, bool entering, VisualTransition transition)
        {
            Timeline matchingTimeline = null;
            if (transition != null)
            {
                matchingTimeline = GetMatchingTimeline(stateTimeline, transition.Storyboard);
            }

            if (matchingTimeline is IKeyFrameAnimation)
            {
                return matchingTimeline.Clone();
            }

            // repeat this pattern for every type
            if (stateTimeline is DoubleAnimationBase)
            {
                double? value = null;
                DoubleAnimation doubleAnimation = stateTimeline as DoubleAnimation;
                DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = stateTimeline as DoubleAnimationUsingKeyFrames;
                DoubleAnimation matchingAnimation = matchingTimeline as DoubleAnimation;

                if (doubleAnimation != null)
                {
                    value = entering ? doubleAnimation.From : doubleAnimation.To;
                }
                if (doubleAnimationUsingKeyFrames != null)
                {
                    DoubleKeyFrame frame = doubleAnimationUsingKeyFrames.KeyFrames[entering ? 0 : (doubleAnimationUsingKeyFrames.KeyFrames.Count - 1)];

                    //bugbug if (frame.KeyTime == KeyTime.FromTimeSpan(TimeSpan.Zero))
                    {
                        value = frame.Value;
                    }
                }
                if (value == null)
                {
                    value = entering ? doubleAnimation.To : doubleAnimation.From;
                }


                if (value != null || (matchingAnimation != null && matchingAnimation.To.HasValue))
                {
                    DoubleAnimation transitionAnimation = new DoubleAnimation();
                    Duration duration = Duration.Automatic;
                    if (matchingAnimation != null)
                    {
                        duration = matchingAnimation.Duration;
                    }
                    else if (transition != null)
                    {
                        duration = transition.Duration;
                    }
                    transitionAnimation.Duration = duration;
                    transitionAnimation.BeginTime = (matchingAnimation != null) ? matchingAnimation.BeginTime : TimeSpan.Zero;
                    transitionAnimation.To = (matchingAnimation != null && matchingAnimation.To.HasValue) ? matchingAnimation.To : (entering ? new double?(value.Value) : null);

                    Storyboard.SetTargetName(transitionAnimation, Storyboard.GetTargetName(stateTimeline));
                    Storyboard.SetTargetProperty(transitionAnimation, Storyboard.GetTargetProperty(stateTimeline));
                    return transitionAnimation;
                }
            }

            if (stateTimeline is ColorAnimationBase)
            {
                Color? value = null;
                ColorAnimation colorAnimation = stateTimeline as ColorAnimation;
                ColorAnimationUsingKeyFrames colorAnimationUsingKeyFrames = stateTimeline as ColorAnimationUsingKeyFrames;
                ColorAnimation matchingAnimation = matchingTimeline as ColorAnimation;

                if (colorAnimation != null)
                {
                    value = entering ? colorAnimation.From : colorAnimation.To;
                }
                if (colorAnimationUsingKeyFrames != null)
                {
                    ColorKeyFrame frame = colorAnimationUsingKeyFrames.KeyFrames[entering ? 0 : (colorAnimationUsingKeyFrames.KeyFrames.Count - 1)];

                    //bugbug if (frame.KeyTime == KeyTime.FromTimeSpan(TimeSpan.Zero))
                    {
                        value = frame.Value;
                    }
                }
                if (colorAnimation != null)
                {
                    value = entering ? colorAnimation.To : colorAnimation.From;
                }

                if (value != null)
                {
                    ColorAnimation transitionAnimation = new ColorAnimation();
                    Duration duration = Duration.Automatic;
                    if (matchingAnimation != null)
                    {
                        duration = matchingAnimation.Duration;
                    }
                    else if (transition != null)
                    {
                        duration = transition.Duration;
                    }
                    transitionAnimation.Duration = duration;
                    transitionAnimation.BeginTime = (matchingAnimation != null) ? matchingAnimation.BeginTime : TimeSpan.Zero;
                    transitionAnimation.To = (matchingAnimation != null && matchingAnimation.To.HasValue) ? matchingAnimation.To : (entering ? new Color?(value.Value) : null);

                    Storyboard.SetTargetName(transitionAnimation, Storyboard.GetTargetName(stateTimeline));
                    Storyboard.SetTargetProperty(transitionAnimation, Storyboard.GetTargetProperty(stateTimeline));
                    return transitionAnimation;
                }
            }

            if (stateTimeline is PointAnimationBase)
            {
                Point? value = null;
                PointAnimation pointAnimation = stateTimeline as PointAnimation;
                PointAnimationUsingKeyFrames pointAnimationUsingKeyFrames = stateTimeline as PointAnimationUsingKeyFrames;
                PointAnimation matchingAnimation = matchingTimeline as PointAnimation;

                if (pointAnimation != null)
                {
                    value = entering ? pointAnimation.From : pointAnimation.To;
                }
                if (pointAnimationUsingKeyFrames != null)
                {
                    PointKeyFrame frame = pointAnimationUsingKeyFrames.KeyFrames[entering ? 0 : (pointAnimationUsingKeyFrames.KeyFrames.Count - 1)];

                    //bugbug if (frame.KeyTime == KeyTime.FromTimeSpan(TimeSpan.Zero))
                    {
                        value = frame.Value;
                    }
                }
                if (pointAnimation != null)
                {
                    value = entering ? pointAnimation.To : pointAnimation.From;
                }

                if (value != null)
                {
                    PointAnimation transitionAnimation = new PointAnimation();
                    Duration duration = Duration.Automatic;
                    if (matchingAnimation != null)
                    {
                        duration = matchingAnimation.Duration;
                    }
                    else if (transition != null)
                    {
                        duration = transition.Duration;
                    }
                    transitionAnimation.Duration = duration;
                    transitionAnimation.BeginTime = (matchingAnimation != null) ? matchingAnimation.BeginTime : TimeSpan.Zero;
                    transitionAnimation.To = (matchingAnimation != null && matchingAnimation.To.HasValue) ? matchingAnimation.To : (entering ? new Point?(value.Value) : null);

                    Storyboard.SetTargetName(transitionAnimation, Storyboard.GetTargetName(stateTimeline));
                    Storyboard.SetTargetProperty(transitionAnimation, Storyboard.GetTargetProperty(stateTimeline));
                    return transitionAnimation;
                }
            }

            return null;
        }

        /// <summary>
        /// Get the most appropriate transition between two states.
        /// </summary>
        /// <param name="element">Element being transitioned.</param>
        /// <param name="group">Group being transitioned.</param>
        /// <param name="from">VisualState being transitioned from.</param>
        /// <param name="to">VisualState being transitioned to.</param>
        /// <returns>
        /// The most appropriate transition between the desired states.
        /// </returns>
        internal static VisualTransition GetTransition(FrameworkElement element, VisualStateGroup group, VisualState from, VisualState to)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            if (group == null)
                throw new ArgumentNullException("group");
            if (to == null)
                throw new ArgumentNullException("to");

            VisualTransition best = null;
            VisualTransition defaultTransition = null;
            int bestScore = -1;

            IList<VisualTransition> transitions = (IList<VisualTransition>)group.Transitions;
            if (transitions != null)
            {
                foreach (VisualTransition transition in transitions)
                {
                    if (defaultTransition == null && transition.IsDefault)
                    {
                        defaultTransition = transition;
                        continue;
                    }

                    int score = -1;

                    VisualState transitionFromState = group.GetState(transition.From);
                    VisualState transitionToState = group.GetState(transition.To);

                    if (from == transitionFromState)
                    {
                        score += 1;
                    }
                    else if (transitionFromState != null)
                    {
                        continue;
                    }

                    if (to == transitionToState)
                    {
                        score += 2;
                    }
                    else if (transitionToState != null)
                    {
                        continue;
                    }

                    if (score > bestScore)
                    {
                        bestScore = score;
                        best = transition;
                    }
                }
            }
            return best ?? defaultTransition;
        }

        private static Timeline GetMatchingTimeline(Timeline timeline, Storyboard storyboard)
        {
            if (storyboard != null)
            {
                string targetName = Storyboard.GetTargetName(timeline);
                PropertyPath targetProperty = Storyboard.GetTargetProperty(timeline);

                foreach (Timeline candidateTimeline in storyboard.Children)
                {
                    string candidateTargetName = Storyboard.GetTargetName(candidateTimeline);
                    PropertyPath candidateTargetProperty = Storyboard.GetTargetProperty(candidateTimeline);

                    if (candidateTargetName == targetName && candidateTargetProperty.Path == targetProperty.Path)
                    {
                        bool paramsEqual = true;

                        for (int i = 0; i < targetProperty.PathParameters.Count; i++)
                        {
                            if (candidateTargetProperty.PathParameters[i] != targetProperty.PathParameters[i])
                            {
                                paramsEqual = false;
                                break;
                            }
                        }

                        if (paramsEqual)
                        {
                            return candidateTimeline;
                        }
                    }
                }
            }

            return null;
        }

        internal static FrameworkElement GetTemplateRoot(Control control)
        {
            if (VisualTreeHelper.GetChildrenCount(control) > 0)
            {
                return VisualTreeHelper.GetChild(control, 0) as FrameworkElement;
            }
            return null;
        }

        private static readonly Duration DurationZero = new Duration(TimeSpan.Zero);
    }
}
