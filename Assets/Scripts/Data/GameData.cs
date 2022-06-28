using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PumpkinShooter.Data
{
    using Tools;
    using Game;

    public class GameData : MonoBehaviour
    {
        [SerializeField] GameSavedData savedData;
        Enemy[] pumpkins;

        public string version => savedData.Version;
        public string title => savedData.Title;
        public float sessionTime => savedData.gameLength;
        public Enemy enemy
        {
            get
            {
                if (pumpkins.Length == 0)
                    Debug.Log("pumpking are missing from the resource folder or from the JSON file");
                return pumpkins[Random.Range(0, pumpkins.Length - 1)];
            }
        }

        public void LoadGameData(UnityAction onLoaded)
        {
            StartCoroutine(CommonTools.LoadFile("GameData", data =>
            {
                savedData = JsonUtility.FromJson<GameSavedData>(data);
                pumpkins = new Enemy[savedData.pumpkinToSpawn.Count];
                for (int i = 0; i < savedData.pumpkinToSpawn.Count; i++)
                {
                    string pumpkin_id = savedData.pumpkinToSpawn[i];
                    pumpkins[i] = Resources.Load<Enemy>(pumpkin_id);
                }
                onLoaded?.Invoke();
            }));
        }
    }

    [System.Serializable]
    public class GameSavedData
    {
        public string Title = "Pumpkin Shooter";
        public string Version = "0.9";
        public float gameLength = 60;
        public List<string> pumpkinToSpawn = new List<string> { "Pumpkin_Blue", "Pumpkin_Green", "Pumpkin_Purple", "Pumpkin_Orange" };
    }
}