using Prism.Events;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestRunner.ViewModels;

namespace TestRunner.Views
{
    /// <summary>
    /// Interaction logic for TestTree.xaml
    /// </summary>
    public partial class TestTree : UserControl
    {
        private readonly IEventAggregator _eventAggregator;
        private void YourView_Loaded(object sender, RoutedEventArgs e)
        {
           

                _eventAggregator.GetEvent<ScrollToEndEvent>().Subscribe(ScrollToEnd);
        }

        private void ScrollToEnd()
        {
            scrollTextLogger.ScrollToBottom();
        }
        public TestTree()
        {
            var container = (Application.Current as PrismApplication)!.Container;
            _eventAggregator = container.Resolve<IEventAggregator>();
            this.Loaded += YourView_Loaded;
            InitializeComponent();

        }
    }
}
