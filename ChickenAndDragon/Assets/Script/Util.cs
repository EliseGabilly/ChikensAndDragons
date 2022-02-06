using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util{

    public static Vector3 GetRandomDestination(this Transform localisation) {
        return GetRandomLoc(localisation.position.y);
    }
    public static Vector3 GetRandomLoc(float y, bool isRestricted = false) {
        float x = Random.Range(-23, 24);
        float z = Random.Range(-23, 24);
        if (isRestricted) {
            x = Random.Range(-18, 19);
            z = Random.Range(-18, 19);
        }
        return new Vector3(x, y, z);
    }
}
