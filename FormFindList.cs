using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace android_tool
{
    public partial class FormFindList : Form
    {
        private static SynchronizationContext mSyn;
        private InterfaceOutput mInterface;
        private bool bState = true;
        public FormFindList()
        {
            mSyn = SynchronizationContext.Current;
            InitializeComponent();

            ToolStripMenuItem rightMenu = new ToolStripMenuItem("Copy");
            rightMenu.Click += new EventHandler(Copy_Click);

            ContextMenuStrip context = new ContextMenuStrip();
            context.Items.AddRange(new ToolStripItem[] { rightMenu });

            this.ContextMenuStrip = context;
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            if (listBoxFind.SelectedIndex == -1)
                return;
            //Console.WriteLine("index:" + listBoxFind.SelectedIndex + " val:" + listBoxFind.Items[listBoxFind.SelectedIndex].ToString());
            Clipboard.SetText(listBoxFind.Items[listBoxFind.SelectedIndex].ToString());
        }

        public void setState(bool b)
        {
            bState = b;
        }

        public void setFindCall(InterfaceOutput inter)
        {
            mInterface = inter;
        }
        public void addItem(String s)
        {
           
            mSyn.Post(__addItem, s);
        }

        public void __addItem(Object obj)
        {
            String s = (String)obj;
            this.listBoxFind.Items.Add(s);
        }

        public void addListen(Enums.ptr_func_showLog func)
        {

        }
    }
}
