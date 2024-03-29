﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    public float RadiusExplosion;
    public float PushExplosion;
    public float force;

    public ExplosiveBullet SetForce(float newForce)
    {
        force = newForce;
        return this;
    }
    public ExplosiveBullet SetRadiusExplosion(float newRadiusExplosion)
    {
        RadiusExplosion = newRadiusExplosion;
        return this;
    }
    public ExplosiveBullet SetPushExplosion(float newPushExplosion)
    {
        PushExplosion = newPushExplosion;
        return this;
    }

    protected override void Explode()
    {
        Vector3 explosionposition = transform.position;
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, RadiusExplosion);
        foreach (Collider2D hit in objects)
        {
            Vector2 direction = hit.transform.position - transform.position;
            hit.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
        Collider2D[] pushables = Physics2D.OverlapCircleAll(transform.position, PushExplosion);
        foreach (Collider2D item in pushables)
        {
            if (item.GetComponent<PlayerController>() == null)
            {


                ResetPool();


            }
        }
    }
    public void ResetPool()
    {
        _explosivepool.ReturnToPool(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadiusExplosion);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Explode();
        }
    }

}
