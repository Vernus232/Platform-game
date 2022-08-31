using UnityEngine;
using System.Collections;
using DragonBones;

public class KatanaAnimation : MonoBehaviour
{
    public float fps = 30.0f;
    public Sprite[] frames;

    private SpriteRenderer rendererMy;
    [SerializeField] GameObject playerSprite;
    [SerializeField] GameObject katanaSprite;
    [SerializeField] GameObject rukiKatanaSprite;

    void Start()
    {
        rendererMy = GetComponent<SpriteRenderer>();
    }

    public IEnumerator SwingAnimation()
    {
        rendererMy.enabled = true;
        playerSprite.SetActive(false);
        katanaSprite.SetActive(false);
        rukiKatanaSprite.SetActive(false);

        for (int i = 0; i < frames.Length; i++)
        {
            rendererMy.sprite = frames[i];
            yield return new WaitForSeconds(1 / fps);
        }

        rendererMy.enabled = false;
        playerSprite.SetActive(true);
        katanaSprite.SetActive(true);
        rukiKatanaSprite.SetActive(true);
    }

    public void OnWeaponDisable()
    {
        StopAllCoroutines();

        rendererMy.enabled = false;
        playerSprite.SetActive(true);
        katanaSprite.SetActive(true);
        rukiKatanaSprite.SetActive(true);
    }
}