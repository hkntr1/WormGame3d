using UnityEngine;
using UnityEngine.Windows;

public class ArrowController : MonoBehaviour
{
    FixedJoystick fixedJoystick;

    void Start()
    {
        fixedJoystick = GameObject.FindObjectOfType<FixedJoystick>();
    }
    void Update()
    {
        Vector3 direction = new Vector3(fixedJoystick.Direction.x, fixedJoystick.Direction.y, 0f).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        if (direction != Vector3.zero)
        {
            transform.rotation = rotation;
        }
    }
}
