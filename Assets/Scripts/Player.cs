using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] LayerMask ignoredLayers;
    [SerializeField] Rigidbody2D body;
    [SerializeField] GameObject defaultSpriteObject;
    [SerializeField] GameObject jumpingSpriteObject;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] Transform raycastTransform;
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpPower = 5;
    [SerializeField] float moveJumpFactor = 5;
    [SerializeField] public bool jumping = false;

    private void Start() {
        jumpingSpriteObject.SetActive(false);
        ignoredLayers = ~ignoredLayers; //~ is a logical operation, masks operate kinda differently, so this is how we "flip" our mask
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        if (x != 0)
        {
            if(Mathf.Abs(body.velocity.x) <= maxSpeed)
                body.AddForce(Vector2.right * x * (jumping ? speed/2 : speed), ForceMode2D.Force);
            defaultSpriteObject.transform.eulerAngles = x > 0 ? Vector3.zero : new Vector3(0, 180, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            if (Physics2D.Raycast(raycastTransform.position, Vector2.down, 0.02f, ignoredLayers))
            {
                body.AddForce(Vector2.up * (jumpPower + Mathf.Abs(body.velocity.x) * moveJumpFactor), ForceMode2D.Impulse);
                StartCoroutine(JumpCoroutine(jumpPower));
            }
        }
    }

    private IEnumerator JumpCoroutine(float jumpPower)
    {
        jumping = true;
        jumpingSpriteObject.transform.localRotation = Quaternion.identity; //reset the spinning sprite's rotation
        defaultSpriteObject.SetActive(false);
        jumpingSpriteObject.SetActive(true);
        int direction = body.velocity.x >= 0 ? 1 : -1;
        Vector3 rotation = new Vector3(0, 0, 4) * direction;

        particleSystem.Play();

        while (jumping) 
        {

            jumpingSpriteObject.transform.eulerAngles += rotation;
            yield return new WaitForEndOfFrame();

        }
        particleSystem.Stop();
        defaultSpriteObject.SetActive(true);
        jumpingSpriteObject.SetActive(false);
    }

    private void OnDestroy() 
    {
        HighScoreManager.instance.SaveHighScore();
    }
}
