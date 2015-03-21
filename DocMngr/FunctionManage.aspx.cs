using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FunctionGroup.utils;
using System.Drawing;
using log4net;
using Dao;
using System.Data;
using FunctionGroup.Dao;

namespace DocMngr
{

    public partial class FunctionManage : System.Web.UI.Page
    {
        private DocMngrDataDataContext dataContext = new DocMngrDataDataContext();
        private string formStatus = Constants.FORM_STATUS_VIEWING;
        private function currentFunction = null;
        private List<function> lstFunction = null;
        private int currentSelectedRow = -1;
        private int currentSelectedId = 0;
        ILog logger = log4net.LogManager.GetLogger("File");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var varFunctions = from p in dataContext.functions select p;
                lstFunction = varFunctions.ToList();
                if (lstFunction == null)
                {
                    lstFunction = new List<function>();
                }
                //tblFunction.
                tblFunction.DataSource = lstFunction;
                tblFunction.DataBind();
                refreshView();
                logger.Info("FunctionManager loaded!");
            }
            catch (Exception ex)
            {
                logger.Error("Query database error: ", ex);
            }
        }
        private void refreshView()
        {
            if (Constants.FORM_STATUS_VIEWING.Equals(formStatus) || Constants.FORM_STATUS_SELECTING.Equals(formStatus))
            {
                BtnAdd.Enabled = true;
                BtnEdit.Enabled = true;
                BtnDelete.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                TxtFunctionCode.Enabled = false;
                TxtFunctionName.Enabled = false;
                TxtPath.Enabled = false;
            }
            else if (Constants.FORM_STATUS_EDITTING.Equals(formStatus))
            {
                BtnAdd.Enabled = false;
                BtnEdit.Enabled = false;
                BtnDelete.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                TxtFunctionCode.Enabled = true;
                TxtFunctionName.Enabled = true;
                TxtPath.Enabled = true;
            }
            else if (Constants.FORM_STATUS_INSERTING.Equals(formStatus))
            {
                BtnAdd.Enabled = false;
                BtnEdit.Enabled = false;
                BtnDelete.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                TxtFunctionCode.Enabled = true;
                TxtFunctionName.Enabled = true;
                TxtPath.Enabled = true;
            }
            foreach (GridViewRow r in tblFunction.Rows)
            {
                r.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(tblFunction, "Select$" + r.RowIndex);
                r.ToolTip = "Click để chọn.";
            }
        }

        protected void onSelectedRow(object sender, EventArgs e)
        {
            if (Constants.FORM_STATUS_SELECTING.Equals(formStatus) || Constants.FORM_STATUS_VIEWING.Equals(formStatus))
            {
                foreach (GridViewRow row in tblFunction.Rows)
                {
                    if (row.RowIndex == tblFunction.SelectedIndex && row.RowType == DataControlRowType.DataRow)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#006BB9");
                        row.ForeColor = ColorTranslator.FromHtml("White");
                        row.ToolTip = string.Empty;
                        currentSelectedRow = tblFunction.SelectedIndex;
                        currentSelectedId = 0;
                        try
                        {
                            currentSelectedId = lstFunction[tblFunction.SelectedIndex].function_id;
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Function manager parse selected id error: id = " + row.Cells[0].Text, ex);
                        }
                        if (currentSelectedId > 0)
                        {
                            try
                            {
                                currentFunction = dataContext.functions.Single(p => p.function_id == currentSelectedId);
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Get function error: ", ex);
                            }
                            if (currentFunction != null)
                            {
                                TxtFunctionCode.Text = currentFunction.function_code;
                                TxtFunctionName.Text = currentFunction.function_name;
                                TxtPath.Text = currentFunction.function_path;
                            }
                        }
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("White");
                        row.ForeColor = ColorTranslator.FromHtml("#333333");
                        row.ToolTip = "Click để chọn";
                    }
                }
            }
            else
            {
                if (currentSelectedRow >= 0)
                {
                    tblFunction.SelectedIndex = currentSelectedRow;
                }
            }
        }

        protected void onDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                function f = (function)e.Row.DataItem;
                if (f != null)
                {
                    Label lblRowNum = (Label)e.Row.Cells[0].FindControl("lblRowNum");
                    if (lblRowNum != null)
                    {
                        lblRowNum.Text = (e.Row.RowIndex + 1).ToString();
                    }
                    Label lblCode = (Label)e.Row.Cells[1].FindControl("lblFunctionCode");
                    if (lblCode != null)
                    {
                        lblCode.Text = f.function_code;
                    }
                    Label lblName = (Label)e.Row.Cells[2].FindControl("lblFunctionName");
                    if (lblName != null)
                    {
                        lblName.Text = f.function_name;
                    }
                    Label lblPath = (Label)e.Row.Cells[3].FindControl("lblFunctionPath");
                    if (lblPath != null)
                    {
                        lblPath.Text = f.function_path;
                    }
                    CheckBox chkActive = (CheckBox)e.Row.Cells[4].FindControl("chkActive");

                    if (chkActive != null)
                    {
                        chkActive.Checked = f.active > 0;
                    }
                }
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(tblFunction, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click để chọn.";
            }

        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}