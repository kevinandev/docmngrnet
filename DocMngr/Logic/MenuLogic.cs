using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FunctionGroup.Dao;
using log4net;
using System.Web.Security;

namespace FunctionGroup.Logic
{
    public class MenuLogic
    {
        ILog logger = log4net.LogManager.GetLogger("File");
        private DocMngrDataDataContext dataContext = new DocMngrDataDataContext();
        public const string MENU_LOGIC_SEC_ID = "MENU_LOGIC_SEC_ID";
        public List<menu> getAll()
        {
            List<menu> lstRS = new List<menu>();
            try
            {
                lstRS = dataContext.menus.Where(p => p.active > 0).OrderBy(x => x.master_id).ThenBy(x => x.order).ToList();
                menu root = new menu();
                root.active = 1;
                root.code = "ROOT";
                root.id = 0;
                root.master_id = 0;
                root.name = "Menu chính";
                root.order = 1;
                root.secure_level = 0;
                root.selectable = 1;
                root.text = "Menu chính";
                root.type = 0;
                root.url = "#";
                lstRS.Insert(0, root);
            }
            catch (Exception ex)
            {
                logger.Error("getAll error: ", ex);
            }
            return lstRS;
        }
        public List<menu> getRootMenu()
        {
            List<menu> lstRS = new List<menu>();
            try
            {
                lstRS = dataContext.menus.Where(q=>q.type==0).OrderBy(x=>x.order).ToList();
            }
            catch (Exception ex)
            {
                logger.Error("Menu logic getRootMenu error: ",ex);
            }
            return lstRS;
        }
        public List<menu> getMenus(menu m)
        {
            List<menu> lstRS = new List<menu>();
            try
            {
                lstRS = dataContext.menus.Where(q => (m == null && q.type == 0)||(m!=null&&q.master_id==m.id)).OrderBy(x => x.order).ToList();
            }
            catch (Exception ex)
            {
                logger.Error("Menu logic getRootMenu error: ", ex);
            }
            return lstRS;
        }
        
        public menu findById(int id)
        {
            menu rs = null;
            try
            {
                rs = dataContext.menus.Where(p => p.id == id).First();
            }
            catch (Exception ex)
            {
                logger.Error("getAll error: ", ex);
            }
            return rs;
        }
        public bool validateInsert(menu sec)
        {
            bool rt = true;
            try
            {
                List<menu> rs = dataContext.menus.Where(p => (p.code.Equals(sec.code))).ToList();
                if (rs != null && rs.Count > 0)
                {
                    rt = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("validateInsert error: ", ex);
            }
            return rt;

        }
        public bool validateUpdate(menu sec)
        {
            bool rt = true;
            try
            {
                List<menu> rs = dataContext.menus.Where(p => (p.id != sec.id && p.code.Equals(sec.code) && p.code != null && p.code.Length > 0)).ToList();
                if (rs != null && rs.Count > 0)
                {
                    rt = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("validateUpdate error: ", ex);
            }
            return rt;

        }

        public int insert(menu sec)
        {
            int newId = 0;
            try
            {
                dataContext.menus.InsertOnSubmit(sec);
                dataContext.SubmitChanges();
                newId = sec.id;
            }
            catch (Exception ex)
            {
                logger.Error("insert error: ", ex);
            }
            return newId;
        }
        public bool update(menu sec)
        {
            bool rs = false;
            try
            {
                menu attachObj = dataContext.menus.Where(p => p.id == sec.id).First();
                attachObj.master_id = sec.master_id;
                attachObj.name = sec.name;
                attachObj.order = sec.order;
                attachObj.text = sec.text;
                attachObj.type = sec.type;
                attachObj.url = sec.url;
                dataContext.SubmitChanges();
                rs = true;
            }
            catch (Exception ex)
            {
                logger.Error("update error: ", ex);
            }
            return rs;
        }
        public bool delete(int id)
        {
            bool rs = false;
            try
            {
                menu attachObj = dataContext.menus.Where(p => p.id == id).First();
                dataContext.menus.DeleteOnSubmit(attachObj);
                dataContext.SubmitChanges();
                rs = true;
            }
            catch (Exception ex)
            {
                logger.Error("update error: ", ex);
            }
            return rs;
        }
    }
}