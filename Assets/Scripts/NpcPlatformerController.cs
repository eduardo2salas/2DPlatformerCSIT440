using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPlatformerController : PhysicsObject {

    private SpriteRenderer spriteRenderer;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    public bool isWalking;
    private bool direction;

    // Use this for initialization
    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        waitCounter = waitTime;
        walkCounter = walkTime;
        direction = true;
    }

    protected override void ComputeVelocity() {
        Vector2 move;
        if (direction && isWalking) {
            move = new Vector2(2, 0);
            walkCounter -= Time.deltaTime;
            if(walkCounter < 0) {
                isWalking = false;
            }
        } else if (isWalking){
            move = new Vector2(-2, 0);
            walkCounter -= Time.deltaTime;
            if (walkCounter < 0) {
                isWalking = false;
            }
        } else {
            move = Vector2.zero;
            if (waitCounter < 0) {
                isWalking = true;
                waitCounter = waitTime;
                walkCounter = walkTime;
                if(direction) {
                    direction = false;
                } else {
                    direction = true;
                }
            } else {
                waitCounter -= Time.deltaTime;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x < 0.01f) : (move.x > 0.01f));
        if (flipSprite) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        targetVelocity = move;
    }
}