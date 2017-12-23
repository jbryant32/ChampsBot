using PhantomJSChamps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhantomJSChamps
{
    //handle disk loading
    interface IBotIO
    {
        string[] LoadProxyList();
        UserInfo LoadUserInfo();
    }
}
