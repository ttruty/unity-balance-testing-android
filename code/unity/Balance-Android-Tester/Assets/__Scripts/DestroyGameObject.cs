using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public int destroyAfter;
    public GameObject gameObjectToDestory;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObjectToDestory, destroyAfter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
