using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.WXHelper.Dto
{
    /// <summary>
    /// 获取用户列表返回值
    /// </summary>
    public class UserListResult
    {
        /// <summary>
        /// 总用户数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// openid列表
        /// </summary>
        public UserOpenID data { get; set; }
        /// <summary>
        /// 最后一个openid值 
        /// </summary>
        public string next_openid { get; set; }
    }
    /// <summary>
    /// 用户基本信息
    /// </summary>
    public class UserInfo
    {

        /// <summary>
        /// openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 省份 
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }


    }
    /// <summary>
    /// 批量获取用户信息参数基类
    /// </summary>
    public class UserInfoListParaBase
    {
        /// <summary>
        /// openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 语言只拉取中文
        /// </summary>
        public string lang { get { return "zh_CN"; } }


    }
    /// <summary>
    /// 批量获取用户信息参数
    /// </summary>
    public class UserInfoListPara
    {
        /// <summary>
        /// 用户openid列表
        /// </summary>
        public IEnumerable<UserInfoListParaBase> user_list { get; set; }
    }
    /// <summary>
    /// 批量获取用户基本信息返回列表
    /// </summary>
    public class UserInfoListResult
    {
        /// <summary>
        /// 用户基本信息列表
        /// </summary>
        public IEnumerable<UserInfo> user_info_list { get; set; }
        /// <summary>
        /// 下次获取openid的开始值
        /// </summary>
        public string next_openid { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int total { get; set; }
    }

    public class UserOpenID
    {
        public IEnumerable<string>openid { get; set; }

    }
    /// <summary>
    /// 设备用户备注
    /// </summary>
    public class SetUserReamrk
    {
        /// <summary>
        ///用户openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
