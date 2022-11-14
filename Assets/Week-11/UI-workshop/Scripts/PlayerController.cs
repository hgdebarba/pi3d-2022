using System;
using UnityEngine;

namespace ctsalidis.Scripts {
    public class PlayerController : MonoBehaviour {
        private CharacterController controller;
        /*
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;

        private void Start() => controller = gameObject.GetComponent<CharacterController>();

        private void Update() {
            
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0) playerVelocity.y = 0f;

            var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero) gameObject.transform.forward = move;

            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer) {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
            
            var mousePosition = _playerInput.Player.MousePosition.ReadValue<Vector2>();
            var mousePositionZ = _camera.farClipPlane * .5f;

            var mouseWorldPosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mousePositionZ)); // _camera.ScreenToViewportPoint(mousePosition);
            
            
            // Get the angle between the points
            // Use the x and z from the object/mouse, since we're looking along the y axis
            var angle = AngleBetweenTwoPoints(new Vector2(transform.position.x, transform.position.z), new Vector2(mouseWorldPosition.x, mouseWorldPosition.z));

            transform.rotation = Quaternion.Euler(new Vector3(0f, -angle, 0f));
            
        }
        */
/*
        public float speed = 10.0f;
        private float translation;
        private float straffe;

        // Use this for initialization
        void Start() {
            // turn off the cursor
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update() {
            // Input.GetAxis() is used to get the user's input
            // You can furthor set it on Unity. (Edit, Project Settings, Input)
            translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(straffe, 0, translation);

            if (Input.GetKeyDown("escape")) {
                // turn on the cursor
                Cursor.lockState = CursorLockMode.None;
            }
        }
        */
        [SerializeField] private float moveSpeed = 2;
        [SerializeField] private float rotateSpeed = 2;

        private void Start() => Cursor.lockState = CursorLockMode.Locked;

        private void Update() {
            float horizontal = Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
            float vertical = Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;
            Vector3 moveInput = new Vector3(horizontal, 0, vertical);
            
            float mouseInputXAxis = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            // float mouseInputYAxis = Input.GetAxis("Mouse Y") * rotateSpeed / 2 * Time.fixedDeltaTime;
            // Vector3 rotationVector = new Vector3(-mouseInputYAxis, mouseInputXAxis, 0);
            Vector3 rotationVector = new Vector3(0, mouseInputXAxis, 0);
            gameObject.transform.Rotate(rotationVector);
            gameObject.transform.Translate(moveInput);
            
            if (Input.GetKeyDown("escape")) {
                // turn on the cursor
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}