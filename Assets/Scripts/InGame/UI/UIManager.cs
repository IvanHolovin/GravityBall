using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _backPopUp;
    [SerializeField] private GameObject _gameOverPopUp;
    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _loadingSlider;
    
    private void Awake()
    {
        GameStateDispatcher.Instance.AddListener(PopUpManagerController);
    }

    private void Start()
    {
        _backButton?.onClick.AddListener(()=> GamePlayManager.Instance.GameStateChanger(GameState.Pause));
    }

    private void OnDestroy()
    {
        GameStateDispatcher.Instance.RemoveListener(PopUpManagerController);
    }

    private void PopUpManagerController(GameState state)
    {
        _backPopUp.gameObject.SetActive(state == GameState.Pause); 
        _gameOverPopUp.SetActive(state == GameState.Death);
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
        _loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            _loadingSlider.value = progress;
            yield return null;
        }
    }
}
