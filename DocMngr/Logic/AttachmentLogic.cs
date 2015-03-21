using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using Function;
using FunctionGroup.Dao;
using log4net;

namespace FunctionGroup.Logic
{
    public class AttachmentLogic
    {
        public const string ATTACHMENT_TYPE_HS = "HS";
        public const string ATTACHMENT_TYPE_VB = "VB";
        public const string SESSION_SEC_ID = "ATTACHMENT_LOGIC_SEC_ID";
        string classobject = "AttachmentLogic";
        ILog logger = log4net.LogManager.GetLogger("File");
        private DocMngrDataDataContext dataContext = new DocMngrDataDataContext();

        public string createAttachment(string fileStore, string type, string name, FileUpload fileUpload, int id)
        {
            int nextVersion = getNextVersion(fileStore, type, id);
            string nextPath = fileStore + "\\" + type + "_" + id.ToString() + "\\V" + nextVersion.ToString();
            try
            {
                fileUpload.PostedFile.SaveAs(nextPath + "\\" + fileUpload.FileName);
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "uploadfile", ex.Message + ex.StackTrace);
            }
            Su_Attachment_Version atv = new Su_Attachment_Version();
            Su_Attachment at = new Su_Attachment();
            at.name = name;
            at.path = nextPath + "\\" + fileUpload.FileName;
            at.mime_type = fileUpload.PostedFile.ContentType;
            dataContext.Su_Attachments.InsertOnSubmit(at);
            try
            {
                dataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                Logger.logmessage(classobject, "uploadfile Su_Attachment submit", ex.Message + ex.StackTrace);
            }
            if (ATTACHMENT_TYPE_VB.Equals(type))
            {
                Su_VanBan_Attachment vAt = new Su_VanBan_Attachment();
                vAt.attachment_id = at.id;
                vAt.vanban_id = id;
                dataContext.Su_VanBan_Attachments.InsertOnSubmit(vAt);
                try
                {
                    dataContext.SubmitChanges();
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "uploadfile Su_VanBan_Attachment submit", ex.Message + ex.StackTrace);
                }
            }
            else if (ATTACHMENT_TYPE_HS.Equals(type))
            {
                Su_HoSo_Attachment hAt = new Su_HoSo_Attachment();
                hAt.attachment_id = at.id;
                hAt.id_hoso = id;
                dataContext.Su_HoSo_Attachments.InsertOnSubmit(hAt);
                try
                {
                    dataContext.SubmitChanges();
                }
                catch (Exception ex)
                {
                    Logger.logmessage(classobject, "uploadfile Su_HoSo_Attachment submit", ex.Message + ex.StackTrace);

                }

            }
            return nextPath + "\\" + fileUpload.FileName;
        }

        private int getNextVersion(string fileStore, string type, int id)
        {
            string cObjectFolder = fileStore+"\\"+type+"_"+id.ToString();
            if(!Directory.Exists(cObjectFolder)){
                Directory.CreateDirectory(cObjectFolder);
                Directory.CreateDirectory(cObjectFolder+"\\V1");
                return 1;

            }else{
                string[] folders = Directory.GetDirectories(fileStore+"\\"+type+"_"+id.ToString());
                if (folders.Length == 0)
                {
                    Directory.CreateDirectory(cObjectFolder + "\\V1");
                    return 1;
                }
                else
                {
                    Directory.CreateDirectory(cObjectFolder + "\\V" + (folders.Length+1).ToString());
                    return folders.Length + 1;

                }
            }
        }
        private List<string> getListFolderIn(string folder)
        {
            string[] folders = Directory.GetDirectories(folder);
            return folders.OfType<string>().ToList();
        }
        public List<Su_Attachment> getAttachment(string type, int id)
        {
            List<Su_Attachment> lstRS = new List<Su_Attachment>();
            if (ATTACHMENT_TYPE_HS.Equals(type))
            {
                try
                {
                    List<Su_HoSo_Attachment> lstHA = dataContext.Su_HoSo_Attachments.Where(p => p.id_hoso == id).ToList();
                    foreach (Su_HoSo_Attachment sHA in lstHA)
                    {
                        try
                        {
                            Su_Attachment sA = dataContext.Su_Attachments.Where(p => p.id == sHA.attachment_id).First();
                            lstRS.Add(sA);
                        }
                        catch (Exception ex2)
                        {
                            logger.Error("Attachment logic getAttachment error: ", ex2);
                        }
                    }

                }
                catch (Exception ex)
                {
                    logger.Error("Attachment logic getAttachment error: ", ex);
                }
            }
            if (ATTACHMENT_TYPE_VB.Equals(type))
            {
                try
                {
                    List<Su_VanBan_Attachment> lstHA = dataContext.Su_VanBan_Attachments.Where(p => p.vanban_id == id).ToList();
                    foreach (Su_VanBan_Attachment sHA in lstHA)
                    {
                        try
                        {
                            Su_Attachment sA = dataContext.Su_Attachments.Where(p => p.id == sHA.attachment_id).First();
                            lstRS.Add(sA);
                        }
                        catch (Exception ex2)
                        {
                            logger.Error("Attachment logic getAttachment error: ", ex2);
                        }
                    }

                }
                catch (Exception ex)
                {
                    logger.Error("Attachment logic getAttachment error: ", ex);
                }
            }
            return lstRS;
        }
        public Su_Attachment getAttachmentByID(int id)
        {
            return dataContext.Su_Attachments.Where(p => p.id == id).First();
        }
        public void deleteAttachmentFromHoSo(int hid, int attachId)
        {
            Su_HoSo_Attachment obj = dataContext.Su_HoSo_Attachments.Where(p => p.id_hoso == hid && p.attachment_id == attachId).FirstOrDefault();
            if (obj != null)
            {
                dataContext.Su_HoSo_Attachments.DeleteOnSubmit(obj);
                dataContext.SubmitChanges();
            }
        }
        public void deleteAttachmentFromVanBan(int vbId, int attachId)
        {
            Su_VanBan_Attachment obj = dataContext.Su_VanBan_Attachments.Where(p => p.vanban_id == vbId && p.attachment_id == attachId).FirstOrDefault();
            if (obj != null)
            {
                dataContext.Su_VanBan_Attachments.DeleteOnSubmit(obj);
                dataContext.SubmitChanges();
            }
        }
    }
}