using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Array_2D_1D
{
    //二维数组转换为一维数组
    public static T[] ConvertTo1D<T>(T[,] source)
    {
        int rows = source.GetLength(0);
        int cols = source.GetLength(1);

        T[] resultArray = new T[rows * cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                resultArray[i * cols + j] = source[i, j];
            }
        }

        return resultArray;
    }
    
    // 一维数组转换为二维数组
    public static T[,] ConvertTo2D<T>(T[] source, int width, int height)
    {
        T[,] resultArray = new T[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                resultArray[i, j] = source[i * width + j];
            }
        }

        return resultArray;
    }
}
