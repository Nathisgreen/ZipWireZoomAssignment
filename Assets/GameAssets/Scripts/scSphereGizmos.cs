using UnityEngine;
using System.Collections;

public class scSphereGizmos : MonoBehaviour {

    public float size = 0.1f;

    public void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, size);
    }
}
