using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform rightHand;
    public Transform leftHand;
    Collider2D[] rightColliders;
    Collider2D[] leftColliders;
    Rigidbody2D righHandRB;
    Rigidbody2D leftHandRB;
    Rigidbody2D playerRB;
    bool isLeftGrabbing;
    bool isRightGrabbing;
    ContactFilter2D filter;
    Vector2 leftGrabPosition;
    Vector2 rightGrabPosition;

    void Start()
    {
        filter.useTriggers = false;
        filter.SetLayerMask(~(1 << LayerMask.NameToLayer("Player")));
        filter.useLayerMask = true;

        righHandRB = rightHand.parent.GetComponent<Rigidbody2D>();
        leftHandRB = leftHand.parent.GetComponent<Rigidbody2D>();

        playerRB = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        righHandRB.AddForceAtPosition(new Vector2(Input.GetAxis("Joystick X"), Input.GetAxis("Joystick Y")), rightHand.position, ForceMode2D.Impulse);
        leftHandRB.AddForceAtPosition(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), leftHand.position, ForceMode2D.Impulse);

        Grabbing("LeftTrigger", ref leftHand, ref leftColliders, ref leftGrabPosition, ref leftHandRB, ref isLeftGrabbing);
        Grabbing("RightTrigger", ref  rightHand, ref rightColliders, ref rightGrabPosition, ref righHandRB, ref isRightGrabbing);
    }

    private void Grabbing(string triggerInput, ref Transform hand, ref Collider2D[] colliders, ref Vector2 grabPosition, ref Rigidbody2D handRB, ref bool isGrabbing)
    {
        if (Input.GetAxis(triggerInput) >= 0.5f)
        {
            if (!isGrabbing)
            {
                colliders = new Collider2D[1];
                hand.GetComponent<Collider2D>().OverlapCollider(filter, colliders);

                if (colliders[0] == null) return;

                if (colliders[0].GetComponent<Rigidbody2D>() == null) colliders[0].gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

                if (colliders[0].GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
                {
                    colliders[0].gameObject.AddComponent<FixedJoint2D>().connectedBody = hand.GetComponent<Rigidbody2D>();
                }
                else
                {
                    colliders[0].gameObject.AddComponent<FixedJoint2D>().connectedBody = handRB;
                }
            }
        }
        else
        {
            if (isGrabbing)
            {
                var joints = colliders[0].GetComponents<Joint2D>();
                foreach (var item in joints)
                {
                    if (item.connectedBody == hand.GetComponent<Rigidbody2D>() || item.connectedBody == handRB)
                    {
                        Destroy(item);
                    }
                }
            }
        }

        isGrabbing = Input.GetAxis(triggerInput) > 0.5f ? true : false;
    }

    //private void Grabbing(string triggerInput, ref Transform hand, ref Collider2D[] colliders, ref Vector2 grabPosition, ref Rigidbody2D handRB, ref bool grabbingCheck)
    //{
    //    if (Input.GetAxis(triggerInput) >= 0.5f)
    //    {
    //        if (!grabbingCheck)
    //        {
    //            colliders = new Collider2D[1];
    //            hand.GetComponent<Collider2D>().OverlapCollider(filter, colliders);
    //            if (colliders[0] == null) return;
    //            grabPosition = colliders[0].transform.InverseTransformPoint(hand.position); // we move that into the local space of the target, so we have a local position 
    //        }

    //        Vector2 force = (colliders[0].transform.TransformPoint(grabPosition) - hand.position) * playerRB.mass; // we then transform that out of the local space of target and back into world

    //        handRB.velocity = Vector2.zero;

    //        handRB.AddForce(force * force.magnitude, ForceMode2D.Impulse);
    //    }

    //    grabbingCheck = Input.GetAxis(triggerInput) > 0.5f ? true : false;
    //}
}
