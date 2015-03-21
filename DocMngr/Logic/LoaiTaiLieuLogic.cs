using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FunctionGroup.Dao;
using log4net;

namespace FunctionGroup.Logic
{
    public class LoaiTaiLieuLogic
    {
        ILog logger = log4net.LogManager.GetLogger("File");
        private DocMngrDataDataContext dataContext = new DocMngrDataDataContext();
        public const string LOAI_TAI_LIEU_LOGIC_SEC_ID = "LOAI_TAI_LIEU_LOGIC_SEC_ID";
        public List<Su_LoaiTaiLieu> getAll()
        {
            List<Su_LoaiTaiLieu> lstRS = new List<Su_LoaiTaiLieu>();
            try
            {
                lstRS = dataContext.Su_LoaiTaiLieus.Where(p => (p.Active > 0 || p.Active == null)).ToList();
            }
            catch (Exception ex)
            {
                logger.Error("getAll error: ", ex);
            }
            return lstRS;
        }
        public Su_LoaiTaiLieu findById(int id)
        {
            Su_LoaiTaiLieu rs = null;
            try
            {
                rs = dataContext.Su_LoaiTaiLieus.Where(p => ((p.Active > 0 || p.Active == null) && p.ID == id)).First();
            }
            catch (Exception ex)
            {
                logger.Error("getAll error: ", ex);
            }
            return rs;
        }
        public bool validateInsert(Su_LoaiTaiLieu sec)
        {
            bool rt = true;
            try
            {
                List<Su_LoaiTaiLieu> rs = dataContext.Su_LoaiTaiLieus.Where(p => (p.Code.Equals(sec.Code) || p.Name.ToUpper().Equals(sec.Name.ToUpper())) && p.Active > 0).ToList();
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
        public bool validateUpdate(Su_LoaiTaiLieu sec)
        {
            bool rt = true;
            try
            {
                List<Su_LoaiTaiLieu> rs = dataContext.Su_LoaiTaiLieus.Where(p => (p.ID != sec.ID && p.Name.ToUpper().Equals(sec.Name.ToUpper())) && p.Active > 0).ToList();
                if (rs != null && rs.Count > 0)
                {
                    rt = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("getAll error: ", ex);
            }
            return rt;

        }

        public int insert(Su_LoaiTaiLieu sec)
        {
            try
            {

                dataContext.Su_LoaiTaiLieus.InsertOnSubmit(sec);
                dataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                logger.Error("insert error: ", ex);
            }
            return sec.ID;
        }
        public bool update(Su_LoaiTaiLieu sec)
        {
            bool rs = false;
            try
            {
                Su_LoaiTaiLieu attachObj = dataContext.Su_LoaiTaiLieus.Where(p => p.ID == sec.ID).First();
                attachObj.Name = sec.Name;
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
        public bool delete(int id)
        {
            bool rs = false;
            try
            {
                Su_LoaiTaiLieu attachObj = dataContext.Su_LoaiTaiLieus.Where(p => p.ID == id).First();
                attachObj.Active = 0;
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