using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFiniteBackup : MonoBehaviour {

    // Update is called once per frame
    public float x = 99999999f;
    public float y = 99999999f;
    public Vector3 pos;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        x = SyncData.worldSize * 1000;
        y = 500;
    }

	void LateUpdate () {
        if (transform.position.x > x || transform.position.x < -x || transform.position.y > y || transform.position.y < -y || transform.position.z > 200 || transform.position.z < -200 || transform.position.x == float.NaN || transform.position.y == float.NaN || transform.position.z == float.NaN)
        {
            transform.position = pos;
        }
        else
        {
            pos = transform.position;
        }
        if (pos.x == float.NaN || pos.y == float.NaN || pos.z == float.NaN)
        {
            Destroy(gameObject);
        }
	}
}
