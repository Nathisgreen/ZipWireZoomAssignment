using UnityEngine;
using System.Collections;

public class scParticleController : MonoBehaviour {

    private ParticleSystem ps;

	void Start () {
        ps = this.GetComponent<ParticleSystem>();
	}
	
	void Update () {
        if (ps != null) {
            if (!ps.IsAlive()) {
                Destroy(this.gameObject);
            }
        }
	}
}
