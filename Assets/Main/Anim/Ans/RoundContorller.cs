using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFE3D;
using UFENetcode;
using UnityEngine.SceneManagement;
using System;

public class RoundContorller : MonoBehaviour
{
    [SerializeField] private GameObject _badEndScene;
    [SerializeField] private GameObject _backgroundAnim;
    [SerializeField] private float _maxYPos;
    [SerializeField] private UFE3D.CharacterInfo _enemy;

    private GameObject _background;
    private GameObject _gameUI;
    private bool _roundEnd;
    private bool _roundStart;

    private static bool _secondStart;

    private void Awake()
    {
        UFE.OnRoundBegins += OnRoundBegins;
        UFE.OnRoundEnds += OnRoundEnds;
        UFE.OnBlock += OnBlock;
        UFE.OnMove += OnMove;

        /*_background = GameObject.Find("Background");
        _background.SetActive(false);*/

        if (_secondStart)
        {
            UFE.StartVersusModeAfterBattleScreen();
            /*_background.SetActive(true);*/
        }
    }

    private void OnMove(MoveInfo move, ControlsScript player)
    {
        Debug.Log(move);

        if(player.currentBasicMove == BasicMoveReference.BlockingCrouchingPose)
        {
            

        }
    }

    private void OnBlock(HitBox strokeHitBox, MoveInfo move, ControlsScript player)
    {
        var enemy = UFE.GetPlayer1();


        var playerControl = UFE.GetControlsScript(1);
        var enemyControl = UFE.GetControlsScript(2);

        /*Debug.Log(player.name);*/

        Debug.Log(strokeHitBox.type);

        /*Debug.Log(move.name);*/

        if (player.name == "Player2" && move.name == "RollMove")
        {
            enemyControl.currentLifePoints -= 10;
        }

        if (player.name == "Player1" && move.name == "RollMove")
        {
            Debug.Log("Damage Back");
            playerControl.currentLifePoints -= 10;
        }

        if (player.name == "Player2" && move.name == "Move_Kick")
        {
            enemyControl.currentLifePoints -= 10;
        }

        if (player.name == "Player1" && move.name == "Move_Kick")
        {
            Debug.Log("Damage Back");
            playerControl.currentLifePoints -= 10;
        }

        if (player.name == "Player1" && move.name == "jump_kick_Move")
        {
            enemyControl.currentLifePoints -= 15;
        }

        if (player.name == "Player2" && move.name == "jump_kick_Move")
        {
            playerControl.currentLifePoints -= 15;
        }
    }

    private void OnRoundBegins(int newInt)
    {
        _roundStart = true;
    }

    private void OnRoundEnds(ControlsScript winner, ControlsScript loser)
    {
        _gameUI = GameObject.Find("CanvasGroup");
        
        UFE.StartVersusModeAfterBattleScreen(0.1f);
        _roundEnd = true;
    }

    private void LoadGoblinScene()
    {
        _secondStart = true;
        SceneManager.LoadScene(1);
    }

    private float _lastEnemyYPos, _lastPlayerYPos;

    private void CheckHitInJump(ControlsScript player, ref float lastYPos)
    {
        /*Debug.Log(player.currentState);*/
        /*Debug.Log(player.currentHit != null);*/

        if ((player.currentState == PossibleStates.ForwardJump 
            || player.currentState == PossibleStates.NeutralJump 
            || player.currentState == PossibleStates.BackJump) 
            && (player.currentHit != null))
        {
            var diff = Mathf.Abs(lastYPos - player.transform.position.y);

            player.mirror = -1;

            if (diff < 0.01f)
            {
                player.Physics.ForceGrounded();
            }

            lastYPos = player.transform.position.y;
        }
    }

    /*private void CheckSuperChopPosition(ControlsScript player)
    {
        if (player.currentMove.name == "SuperChop_Move" && player.transform.position.y > _maxYPos)
        {
            

            player.Physics.ForceGrounded();
        }
    }*/

    private void Update()
    {
        if(_roundStart)
        {
            var player = UFE.GetControlsScript(1);
            var enemy = UFE.GetControlsScript(2);

            CheckHitInJump(enemy, ref _lastEnemyYPos);
            CheckHitInJump(player, ref _lastPlayerYPos);

            player.isAirRecovering = false;
            enemy.isAirRecovering = false;

            if (player.currentLifePoints <= 0 || enemy.currentLifePoints <= 0)
            {
                _gameUI = GameObject.Find("CanvasGroup");
                
                _roundEnd = true;
            }
        }
        if (_roundEnd)
        {
            if(_gameUI != null)
                _gameUI.SetActive(false);

            Invoke("LoadGoblinScene", 2f);
        }
    }
}
