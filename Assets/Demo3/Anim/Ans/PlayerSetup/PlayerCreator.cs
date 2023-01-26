using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    [SerializeField] private GameObject _playerConfig, _botConfig;

    private void Awake()
    {
        if (name == "Player1")
        {
            var setuper = gameObject.AddComponent<PlayerSetuper>();
            /*setuper.SetupConfig(_playerConfig);*/
        }
        /*else
        {
            var setuper = gameObject.AddComponent<BotSetuper>();
            setuper.SetupConfig(_botConfig);
        }*/
    }
}
