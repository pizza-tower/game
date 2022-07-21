using UnityEngine;
using System.Collections;

public class CameraShaking : MonoBehaviour
{
    public static CameraShaking Action { set; get; }
    public bool playOnAwake = true;
    private bool enable = false, disable = true;
    public float duration = 5f, magnitude = 1f;
    public bool infiniteDuration;

    private void Awake()
    {
        Action = this;
    }

    private void Start()
    {
        if (playOnAwake)
        {
            disable = false;
            enable = true;
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            if(!disable || playOnAwake)
                transform.localPosition = new Vector3(x, y, originalPos.z);
            else
            {
                transform.localPosition = originalPos;
                break;
            }
            elapsed += Time.deltaTime;
            if (infiniteDuration && !disable && enable)
            {
                duration = 800000000f;
            }
            yield return null;
        }
        if (!infiniteDuration && !disable && enable)
        {
            enable = false;
            disable = true;
            transform.localPosition = originalPos;
            playOnAwake = false;
        }
    }

    public void Play()
    {
        if (!playOnAwake && !enable && disable)
        {
            disable = false;
            enable = true;
            StartCoroutine(Shake());
        }
    }

    public void Stop()
    {
        disable = true;
        enable = false;
        playOnAwake = false;
    }
}
