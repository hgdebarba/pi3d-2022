using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralPlacement : MonoBehaviour
{

	public float radius = 1;
	public Vector2 regionSize = Vector2.one;
	public int rejectionSamples = 30;
	public float displayRadius = 1;
	public GameObject objectPrefab;

	private bool shouldUpdate;
	private List<Vector2> points;
	private List<Transform> transforms;
	// arbitrary limit of objects, so that unity won't freeze if we create too many game objects
	private int hardLimit = 1000; 

	private void CleanupObjects()
    {
		// destroy all objects and clear transforms list
		for(int i = 0; i < transforms.Count; i++)
        {
			Destroy(transforms[i].gameObject);
        }
		transforms.Clear();
	}

	private void InitObjects()
    {
		// create game objects and add to the transforms list
		for (int i = 0; i < points.Count; i++)
		{
			Transform tr = Instantiate(objectPrefab).transform;
			transforms.Add(tr);
		}
	}

	private void UpdateObjects()
    {
		// delete and instantiate objects only if the size of the lists don't match
		if (points.Count != transforms.Count)
		{
			CleanupObjects();
			InitObjects();
		}

		// used to center point around the game object
		Vector3 offset = new Vector3(-regionSize.x, 0, -regionSize.y) / 2.0f;
		for (int i = 0; i < points.Count; i++)
		{
			Vector3 point3D = new Vector3(points[i].x, 0, points[i].y);
			transforms[i].transform.position = point3D + offset;
			// random rotation around y
			float angleDegrees = Random.value * Mathf.PI * 2 * Mathf.Rad2Deg;
			transforms[i].transform.Rotate(0, angleDegrees, 0);
			// random scale variation
			transforms[i].localScale *= 1 + Random.value * 0.3f; // [0.8, 1.2] range
		}
	}

    private void Start()
    {
		if (transforms == null)
			transforms = new List<Transform>();
	}

    private void Update()
    {
		if (!shouldUpdate)
			return;
	
		UpdateObjects();
		shouldUpdate = false;
	}

    void OnValidate()
	{
		if (radius == 0) // radius 0 is invalid
			return;
		points = PoissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSamples);
		if (points.Count > hardLimit)
        {
			Debug.LogError("[ProceduralPlacement] Too many points! Will not create the objects.");
			return;
        }

		shouldUpdate = true;
	}

	void OnDrawGizmos()
	{
		Gizmos.matrix = this.transform.localToWorldMatrix; // make it follow the game object
		Gizmos.color = new Color(1, 1, 1, .3f); // make it semi-transparent
		Vector3 region3D = new Vector3(regionSize.x, 0, regionSize.y);
		Vector3 offset = -region3D / 2.0f; // used to center it to the game object
		Gizmos.DrawWireCube(Vector3.zero, region3D);
		if (points != null)
		{
			foreach (Vector2 point in points)
			{
				Vector3 point3D = new Vector3(point.x, 0, point.y);
				Gizmos.DrawSphere(point3D + offset, displayRadius);
			}
		}
		Gizmos.matrix = Matrix4x4.identity;
	}
}