using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _6_13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.ContextMenuStrip = contextMenuStrip1;//设置树控件的快捷菜单
            TreeNode TopNode = treeView1.Nodes.Add("公司");//建立一个顶级节点（或一级节点、第一层节点）
            //建立4个第二层节点，分别表示4个大的部门
            TreeNode ParentNode1 = new TreeNode("人事部");
            TreeNode ParentNode2 = new TreeNode("财务部");
            TreeNode ParentNode3 = new TreeNode("基础部");
            TreeNode ParentNode4 = new TreeNode("软件开发部");
            //将4个第二层节点添加到第一层节点中
            TopNode.Nodes.Add(ParentNode1);
            TopNode.Nodes.Add(ParentNode2);
            TopNode.Nodes.Add(ParentNode3);
            TopNode.Nodes.Add(ParentNode4);
            //建立6个子节点，作为第三层节点，分别表示6个部门
            TreeNode ChildNode1 = new TreeNode("C#部门");
            TreeNode ChildNode2 = new TreeNode("ASP.NET部门");
            TreeNode ChildNode3 = new TreeNode("VB部门");
            TreeNode ChildNode4 = new TreeNode("VC部门");
            TreeNode ChildNode5 = new TreeNode("JAVA部门");
            TreeNode ChildNode6 = new TreeNode("PHP部门");
            //将6个子节点（第三层节点）添加到对应的（第4个）第二层节点中
            ParentNode4.Nodes.Add(ChildNode1);
            ParentNode4.Nodes.Add(ChildNode2);
            ParentNode4.Nodes.Add(ChildNode3);
            ParentNode4.Nodes.Add(ChildNode4);
            ParentNode4.Nodes.Add(ChildNode5);
            ParentNode4.Nodes.Add(ChildNode6);

            //设置imageList1控件中显示的图像
            imageList1.Images.Add(Image.FromFile("1.png"));
            imageList1.Images.Add(Image.FromFile("2.png"));
            imageList1.ImageSize = new Size(16, 16);

            //设置treeView1的ImageList属性为imageList1
            treeView1.ImageList = imageList1;

            //设置treeView1控件节点的图标在imageList1控件中的索引是0
            treeView1.ImageIndex = 0;
            //选择某个节点后显示的图标在imageList1控件中的索引是1
            treeView1.SelectedImageIndex = 1;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //在AfterSelect事件中，获取控件中选中节点显示的文本
            label1.Text = "选择的部门：" + e.Node.Text;
        }

        private void 全部展开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();//展开所有树节点
        }

        private void 全部折叠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();//折叠所有树节点
        }
    }
}
