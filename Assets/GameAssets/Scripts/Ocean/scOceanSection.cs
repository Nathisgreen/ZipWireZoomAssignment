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
    private List<Vector2> uv = new List<Vector2>();
    private List<Color> colors = new List<Color>();

    private List<Color> allColors = new List<Color>();

    //cache a vector to avoid constantly allocating new memory
    private Vector3 workVector = new Vector3();

    private Mesh mesh;

	void Start () {
        mesh = this.gameObject.AddComponent<MeshFilter>().mesh;

        allColors.Add(new Color(22 / 255f, 171 / 255f, 200 / 255f));
        allColors.Add(new Color(22 / 255f, 189 / 255f, 224 / 255f));
        allColors.Add(new Color(15 / 255f, 129 / 255f, 149 / 255f));

        createPoints();
        constructMesh();
	}

    private void createPoints(){
        for (int w = 0; w < width; w++){
            for (int d = 0; d < depth; d++){
                GameObject oceanPoint = (GameObject)GameObject.Instantiate(Resources.Load("OceanPoint"));

                oceanPoint.transform.parent = this.transform;

                workVector.x = transform.position.x + (w * UnitSize);
                workVector.y = transform.position.y + Random.Range(0,50)/100f;
                workVector.z = transform.position.z + (d * UnitSize);

                oceanPoint.transform.position = workVector;

                vertices.Add(oceanPoint.transform.position - oceanPoint.transform.parent.position);
                uv.Add(new Vector2(0,0));
                colors.Add(allColors[Random.Range(0, allColors.Count)]);

 

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
        mesh.uv = uv.ToArray();
        mesh.colors = colors.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update() {

	}
}
