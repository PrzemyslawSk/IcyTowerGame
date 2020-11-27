using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAdjuster : MonoBehaviour
{
    [SerializeField] SpriteRenderer platformSpriteRenderer;
    [SerializeField] BoxCollider2D platformTrigger;
    [SerializeField] BoxCollider2D platformCollider;

    public void CollidersAdjust() // Adjust the Renderer size and call this later on
    {
        platformCollider.size = new Vector2(platformSpriteRenderer.size.x, platformCollider.size.y);
        platformTrigger.size = platformSpriteRenderer.size + new Vector2(0.5f, 0.25f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
        //So remember, in order not to get lost, just remember in which object we are right now.
        //Which is the platform, each and every one of them.
    {
        if (collision.CompareTag("Player")) { //so we are the platform and a player hit our trigger (the outer collider)
            //we gotta prepare for the impact, but only if he is above us. If he is below us, we have to make way.
            if (collision.transform.position.y - transform.position.y > 0.5f) {
                platformCollider.enabled = true;
                //We are stopping coroutine as player hit a platform from above
                collision.GetComponent<Player>().jumping = false;
            }
            else { //And if player is not above us, then we're disabling collider
                platformCollider.enabled = false;
            }
        }
    }
}
