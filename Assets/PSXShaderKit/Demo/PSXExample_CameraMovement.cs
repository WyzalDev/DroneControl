using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PSXShaderKit
{
    public class PSXExample_CameraMovement : MonoBehaviour
    {
        InputAction moveAction;
        public float speed = 0.5f;
        // Start is called before the first frame update
        void Start()
        {
            moveAction = InputSystem.actions.FindAction("Move");
        }

        // Update is called once per frame
        void Update()
        {
            var moveValue = moveAction.ReadValue<Vector2>();
            transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * moveValue.y  * speed * Time.deltaTime;
            transform.position += new Vector3(transform.right.x, 0, transform.right.z) * moveValue.x * speed * Time.deltaTime;
        }
    }
}
