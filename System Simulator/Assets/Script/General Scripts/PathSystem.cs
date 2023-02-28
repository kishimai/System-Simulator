using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public float curveStrength = 1f;

    private LineRenderer lineRenderer;

    void Start() {

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = gameObjects.Count * 3;
        for (int i = 0; i < gameObjects.Count - 1; i++) {
            Vector3 start = gameObjects[i].transform.position;
            Vector3 end = gameObjects[i + 1].transform.position;
            Vector3 control = (start + end) / 2 + new Vector3(0, curveStrength, 0);
            lineRenderer.SetPosition(i * 3, start);
            lineRenderer.SetPosition(i * 3 + 1, control);
            lineRenderer.SetPosition(i * 3 + 2, end);
        }
    }
}
