using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace WatsEMIAnalyzer.EMI
{
    public class EMIFileParser
    {
        private Form mAttachedForm;
        public delegate void parseSuccessfully(string emiName, EMIFileData emiFileData, object context);
        public event parseSuccessfully onParseSuccessfully;
        public delegate void parseFailed(string emiName, string errorMessage, object context);
        public event parseFailed onParseFailed;

        public Form AttachedForm
        {
            set { mAttachedForm = value; }
        }

        public EMIFileParser()
        {
        }

        private string getAnsiString(BinaryReader br)
        {
            UInt32 len = br.ReadUInt32();
            if (len == 0)
                return null;

            byte[] strBytes = br.ReadBytes((int)len);
            return Encoding.ASCII.GetString(strBytes);
        }

        private string getAnsiString(BinaryReader br, int n)
        {
            byte[] strBytes = br.ReadBytes(n);
            return Encoding.ASCII.GetString(strBytes);
        }

        private string getUnicodeString(BinaryReader br)
        {
            UInt32 len = br.ReadUInt32();
            if (len == 0)
                return null;

            byte[] strBytes = br.ReadBytes((int)len * 2);
            return Encoding.Unicode.GetString(strBytes);
        }

        private string getUnicodeString(BinaryReader br, int n)
        {
            byte[] strBytes = br.ReadBytes((int)n * 2);

            return Encoding.Unicode.GetString(strBytes);
        }

        public EMIFileData ParseSync(string emiFileName)
        {
            using (FileStream fs = new FileStream(emiFileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader emiReader = new BinaryReader(fs))
                {
                    EMIFileData emiFile = new EMIFileData();
                    try
                    {
                        byte[] FileFlag = emiReader.ReadBytes(8);
                        if (!EMIFileData.FileFlag.SequenceEqual(FileFlag))
                            return null;

                        emiFile.MajorVersion = emiReader.ReadUInt32();
                        emiFile.MinorVersion = emiReader.ReadUInt32();
                        emiFile.HeadLength = emiReader.ReadUInt32();
                        if (emiFile.HeadLength == 0)
                            return null;

                        byte[] HeadContent = emiReader.ReadBytes((int)emiFile.HeadLength);
                        MemoryStream headContentMs = new MemoryStream(HeadContent);
                        using (BinaryReader headContentReader = new BinaryReader(headContentMs))
                        {
                            emiFile.HI_Base_Length = headContentReader.ReadUInt32();
                            if (emiFile.HI_Base_Length == 0)
                                return null;

                            byte[] HI_Base_Contents = headContentReader.ReadBytes((int)emiFile.HI_Base_Length);
                            MemoryStream hiBaseContentsMs = new MemoryStream(HI_Base_Contents);
                            using (BinaryReader hiBaseContentsReader = new BinaryReader(hiBaseContentsMs))
                            {
                                /************************************************************************/
                                /* Program App                                                          */
                                /************************************************************************/
                                emiFile.Program_App_Length = hiBaseContentsReader.ReadUInt32();
                                if (emiFile.Program_App_Length == 0)
                                    return null;

                                byte[] Program_App_Contents = hiBaseContentsReader.ReadBytes((int)emiFile.Program_App_Length);
                                MemoryStream programAppContentsMs = new MemoryStream(Program_App_Contents);
                                using (BinaryReader programAppContentsReader = new BinaryReader(programAppContentsMs))
                                {
                                    emiFile.PA_Name = getUnicodeString(programAppContentsReader);
                                    if (emiFile.PA_Name == null)
                                        return null;

                                    emiFile.PA_Version = getUnicodeString(programAppContentsReader);
                                    if (emiFile.PA_Version == null)
                                        return null;

                                    emiFile.PA_TestTime = getUnicodeString(programAppContentsReader, 14);
                                    if (emiFile.PA_TestTime == null)
                                        return null;

                                    emiFile.PA_UserName = getUnicodeString(programAppContentsReader);
                                    if (emiFile.PA_UserName == null)
                                        return null;

                                    emiFile.PA_DataFile = getUnicodeString(programAppContentsReader);
                                    if (emiFile.PA_DataFile == null)
                                        return null;
                                }

                                /************************************************************************/
                                /* Project INFO                                                         */
                                /************************************************************************/
                                emiFile.Project_Info_Length = hiBaseContentsReader.ReadUInt32();
                                if (emiFile.Project_Info_Length == 0)
                                    return null;

                                byte[] Project_Info_Contents = hiBaseContentsReader.ReadBytes((int)emiFile.Project_Info_Length);
                                MemoryStream projectInfoContentsMs = new MemoryStream(Project_Info_Contents);
                                using (BinaryReader projectInfoContentsReader = new BinaryReader(projectInfoContentsMs))
                                {
                                    emiFile.PI_ID = getUnicodeString(projectInfoContentsReader);
                                    if (emiFile.PI_ID == null)
                                        return null;

                                    emiFile.PI_TestMode = projectInfoContentsReader.ReadByte();
                                    if (emiFile.PI_TestMode != 0 && emiFile.PI_TestMode != 1)
                                        return null;

                                    emiFile.PI_TestPriority = projectInfoContentsReader.ReadByte();
                                    if (emiFile.PI_TestPriority != 0 && emiFile.PI_TestPriority != 1)
                                        return null;

                                    emiFile.PI_AntennaPolarization = projectInfoContentsReader.ReadByte();
                                    if (emiFile.PI_AntennaPolarization != 0 && emiFile.PI_AntennaPolarization != 1
                                        && emiFile.PI_AntennaPolarization != 2 && emiFile.PI_AntennaPolarization != 3)
                                        return null;


                                    /************************************************************************/
                                    /* Azimuth                                                              */
                                    /************************************************************************/
                                    emiFile.PI_Azimuth_Length = projectInfoContentsReader.ReadUInt32();
                                    if (emiFile.PI_Azimuth_Length == 0)
                                        return null;

                                    byte[] PI_Azimuth_Contents = projectInfoContentsReader.ReadBytes((int)emiFile.PI_Azimuth_Length);
                                    MemoryStream pIAzimuthContentsMs = new MemoryStream(PI_Azimuth_Contents);
                                    using (BinaryReader pIAzimuthContentsMsReader = new BinaryReader(pIAzimuthContentsMs))
                                    {
                                        emiFile.Azimuth_Item_Count = pIAzimuthContentsMsReader.ReadUInt32();
                                        if (emiFile.Azimuth_Item_Count > 0)
                                            emiFile.Azimuth_Data = new Azimuth_DATA_TYPE[emiFile.Azimuth_Item_Count];

                                        for (int i = 0; i < emiFile.Azimuth_Item_Count; i++)
                                        {
                                            Azimuth_DATA_TYPE data = new Azimuth_DATA_TYPE();
                                            data.Azimuth_Length = pIAzimuthContentsMsReader.ReadUInt32();
                                            data.Azimuth_Start = pIAzimuthContentsMsReader.ReadDouble();
                                            data.Azimuth_End = pIAzimuthContentsMsReader.ReadDouble();
                                            data.Azimuth_Step = pIAzimuthContentsMsReader.ReadDouble();
                                            emiFile.Azimuth_Data[i] = data;
                                        }
                                    }

                                    /************************************************************************/
                                    /* Frequency                                                            */
                                    /************************************************************************/
                                    emiFile.PI_Freq_Length = projectInfoContentsReader.ReadUInt32();
                                    if (emiFile.PI_Freq_Length == 0)
                                        return null;

                                    byte[] PI_Freq_Contents = projectInfoContentsReader.ReadBytes((int)emiFile.PI_Freq_Length);
                                    MemoryStream pIFreqContentsMs = new MemoryStream(PI_Freq_Contents);
                                    using (BinaryReader pIFreqContentsReader = new BinaryReader(pIFreqContentsMs))
                                    {
                                        emiFile.Freq_Item_Count = pIFreqContentsReader.ReadUInt32();
                                        if (emiFile.Freq_Item_Count > 0)
                                            emiFile.Frequency_Data = new Frequency_Data_Type[emiFile.Freq_Item_Count];
                                        for (int i = 0; i < emiFile.Freq_Item_Count; i++)
                                        {
                                            Frequency_Data_Type data = new Frequency_Data_Type();
                                            data.Frequency_Length = pIFreqContentsReader.ReadUInt32();
                                            data.Frequency_ID = getUnicodeString(pIFreqContentsReader);
                                            data.Frequency_Start = pIFreqContentsReader.ReadDouble();
                                            data.Frequency_End = pIFreqContentsReader.ReadDouble();
                                            data.Frequency_Comment = getUnicodeString(pIFreqContentsReader);
                                            emiFile.Frequency_Data[i] = data;
                                        }
                                    }

                                    /************************************************************************/
                                    /* SA                                                                   */
                                    /************************************************************************/
                                    emiFile.PI_SA_Length = projectInfoContentsReader.ReadUInt32();
                                    if (emiFile.PI_SA_Length == 0)
                                        return null;

                                    byte[] PI_SA_Contents = projectInfoContentsReader.ReadBytes((int)emiFile.PI_SA_Length);
                                    MemoryStream pISAContentsMs = new MemoryStream(PI_SA_Contents);
                                    using (BinaryReader pISAContentsReader = new BinaryReader(pISAContentsMs))
                                    {
                                        emiFile.SA_ID = getUnicodeString(pISAContentsReader);
                                        emiFile.SA_Span = pISAContentsReader.ReadDouble();
                                        emiFile.SA_REF_LEVEL = pISAContentsReader.ReadDouble();
                                        emiFile.SA_RBW = pISAContentsReader.ReadDouble();
                                        emiFile.SA_VBW = pISAContentsReader.ReadDouble();
                                        emiFile.SA_Detector = getUnicodeString(pISAContentsReader);
                                        if (!"Positive".Equals(emiFile.SA_Detector) && !"RMS".Equals(emiFile.SA_Detector)
                                            && !"Negative".Equals(emiFile.SA_Detector) && !"Sample".Equals(emiFile.SA_Detector)
                                            && !"None".Equals(emiFile.SA_Detector))
                                            return null;

                                        emiFile.SA_Trace = getUnicodeString(pISAContentsReader);
                                        if (!"Normal".Equals(emiFile.SA_Trace) && !"Average".Equals(emiFile.SA_Trace)
                                            && !"Max Hold".Equals(emiFile.SA_Trace) && !"Min Hold".Equals(emiFile.SA_Trace)
                                            && !"None".Equals(emiFile.SA_Trace))
                                            return null;

                                        emiFile.SA_Trace_Count = pISAContentsReader.ReadUInt16();
                                        emiFile.SA_Filter = getUnicodeString(pISAContentsReader);
                                        emiFile.SA_PreAmplify = getUnicodeString(pISAContentsReader);
                                        if (!"ON".Equals(emiFile.SA_PreAmplify) && !"OFF".Equals(emiFile.SA_PreAmplify)
                                            && !"None".Equals(emiFile.SA_PreAmplify))
                                            return null;

                                        emiFile.SA_Attenuation = getUnicodeString(pISAContentsReader);
                                        if (!"Auto".Equals(emiFile.SA_Attenuation) && !"Assign".Equals(emiFile.SA_Attenuation))
                                            return null;

                                        emiFile.SA_Attenuation_Value = pISAContentsReader.ReadByte();
                                        emiFile.SA_Sweep_Mode = getUnicodeString(pISAContentsReader);
                                        if (!"None".Equals(emiFile.SA_Sweep_Mode) && !"Fast".Equals(emiFile.SA_Sweep_Mode)
                                            && !"NO FFT".Equals(emiFile.SA_Sweep_Mode) && !"Performance".Equals(emiFile.SA_Sweep_Mode))
                                            return null;
                                    }

                                    /************************************************************************/
                                    /* Device                                                               */
                                    /************************************************************************/
                                    emiFile.PI_Device_Length = projectInfoContentsReader.ReadUInt32();
                                    if (emiFile.PI_Device_Length == 0)
                                        return null;

                                    byte[] PI_Device_Contents = projectInfoContentsReader.ReadBytes((int)emiFile.PI_Device_Length);
                                    MemoryStream pIDeviceContentsMs = new MemoryStream(PI_Device_Contents);
                                    using (BinaryReader pIDeviceContentsReader = new BinaryReader(pIDeviceContentsMs))
                                    {
                                        emiFile.Device_GPS_Length = pIDeviceContentsReader.ReadUInt32();
                                        if (emiFile.Device_GPS_Length == 0)
                                            return null;

                                        emiFile.GPS_ID = getUnicodeString(pIDeviceContentsReader);

                                        emiFile.Device_Compass_Length = pIDeviceContentsReader.ReadUInt32();
                                        if (emiFile.Device_Compass_Length == 0)
                                            return null;

                                        emiFile.Compass_ID = getUnicodeString(pIDeviceContentsReader);

                                        emiFile.Device_PT_Length = pIDeviceContentsReader.ReadUInt32();
                                        if (emiFile.Device_PT_Length == 0)
                                            return null;

                                        emiFile.PT_ID = getUnicodeString(pIDeviceContentsReader);

                                        emiFile.Device_Antenna_Length = pIDeviceContentsReader.ReadUInt32();
                                        if (emiFile.Device_Antenna_Length == 0)
                                            return null;

                                        emiFile.Antenna_ID = getUnicodeString(pIDeviceContentsReader);

                                        emiFile.Device_Cable_Length = pIDeviceContentsReader.ReadUInt32();
                                        if (emiFile.Device_Cable_Length == 0)
                                            return null;

                                        emiFile.Cable_ID = getUnicodeString(pIDeviceContentsReader);

                                        emiFile.Device_LNA_Length = pIDeviceContentsReader.ReadUInt32();
                                        if (emiFile.Device_LNA_Length == 0)
                                            return null;

                                        emiFile.LNA_ID = getUnicodeString(pIDeviceContentsReader);
                                    }

                                    /************************************************************************/
                                    /* Site                                                                 */
                                    /************************************************************************/
                                    emiFile.PI_Site_Length = projectInfoContentsReader.ReadUInt32();
                                    if (emiFile.PI_Site_Length == 0)
                                        return null;

                                    byte[] PI_Site_Contents = projectInfoContentsReader.ReadBytes((int)emiFile.PI_Device_Length);
                                    MemoryStream pISiteContentsMs = new MemoryStream(PI_Site_Contents);
                                    using (BinaryReader pISiteContentsReader = new BinaryReader(pISiteContentsMs))
                                    {
                                        emiFile.Site_ID = getUnicodeString(pISiteContentsReader);
                                        emiFile.Site_SerialNo = getUnicodeString(pISiteContentsReader);
                                        emiFile.Site_Address = getUnicodeString(pISiteContentsReader);
                                        emiFile.Site_Longitude = pISiteContentsReader.ReadDouble();
                                        emiFile.Site_Latitude = pISiteContentsReader.ReadDouble();
                                        emiFile.Site_Altitude = pISiteContentsReader.ReadDouble();
                                        emiFile.Site_MagDeclination = pISiteContentsReader.ReadDouble();
                                        emiFile.Site_CreateTime = getUnicodeString(pISiteContentsReader, 14);
                                        emiFile.Site_Comment = getUnicodeString(pISiteContentsReader);
                                    }
                                }
                            }

                            emiFile.HI_Extent_Length = headContentReader.ReadUInt32();
                            if (emiFile.HI_Extent_Length > 0)
                            {
                                byte[] HI_Extent_Contents = headContentReader.ReadBytes((int)emiFile.HI_Extent_Length);
                            }
                        }

                        /************************************************************************/
                        /* Data                                                                 */
                        /************************************************************************/
                        emiFile.DataLength = emiReader.ReadUInt32();
                        if (emiFile.DataLength == 0)
                            return null;

                        byte[] Data_Contents = emiReader.ReadBytes((int)emiFile.DataLength);
                        MemoryStream dataContentsMs = new MemoryStream(Data_Contents);
                        using (BinaryReader dataContentReader = new BinaryReader(dataContentsMs))
                        {
                            emiFile.Data_Head_Length = dataContentReader.ReadUInt32();
                            if (emiFile.Data_Head_Length == 0)
                                return null;

                            emiFile.DHI_DG_Count = dataContentReader.ReadUInt32();
                            if (emiFile.DHI_DG_Count > 0)
                            {
                                emiFile.DataGroups = new DG_Type[emiFile.DHI_DG_Count];
                                for (int i = 0; i < emiFile.DHI_DG_Count; i++)
                                {
                                    DG_Type dataGroup = new DG_Type();
                                    dataGroup.Data_Group_Length = dataContentReader.ReadUInt32();
                                    dataGroup.DG_HI_Length = dataContentReader.ReadUInt32();
                                    dataGroup.DG_FB_Start = dataContentReader.ReadDouble();
                                    dataGroup.DG_FB_End = dataContentReader.ReadDouble();
                                    dataGroup.DG_FB_Angle = dataContentReader.ReadDouble();
                                    dataGroup.DB_FB_Antenna = getUnicodeString(dataContentReader);
                                    dataGroup.DB_FB_AntennaPolarization = dataContentReader.ReadByte();
                                    if (dataGroup.DB_FB_AntennaPolarization != 0 && dataGroup.DB_FB_AntennaPolarization != 1)
                                        return null;

                                    dataGroup.DB_FB_TestTime = getUnicodeString(dataContentReader, 14);

                                    //Ver2.3 or higher
                                    if (emiFile.MajorVersion > 2
                                        || emiFile.MajorVersion == 2 && emiFile.MinorVersion >= 3)
                                    {
                                        dataGroup.DB_FB_Pic = getUnicodeString(dataContentReader);
                                    }

                                    dataGroup.DG_Item_Length = dataContentReader.ReadUInt32();
                                    if (dataGroup.DG_Item_Length == 0)
                                        return null;

                                    dataGroup.DG_Item_Count = dataContentReader.ReadUInt32();
                                    if (dataGroup.DG_Item_Count > 0)
                                        dataGroup.DGDatas = new DG_Data_Type[dataGroup.DG_Item_Count];
                                    for (int j = 0; j < dataGroup.DG_Item_Count; j++)
                                    {
                                        DG_Data_Type dgData = new DG_Data_Type();
                                        dgData.DG_DI_RSSI = dataContentReader.ReadDouble();
                                        dgData.DG_DI_Freq = dataContentReader.ReadDouble();

                                        dataGroup.DGDatas[j] = dgData;
                                    }

                                    emiFile.DataGroups[i] = dataGroup;
                                }
                            }
                        }

                        return emiFile;
                    }
                    catch (System.Exception e)
                    {
                        return null;
                    }
                }
            }
        }

        public void Parse(string emiFileName, object context)
        {
            if (onParseFailed == null || onParseSuccessfully == null)
            {
                return;
            }

            new Thread(delegate()
            {
                using (FileStream fs = new FileStream(emiFileName, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader emiReader = new BinaryReader(fs))
                    {
                        EMIFileData emiFile = new EMIFileData();
                        try
                        {
                            byte[] FileFlag = emiReader.ReadBytes(8);
                            if (!EMIFileData.FileFlag.SequenceEqual(FileFlag))
                            {
                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid FileFlag", context);
                                return;
                            }

                            emiFile.MajorVersion = emiReader.ReadUInt32();
                            emiFile.MinorVersion = emiReader.ReadUInt32();
                            emiFile.HeadLength = emiReader.ReadUInt32();
                            if (emiFile.HeadLength == 0)
                            {
                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid HeadLength: 0", context);
                                return;
                            }

                            byte[] HeadContent = emiReader.ReadBytes((int)emiFile.HeadLength);
                            MemoryStream headContentMs = new MemoryStream(HeadContent);
                            using (BinaryReader headContentReader = new BinaryReader(headContentMs))
                            {
                                emiFile.HI_Base_Length = headContentReader.ReadUInt32();
                                if (emiFile.HI_Base_Length == 0)
                                {
                                    mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid HI_Base_Length: 0", context);
                                    return;
                                }

                                byte[] HI_Base_Contents = headContentReader.ReadBytes((int)emiFile.HI_Base_Length);
                                MemoryStream hiBaseContentsMs = new MemoryStream(HI_Base_Contents);
                                using (BinaryReader hiBaseContentsReader = new BinaryReader(hiBaseContentsMs))
                                {
                                    /************************************************************************/
                                    /* Program App                                                          */
                                    /************************************************************************/
                                    emiFile.Program_App_Length = hiBaseContentsReader.ReadUInt32();
                                    if (emiFile.Program_App_Length == 0)
                                    {
                                        mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid Program_App_Length: 0", context);
                                        return;
                                    }

                                    byte[] Program_App_Contents = hiBaseContentsReader.ReadBytes((int)emiFile.Program_App_Length);
                                    MemoryStream programAppContentsMs = new MemoryStream(Program_App_Contents);
                                    using (BinaryReader programAppContentsReader = new BinaryReader(programAppContentsMs))
                                    {
                                        emiFile.PA_Name = getUnicodeString(programAppContentsReader);
                                        if (emiFile.PA_Name == null)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PA_Name", context);
                                            return;
                                        }
                                        emiFile.PA_Version = getUnicodeString(programAppContentsReader);
                                        if (emiFile.PA_Version == null)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PA_Version", context);
                                            return;
                                        }
                                        emiFile.PA_TestTime = getUnicodeString(programAppContentsReader, 14);
                                        if (emiFile.PA_TestTime == null)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PA_TestTime", context);
                                            return;
                                        }
                                        emiFile.PA_UserName = getUnicodeString(programAppContentsReader);
                                        if (emiFile.PA_UserName == null)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PA_UserName", context);
                                            return;
                                        }
                                        emiFile.PA_DataFile = getUnicodeString(programAppContentsReader);
                                        if (emiFile.PA_DataFile == null)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PA_DataFile", context);
                                            return;
                                        }
                                    }

                                    /************************************************************************/
                                    /* Project INFO                                                         */
                                    /************************************************************************/
                                    emiFile.Project_Info_Length = hiBaseContentsReader.ReadUInt32();
                                    if (emiFile.Project_Info_Length == 0)
                                    {
                                        mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid Project_Info_Length: 0", context);
                                        return;
                                    }
                                    byte[] Project_Info_Contents = hiBaseContentsReader.ReadBytes((int)emiFile.Project_Info_Length);
                                    MemoryStream projectInfoContentsMs = new MemoryStream(Project_Info_Contents);
                                    using (BinaryReader projectInfoContentsReader = new BinaryReader(projectInfoContentsMs))
                                    {
                                        emiFile.PI_ID = getUnicodeString(projectInfoContentsReader);
                                        if (emiFile.PI_ID == null)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PI_ID", context);
                                            return;
                                        }
                                        emiFile.PI_TestMode = projectInfoContentsReader.ReadByte();
                                        if (emiFile.PI_TestMode != 0 && emiFile.PI_TestMode != 1)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PI_TestMode " + emiFile.PI_TestMode, context);
                                            return;
                                        }
                                        emiFile.PI_TestPriority = projectInfoContentsReader.ReadByte();
                                        if (emiFile.PI_TestPriority != 0 && emiFile.PI_TestPriority != 1)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PI_TestPriority " + emiFile.PI_TestPriority, context);
                                            return;
                                        }
                                        emiFile.PI_AntennaPolarization = projectInfoContentsReader.ReadByte();
                                        if (emiFile.PI_AntennaPolarization != 0 && emiFile.PI_AntennaPolarization != 1
                                            && emiFile.PI_AntennaPolarization != 2 && emiFile.PI_AntennaPolarization != 3)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PI_AntennaPolarization " + emiFile.PI_AntennaPolarization, context);
                                            return;
                                        }

                                        /************************************************************************/
                                        /* Azimuth                                                              */
                                        /************************************************************************/
                                        emiFile.PI_Azimuth_Length = projectInfoContentsReader.ReadUInt32();
                                        if (emiFile.PI_Azimuth_Length == 0)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PI_Azimuth_Length: 0", context);
                                            return;
                                        }
                                        byte[] PI_Azimuth_Contents = projectInfoContentsReader.ReadBytes((int)emiFile.PI_Azimuth_Length);
                                        MemoryStream pIAzimuthContentsMs = new MemoryStream(PI_Azimuth_Contents);
                                        using (BinaryReader pIAzimuthContentsMsReader = new BinaryReader(pIAzimuthContentsMs))
                                        {
                                            emiFile.Azimuth_Item_Count = pIAzimuthContentsMsReader.ReadUInt32();
                                            if (emiFile.Azimuth_Item_Count > 0)
                                                emiFile.Azimuth_Data = new Azimuth_DATA_TYPE[emiFile.Azimuth_Item_Count];

                                            for (int i = 0; i < emiFile.Azimuth_Item_Count; i++)
                                            {
                                                Azimuth_DATA_TYPE data = new Azimuth_DATA_TYPE();
                                                data.Azimuth_Length = pIAzimuthContentsMsReader.ReadUInt32();
                                                data.Azimuth_Start = pIAzimuthContentsMsReader.ReadDouble();
                                                data.Azimuth_End = pIAzimuthContentsMsReader.ReadDouble();
                                                data.Azimuth_Step = pIAzimuthContentsMsReader.ReadDouble();
                                                emiFile.Azimuth_Data[i] = data;
                                            }
                                        }

                                        /************************************************************************/
                                        /* Frequency                                                            */
                                        /************************************************************************/
                                        emiFile.PI_Freq_Length = projectInfoContentsReader.ReadUInt32();
                                        if (emiFile.PI_Freq_Length == 0)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PI_Freq_Length: 0", context);
                                            return;
                                        }
                                        byte[] PI_Freq_Contents = projectInfoContentsReader.ReadBytes((int)emiFile.PI_Freq_Length);
                                        MemoryStream pIFreqContentsMs = new MemoryStream(PI_Freq_Contents);
                                        using (BinaryReader pIFreqContentsReader = new BinaryReader(pIFreqContentsMs))
                                        {
                                            emiFile.Freq_Item_Count = pIFreqContentsReader.ReadUInt32();
                                            if (emiFile.Freq_Item_Count > 0)
                                                emiFile.Frequency_Data = new Frequency_Data_Type[emiFile.Freq_Item_Count];
                                            for (int i = 0; i < emiFile.Freq_Item_Count; i++)
                                            {
                                                Frequency_Data_Type data = new Frequency_Data_Type();
                                                data.Frequency_Length = pIFreqContentsReader.ReadUInt32();
                                                data.Frequency_ID = getUnicodeString(pIFreqContentsReader);
                                                data.Frequency_Start = pIFreqContentsReader.ReadDouble();
                                                data.Frequency_End = pIFreqContentsReader.ReadDouble();
                                                data.Frequency_Comment = getUnicodeString(pIFreqContentsReader);
                                                emiFile.Frequency_Data[i] = data;
                                            }
                                        }

                                        /************************************************************************/
                                        /* SA                                                                   */
                                        /************************************************************************/
                                        emiFile.PI_SA_Length = projectInfoContentsReader.ReadUInt32();
                                        if (emiFile.PI_SA_Length == 0)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PI_SA_Length: 0", context);
                                            return;
                                        }
                                        byte[] PI_SA_Contents = projectInfoContentsReader.ReadBytes((int)emiFile.PI_SA_Length);
                                        MemoryStream pISAContentsMs = new MemoryStream(PI_SA_Contents);
                                        using (BinaryReader pISAContentsReader = new BinaryReader(pISAContentsMs))
                                        {
                                            emiFile.SA_ID = getUnicodeString(pISAContentsReader);
                                            emiFile.SA_Span = pISAContentsReader.ReadDouble();
                                            emiFile.SA_REF_LEVEL = pISAContentsReader.ReadDouble();
                                            emiFile.SA_RBW = pISAContentsReader.ReadDouble();
                                            emiFile.SA_VBW = pISAContentsReader.ReadDouble();
                                            emiFile.SA_Detector = getUnicodeString(pISAContentsReader);
                                            if (!"Positive".Equals(emiFile.SA_Detector) && !"RMS".Equals(emiFile.SA_Detector)
                                                && !"Negative".Equals(emiFile.SA_Detector) && !"Sample".Equals(emiFile.SA_Detector)
                                                && !"None".Equals(emiFile.SA_Detector))
                                            {
                                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid SA_Detector: " + emiFile.SA_Detector, context);
                                                return;
                                            }
                                            emiFile.SA_Trace = getUnicodeString(pISAContentsReader);
                                            if (!"Normal".Equals(emiFile.SA_Trace) && !"Average".Equals(emiFile.SA_Trace)
                                                && !"Max Hold".Equals(emiFile.SA_Trace) && !"Min Hold".Equals(emiFile.SA_Trace)
                                                && !"None".Equals(emiFile.SA_Trace))
                                            {
                                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid SA_Trace: " + emiFile.SA_Trace, context);
                                                return;
                                            }
                                            emiFile.SA_Trace_Count = pISAContentsReader.ReadUInt16();
                                            emiFile.SA_Filter = getUnicodeString(pISAContentsReader);
                                            emiFile.SA_PreAmplify = getUnicodeString(pISAContentsReader);
                                            if (!"ON".Equals(emiFile.SA_PreAmplify) && !"OFF".Equals(emiFile.SA_PreAmplify)
                                                && !"None".Equals(emiFile.SA_PreAmplify))
                                            {
                                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid SA_PreAmplify: " + emiFile.SA_PreAmplify, context);
                                                return;
                                            }
                                            emiFile.SA_Attenuation = getUnicodeString(pISAContentsReader);
                                            if (!"Auto".Equals(emiFile.SA_Attenuation) && !"Assign".Equals(emiFile.SA_Attenuation))
                                            {
                                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid SA_Attenuation: " + emiFile.SA_Attenuation, context);
                                                return;
                                            }
                                            emiFile.SA_Attenuation_Value = pISAContentsReader.ReadByte();
                                            emiFile.SA_Sweep_Mode = getUnicodeString(pISAContentsReader);
                                            if (!"None".Equals(emiFile.SA_Sweep_Mode) && !"Fast".Equals(emiFile.SA_Sweep_Mode)
                                                && !"NO FFT".Equals(emiFile.SA_Sweep_Mode) && !"Performance".Equals(emiFile.SA_Sweep_Mode))
                                            {
                                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid SA_Sweep_Mode: " + emiFile.SA_Sweep_Mode, context);
                                                return;
                                            }
                                        }

                                        /************************************************************************/
                                        /* Device                                                               */
                                        /************************************************************************/
                                        emiFile.PI_Device_Length = projectInfoContentsReader.ReadUInt32();
                                        if (emiFile.PI_Device_Length == 0)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PI_Device_Length: 0", context);
                                            return;
                                        }
                                        byte[] PI_Device_Contents = projectInfoContentsReader.ReadBytes((int)emiFile.PI_Device_Length);
                                        MemoryStream pIDeviceContentsMs = new MemoryStream(PI_Device_Contents);
                                        using (BinaryReader pIDeviceContentsReader = new BinaryReader(pIDeviceContentsMs))
                                        {
                                            emiFile.Device_GPS_Length = pIDeviceContentsReader.ReadUInt32();
                                            if (emiFile.Device_GPS_Length == 0)
                                            {
                                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid Device_GPS_Length: 0", context);
                                                return;
                                            }
                                            emiFile.GPS_ID = getUnicodeString(pIDeviceContentsReader);

                                            emiFile.Device_Compass_Length = pIDeviceContentsReader.ReadUInt32();
                                            if (emiFile.Device_Compass_Length == 0)
                                            {
                                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid Device_Compass_Length: 0", context);
                                                return;
                                            }
                                            emiFile.Compass_ID = getUnicodeString(pIDeviceContentsReader);

                                            emiFile.Device_PT_Length = pIDeviceContentsReader.ReadUInt32();
                                            if (emiFile.Device_PT_Length == 0)
                                            {
                                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid Device_PT_Length: 0", context);
                                                return;
                                            }
                                            emiFile.PT_ID = getUnicodeString(pIDeviceContentsReader);

                                            emiFile.Device_Antenna_Length = pIDeviceContentsReader.ReadUInt32();
                                            if (emiFile.Device_Antenna_Length == 0)
                                            {
                                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid Device_Antenna_Length: 0", context);
                                                return;
                                            }
                                            emiFile.Antenna_ID = getUnicodeString(pIDeviceContentsReader);

                                            emiFile.Device_Cable_Length = pIDeviceContentsReader.ReadUInt32();
                                            if (emiFile.Device_Cable_Length == 0)
                                            {
                                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid Device_Cable_Length: 0", context);
                                                return;
                                            }
                                            emiFile.Cable_ID = getUnicodeString(pIDeviceContentsReader);

                                            emiFile.Device_LNA_Length = pIDeviceContentsReader.ReadUInt32();
                                            if (emiFile.Device_LNA_Length == 0)
                                            {
                                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid Device_LNA_Length: 0", context);
                                                return;
                                            }
                                            emiFile.LNA_ID = getUnicodeString(pIDeviceContentsReader);
                                        }

                                        /************************************************************************/
                                        /* Site                                                                 */
                                        /************************************************************************/
                                        emiFile.PI_Site_Length = projectInfoContentsReader.ReadUInt32();
                                        if (emiFile.PI_Site_Length == 0)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid PI_Site_Length: 0", context);
                                            return;
                                        }
                                        byte[] PI_Site_Contents = projectInfoContentsReader.ReadBytes((int)emiFile.PI_Device_Length);
                                        MemoryStream pISiteContentsMs = new MemoryStream(PI_Site_Contents);
                                        using (BinaryReader pISiteContentsReader = new BinaryReader(pISiteContentsMs))
                                        {
                                            emiFile.Site_ID = getUnicodeString(pISiteContentsReader);
                                            emiFile.Site_SerialNo = getUnicodeString(pISiteContentsReader);
                                            emiFile.Site_Address = getUnicodeString(pISiteContentsReader);
                                            emiFile.Site_Longitude = pISiteContentsReader.ReadDouble();
                                            emiFile.Site_Latitude = pISiteContentsReader.ReadDouble();
                                            emiFile.Site_Altitude = pISiteContentsReader.ReadDouble();
                                            emiFile.Site_MagDeclination = pISiteContentsReader.ReadDouble();
                                            emiFile.Site_CreateTime = getUnicodeString(pISiteContentsReader, 14);
                                            emiFile.Site_Comment = getUnicodeString(pISiteContentsReader);
                                        }
                                    }
                                }

                                emiFile.HI_Extent_Length = headContentReader.ReadUInt32();
                                if (emiFile.HI_Extent_Length > 0)
                                {
                                    byte[] HI_Extent_Contents = headContentReader.ReadBytes((int)emiFile.HI_Extent_Length);
                                }
                            }

                            /************************************************************************/
                            /* Data                                                                 */
                            /************************************************************************/
                            emiFile.DataLength = emiReader.ReadUInt32();
                            if (emiFile.DataLength == 0)
                            {
                                mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid DataLength: 0", context);
                                return;
                            }

                            byte[] Data_Contents = emiReader.ReadBytes((int)emiFile.DataLength);
                            MemoryStream dataContentsMs = new MemoryStream(Data_Contents);
                            using (BinaryReader dataContentReader = new BinaryReader(dataContentsMs))
                            {
                                emiFile.Data_Head_Length = dataContentReader.ReadUInt32();
                                if (emiFile.Data_Head_Length == 0)
                                {
                                    mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "Invalid Data_Head_Length: 0", context);
                                    return;
                                }
                                emiFile.DHI_DG_Count = dataContentReader.ReadUInt32();
                                if (emiFile.DHI_DG_Count > 0)
                                {
                                    emiFile.DataGroups = new DG_Type[emiFile.DHI_DG_Count];
                                    for (int i = 0; i < emiFile.DHI_DG_Count; i++)
                                    {
                                        DG_Type dataGroup = new DG_Type();
                                        dataGroup.Data_Group_Length = dataContentReader.ReadUInt32();
                                        dataGroup.DG_HI_Length = dataContentReader.ReadUInt32();
                                        dataGroup.DG_FB_Start = dataContentReader.ReadDouble();
                                        dataGroup.DG_FB_End = dataContentReader.ReadDouble();
                                        dataGroup.DG_FB_Angle = dataContentReader.ReadDouble();
                                        dataGroup.DB_FB_Antenna = getUnicodeString(dataContentReader);
                                        dataGroup.DB_FB_AntennaPolarization = dataContentReader.ReadByte();
                                        if (dataGroup.DB_FB_AntennaPolarization != 0 && dataGroup.DB_FB_AntennaPolarization != 1)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "DB_FB_AntennaPolarization: " + dataGroup.DB_FB_AntennaPolarization, context);
                                            return;
                                        }
                                        dataGroup.DB_FB_TestTime = getUnicodeString(dataContentReader, 14);
                                        
                                        //Ver2.3 or higher
                                        if (emiFile.MajorVersion > 2
                                            || emiFile.MajorVersion == 2 && emiFile.MinorVersion >= 3)
                                        {
                                            dataGroup.DB_FB_Pic = getUnicodeString(dataContentReader);
                                        }
                                        
                                        dataGroup.DG_Item_Length = dataContentReader.ReadUInt32();
                                        if (dataGroup.DG_Item_Length == 0)
                                        {
                                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, "DG_Item_Length: 0", context);
                                            return;
                                        }
                                        dataGroup.DG_Item_Count = dataContentReader.ReadUInt32();
                                        if (dataGroup.DG_Item_Count > 0)
                                            dataGroup.DGDatas = new DG_Data_Type[dataGroup.DG_Item_Count];
                                        for (int j = 0; j < dataGroup.DG_Item_Count; j++)
                                        {
                                            DG_Data_Type dgData = new DG_Data_Type();
                                            dgData.DG_DI_RSSI = dataContentReader.ReadDouble();
                                            dgData.DG_DI_Freq = dataContentReader.ReadDouble();

                                            dataGroup.DGDatas[j] = dgData;
                                        }

                                        emiFile.DataGroups[i] = dataGroup;
                                    }
                                }
                            }

                            mAttachedForm.BeginInvoke(onParseSuccessfully, emiFileName, emiFile, context);
                        }
                        catch (System.Exception e)
                        {
                            mAttachedForm.BeginInvoke(onParseFailed, emiFileName, e.Message, context);
                        }
                    }
                }
            }).Start();
        }
    }
}
