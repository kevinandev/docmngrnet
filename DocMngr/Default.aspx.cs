using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FunctionGroup.Dao;

namespace DocMngr
{
    public partial class _Default : System.Web.UI.Page
    {
        private DocMngrDataDataContext dataContext = new DocMngrDataDataContext();
        private introduce cIntroduce = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            var introduce = dataContext.introduces.Where(p => p.active > 0);
            if (introduce != null)
            {
                List<introduce> lst = introduce.ToList<introduce>();
                if (lst.Count > 0)
                {
                    cIntroduce = lst[0];
                }
            }
            if (cIntroduce != null)
            {
                lblTitle.Text = cIntroduce.title;
                divContent.InnerHtml = cIntroduce.body;
            }
        }
    }
}
