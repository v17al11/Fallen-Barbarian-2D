using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UFE3D;

public class GoblinScene : MonoBehaviour
{
    [SerializeField] private Transform _positionToMove;
    [SerializeField] private float _speed;
    [SerializeField] private float _sceneDeley;
    [SerializeField] private GameObject _badEndScene;
    [SerializeField] private float _badEndSceneDeley;
    [SerializeField] private GameObject _hair;

    private void Start()
    {
        Invoke("EnableHair", 2.1f);
        /*Invoke("LoadBattleScene", _sceneDeley);*/
        Invoke("StartBadEndAnimation", _badEndSceneDeley);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _positionToMove.position, _speed * Time.deltaTime);

        if(Input.anyKeyDown)
        {
            LoadBattleScene();
        }
    }

    private void EnableHair()
    {
        _hair.SetActive(true);
    }

    private void LoadBattleScene()
    {
        SceneManager.LoadScene(0);
    }

    private void StartBadEndAnimation()
    {
        _badEndScene.SetActive(true);
    }
}
