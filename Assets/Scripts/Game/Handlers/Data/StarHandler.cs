using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarHandler : DataHandler
{
    public enum StarType
    {
        YellowDwarf,
        RedDwarf,
        RedGiant,
        BlueGiant,
        WhiteDwarf,
        BrownDwarf,
        NeutronStar,
        Pulsar,
        BinaryStar
    }

    public StarType Type;
    public Texture2D Texture;
    public Texture2D CoronaTexture;
    public PlanetHandler[] Planets = new PlanetHandler[5];
    public SolarSystem System = new SolarSystem();

    private GameObject gridModel;

    public StarHandler()
    {
        System.Star = this;
    }

    public override GameObject GetGridModel()
    {
        if (gridModel == null)
        {
            GameObject star = GameObject.Instantiate<GameObject>(PrefabLoader.Prefabs["stargrid".GetHashCode()] as GameObject);
            SGT_Star sgtStar = star.GetComponent<SGT_Star>();
            SGT_Corona sgtCorona = star.GetComponent<SGT_Corona>();

            sgtStar.SurfaceTexture.SetTexture(Texture, 0);
            sgtCorona.CoronaTexture = CoronaTexture;

            gridModel = star;

            return star;
        }
        else
            return gridModel;
    }
    
    public override GameObject GetPreview()
    {
        GameObject star = PrefabLoader.Prefabs["star".GetHashCode()] as GameObject;
        SGT_Star sgtStar = star.GetComponent<SGT_Star>();
        SGT_Corona sgtCorona = star.GetComponent<SGT_Corona>();

        sgtStar.SurfaceTexture.SetTexture(Texture, 0);
        sgtCorona.CoronaTexture = CoronaTexture;

        sgtCorona.CoronaTexture = CoronaTexture;

        return star;
    }
}
