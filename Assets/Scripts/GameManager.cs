using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace PumpkinShooter
{
    using Data;
    using UI;
    using Game;
    using System;

    public class GameManager : MonoBehaviour
    {
        public static UnityEvent<float> CUSTOMUPDATE = new UnityEvent<float>();
        public static GameManager Instance;

        #region Properties
        private UserData _userData;
        public UserData userData
        {
            get
            {
                if (!_userData)
                    _userData = GetComponentInChildren<UserData>();

                return _userData;
            }
        }

        private GameData _gameData;
        public GameData gameData
        {
            get
            {
                if (!_gameData)
                    _gameData = GetComponentInChildren<GameData>();

                return _gameData;
            }
        }
        #endregion

        private void Awake()
        {
            //Set the Singelton instance
            if(Instance == null || Instance == this)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            //Load the data
            gameData.LoadGameData(UpdateCurrentUIPanel);
            userData.LoadUserData();
        }

        public void SetScore(int playerScore)
        {
            userData.SaveHighScore(playerScore);
        }

        void Start()
        {
            //To keep the manager on all the scenes
            DontDestroyOnLoad(gameObject);
        }

        public void GoToGame()
        {
            StartCoroutine(LoadSpecificScene("GameScene"));
        }

        public void GoToMainMenu()
        {
            StartCoroutine(LoadSpecificScene("StartMenu"));
        }

        IEnumerator LoadSpecificScene(string sceneName)
        {
            var asyncOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            while (!asyncOp.isDone)
            {
                yield return null;
            }

            UpdateCurrentUIPanel();
        }

        public void UpdateCurrentUIPanel()
        {
            try
            {
                FindObjectOfType<UIBase>().InitUI();
            }
            catch
            {
                Debug.LogWarning("No UI Was found in the scene");
            }
        }

        void Update()
        {
            CUSTOMUPDATE.Invoke(Time.deltaTime);
        }
    }
}