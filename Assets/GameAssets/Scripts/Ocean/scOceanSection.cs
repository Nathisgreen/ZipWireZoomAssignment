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

    //cache vectors to avoid constantly allocating new memory
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

        createMeshVerticies();
    }

    public void buildMeshSection(List<Vector3> previousSectionVerticies, GameObject previousSection, float worldMoveSpeed) {

        /*calculate the amount an ocean piece moves in a frame to create the next piece at the correct position
        without a gap*/
        Vector3 frameShift = new Vector3(0, 0, worldMoveSpeed * Time.deltaTime);

        /*Add the last row of verticies from the previous ocean section into this mesh
        so they can be connected together seamlessly*/
        if (previousSectionVerticies.Count > 0){
            int numberOfVerticies = (depth * depth);
            for (int i = numberOfVerticies-width; i < numberOfVerticies; i++){

                workVector3 = (previousSectionVerticies[i] + previousSection.transform.position) - transform.position - frameShift;
                vertices.Add(workVector3);

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

    private void createMeshVerticies(){
        for (int depthIndex = 0; depthIndex < depth; depthIndex++) {
            for (int widthIndex = 0; widthIndex < width; widthIndex++) {

                workVector3.x = transform.position.x + (widthIndex * unitSize);
                workVector3.y = transform.position.y + Random.Range(0,50)/100f;
                workVector3.z = transform.position.z + (depthIndex * unitSize);

                vertices.Add(workVector3 - transform.position);
                uv.Add(workVector2);
                colors.Add(allColors[Random.Range(0, allColors.Count)]);
            }
        }
    }

    private void constructMesh(bool connectToPreviousOceanSection) {
        constructTrianglesInMesh();

        if (connectToPreviousOceanSection) {
            constructSeamTrianglesToPreviousOceanSection();
        }

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv.ToArray();
        mesh.colors = colors.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    private void constructTrianglesInMesh(){
        for (int depthIndex = 0; depthIndex < depth - 1; depthIndex++) {
            for (int widthIndex = 0; widthIndex < width - 1; widthIndex++) {
                triangles.Add((depthIndex * depth) + widthIndex);
                triangles.Add((depthIndex + 1) * depth + widthIndex);
                triangles.Add((depthIndex * depth) + widthIndex + 1);

                triangles.Add(depthIndex * depth + (widthIndex + 1));
                triangles.Add((depthIndex + 1) * depth + widthIndex);
                triangles.Add((depthIndex + 1) * depth + (widthIndex + 1));
            }
        }
    }

    private void constructSeamTrianglesToPreviousOceanSection() {
        int numberOfVerticies = depth * depth ;

        for (int widthIndex = 0; widthIndex < width - 1; widthIndex++) {
            triangles.Add((numberOfVerticies) + widthIndex);
            triangles.Add(widthIndex);
            triangles.Add((numberOfVerticies) + widthIndex + 1);

            triangles.Add((numberOfVerticies) + widthIndex + 1);
            triangles.Add(widthIndex);
            triangles.Add(widthIndex + 1);
        }
    }
}
