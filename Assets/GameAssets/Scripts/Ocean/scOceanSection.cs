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

    // Update is called once per frame
    void Update() {
        if (transform.position.z + (depth * unitSize) < -5) {
            Destroy(this.gameObject);
        }
    }

    public void initSection(){
        scOceanController oceanController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<scOceanController>();

        unitSize = oceanController.getUnitSize();
        width = oceanController.getPieceWidth();
        depth = oceanController.getPieceLength();

        mesh = this.GetComponent<MeshFilter>().mesh;

        allColors.Add(new Color(22 / 255f, 171 / 255f, 200 / 255f));
        allColors.Add(new Color(22 / 255f, 189 / 255f, 224 / 255f));
        allColors.Add(new Color(15 / 255f, 129 / 255f, 149 / 255f));

        createPoints();
    }

    public void buildMeshSection(List<Vector3> previousSectionVerticies, GameObject previousSection, float worldMoveSpeed) {

        //calculate the amount a water piece moves in a frame to create the next piece at the correct position
        //without a gap
        Vector3 frameShift = new Vector3(0, 0, worldMoveSpeed * Time.deltaTime);

        //Add the last row of verticies from the previous ocean section into this mesh
        //so they can be connected together seamlessly
        if (previousSectionVerticies.Count > 0){
            for (int i = (depth*depth)-width; i < (depth * depth) ; i++){

                //workVector3.x = (previousSectionVerticies[i].x + previousSection.transform.position.x) - transform.position.x;
                //workVector3.y = (previousSectionVerticies[i].y + previousSection.transform.position.y) - transform.position.y;
                //workVector3.z = (previousSectionVerticies[i].z + previousSection.transform.position.z) - transform.position.z;

                workVector3 = (previousSectionVerticies[i] + previousSection.transform.position) - transform.position - frameShift;

                //workVector3 = (previousSectionVerticies[i] + previousSection.transform.position);
                //workVector3.x += (width * unitSize);
                //workVector3.y = previousSectionVerticies[i].y;
                //workVector3.z -= (depth * unitSize) * 2;
                GameObject oceanPoint = (GameObject)GameObject.Instantiate(Resources.Load("OceanPoint"));
                oceanPoint.transform.position = workVector3;

                oceanPoint.transform.parent = this.transform;
                vertices.Add(oceanPoint.transform.position);

                colors.Add(allColors[Random.Range(0, allColors.Count)]);
                uv.Add(workVector2);
            }

            constructMesh(true);
        }else{
            constructMesh(false);
        }
    }

    public List<Vector3> getVerticies(){
        return vertices;
    }

    private void createPoints(){
        for (int d = 0; d < depth; d++){
            for (int w = 0; w < width; w++){
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

    private void constructMesh(bool connectToPrevious) {
        constructTriangles();

        if (connectToPrevious){
            constructSeamTriangles();
        }

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv.ToArray();
        mesh.colors = colors.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    private void constructTriangles(){
        for (int d = 0; d < depth - 1; d++) {
            for (int w = 0; w < width - 1; w++) {
                triangles.Add((d * depth) + w);
                triangles.Add((d + 1) * depth + w);
                triangles.Add((d * depth) + w + 1);

                triangles.Add(d * depth + (w + 1));
                triangles.Add((d + 1) * depth + w);
                triangles.Add((d + 1) * depth + (w + 1));
            }
        }
    }

    private void constructSeamTriangles() {
        int d = depth ;

        for (int w = 0; w < width - 1; w++) {
            triangles.Add((d*d) + w);
            triangles.Add(w);
            triangles.Add((d* d) + w +1);

            triangles.Add((d * d) + w + 1);
            triangles.Add(w);
            triangles.Add(w+1);
        }
    }
}
