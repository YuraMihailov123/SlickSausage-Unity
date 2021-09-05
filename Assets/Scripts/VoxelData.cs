using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelData
{

    int[,] data = new int[,] { 
        { 0, 1, 1 },
        { 1, 1, 1 },
        { 1, 1, 1 }, 
        { 1, 1, 1 },
        { 1, 1, 1 },
        { 1, 1, 1 }, 
        { 1, 1, 1 }, 
        { 1, 1, 1 }, 
        { 1, 1, 1 }, 
        { 1, 1, 1 }, 
        { 1, 1, 1 } 
    };

    public int Width
    {
        get
        {
            return data.GetLength(0);
        }
    }

    public int Depth
    {
        get
        {
            return data.GetLength(1);
        }
    }

    public int GetCell(int x,int y)
    {
        return data[x, y];
    }

    void PrintData()
    {
        Debug.Log("Printed data:");
        for(int i = 0; i < Width; i++)
        {
            string str = "";
            for(int j = 0; j < Depth; j++)
            {
                str += data[i, j];
            }
            Debug.Log(str);
        }
        Debug.Log("End of printed data.");
    }

    public void GenerateData()
    {
        int depth = Random.Range(3, 7);
        int width = 15;
        data = new int[width, depth];
        int prevCell = -1;
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < depth; j++)
            {
                int cell = Random.Range(0, 2);

                if (i > 0)
                {
                    if(cell == 0 && data[i-1,j] == 1)
                    {
                        cell = 1;
                    }
                }

                if (prevCell == 1)
                {
                    cell = 1;
                }
                data[i, j] = cell;
                prevCell = cell;
            }
            prevCell = -1;
        }
        //PrintData();
    }
}
