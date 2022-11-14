using System.Collections;
using System.Collections.Generic;
using ctsalidis.Scripts;
using UnityEngine;

public class RayCastManager : MonoBehaviour {
    public delegate void RaycastLayerHitAction(GameObject hitObject);

    public static event RaycastLayerHitAction OnRaycastLayerHit;

    [SerializeField] private LayerMask pinLayerMask;
    [SerializeField] private List<LayerMask> LayersToHit = new List<LayerMask>();

    private Camera _camera;

    private void Start() => _camera = Camera.main;

    // Update is called once per frame
    private void Update() {
        var pinRay = _camera.ScreenPointToRay(Input.mousePosition);
#if UNITY_EDITOR
        Debug.DrawRay(pinRay.origin, pinRay.direction * 10, Color.green);
#endif
        if (Input.GetButtonDown("Fire1")) {
            // if the user presses the click button, send a ray to see if it's a pin code number
            RaycastHit pinHit;
            if (Physics.Raycast(pinRay, out pinHit)) {
                if (Utilities.CheckLayer(pinLayerMask, pinHit.transform.gameObject.layer)) {
                    print("Hit number");
                    var pinNumber = pinHit.transform.GetComponent<PinNumber>();
                    if (pinNumber != null) {
                        pinNumber.AddNumberToPin();
                    }
                }
            }
        }

        // send ray from the middle of the screen onwards
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
#if UNITY_EDITOR
        // Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
#endif
        if (!Physics.Raycast(ray, out hit)) return;
        if (Utilities.CheckLayer(LayersToHit, hit.transform.gameObject.layer)) {
            OnRaycastLayerHit?.Invoke(hit.transform.gameObject);
            print("Hit layer");
        }
    }
}