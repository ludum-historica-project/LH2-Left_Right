﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, ICollidable
{

    public KeyData key;

    public static System.Action<KeyData> OnDoorOpen = (kd) => { };

    public SoundValue doorOpenSound;


    // Start is called before the first frame update
    void Start()
    {
        OnDoorOpen += DoorOpened;
        GetComponent<SpriteRenderer>().color = key.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DoorOpened(KeyData data)
    {
        if (data == key) Destroy(gameObject);
    }

    public void CollidedWithCharacterController(CharacterController characterController)
    {
        if (characterController.CanOpenDoor(this))
        {
            OnDoorOpen(key);
            OnDoorOpen -= DoorOpened;
            Director.GetManager<SoundManager>().PlaySound(doorOpenSound);
        }
    }
    private void OnDrawGizmos()
    {
        Start();
    }
}
