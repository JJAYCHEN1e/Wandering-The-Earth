using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ToolCenter
{
    public static void SetActive(Transform tran, bool isActive)
    {
        if (tran != null)
        {
            SetActive(tran.gameObject, isActive);
        }
    }

    public static void SetActive(GameObject obj, bool isActive)
    {
        if (obj != null)
        {
            obj.SetActive(isActive);
        }
    }

    public static void SetActive(List<GameObject> objList, bool isActive)
    {
        SetActive(objList.ToArray(), isActive);
    }


    public static void SetActive(GameObject[] objAry, bool isActive)
    {
        for (int i = 0; i < objAry.Length; i++)
        {
            objAry[i].SetActive(isActive);
        }
    }

    public static void SetActive(List<GameObject> objList, int index, bool isActive)
    {
        SetActive(objList.ToArray(), index, isActive);
    }

    /// <summary>
    /// 唯一显示/隐藏物体
    /// </summary>
    /// <param name="objAry">物体数组</param>
    /// <param name="index">索引</param>
    /// <param name="isActive">显示/隐藏</param>
    public static void SetActive(GameObject[] objAry, int index, bool isActive)
    {
        for (int i = 0; i < objAry.Length; i++)
        {
            if (i == index)
            {
                objAry[i].SetActive(isActive);
            }
            else
            {
                objAry[i].SetActive(!isActive);
            }
        }
    }

    public static void SetActive(List<GameObject> objList, GameObject obj, bool isActive)
    {
        SetActive(objList.ToArray(), obj, isActive);
    }

    /// <summary>
    /// 唯一显示/隐藏物体
    /// </summary>
    /// <param name="objAry">物体数组</param>
    /// <param name="obj">物体</param>
    /// <param name="isActive">显示/隐藏</param>
    public static void SetActive(GameObject[] objAry, GameObject obj, bool isActive)
    {
        for (int i = 0; i < objAry.Length; i++)
        {
            if (objAry[i] == obj)
            {
                objAry[i].SetActive(isActive);
            }
            else
            {
                objAry[i].SetActive(!isActive);
            }
        }
    }
}
