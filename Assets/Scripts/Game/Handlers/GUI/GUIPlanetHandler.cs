using UnityEngine;
using System.Collections;

public class GUIPlanetHandler : GUIHandler
{
    public override void OnGUIStarted()
    {
        PreviewHandler.Instance.SetPreview(GameObject.Instantiate<GameObject>(data.GetPreview()));
    }

    public override void OnGUIUpdated()
    {
    }

    public override void OnGUIEnded()
    {
    }
}
