using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class TitleMenuManager : MonoBehaviour
{
  [SerializeField] GameObject optionPanel;
  [SerializeField] string gamePlayScene;

  [SerializeField] Button startButton, optionButton, optionCloseButton, quitButton;
  
  void Start()
  {
    optionPanel.SetActive(false);
    startButton.Select();

    startButton.onClick.AddListener(StartGame);
    optionButton.onClick.AddListener(OpenOptionPanel);
    optionCloseButton.onClick.AddListener(CloseOptionPanel);
    quitButton.onClick.AddListener(QuitGame);
  }

  void StartGame()
  {
    if (optionPanel.activeInHierarchy)
        return;
    AudioPlayer.Instance.PlaySFX(clickSfx);

    SceneManager.LoadScene(gamePlayScene);
  }

  void OpenOptionPanel()
  {
    AudioPlayer.Instance.PlaySFX(clickSfx);
    optionCloseButton.Select();
    optionPanel.SetActive(true);
  }

  void CloseOptionPanel()
  {
    optionPanel.SetActive(false);
    optionButton.Select();
  }

  void QuitGame()
  {
    Application.Quit();
  }

}
