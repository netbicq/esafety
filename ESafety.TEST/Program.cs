using ESafety.Account.Model.PARA;
using ESafety.Account.Service;
using ESafety.Core;
using ESafety.Core.Model;
using ESafety.Core.Model.DB;
using ESafety.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESafety.TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            runTest();
        }

        private static void runTest ()
        {
            

            ORM.IUnitwork work = new ORM.Unitwork(new ESFdb());

            var bll = new TreeService(work);

            var re = bll.GetTree<Basic_Org, OrgTree>(Guid.Empty);

            string rtt = "";

            //var bll = new Core.AccountService(work);
            //var allu = new Core.Auth_UserService(work);
             
            //allu.Update(new Core.Model.PARA.UserEdit
            //{

            //    ID = Guid.Parse("3B2124B6-98AD-4B7B-B29F-E7DACB58EC18"),
            //    OtherEdit =true , OtherView = true

            //});
            //bll.AddAccount(new Core.Model.PARA.AccountInfoNew
            //{
            //     AccountCode="tet", AccountName ="ttt", Memo ="", ShortName ="testtt"
            //});

            //bll.AddAccount(new Core.Model.PARA.AccountInfoNew
            //{
            //     AccountCode="tests", ShortName ="tests", Memo ="", AccountName ="tttta"
            //});

            //var allu = new Account.Service.PostManageService(work);
            //var s=allu.GetPostsPage(new Core.Model.PagerQuery<PostQuery> {  PageSize=1,PageIndex=1,Query=new PostQuery { Code="1",Name=""} });


            work.Commit();
           
        }
    }
}
