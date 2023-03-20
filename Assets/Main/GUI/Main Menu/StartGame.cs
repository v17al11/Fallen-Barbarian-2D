using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFE3D;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public UFE3D.CharacterInfo Player1;
    public UFE3D.CharacterInfo Player2;

    

    public void StartGameButton()
    {
        UFE.StartGame();
        UFE.SetPlayer1(Player1);
        UFE.SetPlayer2(Player2);
        UFE.SetStage("Main");
    }
}
