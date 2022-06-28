using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace PumpkinShooter.Tools
{
    public static class CommonTools
    {
        public static IEnumerator LoadFile(string fileName, UnityAction<string> onReceivedData)
        {
            string filePath;
            filePath = Path.Combine(Application.streamingAssetsPath + "/", fileName);
            string dataAsJson;
            if (filePath.Contains("://") || filePath.Contains(":///"))
            {
                UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(filePath);
                yield return www.SendWebRequest();
                dataAsJson = www.downloadHandler.text;
            }
            else
            {
                dataAsJson = File.ReadAllText(filePath);
            }

            onReceivedData?.Invoke(dataAsJson);
        }
    }
}