using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.Core.Model.DB.Account;
using ESafety.Core.Model.PARA;
using ESafety.Web.Unity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http; 

namespace ESafety.Account.API.Controllers
{

    /// <summary>
    /// 电子附件
    /// </summary>
    [RoutePrefix("api/attachfile")]
    public class AttachFileController : ESFAPI
    {

        private IAttachFile bll = null;
        
        public AttachFileController(IAttachFile file)
        {

            bll = file;
            BusinessServices =new List<object> { file };

        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delfile/{id:Guid}")]
        [HttpGet]
        public ActionResult<bool> DelFile(Guid id)
        {
            return bll.DelFile(id);
        }
        /// <summary>
        /// 根据业务id获取电子文件
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        [Route("getfiles/{businessid:Guid}")]
        [HttpGet]
        public ActionResult<IEnumerable<Bll_AttachFile>> GetFiles(Guid businessid)
        {
            return bll.GetFiles(businessid);
        }
        /// <summary>
        /// 文件上传，返回服务器文件地址
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("uploadfile")]
        public async Task<ActionResult<string>> Post()
        {
            try
           {
                
                if (!Request.Content.IsMimeMultipartContent())
                {
                    this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = GetMultipartProvider();
                var result = await Request.Content.ReadAsMultipartAsync(provider);
                string privateUploadPath = uploadPath;
                if (!Directory.Exists(privateUploadPath + "/doc"))
                {
                    Directory.CreateDirectory(privateUploadPath + "/doc");
                }
                if (!Directory.Exists(privateUploadPath + "/img"))
                {
                    Directory.CreateDirectory(privateUploadPath + "/img");
                }

                var dbPath = "";
                foreach (var data in result.FileData)
                {

                    string originalFileName = GetDeserializedFileName(data);
                    var uploadedFileInfo = new FileInfo(data.LocalFileName);

                    //var request = new NoteItemRequest();
                    var newName = Guid.NewGuid() + Path.GetExtension(originalFileName);

                    if (".jpg.jpeg.png.gif.bmp".Contains(Path.GetExtension(originalFileName).ToLower()))
                    {

                        var targetFileName = Path.Combine(privateUploadPath + "/img/", newName);
                        File.Move(data.LocalFileName, targetFileName);


                        dbPath = "~/uploads/img/" + newName;
                    }
                    else
                    {
                        var targetFileName = Path.Combine(privateUploadPath + "/doc/", newName);
                        File.Move(data.LocalFileName, targetFileName);
                        dbPath = "~/uploads/doc/" + newName;

                    }

                }
                return new ActionResult<string>(dbPath);
            }
            catch (System.Exception ex)
            {
                return new ActionResult<string>(ex);
            }
        }

        private MultipartFormDataStreamProvider GetMultipartProvider()
        {

            var uploadFolder = "~/App_Data/Tmp/FileUploads";
            var root = System.Web.HttpContext.Current.Server.MapPath(uploadFolder);
            System.IO.Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);

        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        private string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }

    }
}
