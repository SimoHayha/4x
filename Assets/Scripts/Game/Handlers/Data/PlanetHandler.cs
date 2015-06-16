using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetHandler : DataHandler
{
    public SolarSystem System;

    private List<BuildingHandler> buildings = new List<BuildingHandler>();

    public override GameObject GetGridModel()
    {
        GameObject planet = GameObject.Instantiate<GameObject>(PrefabLoader.Prefabs["planetgrid".GetHashCode()] as GameObject);
        planet.GetComponent<SGT_Planet>().PlanetLightSource = System.Star.GetGridModel().GetComponent<SGT_LightSource>();

        return planet;
    }

    public override GameObject GetPreview()
    {
        GameObject planet = GameObject.Instantiate<GameObject>(PrefabLoader.Prefabs["planet".GetHashCode()] as GameObject);

        return planet;
    }
}
