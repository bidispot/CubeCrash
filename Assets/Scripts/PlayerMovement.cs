using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody Rb;

    public float ForwardForce = 2000f;
    public float SideForce = 500f;

    void FixedUpdate()
    {
        Rb.AddForce(0, 0, ForwardForce * Time.deltaTime);

        if (Input.GetKey("d"))
        {
            Rb.AddForce(SideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a"))
        {
            Rb.AddForce(-SideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}