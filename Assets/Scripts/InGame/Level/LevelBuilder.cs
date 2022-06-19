using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using Random = System.Random;

public class LevelBuilder : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private GameObject levelPartStart;
    [SerializeField] private Transform _worldLevelPartContainer;
    [SerializeField] private Transform _enemiesContainer;
    [SerializeField] private Transform _wallContainer;
    [SerializeField] private Transform _playerContainer;
    private List<GameObject> levelPartsToBuild = new List<GameObject>();
    private List<GameObject> levelPool = new List<GameObject>();
    private List<GameObject> enemiesToBuild = new List<GameObject>();
    private List<GameObject> enemiesPool = new List<GameObject>();
    private Random randomNumber;
    private GameObject playerLoaded;
    private GameObject wall;
    private GameObject player;


    private void Awake()
    {
        LoadResourcesParts(Const.LevelPartsPath, out levelPartsToBuild);
        LoadResourcesParts(Const.EnemiesPath, out enemiesToBuild);
        LoadPlayer();
        LoadWall();
        randomNumber = new Random();
        GameStateDispatcher.Instance.AddListener(RestartLevel);
    }

    private void Start()
    {
        InstantiateWall();
        InstantiatePlayer();
        levelPool.Add(levelPartStart);
        player.GetComponent<PlayerController>().SetFollowingWall(target);
    }

    private void Update()
    {

        if (target.gameObject.transform.position.x >
            levelPool[levelPool.Count - 1].GetComponentInChildren<LevelScript>().endPoint.position.x - 40f)
        {
            if (levelPool.Count < Const.LevelPoolSize)
            {
                SpawnPart(levelPartsToBuild[randomNumber.Next(0, levelPartsToBuild.Count - 1)],
                    enemiesToBuild[randomNumber.Next(0, enemiesToBuild.Count - 1)]);
            }
            else
            {
                int rd_num = new Random().Next(0, Const.LevelPoolSize - 2);
                GameObject replacedPart = levelPool[rd_num];
                levelPool.Remove(levelPool[rd_num]);
                replacedPart.transform.position =
                    levelPool[levelPool.Count - 1].GetComponentInChildren<LevelScript>().endPoint.position -
                    replacedPart.GetComponentInChildren<LevelScript>().startPoint.localPosition;
                levelPool.Add(replacedPart);

                int t = randomNumber.Next(1, 3);
                for (int i = 0; i < t; i++)
                {
                    MoveEnemyInPool(ref enemiesPool);
                }
            }
        }

        foreach (var enemy in enemiesPool)
        {
            if (target.transform.position.x > enemy.transform.position.x)
            {
                enemy.gameObject.SetActive(false);
            }
        }
        
        
        
    }

    private void OnDestroy()
    {
        GameStateDispatcher.Instance.RemoveListener(RestartLevel);
    }

    private void LoadResourcesParts(string path, out List<GameObject> list)
    {
        GameObject[] parts = Resources.LoadAll<GameObject>(path);
        list = parts.ToList();
    }

    private void LoadPlayer()
    {
        GameObject player = Resources.Load<GameObject>("Player/Player");
        playerLoaded = player;
    }

    private void SpawnPart(GameObject levelPart, GameObject enemy)
    {
        GameObject newLevelPart = Instantiate(levelPart, _worldLevelPartContainer);
        newLevelPart.transform.position =
            levelPool[levelPool.Count - 1].GetComponentInChildren<LevelScript>().endPoint.position -
            newLevelPart.GetComponentInChildren<LevelScript>().startPoint.localPosition;

        int t = randomNumber.Next(0, 3);
        for (int i = 0; i < t; i++)
        {
            if (enemiesPool.Count > Const.EnemiesPoolSize)
            {
                MoveEnemyInPool(ref enemiesPool);
            }
            else
            {
                int distance = randomNumber.Next(40, 100);
                GameObject newEnemy = Instantiate(enemy, _enemiesContainer);
                newEnemy.gameObject.SetActive(false);
                newEnemy.transform.position =
                    new Vector3(target.transform.position.x + distance, randomNumber.Next(-3, 4), 0);
                        newEnemy.gameObject.SetActive(true);
                enemiesPool.Add(newEnemy);
            }
            
        }

        levelPool.Add(newLevelPart);

    }

    void MoveEnemyInPool(ref List<GameObject> list)
    {
        int distance = randomNumber.Next(40, 100);
        Debug.Log(distance + "replace distance");
        GameObject replacedEnemy = list[1];
        list.Remove(list[1]);
        replacedEnemy.transform.position =
            new Vector3(target.transform.position.x + distance, randomNumber.Next(-3, 4), 0);
        replacedEnemy.gameObject.SetActive(true);
        list.Add(replacedEnemy);
    }

    public void RestartLevel(GameState gameState)
    {
        if (gameState != GameState.Restart) return;
        foreach (Transform levelParts in _worldLevelPartContainer.transform)
        {
            levelParts.gameObject.SetActive(false);
            GameObject.Destroy(levelParts.gameObject);
        }

        levelPool.Clear();
        levelPool.Add(levelPartStart);

        foreach (Transform child in _enemiesContainer)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void LoadWall()
    {
        GameObject wall = Resources.Load<GameObject>("FollowingWall/FollowingEnemy");
        this.wall = wall;
    }

    private void InstantiateWall()
    {
       GameObject wallFollow = Instantiate(wall, _wallContainer);
       Debug.Log(wallFollow);
       wallFollow.transform.position = Const.WallStartPosition;
       target = wallFollow;
    }

    private void InstantiatePlayer()
    {
        GameObject player = Instantiate(playerLoaded, _playerContainer);
        player.transform.position = Const.PlayerStartPosition;
        this.player = player;
    }


}
