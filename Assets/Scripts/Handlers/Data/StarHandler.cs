using UnityEngine;
using System.Collections;

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

    public override GameObject GetGridModel()
    {
        GameObject star = PrefabLoader.Prefabs["stargrid".GetHashCode()] as GameObject;
        SGT_Star sgtStar = star.GetComponent<SGT_Star>();
        SGT_Corona sgtCorona = star.GetComponent<SGT_Corona>();

        sgtStar.SurfaceTexture.SetTexture(Texture, 0);
        sgtCorona.CoronaTexture = CoronaTexture;

        return star;
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
