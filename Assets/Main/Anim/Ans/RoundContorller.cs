using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFE3D;
using System;
using UnityEngine.SceneManagement;

public class RoundContorller : MonoBehaviour
{
    [SerializeField] private GameObject _badEndScene;
    [SerializeField] private GameObject _backgroundAnim;
    [SerializeField] private GameObject _background;

    private GameObject _gameUI;
    private bool _roundEnd;
    private static bool _secondStart;

    private void Awake()
    {
        UFE.OnRoundEnds += OnRoundEnds;
        UFE.OnParry += OnParry;

        if(_secondStart)
        {
            UFE.StartVersusModeAfterBattleScreen();
        }
    }

    private void OnRoundEnds(ControlsScript winner, ControlsScript loser)
    {
        _gameUI = GameObject.Find("CanvasGroup");
        
        /*Debug.Log("OnRoundEnds: " + loser.character.name);
        Debug.Log("OnRoundEnds: " + (loser.character.GetComponent<Animator>() == null));*/
        /*loser.character.GetComponent<MecanimControl>().Play("Outro");*/
        /*_gameUI.SetActive(false);*/
        UFE.StartVersusModeAfterBattleScreen(0.1f);
        _roundEnd = true;
        
    }

    private void LoadGoblinScene()
    {
        _secondStart = true;
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (_roundEnd)
        {
            _gameUI.SetActive(false);
            /*UFE.StartMainMenuScreen();*/
            Invoke("LoadGoblinScene", 2f);
        }
    }

    private void OnParry(HitBox strokeHitBox, MoveInfo move, ControlsScript player)
    {
        var enemy = UFE.GetPlayer2();
        Debug.Log("OnParry: " + player.character.name);
        Debug.Log(move.name);
        Debug.Log(strokeHitBox);
        if (move.name == "RollMove")
        {
            enemy.lifePoints -= 10;
        }
    }
}
