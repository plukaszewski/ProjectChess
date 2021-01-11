using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPatternSquare : MonoBehaviour
{
    public bool Save;

    private void OnValidate()
    {
        Validate();
    }

    public void Validate()
    {
        transform.position += transform.position.x > 0 ? new Vector3(.5f, 0f) : new Vector3(-.5f, 0f);
        transform.position += transform.position.y > 0 ? new Vector3(0f, .5f) : new Vector3(0f, -.5f);
        transform.position = new Vector3((int)transform.position.x, (int)transform.position.y);
        Save = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
