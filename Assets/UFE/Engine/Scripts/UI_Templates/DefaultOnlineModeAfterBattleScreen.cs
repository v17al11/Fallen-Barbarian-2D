using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DefaultOnlineModeAfterBattleScreen : OnlineModeAfterBattleScreen
{
    #region public instance properties
    public AudioClip onLoadSound;
    public AudioClip music;
    public AudioClip selectSound;
    public AudioClip cancelSound;
    public AudioClip moveCursorSound;
    public bool stopPreviousSoundEffectsOnLoad = false;
    public float delayBeforePlayingMusic = 0.1f;
    #endregion

    #region public override methods
    public override void DoFixedUpdate(
        IDictionary<InputReferences, InputEvents> player1PreviousInputs,
        IDictionary<InputReferences, InputEvents> player1CurrentInputs,
        IDictionary<InputReferences, InputEvents> player2PreviousInputs,
        IDictionary<InputReferences, InputEvents> player2CurrentInputs
    )
    {
        base.DoFixedUpdate(player1PreviousInputs, player1CurrentInputs, player2PreviousInputs, player2CurrentInputs);

        this.DefaultNavigationSystem(
            player1PreviousInputs,
            player1CurrentInputs,
            player2PreviousInputs,
            player2CurrentInputs,
            this.moveCursorSound,
            this.selectSound,
            this.cancelSound,
            this.GoToMainMenu
        );
    }

    public override void OnShow()
    {
        base.OnShow();

        if (this.music != null)
        {
            UFE.DelayLocalAction(delegate () { UFE.PlayMusic(this.music); }, this.delayBeforePlayingMusic);
        }

        if (this.stopPreviousSoundEffectsOnLoad)
        {
            UFE.StopSounds();
        }

        if (this.onLoadSound != null)
        {
            UFE.DelayLocalAction(delegate () { UFE.PlaySound(this.onLoadSound); }, this.delayBeforePlayingMusic);
        }

    }

    public void Start()
    {
        UFE.multiplayerAPI.OnPlayerDisconnectedFromMatch += this.OnPlayerDisconnectedFromMatch;

        // Remove subscription from UFE so the game doesn't go for the 'disconnected' screen
        //UFE.multiplayerAPI.OnPlayerDisconnectedFromMatch -= UFE.OnPlayerDisconnectedFromMatch;
    }

    // Use this event to deactivate repeat battle or character select buttons if the other player disconnects
    public void OnPlayerDisconnectedFromMatch(MultiplayerAPI.PlayerInformation player = null)
    {
        UFE.multiplayerAPI.OnPlayerDisconnectedFromMatch -= this.OnPlayerDisconnectedFromMatch;

        GameObject repeatBattleBtn = GameObject.Find("Button_Repeat_Battle");
        GameObject charSelectionScreen = GameObject.Find("Button_Character_Selection_Screen");
        if (repeatBattleBtn != null) repeatBattleBtn.GetComponent<Button>().interactable = false;
        if (charSelectionScreen != null) charSelectionScreen.GetComponent<Button>().interactable = false;
    }
    #endregion
}
