using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Universe : MonoBehaviour
{
    private int turn = 0;

    public void OnTurn()
    {
        turn++;
#if DEBUG
        GameObject.FindGameObjectWithTag("GUIDebugTurn").GetComponent<Text>().text = "Turn " + turn;
#endif
    }
}