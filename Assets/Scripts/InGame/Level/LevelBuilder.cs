using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private GameObject _levelPartStart; 
    [SerializeField] private Transform _worldLevelPartContainer;
    [SerializeField] private Transform _enemiesContainer;
    [SerializeField] private Transform _wallContainer;
    [SerializeField] private Transform _playerContainer;

    private List<GameObject> _levelPartsToBuild = new List<GameObject>();
    private List<GameObject> _levelPool = new List<GameObject>();
    private List<GameObject> _enemiesToBuild = new List<GameObject>();
    private List<GameObject> _enemiesPool = new List<GameObject>();
    
    private Random _randomNumber;
    private GameObject _playerLoaded;
    private GameObject _wall;
    private GameObject _player;
    private GameObject _targetWall;
    
    private void Awake()
    {
        LoadResourcesParts(Const.LEVEL_PARTS_PATH, out _levelPartsToBuild);
        LoadResourcesParts(Const.ENEMIES_PATH, out _enemiesToBuild);
        LoadPlayer();
        LoadWall();
        _randomNumber = new Random();
        GameStateDispatcher.Instance.AddListener(RestartLevel);
    }

    private void Start()
    {
        InstantiateWall();
        InstantiatePlayer();
        _levelPool.Add(_levelPartStart);
        _player.GetComponent<PlayerController>().SetFollowingWall(_targetWall);
    }

    private void Update()
    {

        if (_targetWall.gameObject.transform.position.x >
            _levelPool[_levelPool.Count - 1].GetComponentInChildren<LevelScript>().endPoint.position.x - 40f)
        {
            if (_levelPool.Count < Const.LEVEL_POOL_SIZE)
            {
                SpawnPart(_levelPartsToBuild[_randomNumber.Next(0, _levelPartsToBuild.Count - 1)],
                    _enemiesToBuild[_randomNumber.Next(0, _enemiesToBuild.Count - 1)]);
            }
            else
            {
                int rd_num = new Random().Next(0, Const.LEVEL_POOL_SIZE - 2);
                GameObject replacedPart = _levelPool[rd_num];
                _levelPool.Remove(_levelPool[rd_num]);
                replacedPart.transform.position =
                    _levelPool[_levelPool.Count - 1].GetComponentInChildren<LevelScript>().endPoint.position -
                    replacedPart.GetComponentInChildren<LevelScript>().startPoint.localPosition;
                _levelPool.Add(replacedPart);

                int t = _randomNumber.Next(1, 3);
                for (int i = 0; i < t; i++)
                {
                    MoveEnemyInPool(ref _enemiesPool);
                }
            }
        }

        foreach (var enemy in _enemiesPool)
        {
            if (_targetWall.transform.position.x > enemy.transform.position.x)
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
        _playerLoaded = player;
    }

    private void SpawnPart(GameObject levelPart, GameObject enemy)
    {
        GameObject newLevelPart = Instantiate(levelPart, _worldLevelPartContainer);
        newLevelPart.transform.position =
            _levelPool[_levelPool.Count - 1].GetComponentInChildren<LevelScript>().endPoint.position -
            newLevelPart.GetComponentInChildren<LevelScript>().startPoint.localPosition;

        int t = _randomNumber.Next(0, 3);
        for (int i = 0; i < t; i++)
        {
            if (_enemiesPool.Count > Const.ENEMIES_POOL_SIZE)
            {
                MoveEnemyInPool(ref _enemiesPool);
            }
            else
            {
                int distance = _randomNumber.Next(40, 100);
                GameObject newEnemy = Instantiate(enemy, _enemiesContainer);
                newEnemy.gameObject.SetActive(false);
                newEnemy.transform.position =
                    new Vector3(_targetWall.transform.position.x + distance, _randomNumber.Next(-3, 4), 0);
                        newEnemy.gameObject.SetActive(true);
                _enemiesPool.Add(newEnemy);
            }
        }
        _levelPool.Add(newLevelPart);
    }

    void MoveEnemyInPool(ref List<GameObject> list)
    {
        int distance = _randomNumber.Next(40, 100);
        GameObject replacedEnemy = list[1];
        list.Remove(list[1]);
        replacedEnemy.transform.position =
            new Vector3(_targetWall.transform.position.x + distance, _randomNumber.Next(-3, 4), 0);
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

        _levelPool.Clear();
        _levelPool.Add(_levelPartStart);

        foreach (Transform child in _enemiesContainer)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void LoadWall()
    {
        GameObject wall = Resources.Load<GameObject>("FollowingWall/FollowingEnemy");
        this._wall = wall;
    }

    private void InstantiateWall()
    {
       GameObject wallFollow = Instantiate(_wall, _wallContainer);
       wallFollow.transform.position = Const.WallStartPosition;
       _targetWall = wallFollow;
    }

    private void InstantiatePlayer()
    {
        GameObject player = Instantiate(_playerLoaded, _playerContainer);
        player.transform.position = Const.PlayerStartPosition;
        this._player = player;
    }
}
