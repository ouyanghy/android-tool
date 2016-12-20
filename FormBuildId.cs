using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace android_tool
{
    public partial class FormBuildId : Form
    {
        private Cmd mCmd;
        private String mType;
        private String mBuildId;
        private FileParseBuildProp mParse;
        private MessageDialog mMessageBox;
        public FormBuildId(Cmd cmd)
        {
            InitializeComponent();
            mCmd = cmd;
            mMessageBox = new MessageDialog();
            mParse = new FileParseBuildProp(mCmd);
            mBuildId = mParse.getBuildId();
            mType = mParse.getType();
            textBoxBuild.Text = mBuildId;
            textBoxType.Text = mType;
        }

        private void buttonSureBuild_Click(object sender, EventArgs e)
        {
            String id = textBoxBuild.Text;
            if (id == null || id.Equals("") || mBuildId == null || mBuildId.Equals(""))
            {
                mMessageBox.ShowMessageBoxTimeout(Enums.Error.BUILD_ID, Enums.Title.TIP, 3000);
                return;
            }
            bool ret = mParse.modifyBuildId(mBuildId, id);

            if (ret)
                mMessageBox.ShowMessageBoxTimeout(Enums.Succ.BUILD_ID, Enums.Title.TIP, 3000);
            else
                mMessageBox.ShowMessageBoxTimeout(Enums.Error.BUILD_ID, Enums.Title.TIP, 3000);
        }

        private void buttonSureType_Click(object sender, EventArgs e)
        {
            String type = textBoxType.Text;
            if (type == null || type.Equals("") || mType == null || mType.Equals(""))
            {
                mMessageBox.ShowMessageBoxTimeout(Enums.Error.BUILD_TYPE, Enums.Title.TIP, 3000);
                return;
            }
            bool ret = mParse.modifyType(mType, type);

            if (ret)
                mMessageBox.ShowMessageBoxTimeout(Enums.Succ.BUILD_TYPE, Enums.Title.TIP, 3000);
            else
                mMessageBox.ShowMessageBoxTimeout(Enums.Error.BUILD_TYPE, Enums.Title.TIP, 3000);
        }
    }
}
