using ESafety.Account.Model.PARA;
using ESafety.Account.Model.View;
using ESafety.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.Account.IService
{
    /// <summary>
    /// 资质管理
    /// </summary>
    public interface IDocCertificateService
    {
        ActionResult<bool> AddDocCertificate(DocCertificateNew certificateNew);
        ActionResult<bool> DelDocCertificate(Guid id);
        ActionResult<bool> EditDocCertificate(DocCertificateEdit certificateEdit);
        ActionResult<DocCertificateView> GetDocCertificate(Guid id);
        ActionResult<Pager<DocCertificateView>> GetDocCertificates(PagerQuery<DocCertificateQuery> para);
    }
}
