using UnityEngine;
using System.Collections;

public class scCameraShake : MonoBehaviour {

    private bool isCameraShake = false;
    private float shakeMagnitude = 3;
    private float shakeFreq = 5;
    private float shakeLength = 1;
    private Vector3 cameraShakeFreePos;
    private float shakeStartTime;

    private Vector3 workVector = new Vector3();

    void Start(){
        cameraShakeFreePos = transform.position;
    }

    public void cameraShake(float magnitude, float frequency, float length) {
        if (isCameraShake) {
                return;
        }
        shakeMagnitude = magnitude;
        shakeFreq = frequency;
        shakeLength = length;
        shakeStartTime = Time.time;
        isCameraShake = true;
    }

    void Update() {

        workVector.x = 0;
        workVector.y = 0;

        if (isCameraShake) {
            float currentTime = calculateCurrentShakeElapseTime();
            float currentMagnitude = calculateCurrentShakeMagnitude();

            if (currentMagnitude > 0) {

                float perlinValueX = -1f + (Mathf.PerlinNoise(currentTime * shakeFreq, 0) * 2);
                float perlinValueY = -1f + (Mathf.PerlinNoise(0, currentTime * shakeFreq) * 2);

                workVector.x += perlinValueX * currentMagnitude;
                workVector.y += perlinValueY * currentMagnitude;

                transform.position = cameraShakeFreePos + workVector;
            }
            else {
                isCameraShake = false;
            }
        }
    }

    private float calculateCurrentShakeMagnitude() {
        float t = calculateCurrentShakeElapseTime();
        float timePerc = 1 - (t / shakeLength);

        return shakeMagnitude * timePerc;
    }

    private float calculateCurrentShakeElapseTime() {
        return Time.time - shakeStartTime;
    }
}
