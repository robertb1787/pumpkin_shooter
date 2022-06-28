using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PumpkinShooter.Data
{
    public class UserData : MonoBehaviour
    {
        [SerializeField] UserSavedData savedData;
        public int HighScore => savedData.HighScore;

        public void LoadUserData()
        {
            savedData = new UserSavedData
            {
                HighScore = PlayerPrefs.GetInt("highScore", 0)
            };
        }

        public void SaveHighScore(int score)
        {
            if (HighScore < score)
            {
                //Save locally
                savedData.HighScore = score;
                //Send to the server considering playerpref is the server
                PlayerPrefs.SetInt("highScore", score);
            }
        }
    }

    /// <summary>
    /// This class is usally saved on the server 
    /// </summary>
    [System.Serializable]
    public class UserSavedData
    {
        public int HighScore = 0;
    }
}