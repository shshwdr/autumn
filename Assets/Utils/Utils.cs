using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    static public Vector2[] dir4V2 = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(0, 1), };
    static public Vector2[] dir5V2 = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(0, 1), new Vector2(0, 0), };
    static public bool randomBool()
    {
        return Random.Range(0, 2) > 0;
    }

    static public bool arrayContains<T>(T[] array, T target)
    {
        foreach (T t in array)
        {
            if (target.Equals(t))
            {
                return true;
            }
        }
        return false;
    }
    static public int findClosestIndex<T>(Transform targetTransform, List<T> candicateTransforms) where T : MonoBehaviour
    {
        int res = 0;
        float closestDistance = float.MaxValue;
        for (int i = 0; i < candicateTransforms.Count; i++)
        {
            float distance = (candicateTransforms[i].transform.position - targetTransform.position).magnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                res = i;
            }
        }
        return res;
    }

    static public Vector3 randomVector3(Vector3 origin, float randomness)
    {
        return new Vector3(origin.x + randomFromZero(randomness), origin.y + randomFromZero(randomness), origin.z + randomFromZero(randomness));
    }

    static public Vector3 randomVector3_2d(Vector3 origin, float randomness)
    {
        return new Vector3(origin.x + randomFromZero(randomness), origin.y + randomFromZero(randomness), origin.z);
    }

    static public float randomFromZero(float randomness)
    {
        return Random.Range(-randomness, randomness);
    }

    public static T randomEnumValue<T>()
    {
        var values = System.Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(random);
    }

    static float snapFloat(float gridSize, float origin)
    {
        return Mathf.Round(origin / gridSize) * gridSize;
    }

    static float snapFloatCenter(float gridSize, float origin)
    {
        return Mathf.Round((origin - gridSize / 2f) / gridSize) * gridSize + gridSize / 2f;
    }

    static float floatToGridIndexCenter(float gridSize, float origin)
    {
        return Mathf.RoundToInt((origin - gridSize / 2f) / gridSize);
    }

    public static Vector3 snapToGrid(float gridSize, Vector3 origin)
    {
        return new Vector3(snapFloat(gridSize, origin.x), snapFloat(gridSize, origin.y), snapFloat(gridSize, origin.z));
    }

    public static int distanceToIndex(float gridSize, float distance)
    {
        return Mathf.RoundToInt(distance / gridSize);
    }
    public static int distanceCenterToIndex(float gridSize, float distance)
    {
        return Mathf.RoundToInt((distance - gridSize / 2f) / gridSize);
    }

    public static Vector3 snapToGridCenter(float gridSize, Vector3 origin)
    {
        return new Vector3(snapFloatCenter(gridSize, origin.x), snapFloatCenter(gridSize, origin.y), snapFloatCenter(gridSize, origin.z));
    }

    public static Vector2 positionToGridIndexCenter2d(float gridSize, Vector3 origin)
    {
        return new Vector2(floatToGridIndexCenter(gridSize, origin.x), floatToGridIndexCenter(gridSize, origin.y));
    }
    public static List<int> randomMultipleIndex(int count, int selectCount)
    {
  //      for i := 1 to k
  //    R[i] := S[i]

  //// replace elements with gradually decreasing probability
  //          for i := k + 1 to n
  //  (*randomInteger(a, b) generates a uniform integer from the inclusive range { a, ..., b}
  //      *)
  //  j:= randomInteger(1, i)
  //  if j <= k
  //      R[j] := S[i]
        List<int> res = new List<int>();
        if(count == 0)
        {
            return res;
        }
        for (int i = 0; i < selectCount; i++)
        {
            res.Add(i);

        }
        for(int i = selectCount; i < count; i++)
        {
            int j = Random.Range(0, i);
            if (j < selectCount)
            {
                res[j] = i;
            }
        }


        return res;
    }
    public static bool nextToPositionInGrid(float gridSize, Vector3 p1, Vector3 p2)
    {
        var positionIndex1 = positionToGridIndexCenter2d(gridSize, p1);
        var positionIndex2 = positionToGridIndexCenter2d(gridSize, p2);
        if ((positionIndex1 - positionIndex2).magnitude <= 1.1f)
        {
            return true;
        }
        return false;
    }

    public static Vector2 chaseDir2d(Vector3 chaser, Vector3 chasee)
    {
        var diff = chasee - chaser;
        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            return new Vector2(diff.x > 0 ? 1 : -1, 0);
        }
        else
        {

            return new Vector2(0, diff.y > 0 ? 1 : -1);
        }
    }

    public static Vector2 chaseDir2dSecond(Vector3 chaser, Vector3 chasee)
    {
        var diff = chasee - chaser;
        if (Mathf.Abs(diff.x) <= Mathf.Abs(diff.y))
        {
            return new Vector2(diff.x > 0 ? 1 : -1, 0);
        }
        else
        {

            return new Vector2(0, diff.y > 0 ? 1 : -1);
        }
    }


    static public void destroyAllChildren(Transform tran)
    {
        foreach (Transform child in tran)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    static public void setActiveOfAllChildren(Transform tran, bool active = false)
    {
        foreach (Transform child in tran)
        {
            child.gameObject.SetActive(active);
        }
    }

    static public int[] arrayAggregasion(int[] a, int[] b, int multipler = 1)
    {
        if (a.Length == 0)
        {
            return b;
        }
        if (a.Length != b.Length)
        {
            Debug.LogError("can't solve this!");
            return a;
        }
        for (int i = 0; i < a.Length; i++)
        {
            a[i] += b[i] * multipler;
        }
        return a;
    }
}
