using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab;
    [SerializeField]
    private GameObject finishPointPrefab;
    [SerializeField]
    private string finishPointTag = "FinishPoint";  // This should match the tag in Unity



    
    private MazeCell[,] _mazeGrid;

    void Start()
 {
        // Initialize the maze grid with the size from GameState
        _mazeGrid = new MazeCell[GameState.MazeWidth, GameState.MazeDepth];

        for (int x = 0; x < GameState.MazeWidth; x++)
        {
            for (int z = 0; z < GameState.MazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        GenerateMaze(null, _mazeGrid[0, 0]);

        // Call to place the finish point
        RepositionFinishPoint();

    }


    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
{
    int x = (int)currentCell.transform.position.x;
    int z = (int)currentCell.transform.position.z;

    if (x + 1 < GameState.MazeWidth)
    {
        var cellToRight = _mazeGrid[x + 1, z];
        
        if (!cellToRight.IsVisited)
        {
            yield return cellToRight;
        }
    }

    if (x - 1 >= 0)
    {
        var cellToLeft = _mazeGrid[x - 1, z];

        if (!cellToLeft.IsVisited)
        {
            yield return cellToLeft;
        }
    }

    if (z + 1 < GameState.MazeDepth)
    {
        var cellToFront = _mazeGrid[x, z + 1];

        if (!cellToFront.IsVisited)
        {
            yield return cellToFront;
        }
    }

    if (z - 1 >= 0)
    {
        var cellToBack = _mazeGrid[x, z - 1];

        if (!cellToBack.IsVisited)
        {
            yield return cellToBack;
        }
    }
}


private void RepositionFinishPoint()
{
    GameObject finishPoint = GameObject.FindGameObjectWithTag(finishPointTag);
    if (finishPoint != null)
    {
        // Choose a random cell far enough from the start, ensuring x and z are 1.5 or greater and ending with .5
        int x = Random.Range(1, GameState.MazeWidth - 1); // This ensures x is at least 1.
        int z = Random.Range(1, GameState.MazeDepth - 1); // This ensures z is at least 1.

        // Adjust x and z to end with .5
        float xPosition = x + 0.5f;
        float zPosition = z + 0.0f;

        // Ensure xPosition and zPosition are 1.5 or greater
        xPosition = Mathf.Max(xPosition, 3.5f);
        zPosition = Mathf.Max(zPosition, 3.0f);

        // Set the finish point's new position
        finishPoint.transform.position = new Vector3(xPosition, 0, zPosition);
    }
    else
    {
        Debug.LogError("Finish point not found. Make sure the finish point tag is set correctly.");
    }
}





    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }


}
