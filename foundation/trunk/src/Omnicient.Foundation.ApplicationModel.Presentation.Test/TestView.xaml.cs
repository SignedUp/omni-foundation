using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Omniscient.Foundation.ApplicationModel.Presentation;

namespace Omnicient.Foundation.ApplicationModel.Presentation.Test
{
    /// <summary>
    /// Interaction logic for TestView.xaml
    /// </summary>
    public partial class TestView : UserControl, IView
    {
        public TestView()
        {
            InitializeComponent();
        }

        #region IView Members

        public IModel Model
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void UpdateView()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
