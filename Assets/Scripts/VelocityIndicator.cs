using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityIndicator : MonoBehaviour
{
    public GameObject earth;
    public Rigidbody earthRigidbody;

    // Update is called once per frame
    void Update()
    {
        transform.position = earth.transform.position;
        float theta;
        if (earthRigidbody.velocity.x > 0)
        {
            float cosTheta = Vector2.Dot(new Vector2(0, 1), new Vector2(earthRigidbody.velocity.x, earthRigidbody.velocity.z).normalized);
            theta = Mathf.Rad2Deg * Mathf.Acos(cosTheta);
        } else {
            float cosTheta = Vector2.Dot(new Vector2(0, -1), new Vector2(earthRigidbody.velocity.x, earthRigidbody.velocity.z).normalized);
            theta = 180 + Mathf.Rad2Deg * Mathf.Acos(cosTheta);
        }
        //theta = earthRigidbody.velocity.x == 0 ? 0 : (earthRigidbody.velocity.x > 0 ? 90 - theta : -90 - theta);
        transform.rotation = Quaternion.Euler(0, theta, 0);
    }

}
