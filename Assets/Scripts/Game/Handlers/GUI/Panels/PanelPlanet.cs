using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelPlanet : MonoBehaviour
{
    public Text PlanetName;
    public GameObject Buildings;

    public GameObject ButtonBuilding;

    public void ClearBuildings()
    {
        for (int i = 0; i < Buildings.transform.childCount; ++i)
            Destroy(Buildings.transform.GetChild(i).gameObject);
    }
}