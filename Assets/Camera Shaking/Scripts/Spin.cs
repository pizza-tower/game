using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(0f, 0f, speed);

        if (Input.GetMouseButtonDown(0))
        {
            CameraShaking.Action.Stop();
        }
    }
}
