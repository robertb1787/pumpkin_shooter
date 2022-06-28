using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PumpkinShooter.Game
{
    public class GameSession : MonoBehaviour
    {
        public UnityEvent OnSessionStart;
        public UnityEvent OnSessionEnd;
        public UnityEvent<int> OnAddedScore;
        public UnityEvent<float> OnTimeChanged;

        float timeLeft;
        public int playerScore = 0;

        public enum SessionState
        {
            Paused,
            Active,
            Finished
        }

        private SessionState _state = SessionState.Paused;

        void Start()
        {
            StartSession();
        }

        private void OnEnable()
        {
            GameManager.CUSTOMUPDATE.AddListener(CustomUpdate);
        }

        private void OnDisable()
        {
            GameManager.CUSTOMUPDATE.RemoveListener(CustomUpdate);
        }

        void CustomUpdate(float deltaTime)
        {
            if (_state == SessionState.Active)
            {
                timeLeft -= deltaTime;
                OnTimeChanged.Invoke(timeLeft);
                if (timeLeft <= 0)
                {
                    timeLeft = 0;
                    EndSession();
                }
            }
        }

        public void AddedScore(int scoreToAdd)
        {
            playerScore += scoreToAdd;
            OnAddedScore.Invoke(playerScore);
        }

        void StartSession()
        {
            timeLeft = GameManager.Instance.gameData.sessionTime;
            AddedScore(0);
            _state = SessionState.Active;
            OnSessionStart.Invoke();
        }

        void EndSession()
        {
            OnSessionEnd.Invoke();

            GameManager.Instance.SetScore(playerScore);
        }
    }
}