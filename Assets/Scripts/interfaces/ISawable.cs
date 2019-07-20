using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISawable
{
    void GetSawed(Vector3 direction);

    Vector3 GetPosition();
    Vector3 GetRotation();
    Vector3 GetCollisionPoint();
    Vector3 GetCollisionNormal();

    void EndSaw();
}
