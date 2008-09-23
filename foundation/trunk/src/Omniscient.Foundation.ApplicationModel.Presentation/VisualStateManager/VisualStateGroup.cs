using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace System.Windows
{
    /// <summary>
    /// http://blogs.msdn.com/johngossman/archive/2008/08/08/visualstatemanager-for-desktop-wpf.aspx
    /// </summary>
    internal class VisualStateGroupCollection : FreezableCollection<VisualStateGroup>
    {
    }

	[ContentProperty("States")] 
	[RuntimeNameProperty("Name")]
	public class VisualStateGroup : Freezable 
	{
        protected override Freezable CreateInstanceCore()
        {
            return new VisualStateGroup();
        }

		private string _name;
		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}

        /// <summary>
        /// VisualStates in the group.
        /// </summary>
        public IList States
        {
            get
            {
                if (_states == null)
                    _states = new FreezableCollection<VisualState>();

                return _states;
            }
        }

        /// <summary>
        /// Transitions between VisualStates in the group.
        /// </summary>
        public IList Transitions
        {
            get
            {
                if (_transitions == null)
                    _transitions = new FreezableCollection<VisualTransition>();

                return _transitions;
            }
        }

        /// <summary>
        /// VisualState that is currently applied.
        /// </summary>
        internal VisualState CurrentState { get; set; }

        internal VisualState GetState(string stateName)
        {
            for (int stateIndex = 0; stateIndex < States.Count; ++stateIndex)
            {
                VisualState state = (VisualState)States[stateIndex];
                if (state.Name == stateName)
                {
                    return state;
                }
            }

            return null;
        }

        internal Collection<Storyboard> CurrentStoryboards
        {
            get
            {
                if (_currentStoryboards == null)
                    _currentStoryboards = new Collection<Storyboard>();

                return _currentStoryboards;
            }
        }

        internal void StartNewThenStopOld(FrameworkElement element, params Storyboard[] newStoryboards)
        {
            // Start the new Storyboards
            for (int index = 0; index < newStoryboards.Length; ++index)
            {
                if (newStoryboards[index] == null) continue;
                newStoryboards[index].Begin(element, HandoffBehavior.SnapshotAndReplace, true);
            }

            // Stop the old Storyboards
            for (int index = 0; index < CurrentStoryboards.Count; ++index)
            {
                if (CurrentStoryboards[index] == null) continue;
                CurrentStoryboards[index].Stop(element);
            }

            // Hold on to the running Storyboards
            CurrentStoryboards.Clear();
            for (int index = 0; index < newStoryboards.Length; ++index)
                CurrentStoryboards.Add(newStoryboards[index]);
        }

        private Collection<Storyboard> _currentStoryboards;
        private FreezableCollection<VisualState> _states;
        private FreezableCollection<VisualTransition> _transitions;
	} 
}
