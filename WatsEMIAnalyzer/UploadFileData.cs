using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace WatsEMIAnalyzer
{
    public class UploadFileData
    {
        private string mShortName;
        private string mUploader;
        private string mMd5;
        private byte[] mFileContent;
        private byte[] mParseData;

        public string ShortName
        {
            get { return mShortName; }
            set { mShortName = value; }
        }

        public string Uploader
        {
            set { mUploader = value; }
            get { return mUploader; }
        }

        public string Md5
        {
            set { mMd5 = value; }
            get { return mMd5; }
        }

        public byte[] FileContent
        {
            set { mFileContent = value; }
            get { return mFileContent; }
        }

        public byte[] ParseData
        {
            set { mParseData = value; }
            get { return mParseData; }
        }

        public byte[] ToBytes()
        {
            byte[] shortNameBytes = Encoding.UTF8.GetBytes(mShortName);
            byte[] uploaderBytes = Encoding.UTF8.GetBytes(mUploader);
            byte[] md5Bytes = Encoding.UTF8.GetBytes(mMd5);

            int totlaLength = 4 + shortNameBytes.Length
                + 4 + uploaderBytes.Length
                + 4 + md5Bytes.Length
                + 4 + mFileContent.Length
                + 4 + (mParseData == null ? 0 : mParseData.Length);

            byte[] data = new byte[totlaLength];

            int pos = 0;

            //shortName
            int shortNameLen = IPAddress.HostToNetworkOrder(shortNameBytes.Length);
            Array.Copy(BitConverter.GetBytes(shortNameLen), data, 4);
            Array.Copy(shortNameBytes, 0, data, 4, shortNameBytes.Length);
            pos += 4 + shortNameBytes.Length;

            //upLoader
            int uploaderLen = IPAddress.HostToNetworkOrder(uploaderBytes.Length);
            Array.Copy(BitConverter.GetBytes(uploaderLen), 0, data, pos, 4);
            Array.Copy(uploaderBytes, 0, data, pos + 4, uploaderBytes.Length);
            pos += 4 + uploaderBytes.Length;

            //md5
            int md5Len = IPAddress.HostToNetworkOrder(md5Bytes.Length);
            Array.Copy(BitConverter.GetBytes(md5Len), 0, data, pos, 4);
            Array.Copy(md5Bytes, 0, data, pos + 4, md5Bytes.Length);
            pos += 4 + md5Bytes.Length;

            //file content
            int fileContentLen = IPAddress.HostToNetworkOrder(mFileContent.Length);
            Array.Copy(BitConverter.GetBytes(fileContentLen), 0, data, pos, 4);
            Array.Copy(mFileContent, 0, data, pos + 4, mFileContent.Length);
            pos += 4 + mFileContent.Length;

            //parse data
            int parseDataLen = IPAddress.HostToNetworkOrder(mParseData.Length);
            Array.Copy(BitConverter.GetBytes(parseDataLen), 0, data, pos, 4);
            if (parseDataLen > 0)
                Array.Copy(mParseData, 0, data, pos + 4, mParseData.Length);

            return data;
        }
    }
}
