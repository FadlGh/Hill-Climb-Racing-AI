using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TerrainGenerator : MonoBehaviour
{

    public SpriteShapeController shape;

    [Range(0, 1000)]
    public int scale;

    [Range(0, 7)]
    public int smoothing;

    [Range(0, 10)]
    public int height;

    [Range(0, 500)]
    public int numOfPoints;

    [Range(0, 10)]
    public int numOfBoxes;

    public GameObject Boxes;
    public GameObject finishLine;
    // Start is called before the first frame update
    void Start()
    {
        shape = GetComponent<SpriteShapeController>();
        shape.spline.SetPosition(2, shape.spline.GetPosition(2) + Vector3.right * scale);
        shape.spline.SetPosition(3, shape.spline.GetPosition(3) + Vector3.right * scale);

        for (int i = 0; i <= numOfPoints; i++)
        {
            float distanceBtwnPoints = scale / (float)numOfPoints;
            float xPos = shape.spline.GetPosition(i + 1).x + distanceBtwnPoints;
            float randomNumber = Random.Range(4f, 6f);

            if (i < 2)
            {
                randomNumber = Random.Range(shape.spline.GetPosition(2).y, shape.spline.GetPosition(2).y + 2);
            }
            //Using perlin noise to make random hills
            shape.spline.InsertPointAt(i + 2, new Vector3(xPos, height * Mathf.PerlinNoise(0, i * randomNumber)));

            if (i == numOfPoints - 1)
            {
                //Creating the finish line
                Instantiate(finishLine, shape.spline.GetPosition(i) + new Vector3(0, 0.5f, 0), Quaternion.identity);
            }
        }

        for (int i = 2; i <= numOfPoints + 2; i++)
        {
            //Add smoothing to the hills
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            shape.spline.SetLeftTangent(i, new Vector3(-smoothing, 0, 0));
            shape.spline.SetRightTangent(i, new Vector3(smoothing, 0, 0));
        }

        for (int i = 0; i <= numOfBoxes; i++)
        {
            //Spawning random boxes to decrease the player speed
            Vector3 Pos = shape.spline.GetPosition(Random.Range(10, numOfPoints - 10)) + new Vector3(0, 5, 0);
            Instantiate(Boxes, Pos - new Vector3(0, 2, 0), Quaternion.identity);
        }
    }
}
