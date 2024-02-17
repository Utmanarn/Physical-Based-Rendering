using UnityEngine;

public class MouseLookController : MonoBehaviour {

    private Transform head;
    private Transform body;

    [SerializeField]
    private float mouseSpeed = 1.0f;

    [SerializeField, Range(0, 180)]
    private float verticalAngle = 160;
    
    void Start() {
        head = GetComponentInChildren<Camera>().transform;
        body = transform;
    }

    void Update() {
        var hor = Input.GetAxis("Mouse X");
        var ver = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(hor) > float.Epsilon) {
            body.Rotate(Vector3.up, hor * mouseSpeed);
        }

        if (Mathf.Abs(ver) > float.Epsilon) {
            var rot = head.localRotation;
            var aim = Quaternion.AngleAxis(-0.5f * verticalAngle * Mathf.Sign(ver), Vector3.right);
            var delta = Quaternion.RotateTowards(rot, aim, Mathf.Sign(ver) * ver * mouseSpeed);
            head.localRotation = delta;
        }
    }
}
