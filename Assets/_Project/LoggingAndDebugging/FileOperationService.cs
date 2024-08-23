using System.IO;
using System.IO.Compression;
using System.Linq;
using _Project.Utilities;
using SSS.UnityFileDebug;
using UnityEngine;

namespace _Project.LoggingAndDebugging
{
    public class FileOperationService : MonoBehaviour//Lazy
    {
        [Inject]
        public IFileOperationService FileOperationServiceInstance {get;set;}        
        
        [Inject]
        public INativeShareService NativeShareServiceInstance {get;set;}
        
        private UnityFileDebug _unityFileDebug;
        
        protected void Awake()
        {
            _unityFileDebug = GameObject.FindGameObjectWithTag("UnityFileDebug").GetComponent<UnityFileDebug>();//TODO: Inject this.

            SubscribeToSRDebuggerOptionButtons();
        }
        
        private void SubscribeToSRDebuggerOptionButtons()
        {
            SROptions.OnZipAllLogsButtonPressed += HandleZipAllLogsButtonPressed;
            SROptions.OnSendLogZipButtonPressed += HandleSendLogZipButtonPressed;
        }
        
        private void HandleZipAllLogsButtonPressed()
        {
            //_unityFileDebug.CloseFile();//Check bottom of this class.
            CompressDirectory(_unityFileDebug.filePath, Constants.LOGS_PATH);
        }        
        
        private void HandleSendLogZipButtonPressed()
        {
            NativeShareServiceInstance.ShareFile(Constants.LOGS_PATH, () =>
            {
                //_unityFileDebug.OpenFile();//Check bottom of this class.
                FileOperationServiceInstance.DeleteFile(Constants.LOGS_PATH);
            });
        }

        public void CompressDirectory(string path, string zipPath)
        {
            DeleteFile(zipPath);
            CreateZipFile(path, zipPath);
        }
    
        private void CreateZipFile(string path, string zipPath)
        {
            UnityEngine.Debug.Log("zipPath is: "+ zipPath);
            /*UnityEngine.Debug.Log("DiskUtils.CheckAvailableSpace() is: " + DiskUtils.CheckAvailableSpace());
            if (DiskUtils.CheckAvailableSpace() < 512)
            {
                UnityEngine.Debug.LogError("AvailableSpace was below 512, could not CreateZipFile");
                return;
            }*/
            
            ZipFile.CreateFromDirectory(path, zipPath);
        }

        public void CreateDirectory(string path)
        {
            /*UnityEngine.Debug.Log("ppp DiskUtils.CheckAvailableSpace() is: " + DiskUtils.CheckAvailableSpace());
            if (DiskUtils.CheckAvailableSpace() < 512) { return; }*/
            
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }
        
        public void DeleteDirectory(string path)
        {
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                Directory.Delete(path);
            }
        }
    
        public void DeleteFile(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                File.Delete(path);
            }
        }
    
        public void DeleteFiles(string path, int count)
        {
            if (string.IsNullOrEmpty(path)) return;
            
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles().OrderBy(file => file.Name).ToArray();
    
            if (files.Length <= count) return;
                
            foreach (FileInfo file in files)
            {
                DeleteFile(file.FullName);
                if (directory.GetFiles().Length > count) continue;
                break;
            }
        }
        
        public void SaveValue(string path, string value)
        {
            /*UnityEngine.Debug.Log("DiskUtils.CheckAvailableSpace() is: " + DiskUtils.CheckAvailableSpace());
            if (DiskUtils.CheckAvailableSpace() < 512) { return; }*/
            
            if (string.IsNullOrEmpty(path)) return;
            
            if (File.Exists(path))
            {
                File.WriteAllLines(path, System.Array.Empty<string>());
            }
            
            using (StreamWriter writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                writer.WriteLine(value);
                writer.Close();
            }
        }
    
        public string GetValue(string path)
        {
            if (string.IsNullOrEmpty(path) == false && File.Exists(path))
            {
                using (StreamReader file = new StreamReader(path))
                {
                    string value = file.ReadToEnd();
                    file.Close();
                    return value;
                }
            }
    
            return "";
        }

    }
}

//FOLLOWING LINES WERE IN UnityFileDebug in earlier versions.

/*
 *             public void OpenFile()
            {
                UpdateFilePath();
                if (Application.isPlaying)
                {
                    count = 0;
                    fileWriter = new System.IO.StreamWriter(filePathFull, false);
                    fileWriter.AutoFlush = true;
                    switch (fileType)
                    {
                        case FileType.CSV:
                            fileWriter.WriteLine("type,time,log,stack");
                            break;
                        case FileType.JSON:
                            fileWriter.WriteLine("[");
                            break;
                        case FileType.TSV:
                            fileWriter.WriteLine("type\ttime\tlog\tstack");
                            break;
                    }
                    Application.logMessageReceivedThreaded += HandleLog;
                }
            }

            public void CloseFile()
            {
                if (Application.isPlaying)
                {
                    Application.logMessageReceivedThreaded -= HandleLog;

                    switch (fileType)
                    {
                        case FileType.JSON:
                            fileWriter.WriteLine("\n]"); // TODO: This is for closing JSON array but This line gives error: "Cannot write to a closed TextWriter"
                            break;
                        case FileType.CSV:
                        case FileType.TSV:
                        default:
                            break;
                    }
                    fileWriter.Close();
                }
            }
 */