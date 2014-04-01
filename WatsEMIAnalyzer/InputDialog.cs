using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WatsEMIAnalyzer
{
    public delegate bool CheckInputDelegate(string input);

    public partial class InputDialog : Form
    {
        private string mCaption;
        private string mPrompt;
        private string mText;
        private bool mOKInvoked;
        public CheckInputDelegate mCheckInputDelegate;

        public static DialogResult Show(string caption, string prompt, ref string inputText, CheckInputDelegate checkDelegate)
        {
            InputDialog inputDialog = new InputDialog();
            inputDialog.mCaption = caption;
            inputDialog.mPrompt = prompt;
            inputDialog.mText = inputText;
            inputDialog.mCheckInputDelegate = checkDelegate;

            DialogResult result = inputDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                inputText = inputDialog.mText;
            }

            return result;
        }

        private InputDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            mOKInvoked = false;
        }

        private void InputDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mOKInvoked)
                return;

            mOKInvoked = false;
            mText = InputEditor.Text.Trim();
            if (mText.Length == 0)
            {
                MessageBox.Show("Input is empty !");
                InputEditor.Focus();
                e.Cancel = true;
                return;
            }

            if (mCheckInputDelegate != null && !mCheckInputDelegate.Invoke(mText))
            {
                e.Cancel = true;
                return;
            }

        }

        private void InputDialog_Load(object sender, EventArgs e)
        {
            this.Text = mCaption;
            this.PromptLabel.Text = mPrompt;
            this.InputEditor.Text = mText;
        }
    }
}
