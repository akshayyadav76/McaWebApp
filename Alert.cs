using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace McaWebApp
{
    public static class AlertDialog
    {
        public static void MyAlert(System.Web.UI.Page AspxPage, string StrMessage, string StrKey)
        {
            string StrScript;
            StrScript = "<script language=javaScript>alert('" + StrMessage + "')</script>";
            if (AspxPage.IsStartupScriptRegistered(StrKey) == false)
            {
                AspxPage.RegisterStartupScript(StrKey, StrScript);
            }
        }
    }
}