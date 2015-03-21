using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FunctionGroup.utils;

namespace DocMngr
{
    public partial class UserManagement : System.Web.UI.Page
    {

        private string status = Constants.FORM_STATUS_VIEWING;
        private string currentUserID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void setStatus()
        {
            if (Constants.FORM_STATUS_VIEWING.Equals(status))
            {
                BtnResetPassword.Enabled = false;
                BtnAdd.Enabled = true;
                BtnEdit.Enabled = false;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                TxtConfirmPassword.Enabled = false;
                TxtEmail.Enabled = false;
                TxtFullName.Enabled = false;
                TxtPassword.Enabled = false;
                TxtUserName.Enabled = false;
            }
            else if (Constants.FORM_STATUS_EDITTING.Equals(status))
            {
                BtnResetPassword.Enabled = false;
                BtnAdd.Enabled = false;
                BtnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                TxtConfirmPassword.Enabled = true;
                TxtEmail.Enabled = true;
                TxtFullName.Enabled = true;
                TxtPassword.Enabled = true;
                TxtUserName.Enabled = true;

            }

        }
    }
}