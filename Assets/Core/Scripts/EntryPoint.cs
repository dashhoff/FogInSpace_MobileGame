using System.Collections;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{

    private IEnumerator Start()
    {
        Application.targetFrameRate = Settings.Instance.TargetFPS;

        yield return null;
    }
}
