using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform desiredFollowedTargetPosition;
    [SerializeField] Transform followedTarget;

    void Update()
    {
        Vector2 deltaPos = new Vector2(0.0f, followedTarget.position.y - desiredFollowedTargetPosition.position.y);
        if(deltaPos.y > 0.2f)
            transform.Translate(deltaPos * Time.deltaTime);
        //Time.deltaTime in stuff like movement or rotation gives more smoothness in the moves.
    }
}
