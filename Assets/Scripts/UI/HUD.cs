using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PumpkinShooter.UI
{
    public class HUD : UIBase
    {
        [Header("Game Session Info")]
        [SerializeField] private Text _scoreValue = null;
        [SerializeField] private GameObject _timeRemaining = null;
        [SerializeField] private Text _timeRemainingValue = null;
        [Header("Game Over")]
        [SerializeField] private GameObject _gameOverScreen = null;
        [Header("Loading")]
        [SerializeField] private GameObject _loadingScreen = null;

        string GetFormattedTimeFromSeconds(float seconds)
        {
            return Mathf.FloorToInt(seconds / 60.0f).ToString("0") + ":" + Mathf.FloorToInt(seconds % 60.0f).ToString("00");
        }

        public void HandleSessionEnded()
        {
            _timeRemaining.SetActive(false);
            _gameOverScreen.SetActive(true);
        }

        public void SetTime(float timeLeft)
        {
            _timeRemainingValue.text = GetFormattedTimeFromSeconds(timeLeft);
        }

        public void SetScore(int score)
        {
            _scoreValue.text = score.ToString();
        }

        public override void InitUI()
        {
            _loadingScreen.SetActive(false);
            _gameOverScreen.SetActive(false);
        }

        public void ReloadGame()
        {
            _loadingScreen.SetActive(true);
            GameManager.Instance.GoToGame();
        }

        public void BackToMenu()
        {
            _loadingScreen.SetActive(true);
            GameManager.Instance.GoToMainMenu();
        }
    }
}