using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody Rb;

    public float ForwardForce = 2000f;
    public float SideForce = 500f;
    private Vector2 touchOrigin = -Vector2.one;

    void FixedUpdate()
    {
        var left = 0;
        var right = 0;
        Rb.AddForce(0, 0, ForwardForce * Time.deltaTime);

#if UNITY_STANDALONE || UNITY_WEBPLAYER //|| UNITY_EDITOR
        if (Input.GetKey("d"))
        {
            right = 1;
            left = 0;
        }

        if (Input.GetKey("a"))
        {
            right = 0;
            left = 1;
        }
#else
        SideForce = 30f;
        if (Input.touchCount > 0)
        {
            var myTouch = Input.touches[0];
            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            else if (myTouch.phase == TouchPhase.Moved && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                var x = touchEnd.x - touchOrigin.x;

                if (x < 0)
                {
                    left = 1;
                    right = 0;
                }
                else
                {
                    left = 0;
                    right = 1;
                }
            }
        }
#endif

        if (right > 0)
        {
            Rb.AddForce(SideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        else if (left > 0)
        {
            Rb.AddForce(-SideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}