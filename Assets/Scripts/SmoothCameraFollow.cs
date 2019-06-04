﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [Range(0, 1)]
    public float lerp;
    public Transform target;
    public Vector3 followOffset;
    public float mouseOffsetMultiplier;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 newPos = target.transform.position + followOffset;
            //newPos.y = LevelChunk.size.y * (+.25f)f + (newPos.y + .5f) / 2;
            float height = (Mathf.FloorToInt((target.position.y + .5f) / LevelChunk.size.y) * LevelChunk.size.y);
            float heightOffset = (LevelChunk.size.y - 1) / 2f;

            Vector2 cameraPixelSize = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

            Vector2 screenCenter = cameraPixelSize / 2;
            Vector2 mousePosition = Input.mousePosition;
            Vector2 mouseOffsetDirection = (mousePosition - screenCenter);
            mouseOffsetDirection = mouseOffsetDirection / screenCenter.y * mouseOffsetMultiplier;
            mouseOffsetDirection = mouseOffsetDirection.normalized * Mathf.Sqrt(mouseOffsetDirection.magnitude);


            //float smoothOffset = ((((target.position.y - height) / LevelChunk.size.y) - .5f) / 2) * LevelChunk.size.y;
            newPos.y = height + heightOffset;
            newPos += new Vector3(mouseOffsetDirection.x, mouseOffsetDirection.y, 0);

            transform.position = Vector3.Lerp(transform.position, newPos, lerp);
        }
    }
}