using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class GUIHandler
{
    private GUIContext context_;
    protected GUIContext context
    {
        get
        {
            if (context_ == null)
                context_ = GameObject.FindGameObjectWithTag("GUIContext").GetComponent<GUIContext>();
            return context_;
        }
        set
        {
            context_ = value;
        }
    }
    protected DataHandler data;

    public void SetDataHandler(DataHandler data_)
    {
        data = data_;
    }

    public abstract void OnGUIStarted();
    public abstract void OnGUIUpdated();
    public abstract void OnGUIEnded();
}
