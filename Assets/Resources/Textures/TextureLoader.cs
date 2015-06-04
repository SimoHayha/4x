using UnityEngine;
using System.Collections;
using UnityEditor;

[InitializeOnLoad]
public class TextureLoader
{
    public static Texture2D[] StarTextures;
    public static Texture2D[] CoronaTextures;

    static TextureLoader()
    {
        StarTextures = Resources.LoadAll<Texture2D>("Textures/Stars");
        CoronaTextures = Resources.LoadAll<Texture2D>("Textures/Corona");
    }
}
