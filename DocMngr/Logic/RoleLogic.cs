using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FunctionGroup.Dao;
using log4net;
using System.Web.Security;

namespace FunctionGroup.Logic
{
    public class RoleLogic
    {
        ILog logger = log4net.LogManager.GetLogger("File");
        private DocMngrDataDataContext dataContext = new DocMngrDataDataContext();
        public const string ROLE_LOGIC_SEC_ID = "ROLE_LOGIC_SEC_ID";
        public List<aspnet_Role> getAll()
        {
            List<aspnet_Role> lstRS = new List<aspnet_Role>();
            try
            {
                lstRS = dataContext.aspnet_Roles.ToList();
            }
            catch (Exception ex)
            {
                logger.Error("getAll error: ", ex);
            }
            return lstRS;
        }
        public aspnet_Role findById(string id)
        {
            aspnet_Role rs = null;
            Guid gId;
            Guid.TryParse(id, out gId);
            try
            {
                rs = dataContext.aspnet_Roles.Where(p => p.RoleId.Equals(gId)).First();
            }
            catch (Exception ex)
            {
                logger.Error("getAll error: ", ex);
            }
            return rs;
        }
        public aspnet_Role findByCode(string code)
        {
            aspnet_Role rs = null;
            try
            {
                rs = dataContext.aspnet_Roles.Where(p => p.Code.Equals(code)).First();
            }
            catch (Exception ex)
            {
                logger.Error("Role findByCode error: ", ex);
            }
            return rs;
        }
        public aspnet_Role findByName(string name)
        {
            aspnet_Role rs = null;
            try
            {
                rs = dataContext.aspnet_Roles.Where(p => p.RoleName.Equals(name)).First();
            }
            catch (Exception ex)
            {
                logger.Error("getAll error: ", ex);
            }
            return rs;
        }
        public bool validateInsert(aspnet_Role sec)
        {
            bool rt = true;
            try
            {
                List<aspnet_Role> rs = dataContext.aspnet_Roles.Where(p => (p.Code.Equals(sec.Code) || p.RoleName.ToUpper().Equals(sec.RoleName.ToUpper()))).ToList();
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
        public bool validateUpdate(aspnet_Role sec)
        {
            bool rt = true;
            try
            {
                List<aspnet_Role> rs = dataContext.aspnet_Roles.Where(p => (!p.RoleId.Equals(sec.RoleId) && p.RoleName.ToUpper().Equals(sec.RoleName.ToUpper()))).ToList();
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

        public string insert(aspnet_Role sec)
        {
            string newId = "";
            try
            {
                Roles.CreateRole(sec.RoleName);
                aspnet_Role r =  findByName(sec.RoleName);
                r.Code = sec.Code;
                r.Description = sec.Description;
                dataContext.SubmitChanges();
                newId = r.RoleId.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("insert error: ", ex);
            }
            return newId;
        }
        public bool update(aspnet_Role sec)
        {
            bool rs = false;
            try
            {
                aspnet_Role attachObj = dataContext.aspnet_Roles.Where(p => p.RoleId.Equals(sec.RoleId)).First();
                attachObj.RoleName = sec.RoleName;
                attachObj.Description = sec.Description;
                dataContext.SubmitChanges();
                rs = true;
            }
            catch (Exception ex)
            {
                logger.Error("update error: ", ex);
            }
            return rs;
        }
        public bool delete(string name)
        {
            bool rs = false;
            try
            {
                Roles.DeleteRole(name);
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