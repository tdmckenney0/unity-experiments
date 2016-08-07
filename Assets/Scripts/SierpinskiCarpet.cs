using UnityEngine;
using System.Collections;

public class SierpinskiCarpet : MonoBehaviour {

    public int carpetSize;
    public GameObject spawn;

	// Use this for initialization
	void Start () {
        Carpet(carpetSize);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Carpet(int n)
    {
        n = (int) Mathf.Pow(3, n);

        for(int x = 0; x < n; x++)
        {
            for (int y = 0; y < n; y++)
            {
                if(InCarpet(x, y))
                {
                    Instantiate(spawn).transform.position = new Vector3(x, y);
                }
            }
        }
    }

    bool InCarpet(long x, long y)
    {
        while (x != 0 && y != 0)
        {
            if((x % 3 == 1) && (y % 3 == 1))
            {
                return false; 
            }

            x /= 3;
            y /= 3;
        }

        return true;
    }
}
