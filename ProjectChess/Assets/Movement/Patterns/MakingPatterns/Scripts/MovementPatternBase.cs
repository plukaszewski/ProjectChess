using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPatternBase : MonoBehaviour
{
    public bool Save;

    private void OnValidate()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        Save = false;

        if (GetComponentInParent<MovementPattern>())
        {
            foreach (var Item in GetComponentInParent<MovementPattern>().GetComponentsInChildren<MovementPatternSquare>())
            {
                Item.Validate();
            }
        }
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
