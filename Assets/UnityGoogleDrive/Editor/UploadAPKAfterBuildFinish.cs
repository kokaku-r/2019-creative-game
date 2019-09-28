using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

/// <summary>
/// require -> https://github.com/Elringus/UnityGoogleDrive
/// </summary>
public class UploadAPKAfterBuildFinish
{
    //このAttributeをつけた関数はBuild後に実行される
    [PostProcessBuild(100)]
    public static void OnAndroidBuildFinish(BuildTarget target, string pathToBuildProject)
    {
        if (target == BuildTarget.Android)
        {
            UploadApk(pathToBuildProject);
        }
    }

    static void UploadApk(string pathToBuildProject)
    {
        //Debug.Log($"Build Target is Android Start Uploading projectPath:{pathToBuildProject}");
        var apkName = pathToBuildProject.Split(new[] {'/'}).Last();// apkのファイル名を取得
//                Debug.Log($"Name:{apkName}");

        if (File.Exists(pathToBuildProject))
        {
            var apk = new UnityGoogleDrive.Data.File
                {Name = apkName, Content = File.ReadAllBytes(pathToBuildProject)};
            var req = UnityGoogleDrive.GoogleDriveFiles.Create(apk);
            req.OnDone += response =>
            {
                if (req.IsError) Debug.LogError(req.Error);
                if (req.IsDone)
                {
                    Debug.Log("Upload Success! See your Google Drive");
                    Debug.Log(response.WebViewLink);
                    if(!string.IsNullOrEmpty(response.WebViewLink))
                        Application.OpenURL(req.ResponseData.WebViewLink);
                }
            };
            
            req.Send();//upload

            
        }
        else
        {
            Debug.Log($"Built File Path:{pathToBuildProject} File Not Exists");
        }
    }
}