using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Toppine.Server.Forms
{

   public class ASDCommonInfo
   {
       public static string AutoExportPath = "";
       public static string ContractNoForReport="";
       //{
       //    get
       //    {
       //        return "TCW052";
       //    }
       //}
       public static string Contractor="";
       //{
       //    get
       //    {
       //        return "contractor";
       //    }
       //}
       public static string WebPhysicalRootPath = "";  //web服务器根目录位置，如D:\wwwroot\EMMS2007\EMMS2007_Web\EMMS.Web.Web
       //{
       //    get
       //    {
       //        return @"D:\Toppine\TCW\Project\ASD_Web\Toppine.ASD.Web.Web";
       //    }
       //}
       public static string CurrentStorageFolder = "";   //	<CurrentStoragePath>ASD_Files</CurrentStoragePath>
       //{
       //    get
       //    {
       //        return "ASD_Files";
       //    }
       //}
       
       //	<CurrentStoragePath>ASD_Files</CurrentStoragePath> 所在的路径
       //如@"D:\Toppine\TCW\Project\ASD_Web\Toppine.ASD.Web.Web\ASD_Files";
       public static string CurrentStorageFolderRootPath = "";   
       //{
       //    get
       //    {
       //        return @"D:\Toppine\TCW\Project\ASD_Web\Toppine.ASD.Web.Web\ASD_Files";
       //    }
       //}
       public static string AisExportTime = "";
       public static string GetAisExportPath="";
       //{
       //    get
       //    {
       //        return "AIS";
       //    }
       //}
       public static string ConnectionString="";
       //{
       //    get
       //    {
       //        return "connectionString";
       //    }
       //}

       public static int ReportPictureMax
       {
           get
           {
               return 360;
           }
       }
   }
}
