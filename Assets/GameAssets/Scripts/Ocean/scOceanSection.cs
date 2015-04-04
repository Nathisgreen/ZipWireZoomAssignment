using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scOceanSection : MonoBehaviour {

    private float unitSize;
    private int width;
    private int depth;

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uv = new List<Vector2>();
    private List<Color> colors = new List<Color>();

    private List<Color> allColors = new List<Color>();

    //cache a vector to avoid constantly allocating new memory
    private Vector3 workVector3 = new Vector3();
    private Vector2 workVector2 = new Vector2();

    private Mesh mesh;

	void Start () {

        scOceanController oceanController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<scOceanController>();

        unitSize = oceanController.getUnitSize();
        width = oceanController.getPieceWidth();
        depth = oceanController.getPieceLength();


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

                workVector3.x = transform.position.x + (w * unitSize);
                workVector3.y = transform.position.y + Random.Range(0,50)/100f;
                workVector3.z = transform.position.z + (d * unitSize);

                oceanPoint.transform.position = workVector3;

                vertices.Add(oceanPoint.transform.position - oceanPoint.transform.parent.position);
                uv.Add(workVector2);
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
