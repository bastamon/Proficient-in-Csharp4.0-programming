using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex6_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAddRoot_Click(object sender, EventArgs e)
        {
            //构造节点，给出节点显示内容、节点取消选定时所显示图像的索引号、选定时所显示图像的索引号
            TreeNode newNode = new TreeNode(this.textBoxRoot.Text, 0, 1);
            this.treeView1.Nodes.Add(newNode);
            this.treeView1.Select();
        }

        private void buttonAddChild_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("添加子节点之前必须先选中一个节点。", "提示信息");
                return;
            }
            TreeNode newNode = new TreeNode(this.textBoxChild.Text, 2, 3);
            selectedNode.Nodes.Add(newNode);                      //在选中节点（系部节点）下添加子节点（班级节点）
            selectedNode.Expand();
            this.treeView1.Select();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("删除节点之前先选中一个节点。", "提示信息");
                return;
            }
            TreeNode parentNode = selectedNode.Parent;            //找到待删除节点的子节点
            if (parentNode == null)
                this.treeView1.Nodes.Remove(selectedNode);  //若是顶级节点（系部节点），就通过TreeView的节点集合删除
            else
                parentNode.Nodes.Remove(selectedNode);      //否则，是班级节点，通过父亲节点（系部节点）删除

            this.treeView1.Select();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear(); 
        }
    }
}
