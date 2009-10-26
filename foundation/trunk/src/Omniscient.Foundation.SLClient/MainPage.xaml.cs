using System.Windows.Controls;
using Omniscient.Foundation.ApplicationModel.Configuration;

namespace Omniscient.Foundation.SLClient
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += new System.Windows.RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var t = (Test)ConfigurationManager.GetSection("test");
            var t2 = (Test)ConfigurationManager.GetSection("test");
        }
    }
}
