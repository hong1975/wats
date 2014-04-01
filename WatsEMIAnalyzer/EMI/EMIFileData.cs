using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatsEMIAnalyzer.EMI
{
    [Serializable]
    public class Azimuth_DATA_TYPE
    {
        public UInt32 Azimuth_Length;
        public Double Azimuth_Start;
        public Double Azimuth_End;
        public Double Azimuth_Step;
    }

    [Serializable]
    public class Frequency_Data_Type
    {
        public UInt32 Frequency_Length;
        public string Frequency_ID;
        public Double Frequency_Start;
        public Double Frequency_End;
        public string Frequency_Comment;
    }

    [Serializable]
    public class DG_Type
    {
        public UInt32 Data_Group_Length;
        #region Data_Group_Contents
            public UInt32 DG_HI_Length;
            #region DG_HI_Contents
                public Double DG_FB_Start;
                public Double DG_FB_End;
                public Double DG_FB_Angle;
                public string DB_FB_Antenna;
                public byte DB_FB_AntennaPolarization;
                public string DB_FB_TestTime;
                public string DB_FB_Pic;
            #endregion
        #endregion

        #region DG_Item_Contents
            public UInt32 DG_Item_Length;
            public UInt32 DG_Item_Count;
            public DG_Data_Type[] DGDatas;
        #endregion
    }

    [Serializable]
    public class DG_Data_Type
    {
        public Double DG_DI_RSSI;
        public Double DG_DI_Freq;
    }

    [Serializable]
    public class EMIFileData
    {
        public override string ToString()
        {
            return "(" + Site_ID + ") " + PA_DataFile + " <" + PA_UserName + ", " + GetEmiTime(PA_TestTime) + ">";
        }

        private static string GetEmiTime(string emiTimeStr)
        {
            if (emiTimeStr.Length != 14)
                return emiTimeStr;

            return emiTimeStr.Substring(0, 4)
                + "-"
                + emiTimeStr.Substring(4, 2)
                + "-"
                + emiTimeStr.Substring(6, 2)
                + " "
                + emiTimeStr.Substring(8, 2)
                + ":"
                + emiTimeStr.Substring(10, 2)
                + ":"
                + emiTimeStr.Substring(12, 2);
        }

        [NonSerialized]
        public long UID = 0;

        //EMIDBINF
        public static byte[] FileFlag = {69, 77, 73, 68, 66, 73, 78, 70};
        public UInt32 MajorVersion;
        public UInt32 MinorVersion;
        public UInt32 HeadLength;

        /************************************************************************/
        /* Header                                                               */
        /************************************************************************/
    #region HeadContent
        public UInt32 HI_Base_Length;
        #region HI_Base_Contents
            public UInt32 Program_App_Length;
            #region Program_App_Contents
                public string PA_Name;
                public string PA_Version;
                public string PA_TestTime;
                public string PA_UserName;
                public string PA_DataFile;
            #endregion Program_App_Contents

            public UInt32 Project_Info_Length;
            #region Project_Info_Contents
                public string PI_ID;
                public byte PI_TestMode;
                public byte PI_TestPriority;
                public byte PI_AntennaPolarization;
                
                public UInt32 PI_Azimuth_Length;
                #region PI_Azimuth_Contents
                    public UInt32 Azimuth_Item_Count;
                    #region Azimuth_Data
                        public Azimuth_DATA_TYPE[] Azimuth_Data;
                    #endregion
                    
                #endregion
                
                public UInt32 PI_Freq_Length;
                #region PI_Freq_Contents
                    public UInt32 Freq_Item_Count;
                    #region Frequency_DATA
                    public Frequency_Data_Type[] Frequency_Data;
                    #endregion

                #endregion
                
                public UInt32 PI_SA_Length;
                #region PI_SA_Contents
                    public string SA_ID;
                    public Double SA_Span;
                    public Double SA_REF_LEVEL;
                    public Double SA_RBW;
                    public Double SA_VBW;
                    public string SA_Detector;
                    public string SA_Trace;
                    public UInt16 SA_Trace_Count;
                    public string SA_Filter;
                    public string SA_PreAmplify;
                    public string SA_Attenuation;
                    public byte SA_Attenuation_Value;
                    public string SA_Sweep_Mode;
                #endregion

                public UInt32 PI_Device_Length;
                #region PI_Device_Contents
                    public UInt32 Device_GPS_Length;
                    #region Device_GPS_Contents
                        public string GPS_ID;
                    #endregion

                    public UInt32 Device_Compass_Length;
                    #region Device_Compass_Contents
                        public string Compass_ID;
                    #endregion

                    public UInt32 Device_PT_Length;
                    #region Device_PT_Contents
                        public string PT_ID;
                    #endregion

                    public UInt32 Device_Antenna_Length;
                    #region Device_Antenna_Contents
                        public string Antenna_ID;
                    #endregion

                    public UInt32 Device_Cable_Length;
                    #region Device_Cable_Contents
                        public string Cable_ID;
                    #endregion

                    public UInt32 Device_LNA_Length;
                    #region Device_LNA_Contents
                        public string LNA_ID;
                    #endregion
                #endregion

                    public UInt32 PI_Site_Length;
                    #region PI_Site_Contents
                        public string Site_ID;
                        public string Site_SerialNo;
                        public string Site_Address;
                        public Double Site_Longitude;
                        public Double Site_Latitude;
                        public Double Site_Altitude;
                        public Double Site_MagDeclination;
                        public string Site_CreateTime;
                        public string Site_Comment;
                    #endregion

                public string PI_CreateTime;
                public string PI_Comment;
            #endregion
        #endregion

        public UInt32 HI_Extent_Length;
        #region HI_Extent_Content
        #endregion

    #endregion

        /************************************************************************/
        /* Data                                                                 */
        /************************************************************************/
        public UInt32 DataLength;
    #region DataContents
        public UInt32 Data_Head_Length;
        #region Data_Head_Contents
            public UInt32 DHI_DG_Count;
        #endregion

        #region Data_Body
            public DG_Type[] DataGroups;            
        #endregion

    #endregion



    }
}
