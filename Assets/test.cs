using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extention
{
    public static float GetAngle(this Vector3 vec)
    {
        return Vector3.Angle(Vector3.right, vec);
    }
}
public class test : MonoBehaviour
{
    private Rigidbody rgbd;
    private float Speed=5;
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        rgbd.velocity = rgbd.rotation*Vector3.right*Speed;
    }

    private void FixedUpdate()
    {
        rgbd.MovePosition(rgbd.position + rgbd.velocity*Time.deltaTime);
    }
    private void ContactToPlayer(Collision coll)
    {
        var ballColl = GetComponent<SphereCollider>();
        var ballCenter = ballColl.bounds.center;
        var boxColl = coll.collider as BoxCollider;
        var boxCenter = boxColl.bounds.center;
        var boxExtents = boxColl.bounds.extents;
        var moveAngle = rgbd.rotation.eulerAngles.z;
        var ballCenterToBoxCenter = boxCenter - ballCenter;
        var inLeft = ballCenter.x < boxCenter.x;
        var inTop = ballCenter.y > boxCenter.y;
        var inXSide = Math.Abs(ballCenterToBoxCenter.x) > boxExtents.x;
        var inYSide = Math.Abs(ballCenterToBoxCenter.y) > boxExtents.y;
        var collidedToVertex = inXSide && inYSide;
        if (collidedToVertex)
        {
            var vertex = boxCenter;
            vertex.x += boxExtents.x * (inLeft ? -1 : 1);
            vertex.y += boxExtents.y * (inTop ? 1 : -1);
            var contactAngle = (vertex - ballCenter).GetAngle();
            var reflectAngle = 90f - moveAngle + contactAngle;
            rgbd.rotation = Quaternion.Euler(0, 0, (moveAngle + reflectAngle * 2) % 360);
            rgbd.velocity = rgbd.rotation * Vector3.right * Speed; ;
        }
        else
        {
            if (!inTop)
                return;
            var contactAngle = inXSide ? (inLeft ? 0f : 180f) : (inTop ? 90f : 270f);
            var reflectAngle = 90f - moveAngle + contactAngle;
            reflectAngle += (boxCenter.x - ballCenter.x) / boxExtents.x * 30f;
            var lateAngle = (moveAngle + reflectAngle * 2) % 360;
            if (lateAngle > 175f)
                lateAngle = 175f;
            else if (lateAngle < 5)
                lateAngle = 5f;
            rgbd.rotation = Quaternion.Euler(0, 0, lateAngle);
        }
        rgbd.velocity = rgbd.rotation * Vector3.right * Speed; ;
    }
    private void ContactToBrick(Collision coll)
    {
        var ballColl = GetComponent<SphereCollider>();
        var ballCenter = ballColl.bounds.center;
        var boxColl = coll.collider as BoxCollider;
        var boxCenter = boxColl.bounds.center;
        var boxExtents = boxColl.bounds.extents;
        var moveAngle = rgbd.rotation.eulerAngles.z;
        var ballCenterToBoxCenter = boxCenter - ballCenter;
        var inLeft = ballCenter.x < boxCenter.x;
        var inTop = ballCenter.y > boxCenter.y;
        var inXSide = Math.Abs(ballCenterToBoxCenter.x) > boxExtents.x;
        var inYSide = Math.Abs(ballCenterToBoxCenter.y) > boxExtents.y;
        float contactAngle;
        var collidedToVertex = inXSide && inYSide;
        if (collidedToVertex)
        {
            var vertex = boxCenter;
            vertex.x += boxExtents.x * (inLeft ? -1 : 1);
            vertex.y += boxExtents.y * (inTop ? 1 : -1);
            contactAngle = (vertex - ballCenter).GetAngle();

        }
        else
        {
            contactAngle = inXSide ? (inLeft ? 0f : 180f) : (inTop ? 90f : 270f);
        }
        var reflectAngle = 90f - moveAngle + contactAngle;
        rgbd.rotation = Quaternion.Euler(0, 0, (moveAngle + reflectAngle * 2) % 360);
        rgbd.velocity = rgbd.rotation * Vector3.right * Speed; ;
    }
    private void Contact(Collision coll)
    {
        ContactToBrick(coll);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Contact(collision);
    }
}
