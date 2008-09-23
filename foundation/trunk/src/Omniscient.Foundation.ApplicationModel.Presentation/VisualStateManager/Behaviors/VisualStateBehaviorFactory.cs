using System;


namespace System.Windows.Controls.Internal
{
    /// <summary>
    /// http://blogs.msdn.com/johngossman/archive/2008/08/08/visualstatemanager-for-desktop-wpf.aspx
    /// </summary>
    internal sealed class VisualStateBehaviorFactory : TypeHandlerFactory<VisualStateBehavior>
    {
        private static readonly VisualStateBehaviorFactory instance = new VisualStateBehaviorFactory();

        public static VisualStateBehaviorFactory Instance
        {
            get { return VisualStateBehaviorFactory.instance; }
        }

        private VisualStateBehaviorFactory()
        {
        }

        public static void AttachBehavior(Control control)
        {
            VisualStateBehavior behavior = VisualStateBehaviorFactory.Instance.GetBehavior(control.GetType());
            if (behavior != null)
            {
                // We don't attach a VisualStateBehavior if the template contains Triggers, so
                // the Triggers have priority over the behavior.  This will allow us to Trigger
                // VisualStateManager from a Trigger.
                if ((control.Template == null) || (control.Template.Triggers.Count < 1))
                {
                    behavior.Attach(control);
                }
            }
        }

        public VisualStateBehavior GetBehavior(Type type)
        {
            return this.GetHandler(type);
        }

        protected override Type GetBaseType(VisualStateBehavior behavior)
        {
            return behavior.TargetType;
        }

        protected override void Initialize()
        {
            base.Initialize();

            // Add new control behaviors here
            this.RegisterHandler(new ButtonBaseBehavior());
        }

        public void AddControlBehavior(VisualStateBehavior behavior)
        {
            this.RegisterHandler(behavior);
        }
    }
}
