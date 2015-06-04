using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class DataHandler
{
    public GUIHandler GUI;

    public void SetGUIHandler(GUIHandler GUI_)
    {
        GUI = GUI_;
        GUI.SetDataHandler(this);
    }

    public abstract GameObject GetGridModel();
    public abstract GameObject GetPreview();
}
