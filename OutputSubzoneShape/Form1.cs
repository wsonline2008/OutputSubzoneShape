using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
//using Toppine.Common;
using ESRI.MapObjects2.Core;
//using Toppine.Server.Logic;
using Toppine.Server.Forms;
using System.Xml;
//using Toppine.ASD.Web.Entity;
using System.Data.SqlClient;
using DotNet.Utilities;
//using Toppine.Server.Logic.ErrorLog;


namespace OutputSubzoneShape
{
    public partial class Form1 : Form
    {
        DataTable dt = null;
        string B1KImportPath = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Build929("", "B1K_BLDGPOLY_dissolve", @"H:\workspace\929\B1K_BLDGPOLY_dissolve_20191206");
            return;
            //ASDDatabaseDataContext db = ASDDatabaseFactory.GetDataContext();
            //db.D_Inventory.
            try
            {
                ConnectSql();
                MessageBox.Show("" + dt.Rows.Count);
                //MessageBox.Show(dt.Rows[0]["MasterZoneID"].ToString());
                //MessageBox.Show(dt.Rows[0]["EqualToMasterZone"].ToString());
                //return;
                if (dt.Rows.Count > 0)
                {
                    if (B1KImportPath == "")
                    {
                        SetLog("Error:B1KImportPath is empty!");
                    }
                    else
                    {
                        //Build929AIMS1("", "B1K_BLDGPOLY", B1KImportPath);
                        if (radioOne.Checked)
                        {
                            Build929("", "SLOPE_MR_POLY_Dissolve", @"H:\workspace\929\slope_mr_poly_20151028");
                        }
                        else
                        {
                            Build929AIMS1("", "SLOPE_MR_POLY_Dissolve", @"H:\workspace\929\slope_mr_poly_20151028");
                        }
                    }
                }
                else
                {
                    SetLog("Error:No Data");
                }
                //MessageBox.Show("" + dt.Rows.Count);
                //MessageBox.Show(dt.Rows[0][1].ToString());
            }
            catch (Exception er)
            {
                //Console.Write(er.ToString());
                SetLog("Error:" + er.ToString());
            }
        }
        private void ConnectSql()
        {
            //if (dt.Rows.Count > 0)
            //{
            //    dt.Clear();
            //}
            try
            {
                //SqlConnection sqlconn = new SqlConnection(ASDCommonInfo.ConnectionString);
                //sqlconn.Open();
                //SqlCommand sqlCmd = new SqlCommand();   //创建命令对象
                //sqlCmd.CommandText = "SELECT (buildid),SIStatus FROM [TCB929].[dbo].[D_Inventory] where SIStatus='Y' and DistrictCode IS NOT NULL order by BuildId";   //写SQL语句
                //sqlCmd.Connection = sqlconn;             //指定连接对象，即上面创建的

                //SqlDataAdapter dbAdapter = new SqlDataAdapter(sqlCmd); //注意与上面的区分开
                //DataSet ds = new DataSet(); //创建数据集对象
                //dbAdapter.Fill(ds); //用返回的结果集填充数据集，这个数据集可以被能操作数据的控件DataBind

                //dt = ds.Tables[0];
                //sqlconn.Close();

                DataSet ds = new DataSet();
                string UpdateOn = "1900-01-01";
                //SqlParameter[] paraNull = { new SqlParameter("@UpdateOn", UpdateOn) };
                SqlParameter[] paraNull = { new SqlParameter("@SlopeID", "") };
                ds = SqlHelper.ExecuteDataSetProducts("P_TDI002_MasterZone", paraNull);
                dt = ds.Tables[0];
                //SetLog("ok:sqlconn");
                //MessageBox.Show("" + dt.Rows.Count);
            }
            catch (Exception er)
            {
                //Console.Write(er.ToString());
                SetLog("Error:" + er.ToString());
            }
        }
        private void Build929AIMS1(string strReadCsvPath, string exportName, string exportPath)
        {
            #region RoadLighting
            string strID = "";
            string strZoneID = "";
            long strNO = 0;
            try
            {
                Hashtable hasInfo = new Hashtable();
                Hashtable hasInfoRe = new Hashtable();
                
                //string strPath1 = GetExportVirthPath("TDI002", "in") + "shp" + @"\";
                string strPath1 = @"F:\shp\Subzone20171204" + @"\";
                string strPath2 = "";
                lState.Text = "Get Export Path!";
                #region 读取输出shp，加载到数据集与图层

                MapLayer outLayer = null;
                ESRI.MapObjects2.Core.GeoDataset outDataset = null;

                ESRI.MapObjects2.Core.DataConnection outConnection = new DataConnection();
                outConnection.Database = exportPath;
                lState.Text = "Connect shp!";
                if (outConnection.Connect())
                {
                    outLayer = new MapLayer();
                    outDataset = outConnection.FindGeoDataset(exportName);

                    if (outDataset != null)
                    {
                        outLayer.GeoDataset = outDataset;
                    }
                }

                if (!outLayer.Valid)
                {
                    MessageBox.Show(exportName + " is no exist!", "error");
                    return;
                }
                #endregion
                lState.Text = "Check Directory!";
                #region 读取内存数据，AddNew到输出shp
                if (!System.IO.Directory.Exists(strPath1))
                {
                    // 目录不存在，建立目录
                    System.IO.Directory.CreateDirectory(strPath1);
                }
                lState.Text = "Start Export!";

                XmlElement theMasterZoneID = null, theSHPShapeFilename = null, theSHXShapeFilename = null, theDBFShapeFilename = null, root = null;
                
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load("20150917200500_ArchSD_Masterzone.xml");
                root = xmldoc.DocumentElement;
                //outLayer.FilterExpression = "RefSlope='10NW-B/C16'";
                //for (int i = 0; i < List.Count; i++)
                //MessageBox.Show(dt.Rows.Count + "!", "error");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    strID = dt.Rows[i]["BuildId"].ToString();
                    lState.Text = strID;
                    
                    strNO = i;
                    if (radioOne.Checked)
                    {
                        if (textOne.Text != strID)
                        {
                            continue;
                        }
                    }
                    textOne.Text = strID;

                    //strPath2 = strPath1;// +strID.Replace("/", "_") + @"\";
                    strPath2 = strPath1 +strID.Replace("/", "_") + @"\";
                    if (!System.IO.Directory.Exists(strPath2))
                    {
                        // 目录不存在，建立目录
                        System.IO.Directory.CreateDirectory(strPath2);
                    }

                    if (strID==@"11NE-A/C879")
                    {
                        string text1 = "1";
                    }

                    strZoneID = dt.Rows[i]["MasterZoneID"].ToString();
                    //XmlNode rootMasterZone = root.SelectSingleNode("/MasterZones/MasterZone[MasterZoneID='" + strZoneID + "']");

                    //XmlNode Subzones = rootMasterZone.SelectSingleNode("Subzones");
                    //XmlNode Subzone = Subzones.SelectSingleNode("Subzone");
                    //XmlNode Polygon = Subzone.SelectSingleNode("Polygon");

                    //String SHPShapeFilename = (Polygon.SelectSingleNode("SHPShapeFilename")).InnerText;
                    //String SHXShapeFilename = (Polygon.SelectSingleNode("SHXShapeFilename")).InnerText;
                    //String DBFShapeFilename = (Polygon.SelectSingleNode("DBFShapeFilename")).InnerText;
                    //SHPShapeFilename = SHPShapeFilename.Replace("shp/TCB929/", "");
                    //SHXShapeFilename = SHXShapeFilename.Replace("shp/TCB929/", "");
                    //DBFShapeFilename = DBFShapeFilename.Replace("shp/TCB929/", "");

                    String SHPShapeFilename = "Subzone_Shape.shp";
                    String SHXShapeFilename = "Subzone_Shape.shx";
                    String DBFShapeFilename = "Subzone_Shape.dbf";



                    //if (SHPShapeFilename.IndexOf("Subzone_Shape_") < 0)
                    //{
                    //    richText1.AppendText("xml lost:" + strZoneID + "," + SHPShapeFilename + "\n");
                    //    continue;
                    //}

                    String sourcePath = Application.StartupPath + @"\template\temp.shp";
                    String targetPath = strPath2 + SHPShapeFilename;// @"Subzone_Shape_" + (i + 1) + ".shp";
                    //String targetPath = exportPath + @"\Subzone_Shape\" + List[i].ToString().Replace("/", "_") + ".shp";
                    bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
                    System.IO.File.Copy(sourcePath, targetPath, isrewrite);

                    sourcePath = Application.StartupPath + @"\template\temp.shx";
                    targetPath = strPath2 + SHXShapeFilename;// @"Subzone_Shape_" + (i + 1) + ".shx";
                    System.IO.File.Copy(sourcePath, targetPath, isrewrite);

                    sourcePath = Application.StartupPath + @"\template\temp.dbf";
                    targetPath = strPath2 + DBFShapeFilename;// @"Subzone_Shape_" + (i + 1) + ".dbf";
                    System.IO.File.Copy(sourcePath, targetPath, isrewrite);

                    //sourcePath = Application.StartupPath + @"\template\temp.prj";
                    //targetPath = strPath2 +  @"Subzone_Shape_" + (i + 1) + ".prj";
                    //System.IO.File.Copy(sourcePath, targetPath, isrewrite);

                    MapLayer cLayer = null;
                    ESRI.MapObjects2.Core.GeoDataset cDataset = null;

                    ESRI.MapObjects2.Core.DataConnection cConnection = new DataConnection();
                    cConnection.Database = strPath2;

                    if (cConnection.Connect())
                    {
                        cLayer = new MapLayer();
                        //cDataset = cConnection.FindGeoDataset(@"Subzone_Shape_" + (i + 1));
                        cDataset = cConnection.FindGeoDataset(SHPShapeFilename.Replace(".shp", ""));
                        if (cDataset != null)
                        {
                            cLayer.GeoDataset = cDataset;
                        }
                    }

                    if (!cLayer.Valid)
                    {
                        MessageBox.Show(exportName + " is no exist!", "error");
                        return;
                    }
                    Recordset cRecs = cLayer.Records;
                    if (!cRecs.Updatable)
                    {
                        MessageBox.Show(exportName + " is not editable!", "error");
                        return;
                    }

                    Recordset rs = outLayer.SearchExpression("\"SLOPE_NO\" ='" + strID + "'");
                    string BuildingID = "";
                    string SIStatus = "";
                    rs.MoveFirst();
                    if (!rs.EOF)
                    {

                        while (!rs.EOF)
                        {
                            //BuildingID = rs.Fields.Item("BuildingID").ValueAsString;
                            //string RefSlope = rs.Fields.Item("RefSlope").ValueAsString;
                            //string SubDivNo = rs.Fields.Item("SubDivNo").ValueAsString;
                            //SIStatus = dt.Rows[i]["EqualToMasterZone"].ToString();//hasInfo[BuildingID] as string;

                            cRecs.MoveFirst();
                            cRecs.AddNew();
                            object geometry = rs.Fields.Item("Shape").Value;
                            cRecs.Fields.Item("Shape").Value = geometry;

                            //if (long.TryParse(sss[1], out y))
                            //    outRecs.Fields.Item("INV_GID").Value = (long)y;

                            //SetLog("SubZoneID," + RefSlope);
                            SetLog("SubZoneID," + strID);

                            cRecs.Fields.Item("SubZoneID").Value = strID;// RefSlope;
                            //cRecs.Fields.Item("SubDivNo").Value = SubDivNo;
                            //cRecs.Fields.Item("Status").Value = SIStatus;

                            cRecs.Update();

                            rs.MoveNext();
                        }
                        cRecs.StopEditing();
                        
                    }
                    else
                    {
                        //BuildingID = rs.Fields.Item("BuildingID").ValueAsString;
                        richText1.AppendText(strZoneID + "," + SHPShapeFilename + "\n");
                        //日志记录
                        SetLog("No Record BuildingID:" + strZoneID + "," + SHPShapeFilename);

                    }
                }

                //true追加内容，False为覆盖
                //StreamWriter sw = new StreamWriter(strReadCsvPath.Replace(".csv", ".sql"), true);
                //string txtOutPut = "";

                //lisError.Items.Add("ok!" + exportName + "|" + IDCnt + "|" + hasInfo.Count + "|" + IDReCnt);
                //System.DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss：ffff");
                #endregion
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                SetLog("Error:" + ex.Message.ToString() + " @" + strID + "|" );
            }
            #endregion
        }
        private void Build929(string strReadCsvPath, string exportName, string exportPath)
        {
            #region RoadLighting
            string strID = "";
            string strZoneID = "";
            long strNO = 0;
            try
            {
                Hashtable hasInfo = new Hashtable();
                Hashtable hasInfoRe = new Hashtable();

                //string strPath1 = GetExportVirthPath("TDI002", "in") + "shp" + @"\";
                string strPath1 = @"F:\shp\Subzone" + exportPath.Substring(exportPath.Length-8) + @"\";
                string strPath2 = "";
                lState.Text = "Get Export Path!";
                #region 读取输出shp，加载到数据集与图层

                MapLayer outLayer = null;
                ESRI.MapObjects2.Core.GeoDataset outDataset = null;

                ESRI.MapObjects2.Core.DataConnection outConnection = new DataConnection();
                outConnection.Database = exportPath;
                lState.Text = "Connect shp!";
                if (outConnection.Connect())
                {
                    outLayer = new MapLayer();
                    outDataset = outConnection.FindGeoDataset(exportName);

                    if (outDataset != null)
                    {
                        outLayer.GeoDataset = outDataset;
                    }
                }
                if (!outLayer.Valid)
                {
                    MessageBox.Show(exportName + " is no exist!", "error");
                    return;
                }
                #endregion
                lState.Text = "Check Directory!";
                #region 读取内存数据，AddNew到输出shp
                if (!System.IO.Directory.Exists(strPath1))
                {
                    // 目录不存在，建立目录
                    System.IO.Directory.CreateDirectory(strPath1);
                }
                lState.Text = "Start Export!";

                XmlElement theMasterZoneID = null, theSHPShapeFilename = null, theSHXShapeFilename = null, theDBFShapeFilename = null, root = null;

                //XmlDocument xmldoc = new XmlDocument();
                //xmldoc.Load("20150917200500_ArchSD_Masterzone.xml");
                //root = xmldoc.DocumentElement;
                {
                    strID = textOne.Text;
                    lState.Text = strID;

                    //strPath2 = strPath1;// +strID.Replace("/", "_") + @"\";
                    strPath2 = strPath1 + strID.Replace("/", "_") + @"\";
                    if (!System.IO.Directory.Exists(strPath2))
                    {
                        // 目录不存在，建立目录
                        System.IO.Directory.CreateDirectory(strPath2);
                    }

                    String SHPShapeFilename = "Subzone_Shape.shp";
                    String SHXShapeFilename = "Subzone_Shape.shx";
                    String DBFShapeFilename = "Subzone_Shape.dbf";

                    String sourcePath = Application.StartupPath + @"\template\temp.shp";
                    String targetPath = strPath2 + SHPShapeFilename;// @"Subzone_Shape_" + (i + 1) + ".shp";
                    //String targetPath = exportPath + @"\Subzone_Shape\" + List[i].ToString().Replace("/", "_") + ".shp";
                    bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
                    System.IO.File.Copy(sourcePath, targetPath, isrewrite);

                    sourcePath = Application.StartupPath + @"\template\temp.shx";
                    targetPath = strPath2 + SHXShapeFilename;// @"Subzone_Shape_" + (i + 1) + ".shx";
                    System.IO.File.Copy(sourcePath, targetPath, isrewrite);

                    sourcePath = Application.StartupPath + @"\template\temp.dbf";
                    targetPath = strPath2 + DBFShapeFilename;// @"Subzone_Shape_" + (i + 1) + ".dbf";
                    System.IO.File.Copy(sourcePath, targetPath, isrewrite);

                    MapLayer cLayer = null;
                    ESRI.MapObjects2.Core.GeoDataset cDataset = null;

                    ESRI.MapObjects2.Core.DataConnection cConnection = new DataConnection();
                    cConnection.Database = strPath2;

                    if (cConnection.Connect())
                    {
                        cLayer = new MapLayer();
                        //cDataset = cConnection.FindGeoDataset(@"Subzone_Shape_" + (i + 1));
                        cDataset = cConnection.FindGeoDataset(SHPShapeFilename.Replace(".shp", ""));
                        if (cDataset != null)
                        {
                            cLayer.GeoDataset = cDataset;
                        }
                    }

                    if (!cLayer.Valid)
                    {
                        MessageBox.Show(exportName + " is no exist!", "error");
                        return;
                    }
                    Recordset cRecs = cLayer.Records;
                    if (!cRecs.Updatable)
                    {
                        MessageBox.Show(exportName + " is not editable!", "error");
                        return;
                    }

                    Recordset rs = outLayer.SearchExpression("\"RefSlope\" ='" + strID + "'");
                    string BuildingID = "";
                    string SIStatus = "";
                    rs.MoveFirst();
                    if (!rs.EOF)
                    {
                        while (!rs.EOF)
                        {
                            cRecs.MoveFirst();
                            cRecs.AddNew();
                            object geometry = rs.Fields.Item("Shape").Value;
                            cRecs.Fields.Item("Shape").Value = geometry;
                            SetLog("SubZoneID," + strID);
                            cRecs.Fields.Item("SubZoneID").Value = strID;// RefSlope;
                            cRecs.Update();
                            rs.MoveNext();
                        }
                        cRecs.StopEditing();
                    }
                    else
                    {
                        //BuildingID = rs.Fields.Item("BuildingID").ValueAsString;
                        richText1.AppendText(strZoneID + "," + SHPShapeFilename + "\n");
                        //日志记录
                        SetLog("No Record BuildingID:" + strZoneID + "," + SHPShapeFilename);
                    }
                }
                #endregion
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                SetLog("Error:" + ex.Message.ToString() + " @" + strID + "|");
            }
            #endregion
        }
        public string GetExportVirthPath(string str1, string str2)
        {
            //设置导出路径  
            return ASDCommonInfo.CurrentStorageFolderRootPath + @"\" + ASDCommonInfo.CurrentStorageFolder + @"\" + ASDCommonInfo.GetAisExportPath +
             @"\" + str1 + @"\" + str2 + @"\";
        }
        string initSetPath = System.Windows.Forms.Application.StartupPath + @"\Config\InitSet.xml";
        private void Form1_Load(object sender, EventArgs e)
        {
            //richText1.AppendText(initSetPath + "*");
            XmlDocument doc = new XmlDocument();
            doc.Load(initSetPath);
            ASDCommonInfo.ContractNoForReport = doc.GetElementsByTagName("ContractNoForReport")[0].InnerText;
            ASDCommonInfo.Contractor = doc.GetElementsByTagName("Contractor")[0].InnerText;

            ASDCommonInfo.WebPhysicalRootPath = doc.GetElementsByTagName("WebPhysicalRootPath")[0].InnerText;
            ASDCommonInfo.CurrentStorageFolder = doc.GetElementsByTagName("CurrentStorageFolder")[0].InnerText;
            ASDCommonInfo.CurrentStorageFolderRootPath = doc.GetElementsByTagName("CurrentStorageFolderRootPath")[0].InnerText;
            ASDCommonInfo.GetAisExportPath = doc.GetElementsByTagName("GetAisExportPath")[0].InnerText;
            ASDCommonInfo.AisExportTime = doc.GetElementsByTagName("AISExportTime")[0].InnerText;

            ASDCommonInfo.ConnectionString = doc.GetElementsByTagName("ConnectionString")[0].InnerText;
            B1KImportPath = doc.GetElementsByTagName("B1KImportPath")[0].InnerText;
            //richText1.AppendText(ASDCommonInfo.ConnectionString+"*");
            
            //ASDCommonInfo.AutoExportPath=txtPath.Text = doc.GetElementsByTagName("AutoExportPath")[0].InnerText;
            //Toppine.Server.Logic.ErrorLog.LogPath = Application.StartupPath + @"\Log.txt";
            //Toppine.ASD.Web.Entity.ASDDatabaseFactory.ConectionString = ASDCommonInfo.ConnectionString;
            SqlHelper.connectionString = ASDCommonInfo.ConnectionString;
            //MessageBox.Show(SqlHelper.connectionString);
            //SqlConnection sqlconn = new SqlConnection(ASDCommonInfo.ConnectionString);
            //sqlconn.Open();
            //SqlCommand sqlCmd = new SqlCommand();   //创建命令对象
            //sqlCmd.CommandText = "SELECT (buildid),SIStatus FROM [TCB929].[dbo].[D_Inventory] where SIStatus='Y' and DistrictCode IS NOT NULL order by BuildId";   //写SQL语句
            //sqlCmd.Connection = sqlconn;             //指定连接对象，即上面创建的

            //SqlDataAdapter dbAdapter = new SqlDataAdapter(sqlCmd); //注意与上面的区分开
            //DataSet ds = new DataSet(); //创建数据集对象
            //dbAdapter.Fill(ds); //用返回的结果集填充数据集，这个数据集可以被能操作数据的控件DataBind
            //DataTable dt;
            //dt = ds.Tables[0];


            radioAll.Checked = true;
        }

        private void SetLog(string msg)
        {
            try
            {
                //true追加内容，False为覆盖
                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\Log.txt", true);
                string txtOutPut = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + " " + msg;
                sw.WriteLine(txtOutPut);
                sw.Flush();
                sw.Close();
            }
            catch (Exception er)
            {
                //Console.Write(er.ToString());
                SetLog("Error:" + er.ToString());
            }
        }

        private void radioAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radioAll.Checked)
            {
                textOne.Enabled = false;
            }
        }

        private void radioOne_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOne.Checked)
            {
                textOne.Enabled = true;
            }
        }



    }
}
