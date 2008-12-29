using System;
using NUnit.Framework;
using NUnit.Mocks;

namespace Omniscient.Foundation.Commanding
{
    [TestFixture]
    public class TestTransitiveCommand
    {
        private TransitiveCommand _cmd;

        [SetUp]
        public void Setup()
        {
            _cmd = new TransitiveCommand();
        }

        [Test]
        public void CheckCanExecute()
        {
            Assert.IsFalse(_cmd.CanExecute(null));
        }

        [Test]
        public void CheckCanExecuteWithHandler()
        {
            Assert.IsFalse(_cmd.CanExecute(null));

            ICommandHandler handler;
            handler = new Handler();
            _cmd.RegisterHandler(handler);
            Assert.IsTrue(_cmd.CanExecute(null));
            _cmd.UnregisterHandler(handler);
            Assert.IsFalse(_cmd.CanExecute(null));
        }

        [Test]
        public void CheckCanExecuteChanged()
        {
            CommandLauncherMock mock;
            mock = new CommandLauncherMock();
            mock.Expect("OnCanExecuteChanged");
            mock.Expect("OnCanExecuteChanged");

            _cmd.CanExecuteChanged += mock.OnCanExecuteChanged;

            ICommandHandler handler1, handler2;
            handler1 = new Handler();
            handler2 = new Handler();

            _cmd.RegisterHandler(handler1);
            _cmd.RegisterHandler(handler2);
            _cmd.UnregisterHandler(handler1);
            _cmd.UnregisterHandler(handler2);

            mock.Verify();
        }

        [Test]
        public void ExecuteTwoHandlers()
        {
            Handler handler1, handler2;

            handler1 = new Handler();
            handler1.Expect("Execute", 99);

            handler2 = new Handler();
            handler2.Expect("Execute", 99);

            _cmd.RegisterHandler(handler1);
            _cmd.RegisterHandler(handler2);

            _cmd.Execute(99);

            handler1.Verify();
            handler2.Verify();
        }

        public class CommandLauncherMock : Mock
        {
            public void OnCanExecuteChanged(object sender, EventArgs e)
            {
                base.Call("OnCanExecuteChanged");
            }
        }

        public class Handler : Mock, ICommandHandler
        {
            #region ICommandHandler Members

            public void Execute(object param)
            {
                base.Call("Execute", param);
            }

            #endregion
        }
    }

}
