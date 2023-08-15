using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TestRunner
{
    public partial class Form1 : Form
    {
        private string _testPath = string.Empty;

        public Form1(string testDir)
        {
            InitializeComponent();
            this.splitContainer1.Panel1.Controls.Add(TestTree);
            this.splitContainer1.Panel2.Controls.Add(richTextBox1);
            var root = new TreeNode() { Text = "unit test", Name = "root" };
            this.TestTree.Nodes.Add(root);
            this.TestTree.NodeMouseClick += TestTree_NodeMouseClick;
            _testPath = testDir;

            this.richTextBox1.AppendText(testDir);
            UpdateTreeAsync();
            this.timer1.Interval = 800;
            this.timer1.Tick += Timer1_Tick;
            this.comboBox1.Enabled = false;  
        }

        private void Timer1_Tick(object? sender, EventArgs e)
        {
            this.timer1.Stop();
            if (string.IsNullOrWhiteSpace(this.comboBox1.Text) || this.comboBox1.Text.Length < 4)
            {
                return;
            }
            var k = treeSearchMap?.Keys.ToList().FindAll(x => x.Contains(this.comboBox1.Text, StringComparison.OrdinalIgnoreCase))?.FirstOrDefault();
            if (k != null && treeSearchMap?.TryGetValue(k, out var node) == true)
            {
                this.TestTree.SelectedNode.BackColor = Color.White;
                this.TestTree.SelectedNode = node;
                this.TestTree.SelectedNode.BackColor = Color.CornflowerBlue;
                //this.TestTree.Focus();
            }
            this.richTextBox1.Invoke(AppendLog, $"Selected Node:{this.TestTree.SelectedNode.Text}");
        }

        private void TestTree_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node.Name == "root")
                {
                    e.Node.ContextMenuStrip = this.contextMenuStripRoot;
                }
                else if (e.Node.Nodes.Count == 0)
                {
                    e.Node.ContextMenuStrip = this.contextMenuStrip1;
                }
                else
                {
                    e.Node.ContextMenuStrip = null;
                }

            }
        }
        private List<TreeNode> ExecDotnetTest(Action<string> act)
        {

            string command = $"dotnet test --no-build -t -v=q {_testPath}";
            var projectRootName = Path.GetFileName(_testPath);
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

            var dic = new Dictionary<string, TreeNode>();
            var nodes = new List<TreeNode>();
            while (!process.StandardOutput.EndOfStream)
            {
                var curr = process.StandardOutput.ReadLine();
                if (curr != null)
                    act.Invoke(curr);
                if (curr != null && curr.StartsWith("  "))
                {

                    curr = curr.Trim();
                    var parent = string.Empty;
                    foreach (var item in curr.Split(new char[] { '.' }))
                    {
                        if (parent == string.Empty && !dic.ContainsKey(item))
                        {
                            var itemNode = new TreeNode
                            {
                                Text = item,
                                Name = item
                            };
                            nodes.Add(itemNode);
                            dic[item] = itemNode;
                        }
                        if (parent != string.Empty)
                        {
                            var itemNode = new TreeNode
                            {
                                Text = item,
                                Name = item
                            };
                            if (!dic.ContainsKey(item))
                            {
                                dic[parent].Nodes.Add(itemNode);
                                dic[item] = itemNode;
                            }

                        }

                        parent = item;
                    }
                }

            }
            TreeNode[] newArr = new TreeNode[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                TreeNode? top = null;
                List<string> prefixArr = new List<string>();
                var node = nodes[i];
                while (node.Nodes.Count == 1)
                {
                    prefixArr.Add(node.Name);
                    top = node.Nodes[0];
                    node = top;
                }
                if (top != null)
                {
                    prefixArr.Add(top.Name);
                    top.Name = string.Join('.', prefixArr);
                    newArr[i] = top;
                }
                else
                {
                    newArr[i] = node;
                }
            }
            return newArr.ToList();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TestTree.Nodes[0].Nodes.Clear();
            this.comboBox1.Enabled = false;
            UpdateTreeAsync();
        }
        private void UpdateTree(List<TreeNode> nodes)
        {
            if (nodes.Count == 1)
            {
                nodes[0].Expand();
                this.TestTree.Nodes[0].Nodes.Add(nodes[0]);
                this.TestTree.Nodes[0].Expand();
                return;
            }
            this.TestTree.Nodes[0].Nodes.AddRange(nodes.ToArray());
        }
        private void AppendColorLog(string msg,Color color)
        {
            this.richTextBox1.SelectionColor = color;
            this.richTextBox1.AppendText($"{msg}\r\n");
            this.richTextBox1.SelectionColor = SystemColors.InactiveCaption;
        }
        private void AppendLog(string msg)
        {
            var passedIndex = msg.IndexOf("Passed");
            var errorIndex = msg.IndexOf("Failed");
            var successfulIndex = msg.IndexOf("Successful");
            if (passedIndex>-1 || successfulIndex>-1)
            {
                AppendColorLog(msg, Color.Green);
            }else if (errorIndex > -1)
            {
                AppendColorLog(msg,Color.Red);
            }
            else
                this.richTextBox1.AppendText($"{msg} \r\n");
            this.richTextBox1.ScrollToCaret();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.TestTree.SelectedNode == null)
            {
                MessageBox.Show("Please select a test Node.");
                return;
            }
            var fqdnName = GetFQDNNodeName(this.TestTree.SelectedNode);
            if (fqdnName == string.Empty) { return; }
            this.richTextBox1.AppendText($"FQDN TEST NAME:{fqdnName} \r\n");
            var splitChar = "=";
            if (this.TestTree.SelectedNode!.Nodes.Count > 0)
            {
                splitChar = "~";
            }
            string command = $"dotnet test --no-build --logger \"console;verbosity=detailed\" --filter \"FullyQualifiedName{splitChar}{fqdnName}\" {_testPath}";
            var projectRootName = Path.GetFileName(_testPath);
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
            Task.Factory.StartNew(() =>
            {
                process.Start();
                while (!process.StandardOutput.EndOfStream)
                {
                    var curr = process.StandardOutput.ReadLine();
                    this.richTextBox1.Invoke(AppendLog, curr);
                }

            });

        }
        private string GetFQDNNodeName(TreeNode? node)
        {
            if (node == null)
            {
                return string.Empty;
            }
            var names = new List<string>();
            while (node != null && node.Name != "root")
            {
                names.Add(node.Name);
                node = node.Parent;
            }
            names.Reverse();
            return string.Join('.', names);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.TestTree.CollapseAll();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TestTree.ExpandAll();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TestTree.SelectedNode?.Collapse();
        }

        private void expandAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.TestTree.SelectedNode?.ExpandAll();
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var projectFiles = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.csproj", SearchOption.TopDirectoryOnly);
                if (projectFiles == null || projectFiles.Count() < 1)
                {
                    MessageBox.Show(" The current working directory does not contain a project file", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (projectFiles.Count() > 1)
                {
                    MessageBox.Show("The current working directory contains more than one project files.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                _testPath = folderBrowserDialog1.SelectedPath;
                AppendLog($"unit test project is selected : {_testPath}");
                this.comboBox1.Enabled = false;
                UpdateTreeAsync();
            }
        }

        private void UpdateTreeAsync()
        {
            Task.Run(() =>
            {
                var nodes = ExecDotnetTest((curr) =>
                {
                    this.richTextBox1.Invoke(AppendLog, curr);
                });
                this.TestTree.Invoke(UpdateTree, nodes);
                this.comboBox1.Invoke(FillSearchBox);
                this.comboBox1.Invoke(() =>
                {
                    this.comboBox1.Enabled = true;
                });
            });
        }

        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string command = $"dotnet build {_testPath}";
            var projectRootName = Path.GetFileName(_testPath);
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
            Task.Factory.StartNew(() =>
            {
                process.Start();
                while (!process.StandardOutput.EndOfStream)
                {
                    var curr = process.StandardOutput.ReadLine();
                    this.richTextBox1.Invoke(AppendLog, curr);
                }
            });

        }
        Dictionary<string, TreeNode>? treeSearchMap;
        /// <summary>
        /// use DFS to build dictonary.
        /// </summary>
        /// <param name="nodes"></param>
        private void BuildMapOfTree(TreeNodeCollection nodes)
        {
            treeSearchMap ??= new();
            foreach (var obj in nodes)
            {
                if (obj is TreeNode node)
                {
                    if (node.Nodes.Count == 0)
                    {
                        treeSearchMap.TryAdd(node.Name, node);
                        treeSearchMap.TryAdd(node.Parent.Name, node.Parent);
                        continue;
                    }
                    BuildMapOfTree(node.Nodes);
                }
            }
        }
        private void FillSearchBox()
        {
            treeSearchMap?.Clear();
            BuildMapOfTree(this.TestTree.Nodes);
            foreach (var k in treeSearchMap?.Keys ?? Enumerable.Empty<string>())
            {
                this.comboBox1.AutoCompleteCustomSource.Add(k);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            this.timer1.Stop();
            this.timer1.Start();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MessageBox.Show("comboBox1_SelectionChangeCommitted");
        }
    }
}