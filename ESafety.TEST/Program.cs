using ESafety.Core.Model.DB.Platform;
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

            var db = work.Repository<Auth_Role>();

            db.Add(new Auth_Role
            {
                ID = Guid.NewGuid(),
                RoleName = "role1"
            });

           
            work.Commit();
           
        }
    }
}
