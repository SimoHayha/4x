using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIStarHandler : GUIHandler
{
    public override void OnGUIStarted()
    {
        PreviewHandler.Instance.SetPreview(GameObject.Instantiate<GameObject>(data.GetPreview()));
    }

    public override void OnGUIUpdated()
    {
        throw new System.NotImplementedException();
    }

    public override void OnGUIEnded()
    {
        throw new System.NotImplementedException();
    }
}
