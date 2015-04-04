using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scOceanSection : MonoBehaviour {

    //private ArrayList sectionPoints = new ArrayList();

    private const float UnitSize = 1.5f;
    private int width = 10;
    private int depth = 10;

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    List<Vector2> uvList = new List<Vector2>();

    //cache a vector to avoid constantly allocating new memory
    private Vector3 workVector = new Vector3();

    private Mesh mesh;

	// Use this for initialization
	void Start () {
        mesh = this.gameObject.AddComponent<MeshFilter>().mesh;

        createPoints();
        constructMesh();
	}

    private void createPoints(){
        for (int w = 0; w < width; w++){
            for (int d = 0; d < depth; d++){
                GameObject oceanPoint = (GameObject)GameObject.Instantiate(Resources.Load("OceanPoint"));
                
                workVector.x = transform.position.x + (w * UnitSize);
                workVector.y = transform.position.y + Random.Range(0,1);
                workVector.z = transform.position.z + (d * UnitSize);

                oceanPoint.transform.position = workVector;
                
                oceanPoint.transform.parent = this.transform;

                vertices.Add(oceanPoint.transform.position);
                //uvList.Add(new Vector2(1,1));
            }
        }
    }

    private void constructMesh(){
        for (int w = 0; w < width-1; w++){
            for (int d = 0; d< depth-1; d++){
                triangles.Add((w * width) + d);
                triangles.Add((w * width) + d + 1);
                triangles.Add((w+1) * width + d);

                triangles.Add(w * width + (d + 1));
                triangles.Add((w + 1) * width + (d+1));
                triangles.Add((w + 1) * width + d);

            }
        }

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update() {

	}
}
