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
    /// 安全会议
    /// </summary>
    public interface IDocMeetingService
    {
        ActionResult<bool> AddDocMeeting(DocMeetingNew meetingNew);
        ActionResult<bool> DelDocMeeting(Guid id);
        ActionResult<bool> EditDocMeeting(DocMeetingEdit meetingEdit);
        ActionResult<DocMeetingView> GetDocMeeting(Guid id);
        ActionResult<Pager<DocMeetingView>> GetDocMeetings(PagerQuery<DocMeetingQuery> para);
    }
}
