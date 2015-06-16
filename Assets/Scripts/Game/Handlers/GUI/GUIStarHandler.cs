using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIStarHandler : GUIHandler
{
    public override void OnGUIStarted()
    {
        PreviewHandler.Instance.SetPreview(GameObject.Instantiate<GameObject>(data.GetPreview()));

        context.Star.gameObject.SetActive(true);
    }

    public override void OnGUIUpdated()
    {
    }

    public override void OnGUIEnded()
    {
        context.Star.gameObject.SetActive(false);
    }
}
