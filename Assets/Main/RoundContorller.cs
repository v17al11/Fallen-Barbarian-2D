using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFE3D;
using UFENetcode;
using UnityEngine.SceneManagement;
using System;

public class RoundContorller : MonoBehaviour
{
    [SerializeField] private float _maxYPos;
    [SerializeField] private MoveInfo _rollMove, _moveKick;
    [SerializeField] private RoundContorller _roundContorller;

    private GameObject _background;
    private GameObject _gameUI;
    private bool _roundEnd;
    private bool _roundStart;
    private ControlsScript _player;
    private ControlsScript _enemy;

    private static bool _secondStart;
    public static bool EnemyWinner;

    private void Awake()
    {
        _rollMove.hits[0].unblockable = false;

        UFE.OnRoundBegins += OnRoundBegins;
        UFE.OnRoundEnds += OnRoundEnds;
        UFE.OnBlock += OnBlock;
        UFE.OnBasicMove += UFE_OnBasicMove;

        /*_background = GameObject.Find("Background");
        _background.SetActive(false);*/

        if (_secondStart)
        {
            UFE.StartVersusModeAfterBattleScreen();
            /*_background.SetActive(true);*/
        }
    }

    private void UFE_OnBasicMove(BasicMoveReference basicMove, ControlsScript player)
    {
        if (basicMove == BasicMoveReference.BlockingHighHit || basicMove == BasicMoveReference.BlockingHighPose)
        {
            _rollMove.hits[0].unblockable = true;
            Debug.Log(_rollMove.hits[0].unblockable);
        }
        else
        {
            if (_player == null || _enemy == null)
                return;

            if (_player.currentBasicMove is not (BasicMoveReference.BlockingHighHit or BasicMoveReference.BlockingHighPose) 
                && _enemy.currentBasicMove is not (BasicMoveReference.BlockingHighHit or BasicMoveReference.BlockingHighPose))
                _rollMove.hits[0].unblockable = false;

            if (basicMove == BasicMoveReference.HitStandingMidKnockdown)
            {
                var otherPlayer = _enemy == player ? _player : _enemy;

                if (otherPlayer.currentMove.name == _moveKick.name)
                {
                    player.Physics.ResetForces(true, false);
                    player.Physics.AddForce(new FPLibrary.FPVector(-20, 0, 0), otherPlayer.transform.position.x > player.transform.position.x ? 1 : -1);
                }
            }
        }
    }

    private void OnBlock(HitBox strokeHitBox, MoveInfo move, ControlsScript player)
    {
        var enemy = UFE.GetPlayer1();


        var playerControl = UFE.GetControlsScript(1);
        var enemyControl = UFE.GetControlsScript(2);

        if (player.name == "Player2" && move.name == "RollMove" && strokeHitBox.type == HitBoxType.low)
        {
            
            playerControl.currentLifePoints -= 100;
        }

        if (player.name == "Player1" && move.name == "RollMove" && strokeHitBox.type == HitBoxType.low)
        {
            Debug.Log("Damage Back");
            
            enemyControl.currentLifePoints -= 100;
        }

        if (player.name == "Player2" && move.name == "HgAttackMove" && strokeHitBox.type == HitBoxType.low)
        {
            enemyControl.currentLifePoints -= 10;
        }

        if (player.name == "Player1" && move.name == "HgAttackMove" && strokeHitBox.type == HitBoxType.low)
        {
            Debug.Log("Damage Back");
            playerControl.currentLifePoints -= 10;
        }

        if (player.name == "Player1" && move.name == "jump_kick_Move" && strokeHitBox.type == HitBoxType.low)
        {
            playerControl.currentLifePoints -= 15;
        }

        if (player.name == "Player2" && move.name == "jump_kick_Move" && strokeHitBox.type == HitBoxType.low)
        {
            enemyControl.currentLifePoints -= 15;
        }

        if (player.name == "Player1" && move.name == "jump_kick_Move" && strokeHitBox.type == HitBoxType.high)
        {
            enemyControl.currentLifePoints -= 15;
        }

        if (player.name == "Player2" && move.name == "jump_kick_Move" && strokeHitBox.type == HitBoxType.high)
        {
            playerControl.currentLifePoints -= 15;
        }
    }

    private void OnRoundBegins(int newInt)
    {
        _roundStart = true;

        _player = UFE.GetControlsScript(1);
        _enemy = UFE.GetControlsScript(2);
    }

    private void OnRoundEnds(ControlsScript winner, ControlsScript loser)
    {
        if(winner == _enemy)
        {
            EnemyWinner = true;
        }

        _gameUI = GameObject.Find("CanvasGroup");
        
        UFE.StartVersusModeAfterBattleScreen(0.1f);
        _roundEnd = true;
    }

    private void LoadGoblinScene()
    {
        _secondStart = true;

        if(EnemyWinner)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(2);
    }

    private float _lastEnemyYPos, _lastPlayerYPos;

    private void CheckHitInJump(ControlsScript player, ref float lastYPos)
    {
        if ((player.currentState == PossibleStates.ForwardJump 
            || player.currentState == PossibleStates.NeutralJump 
            || player.currentState == PossibleStates.BackJump) 
            && (player.currentHit != null))
        {
            var diff = Mathf.Abs(lastYPos - player.transform.position.y);

            player.Physics.currentAirJumps = 0;

            if (diff < 0.01f)
            {
                player.Physics.ForceGrounded();
            }

            lastYPos = player.transform.position.y;
        }
    }

    private void Update()
    {
        if(_roundStart)
        {
            CheckHitInJump(_enemy, ref _lastEnemyYPos);
            CheckHitInJump(_player, ref _lastPlayerYPos);

            _player.isAirRecovering = false;
            _enemy.isAirRecovering = false;

            _player.airRecoveryType = AirRecoveryType.AllowMoves;
            _enemy.airRecoveryType = AirRecoveryType.AllowMoves;

            if (_player.currentLifePoints <= 0 || _enemy.currentLifePoints <= 0)
            {
                _enemy.isDead = true;
                /*_roundContorller.OnRoundEnds(_player, _enemy);*/
                /*_gameUI = GameObject.Find("CanvasGroup");

                _roundEnd = true;*/
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
