using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.View;
/*软键盘，用于点击键盘信息
 主要模拟键盘功能。
 初始button按钮的text： GoodsNumber_Load
 */
namespace POS.Controls
{
    /// <summary>
    /// 软键盘，用于点击键盘信息
    /// </summary>
    public partial class GoodsNumber : UserControl
    { 
        /// <summary>
        /// 构造函数
        /// </summary>
        public GoodsNumber()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 初始button按钮的text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoodsNumber_Load(object sender, EventArgs e)
        {
            capslock = 1;
            if (!lockcapslock)
            {
                capslock %= 2;
                if (capslock != 0)
                {
                    btnA.Text = "a";
                    btnB.Text = "b";
                    btnC.Text = "c";
                    btnD.Text = "d";
                    btnE.Text = "e";
                    btnF.Text = "f";
                    btnG.Text = "g";
                    btnH.Text = "h";
                    btnI.Text = "i";
                    btnJ.Text = "j";
                    btnK.Text = "k";
                    btnL.Text = "l";
                    btnM.Text = "m";
                    btnN.Text = "n";
                    btnO.Text = "o";
                    btnP.Text = "p";
                    btnQ.Text = "q";
                    btnR.Text = "r";
                    btnS.Text = "s";
                    btnT.Text = "t";
                    btnU.Text = "u";
                    btnV.Text = "v";
                    btnW.Text = "w";
                    btnX.Text = "x";
                    btnY.Text = "y";
                    btnZ.Text = "z";

                }
            }
            else
            {
                btnA.Text = "A";
                btnB.Text = "B";
                btnC.Text = "C";
                btnD.Text = "D";
                btnE.Text = "E";
                btnF.Text = "F";
                btnG.Text = "G";
                btnH.Text = "H";
                btnI.Text = "I";
                btnJ.Text = "J";
                btnK.Text = "K";
                btnL.Text = "L";
                btnM.Text = "M";
                btnN.Text = "N";
                btnO.Text = "O";
                btnP.Text = "P";
                btnQ.Text = "Q";
                btnR.Text = "R";
                btnS.Text = "S";
                btnT.Text = "T";
                btnU.Text = "U";
                btnV.Text = "V";
                btnW.Text = "W";
                btnX.Text = "X";
                btnY.Text = "Y";
                btnZ.Text = "Z";
                capslock = 0;
            }
        } 

        #region//字段
        /// <summary>
        /// 大小写转换
        /// </summary>
        private int capslock = 0;
        /// <summary>
        /// textbox文本框
        /// </summary>
        private TextBox txtSaleId;
        /// <summary>
        ///用于当前使用面板是不是ReturnOfGoods
        /// </summary>
        private bool isGoodsControls=false;
        /// <summary>
        /// 传递主窗体
        /// </summary>
        private MainForm mainForm;
        #endregion


        #region//属性
        /// <summary>
        /// TextBox输出内容的格式当showTextBoxStyle=true是输出的是字母
        /// </summary>
        public bool showTextBoxStyle=true;
        /// <summary>
        /// 该控件是否隐藏，false为没有隐藏true为隐藏
        /// </summary>
        public static bool hide = false;
        /// <summary>
        /// 其值为false时为不锁定大小写，当true时为把键盘锁定为大写
        /// </summary>
        public bool lockcapslock = false;
        /// <summary>
        /// 封装数据
        /// </summary>
        public TextBox TxtSaleId
        {
            get { return txtSaleId; }
            set { txtSaleId = value; }
        }
        /// <summary>
        /// 传递主窗体
        /// </summary>
        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }
        /// <summary>
        /// 判断当前使用面板是不是退货功能面板
        /// </summary>
        public bool IsGoodsControls
        {
            get { return isGoodsControls; }
            set { isGoodsControls = value; }
        }
        #endregion
        
        #region//公共方法
        /// <summary>
        /// 返回当前焦点
        /// </summary>
        /// <param name="textBox">TextBox文本框</param>
        public void ReturnFocus(TextBox textBox)
        {
            
            textBox.Text = txtSaleId.Text;
            textBox.SelectionStart = textBox.TextLength;
            textBox.Focus();             
        }
        #endregion


