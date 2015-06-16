using UnityEngine;
using System.Collections;

public class GUIPlanetHandler : GUIHandler
{
    public override void OnGUIStarted()
    {
        PreviewHandler.Instance.SetPreview(GameObject.Instantiate<GameObject>(data.GetPreview()));

        context.Planet.gameObject.SetActive(true);
        context.Planet.PlanetName.text = "Some planet";

        int r = Random.Range(0, 10);
        for (int i = 0; i < r; ++i)
        {
            GameObject building = GameObject.Instantiate<GameObject>(context.Planet.ButtonBuilding);
            building.transform.SetParent(context.Planet.Buildings.transform);
            building.transform.localPosition = context.Planet.ButtonBuilding.transform.localPosition;
            building.transform.localRotation = context.Planet.ButtonBuilding.transform.localRotation;
            building.transform.localScale = context.Planet.ButtonBuilding.transform.localScale;
        }
    }

    public override void OnGUIUpdated()
    {
    }

    public override void OnGUIEnded()
    {
        context.Planet.ClearBuildings();
        context.Planet.gameObject.SetActive(false);
    }
}
