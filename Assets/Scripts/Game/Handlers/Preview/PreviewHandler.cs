using UnityEngine;
using System.Collections;

public class PreviewHandler : MonoBehaviour
{
    public static PreviewHandler Instance;

    private GameObject currentPreview;

    void Awake()
    {
        Instance = this;
    }

    public void SetPreview(GameObject preview)
    {
        if (currentPreview != null)
            Destroy(currentPreview);
        currentPreview = preview;
        if (currentPreview != null)
        {
            preview.transform.parent = transform;
            preview.transform.localPosition = Vector3.zero;
            currentPreview = preview;
        }
    }
}
