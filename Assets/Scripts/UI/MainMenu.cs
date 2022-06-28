using UnityEngine;
using UnityEngine.UI;

namespace PumpkinShooter.UI
{

    public class MainMenu : UIBase
    {
        [SerializeField] GameObject LoadingPanel;
        [SerializeField] private Text _highScore = null;
        [SerializeField] private Text _version = null;
        [SerializeField] private Text _title = null;

        public void LoadGame()
        {
            GameManager.Instance.GoToGame();
            LoadingPanel.SetActive(true);
        }

        public override void InitUI()
        {
            _highScore.text = GameManager.Instance.userData.HighScore.ToString();
            _version.text = GameManager.Instance.gameData.version;
            _title.text = GameManager.Instance.gameData.title;
            LoadingPanel.SetActive(false);
        }
    }
}