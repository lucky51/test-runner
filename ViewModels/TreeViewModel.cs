using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Animation;
using Unity.Injection;
using System.ComponentModel;
using Prism.Events;

namespace TestRunner.ViewModels
{
    public class TreeViewModel
    {
        public string Header { get; set; }

        public int HeaderIndex { get; set; }

        public int Level { get; set; }

        public TreeViewModel? Root { get; set; }

        public ObservableCollection<TreeViewModel> Children { get; set; }
    }

    public class TreeViewModelStandard
    {
        public string Header { get; set; }

        public int Level { get; set; }

        public int ParentLevel { get; set; }
        public string VisibilityLeaf
        {

            get
            {
                if(this.Children==null ||this.Children.Count==0)
                {
                    return "Visible";
                }
                return "Hidden";
            }
        }
        public string VisibilityRefresh { get {
                if (IsParent)
                {
                    return "Visible";
                }
                return "Hidden";
            } }

        public bool IsParent { get; set; }

        public ObservableCollection<TreeViewModelStandard> Children { get; set; }
    }
    public class ScrollToEndEvent : PubSubEvent
    {
    }
    public class TestTreeViewModel : BindableBase
    {
        public ObservableCollection<TreeViewModelStandard> TreeViewDemoItems { get; set; }

        public DelegateCommand<RoutedPropertyChangedEventArgs<object>> TreeViewSelectedItemCommand => new DelegateCommand<RoutedPropertyChangedEventArgs<object>>(OnTreeViewSelectedItemChanged);
        public DelegateCommand TreeViewDoubleClickCommand => new DelegateCommand(OnTreeViewDoubleClick);
        public DelegateCommand TreeViewItemExpandedCommand => new DelegateCommand(OnTreeViewItemExpanded);
        public DelegateCommand ClickRefreshCommand => new DelegateCommand(OnTreeRefresh);
        public DelegateCommand<RoutedEventArgs> TreeViewItemSelectedCommand => new DelegateCommand<RoutedEventArgs>(OnTreeViewItemSelectedChanged);
        private StringBuilder _logText;

        private readonly IEventAggregator _eventAggregator;

        public TestTreeViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _logText = new StringBuilder();
            TreeViewDemoItems ??= new ObservableCollection<TreeViewModelStandard>() { };
            TreeViewDemoItems.Add(new TreeViewModelStandard {  Header="Unit Test",IsParent=true});
        }

        public void ScrollToEnd()
        {
            _eventAggregator.GetEvent<ScrollToEndEvent>().Publish();
        }
        public string LogText
        {
            get { return _logText.ToString(); }
        }

        public void AppendText(string text)
        {
            _logText.AppendLine(text);
            RaisePropertyChanged(nameof(LogText));

            ScrollToEnd();

        }

        private const string TestDir = " C:\\";
        private  void ExecDontNetTest(Action<string>? act=null)
        {
            string command = "dotnet test --no-build -t -v=q  C:\\work\\Test";
            var projectRootName = Path.GetFileName(TestDir);
            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = "/c " + command
            };

            Process process = new Process()
            {
                StartInfo = processStartInfo
            };

            process.Start();

            // string output = process.StandardOutput.ReadToEnd();
            var children = new ObservableCollection<TreeViewModelStandard>() { }; 
            var dic = new Dictionary<string,TreeViewModelStandard>();
            
            while (!process.StandardOutput.EndOfStream)
            {
                var curr = process.StandardOutput.ReadLine();
                if(curr!=null)
                    act?.Invoke(curr);
                if (curr!=null &&curr.StartsWith("  "))
                {
                    curr = curr.Trim();
                    var parent = string.Empty;
                    foreach(var item in  curr.Split(new char[] { '.' }))
                    {
                        if(parent==string.Empty && !dic.ContainsKey(item)) {
                            var itemNode = new TreeViewModelStandard
                            {
                                Header = item,
                                IsParent=true
                            };
                            children.Add(itemNode);
                            dic[item] = itemNode;
                        }
                        if(parent!=string.Empty)
                        {
                            var itemNode = new TreeViewModelStandard
                            {
                                Header = item,
                            };
    
                            dic[parent].Children ??= new ObservableCollection<TreeViewModelStandard>();
                            if(!dic.ContainsKey(item))
                            {
                                dic[parent].Children.Add(itemNode);
                                dic[item] = itemNode;
                            }
                           
                        }

                        parent = item;
                    }
                }

            }
            TreeViewDemoItems.First().Children = children;
        } 

        private void OnTreeViewSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            MessageBox.Show("xxx");

            System.Diagnostics.Debug.WriteLine("SelectedItem");
        }

        private void OnTreeViewDoubleClick()
        {
            System.Diagnostics.Debug.WriteLine("DoubleClick");
        }

        private void OnTreeViewItemExpanded()
        {
            System.Diagnostics.Debug.WriteLine("Expanded");
        }
        private void OnTreeRefresh()
        {
            MessageBox.Show("refresh");
          //  ExecDontNetTest(AppendText);
        }

        private void OnTreeViewItemSelectedChanged(RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ItemSelected");
        }
    }
}
