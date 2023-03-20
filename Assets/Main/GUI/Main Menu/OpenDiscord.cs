using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDiscord : MonoBehaviour
{
    public string Url = "https://discord.gg/4q9YJgm";

    public void OpenURL()
    {
        Application.OpenURL(Url);
    }
}
