using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatsEMIAnalyzer.HTTP;

namespace WatsEMIAnalyzer
{
    public enum TaskType
    {
        getRegion,
        getUsers,
        getTasks,
        getSites,
        getFileList,
        getFile,
    }

    class Task
    {
        public TaskType mType;
        public HTTPAgent.FileType mFileType;
        public long mFileId;
        public string mFileTitle;

        public Task(TaskType type, HTTPAgent.FileType fileType, long fileId, string fileTitle)
        {
            mType = type;
            mFileType = fileType;
            mFileId = fileId;
            mFileTitle = fileTitle;
        }
    }
}
