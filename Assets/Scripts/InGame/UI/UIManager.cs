using System;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject backPopUp;
    [SerializeField] private GameObject gameOverPopUp;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;


    private void Awake()
    {
        GameStateDispatcher.Instance.AddListener(PopUpManagerController);
    }

    private void Start()
    {
        backButton?.onClick.AddListener(()=> GamePlayManager.Instance.GameStateChanger(GameState.Pause));
    }

    private void OnDestroy()
    {
        GameStateDispatcher.Instance.RemoveListener(PopUpManagerController);
    }

    private void PopUpManagerController(GameState state)
    {
        
        backPopUp.gameObject.SetActive(state == GameState.Pause); 
        gameOverPopUp.SetActive(state == GameState.Death);
        if (state == GameState.MainMenu)
        {
            MenuButton();
        }
        
    }
    
    private void MenuButton()
    {
        StartCoroutine(LoadLevel(SceneID.MainMenu));
    }
    IEnumerator LoadLevel(SceneID sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync((int)sceneIndex);
        
        loadingScreen.SetActive(true);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
