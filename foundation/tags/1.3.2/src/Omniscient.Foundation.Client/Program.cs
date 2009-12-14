using System;
using System.Collections.Generic;
using System.Text;
using Omniscient.Foundation;
using Ninject.Core;

namespace Omniscient.Foundation.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceModel.IServiceProvider provider;
            IKernel kernel;

            kernel = new StandardKernel();
            provider = new ServiceModel.DIServiceProvider(kernel);
            provider.RegisterService<IMyService>(new MyService());

            IMyService srv;
            srv = provider.GetService<IMyService>();
            if (srv == null) Console.WriteLine("null!!");
            else srv.WriteLine("from provider.");

            srv = kernel.Get<IMyService>();
            if (srv == null) Console.WriteLine("null!!");
            else srv.WriteLine("from kernel.");
            System.Console.ReadLine();
        }
    }

    public interface IMyService
    {
        void WriteLine(string msg);
    }

    public class MyService : ServiceModel.ServiceBase<IMyService>, IMyService, Omniscient.Foundation.ApplicationModel.IStartable
    {
        public override IMyService GetImplementation()
        {
            Console.WriteLine("calling GetImplementation");
            return this;
        }

        public void WriteLine(string msg)
        {
            Console.WriteLine(msg);
        }

        public void Start()
        {
            Console.WriteLine("start");
        }

        public void Stop()
        {
            Console.WriteLine("stop");
        }
    }
}
