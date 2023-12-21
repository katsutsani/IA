using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Camera _camera;
    private void Awake()
    {
        gameObject.SetActive(true);
    }
    public void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _camera.nearClipPlane + 10;
        Vector3 worldViewMousePos = _camera.ScreenToWorldPoint(mousePos);
        transform.position = worldViewMousePos;
    }
}
