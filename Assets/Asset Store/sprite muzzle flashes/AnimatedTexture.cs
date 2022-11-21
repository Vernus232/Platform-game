using UnityEngine;
using System.Collections;

public class AnimatedTexture : MonoBehaviour
{
    public float fps = 30.0f;
    public Sprite[] frames;

    private int frameIndex;
    private SpriteRenderer rendererMy;
    
    void Start()
    {
        rendererMy = GetComponent<SpriteRenderer>();
    }

    public IEnumerator ShotAnimation()
    {
        for (int i = 0; i < frames.Length; i++)
        {
            yield return new WaitForSeconds(1 / fps);
            rendererMy.sprite = frames[i];
        }
    }
}