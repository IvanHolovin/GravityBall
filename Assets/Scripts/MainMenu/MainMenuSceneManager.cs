using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Button _playButton;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _loadingSlider;

    private void Awake()
    {
        _playButton?.onClick.AddListener(()=> PlayButton());
        PlayerData.Instance.LoadBestScore();
    }

    void Start()
    {
        _scoreText.text = "" + Mathf.RoundToInt(PlayerData.bestScore);
    }

    private void PlayButton()
    {
        StartCoroutine(LoadLevel(SceneID.InGameScene));
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
