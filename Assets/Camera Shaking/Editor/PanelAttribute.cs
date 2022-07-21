using UnityEngine;

public class PanelAttribute : MonoBehaviour
{
[UnityEditor.MenuItem("GameObject/Camera Shaking")]
    public static void addCameraShaking()
    {
        Instantiate(Resources.Load<GameObject>("Prefab/Camera Shaking"));
    }
}
