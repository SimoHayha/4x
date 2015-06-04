using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class GUIHandler
{
    protected GUIContext context;
    protected DataHandler data;

    public void SetDataHandler(DataHandler data_)
    {
        data = data_;
    }

    public abstract void OnGUIStarted();
    public abstract void OnGUIUpdated();
    public abstract void OnGUIEnded();
}
