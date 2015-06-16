using UnityEngine;
using System.Collections;

public class GUIGridHandler : GUIHandler
{
    public override void OnGUIStarted()
    {
        PreviewHandler.Instance.SetPreview(null);
    }

    public override void OnGUIUpdated()
    {
    }

    public override void OnGUIEnded()
    {
    }
}