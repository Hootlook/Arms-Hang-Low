using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform rightHand;
    public Transform leftHand;
    Rigidbody2D righHandRB;
    Rigidbody2D leftHandRB;
    Rigidbody2D playerRB;
    bool isLeftGrabbing;
    bool isRightGrabbing;
    Collider2D[] leftColliders;
    Collider2D[] rightColliders;
    ContactFilter2D filter;
    Vector3 leftGrabPosition;
    Vector3 rightGrabPosition;

    void Start()
    {
        filter.useTriggers = false;
        filter.SetLayerMask(~(1 << LayerMask.NameToLayer("Player")));
        filter.useLayerMask = true;

        righHandRB = rightHand.parent.GetComponent<Rigidbody2D>();
        leftHandRB = leftHand.parent.GetComponent<Rigidbody2D>();

        playerRB = transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        righHandRB.AddForceAtPosition(new Vector2(Input.GetAxis("Joystick X"), Input.GetAxis("Joystick Y")), rightHand.position, ForceMode2D.Impulse);
        leftHandRB.AddForceAtPosition(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), leftHand.position, ForceMode2D.Impulse);
        
        if (Input.GetAxis("LeftTrigger") >= 0.5f)
        {
            if (!isLeftGrabbing) {
                leftColliders = new Collider2D[1];
                leftHand.GetComponent<Collider2D>().OverlapCollider(filter, leftColliders);
                leftGrabPosition = leftColliders[0].transform.InverseTransformPoint(leftHand.position); // we move that into the local space of the target, so we have a local position 
            }

            Debug.DrawLine(leftColliders[0].transform.TransformPoint(leftGrabPosition), leftHand.position, Color.red, 0.1f); // we then transform that out of the local space of target and back into world

            Vector3 force = (leftColliders[0].transform.TransformPoint(leftGrabPosition) - leftHand.position) * playerRB.mass;

            leftHandRB.velocity = Vector3.zero;

            leftHandRB.AddForce(force, ForceMode2D.Impulse);
        }

        if (Input.GetAxis("LeftTrigger") < 0.5f)
        {
            if (isLeftGrabbing)
            {
                leftColliders = null;
                leftGrabPosition = Vector2.zero;
            }
        }

        isLeftGrabbing = Input.GetAxis("LeftTrigger") > 0.5f ? true : false;

        if (Input.GetAxis("RightTrigger") >= 0.5f)
        {
            if (!isRightGrabbing)
            {
                rightColliders = new Collider2D[1];
                rightHand.GetComponent<Collider2D>().OverlapCollider(filter, rightColliders);
                rightGrabPosition = rightColliders[0].transform.InverseTransformPoint(righHandRB.position); // we move that into the local space of the target, so we have a local position 
            }

            Debug.DrawLine(rightColliders[0].transform.TransformPoint(rightGrabPosition), rightHand.position, Color.red, 0.1f); // we then transform that out of the local space of target and back into world

            Vector3 force = (rightColliders[0].transform.TransformPoint(rightGrabPosition) - rightHand.position) * playerRB.mass;

            righHandRB.velocity = Vector3.zero;

            righHandRB.AddForce(force, ForceMode2D.Impulse);
        }

        if (Input.GetAxis("RightTrigger") < 0.5f)
        {
            if (isRightGrabbing)
            {
                rightColliders = null;
                rightGrabPosition = Vector2.zero;
            }
        }

        isRightGrabbing = Input.GetAxis("RightTrigger") > 0.5f ? true : false;
    }
}
