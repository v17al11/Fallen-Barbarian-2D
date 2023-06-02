using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFE3D;
using UFENetcode;
using UnityEngine.SceneManagement;
using System;

public class RoundContorllerMy : MonoBehaviour
{
    [SerializeField] private GameObject _badEndScene;
    [SerializeField] private GameObject _backgroundAnim;
    [SerializeField] private float _maxYPos;
    [SerializeField] private MoveInfo _moveInfo;
    [SerializeField] private MoveInfo _rollMove;
    /*[SerializeField] private UFE3D.CharacterInfo _enemy;*/

    private GameObject _background;
    private GameObject _gameUI;
    private bool _roundEnd;
    private bool _roundStart;

    private static bool _secondStart;

    private void Awake()
    {
        _rollMove.hits[0].unblockable = false;

        UFE.OnRoundBegins += OnRoundBegins;
        UFE.OnRoundEnds += OnRoundEnds;
        UFE.OnBlock += OnBlock;
        UFE.OnBasicMove += UFE_OnBasicMove;

        _background = GameObject.Find("Background");
        _background.SetActive(false);

        if (_secondStart)
        {
            UFE.StartVersusModeAfterBattleScreen();
            _background.SetActive(true);
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
            _rollMove.hits[0].unblockable = false;
    }

    private void OnBlock(HitBox strokeHitBox, MoveInfo move, ControlsScript player)
    {
        var enemy = UFE.GetPlayer1();


        var playerControl = UFE.GetControlsScript(1);
        var enemyControl = UFE.GetControlsScript(2);

        Debug.Log(player.name);

        Debug.Log(strokeHitBox.type);

        /*Debug.Log(move.name);*/

        if (player.name == "Player2" && move.name == "RollMove" && strokeHitBox.type == HitBoxType.low)
        {
            
            playerControl.currentLifePoints -= 100;
        }

        if (player.name == "Player1" && move.name == "RollMove" && strokeHitBox.type == HitBoxType.low)
        {
            Debug.Log("Damage Back");
            
            enemyControl.currentLifePoints -= 100;
        }

        /*if (player.name == "Player2" && move.name == "HgAttackMove" && strokeHitBox.type == HitBoxType.low)
        {
            enemyControl.currentLifePoints -= 10;
        }

        if (player.name == "Player1" && move.name == "HgAttackMove" && strokeHitBox.type == HitBoxType.low)
        {
            Debug.Log("Damage Back");
            playerControl.currentLifePoints -= 10;
        }*/

        /*if (player.name == "Player1" && move.name == "jump_kick_Move" && strokeHitBox.type == HitBoxType.low)
        {
            playerControl.currentLifePoints -= 15;
        }

        if (player.name == "Player2" && move.name == "jump_kick_Move" && strokeHitBox.type == HitBoxType.low)
        {
            enemyControl.currentLifePoints -= 15;
        }

        if (player.name == "Player2" && move.name == "RollMove" && strokeHitBox.type == HitBoxType.high)
        {
            enemyControl.currentLifePoints -= 10;
            
        }*/

        if (player.name == "Player1" && move.name == "RollMove" && strokeHitBox.type == HitBoxType.high)
        {
            /*_rollMove.hits[0].unblockable = true;*/
            /*playerControl.currentLifePoints -= 10;*/

            /*playerControl.KillCurrentMove();*/

            /*playerControl.currentBasicMove = BasicMoveReference.HitStandingMidKnockdown;*/
            /*player.currentHit.hitStunOnBlock = 1;*/
            //playerControl.currentState = PossibleStates.Down;
            /*playerControl.standUpOverride = StandUpOptions.HighKnockdownClip;
            playerControl.currentSubState = SubStates.Stunned;
            playerControl.stunTime = 1;*/
        }

        /*if (player.name == "Player2" && move.name == "Move_Kick" && strokeHitBox.type == HitBoxType.high)
        {
            playerControl.currentLifePoints -= 10;
        }

        if (player.name == "Player1" && move.name == "HgAttackMove" && strokeHitBox.type == HitBoxType.high)
        {
            Debug.Log("Damage Back");
            enemyControl.currentLifePoints -= 10;
        }*/

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
            var player = UFE.GetControlsScript(1);
            var enemy = UFE.GetControlsScript(2);
            GameObject playerObj = GameObject.Find("Player1");
            var playerPhysics = playerObj.GetComponent<PhysicsScript>();

            CheckHitInJump(enemy, ref _lastEnemyYPos);
            CheckHitInJump(player, ref _lastPlayerYPos);

            player.isAirRecovering = false;
            enemy.isAirRecovering = false;

            player.airRecoveryType = AirRecoveryType.AllowMoves;
            enemy.airRecoveryType = AirRecoveryType.AllowMoves;

            if (player.currentLifePoints <= 0 || enemy.currentLifePoints <= 0)
            {
                _gameUI = GameObject.Find("CanvasGroup");
                
                _roundEnd = true;
            }

            if(player.currentSubState == SubStates.Stunned)
            {
                /*player.currentBasicMove = BasicMoveReference.HitStandingMidKnockdown;*/
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
