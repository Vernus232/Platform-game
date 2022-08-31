using UnityEngine;
using System.Collections;
using DragonBones;

public class KatanaAnimation : MonoBehaviour
{
    public float fps = 30.0f;
    public Sprite[] frames;

    private SpriteRenderer rendererMy;
    [SerializeField] GameObject playerSprite;
    
    void Start()
    {
        rendererMy = GetComponent<SpriteRenderer>();
    }

    public IEnumerator SwingAnimation()
    {
        rendererMy.enabled = true;
        playerSprite.SetActive(false);
        for (int i = 0; i < frames.Length; i++)
        {
            yield return new WaitForSeconds(1 / fps);
            rendererMy.sprite = frames[i];
        }
        rendererMy.enabled = false;
        playerSprite.SetActive(true);
    }
}