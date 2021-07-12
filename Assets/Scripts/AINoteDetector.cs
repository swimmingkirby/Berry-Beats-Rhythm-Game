using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BerryBeats.Rework;
using BerryBeats.BattleSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AINoteDetector : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] Dancer dancer;
    private SpriteRenderer spriteRenderer;

    [Header("Properties")]
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Sprite pressedImage;

    [SerializeField] private ArrowDirection Dir;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultImage;       //? Failsafe
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Note")) return;

        dancer.Hit(Dir);
        levelLoader.DestroyNote(other.gameObject);


    }
}
