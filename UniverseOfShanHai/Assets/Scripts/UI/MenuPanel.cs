using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuPanel : MonoBehaviour
{
    private VisualElement rootElement;
    private Button newGameButton, exitGameButton;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        newGameButton = rootElement.Q<Button>("StartGameButton");
        exitGameButton = rootElement.Q<Button>("ExitGameButton");
        newGameButton.clicked += OnNewGameButtonClicked;
        exitGameButton.clicked += OnExitGameButtonClicked;
        AudioManager.Instance.PlayGameSoundClip(AudioManager.Instance.soundDetailList.GetSoundDetails("Index"));

    }

    private void OnNewGameButtonClicked()
    {
        SceneLoadManager.Instance.LoadIntro();
    }

    private void OnExitGameButtonClicked()
    {
        Application.Quit();
    }
}
