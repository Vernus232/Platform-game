using UnityEngine;

public abstract class Item : BaseEntity
{
    public ItemEnum Type { get; set; }
    [SerializeField] private float timeToLive = 30f;

    protected virtual void Awake()
    {
        if (timeToLive > 0)
        {
            Destroy(gameObject, timeToLive);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && TryDoActionOnPlayer())
        {
            Destroy(gameObject);
        }
    }

    protected abstract bool TryDoActionOnPlayer();
}