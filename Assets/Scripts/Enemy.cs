﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICollidable
{
    public GameObject deathParticle;
    public SoundValue enemyDeathSound;
    public List<Transform> targets = new List<Transform>();
    public float movementSpeed = 3;
    int currentTarget = 0;

    public void CollidedWithCharacterController(CharacterController characterController)
    {

        if (characterController.transform.position.y > transform.position.y)
        {
            Kill();
            characterController.EnemyStomped();
        }
        else
        {
            characterController.Kill();
        }
    }

    void Kill()
    {
        Destroy(Instantiate(deathParticle, transform.position, Quaternion.identity, transform.parent), 5);
        Director.GetManager<SoundManager>().PlaySound(enemyDeathSound);
        gameObject.SetActive(false);
        Invoke("Respawn", 5);
    }

    void Respawn()
    {
        gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if (targets.Count > 0)
        {
            Vector3 directionToTarget = targets[currentTarget].position - transform.position;
            transform.position += (directionToTarget).normalized * Mathf.Min(movementSpeed * Time.deltaTime, directionToTarget.magnitude);
            if (Vector3.Distance(transform.position, targets[currentTarget].position) <= .01f)
            {
                currentTarget = (currentTarget + 1) % targets.Count;
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            Gizmos.DrawLine(targets[i].position, targets[(i + 1) % targets.Count].position);
        }
    }

}
