using UnityEngine;
using System.Collections;

public class scSphereGizmos : MonoBehaviour {

    public void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
