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

            var bll = new Core.AccountService(work);
            var allu = new Core.Auth_UserService(work);
             
            allu.Update(new Core.Model.PARA.UserEdit
            {

                ID = Guid.Parse("3B2124B6-98AD-4B7B-B29F-E7DACB58EC18"),
                OtherEdit =true , OtherView = true

            });
            bll.AddAccount(new Core.Model.PARA.AccountInfoNew
            {
                 AccountCode="tet", AccountName ="ttt", Memo ="", ShortName ="testtt"
            });

            bll.AddAccount(new Core.Model.PARA.AccountInfoNew
            {
                 AccountCode="tests", ShortName ="tests", Memo ="", AccountName ="tttta"
            });

           
            work.Commit();
           
        }
    }
}
