using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        playButton?.onClick.AddListener(()=> PlayButton());
        PlayerData.Instance.LoadBestScore();
    }

    void Start()
    {
        scoreText.text = "" + Mathf.RoundToInt(PlayerData.bestScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayButton()
    {
        StartCoroutine(LoadLevel(SceneID.InGameScene));
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