        #region//事件
        /// <summary>
        /// 按钮退格的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Backspace_Click(object sender, EventArgs e)
        {
            try
            {
                txtSaleId.Text = txtSaleId.Text.Substring(0, txtSaleId.Text.Length - 1);
                ReturnFocus(txtSaleId);
            }
            catch
            {
                txtSaleId.Text = "";
            }
        }
        /// <summary>
        /// 按钮Q的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQ_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text ="Q";
                else
                    txtSaleId.Text = "q";
            }
            else
            {
                 if (capslock == 0)
                    txtSaleId.Text += "Q";
                else
                    txtSaleId.Text += "q";
            }
        }
        /// <summary>
        /// 按钮W的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnW_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "W";
                else
                    txtSaleId.Text = "w";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "W";
                else
                    txtSaleId.Text += "w";
            }
        }
        /// <summary>
        /// 按钮E的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnE_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "E";
                else
                    txtSaleId.Text = "e";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "E";
                else
                    txtSaleId.Text += "e";
            }
        }
        /// <summary>
        /// 按钮R的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnR_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "R";
                else
                    txtSaleId.Text = "r";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "R";
                else
                    txtSaleId.Text += "r";
            }

        }
        /// <summary>
        /// 按钮T的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnT_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "T";
                else
                    txtSaleId.Text = "t";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "T";
                else
                    txtSaleId.Text += "t";
            }
        }
        /// <summary>
        /// 按钮Y的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnY_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "Y";
                else
                    txtSaleId.Text = "y";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "Y";
                else
                    txtSaleId.Text += "y";
            }
        }
        /// <summary>
        /// 按钮U的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnU_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "U";
                else
                    txtSaleId.Text = "u";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "U";
                else
                    txtSaleId.Text += "u";
            }
        }
        /// <summary>
        /// 按钮I的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnI_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "I";
                else
                    txtSaleId.Text = "i";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "I";
                else
                    txtSaleId.Text += "i";
            }
        }
        /// <summary>
        /// 按钮O的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnO_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "O";
                else
                    txtSaleId.Text = "o";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "O";
                else
                    txtSaleId.Text += "o";
            }
        }
        /// <summary>
        /// 按钮P的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnP_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "P";
                else
                    txtSaleId.Text = "p";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "P";
                else
                    txtSaleId.Text += "p";
            }
        }
        /// <summary>
        /// 按钮清空的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!isGoodsControls)
            {
                txtSaleId.Clear();
            }
            else 
            {
                txtSaleId.Clear();
            }
        }
        /// <summary>
        /// 按钮A的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnA_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "A";
                else
                    txtSaleId.Text = "a";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "A";
                else
                    txtSaleId.Text += "a";
            }
        }
        /// <summary>
        /// 按钮S的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnS_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "S";
                else
                    txtSaleId.Text = "s";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "S";
                else
                    txtSaleId.Text += "s";
            }
        }
        /// <summary>
        /// 按钮D的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnD_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "D";
                else
                    txtSaleId.Text = "d";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "D";
                else
                    txtSaleId.Text += "d";
            }
        }
        /// <summary>
        /// 按钮F的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnF_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "F";
                else
                    txtSaleId.Text = "f";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "F";
                else
                    txtSaleId.Text += "f";
            }
        }
        /// <summary>
        /// 按钮G的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnG_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "G";
                else
                    txtSaleId.Text = "g";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "G";
                else
                    txtSaleId.Text += "g";
            }
        }
        /// <summary>
        /// 按钮H的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnH_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "H";
                else
                    txtSaleId.Text = "h";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "H";
                else
                    txtSaleId.Text += "h";
            }
        }
        /// <summary>
        /// 按钮J的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJ_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "J";
                else
                    txtSaleId.Text = "j";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "J";
                else
                    txtSaleId.Text += "j";
            }
        }
        /// <summary>
        /// 按钮K的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnK_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "K";
                else
                    txtSaleId.Text = "k";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "K";
                else
                    txtSaleId.Text += "k";
            }
        }
        /// <summary>
        /// 按钮L的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnL_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "L";
                else
                    txtSaleId.Text = "l";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "L";
                else
                    txtSaleId.Text += "l";
            }
        }
        /// <summary>
        /// 按钮Z的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZ_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "Z";
                else
                    txtSaleId.Text = "z";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "Z";
                else
                    txtSaleId.Text += "z";
            }
        }
        /// <summary>
        /// 按钮X的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnX_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "X";
                else
                    txtSaleId.Text = "x";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "X";
                else
                    txtSaleId.Text += "x";
            }
        }
        /// <summary>
        /// 按钮C的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnC_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "C";
                else
                    txtSaleId.Text = "c";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "C";
                else
                    txtSaleId.Text += "c";
            }
        }
        /// <summary>
        /// 按钮V的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnV_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "V";
                else
                    txtSaleId.Text = "v";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "V";
                else
                    txtSaleId.Text += "v";
            }
        }
        /// <summary>
        /// 按钮B的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnB_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "B";
                else
                    txtSaleId.Text = "b";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "B";
                else
                    txtSaleId.Text += "b";
            }
        }
        /// <summary>
        /// 按钮N的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnN_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "N";
                else
                    txtSaleId.Text = "n";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "N";
                else
                    txtSaleId.Text += "n";
            }
        }
        /// <summary>
        /// 按钮M的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnM_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                if (capslock == 0)
                    txtSaleId.Text = "M";
                else
                    txtSaleId.Text = "m";
            }
            else
            {
                if (capslock == 0)
                    txtSaleId.Text += "M";
                else
                    txtSaleId.Text += "m";
            }
        }
        /// <summary>
        /// 按钮转换大小写的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapsLock_Click(object sender, EventArgs e)
        {
            if (!lockcapslock)
            {
                capslock++;
                capslock %= 2;
            }
            else
            {
                capslock = 0;
            }
            if (capslock == 0)
            {
                btnA.Text = "A";
                btnB.Text = "B";
                btnC.Text = "C";
                btnD.Text = "D";
                btnE.Text = "E";
                btnF.Text = "F";
                btnG.Text = "G";
                btnH.Text = "H";
                btnI.Text = "I";
                btnJ.Text = "J";
                btnK.Text = "K";
                btnL.Text = "L";
                btnM.Text = "M";
                btnN.Text = "N";
                btnO.Text = "O";
                btnP.Text = "P";
                btnQ.Text = "Q";
                btnR.Text = "R";
                btnS.Text = "S";
                btnT.Text = "T";
                btnU.Text = "U";
                btnV.Text = "V";
                btnW.Text = "W";
                btnX.Text = "X";
                btnY.Text = "Y";
                btnZ.Text = "Z";
            }
            else 
            {
                btnA.Text = "a";
                btnB.Text = "b";
                btnC.Text = "c";
                btnD.Text = "d";
                btnE.Text = "e";
                btnF.Text = "f";
                btnG.Text = "g";
                btnH.Text = "h";
                btnI.Text = "i";
                btnJ.Text = "j";
                btnK.Text = "k";
                btnL.Text = "l";
                btnM.Text = "m";
                btnN.Text = "n";
                btnO.Text = "o";
                btnP.Text = "p";
                btnQ.Text = "q";
                btnR.Text = "r";
                btnS.Text = "s";
                btnT.Text = "t";
                btnU.Text = "u";
                btnV.Text = "v";
                btnW.Text = "w";
                btnX.Text = "x";
                btnY.Text = "y";
                btnZ.Text = "z";
            
            }
        }
        /// <summary>
        /// 按钮确定的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEnter_Click(object sender, EventArgs e)
        {
            this.Hide();
            GoodsNumber.hide = true;
            ReturnFocus(txtSaleId);
        }
        /// <summary>
        /// 按钮1的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_Click(object sender, EventArgs e)
        {

            if (!showTextBoxStyle)
            {
                txtSaleId.Text = "1";
            }
            else
            { 
                txtSaleId.Text += "1";
            }
        }
        /// <summary>
        /// 按钮2的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2_Click(object sender, EventArgs e)
        {

            if (!showTextBoxStyle)
            {
                txtSaleId.Text = "2";
            }
            else
            {
                txtSaleId.Text += "2";
            }
        }
        /// <summary>
        /// 按钮3的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn3_Click(object sender, EventArgs e)
        {

            if (!showTextBoxStyle)
            {
                txtSaleId.Text = "3";
            }
            else
            { 
                txtSaleId.Text += "3";
            }
        }
        /// <summary>
        /// 按钮4的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn4_Click(object sender, EventArgs e)
        {

            if (!showTextBoxStyle)
            {
                txtSaleId.Text = "4";
            }
            else
            { 
                txtSaleId.Text += "4";
            }
        }
        /// <summary>
        /// 按钮5的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn5_Click(object sender, EventArgs e)
        {

            if (!showTextBoxStyle)
             {
                 txtSaleId.Text = "5";
             }
             else
             { 
                 txtSaleId.Text += "5";
             }
        }
        /// <summary>
        /// 按钮6的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn6_Click(object sender, EventArgs e)
        {

            if (!showTextBoxStyle)
             {
                 txtSaleId.Text = "6";
             }
             else
             { 
                 txtSaleId.Text += "6";
             }
        }
        /// <summary>
        /// 按钮7的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn7_Click(object sender, EventArgs e)
        {

            if (!showTextBoxStyle)
            {
                txtSaleId.Text = "7";
            }
            else
            { 
                txtSaleId.Text += "7";
            }
        }
        /// <summary>
        /// 按钮8的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn8_Click(object sender, EventArgs e)
        {

            if (!showTextBoxStyle)
            {
                txtSaleId.Text = "8";
            }
            else
            {
                txtSaleId.Text += "8";
            }
        }
        /// <summary>
        /// 按钮9的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn9_Click(object sender, EventArgs e)
        {

            if (!showTextBoxStyle)
            {
                txtSaleId.Text = "9";
            }
            else
            {
                txtSaleId.Text += "9";
            }
        }
        /// <summary>
        /// 按钮0的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn0_Click(object sender, EventArgs e)
        {
            if (!showTextBoxStyle)
            {
                txtSaleId.Text = "0"; 
            }
           
            else
            {
                txtSaleId.Text += "0"; 
            }
        }
        #endregion
            
    }
}
