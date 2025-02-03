using System.Collections;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Settings Settings;

    [SerializeField] private SettingsMenu SettingsMenu;

    private IEnumerator Start()
    {
        Settings.Init();

        SettingsMenu.Init();

        yield return null;
    }
}
