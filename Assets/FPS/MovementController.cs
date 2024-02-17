using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    
    private CharacterController body;
    private float verticalSpeed;
    private bool grounded;
    
    [SerializeField]
    private float movementSpeed = 1.0f;
    
    [SerializeField]
    private float jumpHeight = 1.0f;
    
    [SerializeField]
    private float gravityConstant = 9.0f;
    
    
    void Start() {
        body = transform.GetComponent<CharacterController>();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = grounded ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

    void Update() {
        var t = transform;
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");
        var amount = Mathf.Max(Mathf.Abs(hor), Mathf.Abs(ver));
        grounded = body.isGrounded;

        if (grounded && verticalSpeed > 0.0f) {
            verticalSpeed = 0.0f;
        }
        
        if (amount > float.Epsilon) {
            var dir = t.right * hor + t.forward * ver;
            dir.y = 0.0f;
            dir.Normalize();
            dir *= amount * movementSpeed * Time.deltaTime;
            body.Move(dir);
        }
        
        if (grounded && Input.GetButton("Jump")) {
            verticalSpeed = -Mathf.Sqrt(jumpHeight * 3.0f * gravityConstant);
        }
        
        verticalSpeed += gravityConstant * Time.deltaTime;
        body.Move(Vector3.down * verticalSpeed * Time.deltaTime);
    }
}
