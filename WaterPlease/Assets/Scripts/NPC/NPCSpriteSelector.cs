using System.Collections.Generic;
using UnityEngine;

public class NPCSpriteSelector : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites = new();
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        int random = Random.Range(0, sprites.Count);
        spriteRenderer.sprite = sprites[random];
    }
}
