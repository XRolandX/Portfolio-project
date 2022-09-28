// Usually you use transform.LookAt for this.
// But this can give you more control over the angle

using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        Vector3 relative = transform.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
        transform.Rotate(0, angle, 0);
        Debug.Log(angle);
    }
}
