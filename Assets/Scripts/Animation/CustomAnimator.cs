/*
 * A better solution for animation
 * Author: Hugo4IT
 */

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CustomAnimator : MonoBehaviour
{
    [SerializeField] private float frameDelay = 0.333f;
    [SerializeField] private Sprite[] frames;
    [SerializeField] private bool loop = true;

    private int frame;
    private float timer;
    private SpriteRenderer spriteRenderer;
    private void Start(){spriteRenderer=GetComponent<SpriteRenderer>();}

    // While, yes, this is still an Update() method, it only does something every [frameDelay] seconds.
    void Update()
    {
        // By adding deltaTime each frame, it will count in seconds
        timer += Time.deltaTime;
        
        if (timer > frameDelay)
        {
            frame += 1;
            if (frame > frames.Length)
                if (loop)
                    frame = 0;
                else
                    frame = frames.Length - 1;

            UpdateFrame(); 
            timer = 0;
        }
    }

    /// <summary>
    /// Set sprite to current frame
    /// </summary>
    private void UpdateFrame()
    {
        if (frame < frames.Length)
            spriteRenderer.sprite = frames[frame];
    }
    
    /// <summary>
    /// Sets frames and resets animation
    /// </summary>
    /// <param name="input"></param>
    public void SetFrames(Sprite[] input)
    {
        timer = frameDelay;
        frames = input;
        frame = 0;
    }
}
