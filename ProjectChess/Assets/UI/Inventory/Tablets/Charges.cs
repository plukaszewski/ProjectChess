using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charges : MonoBehaviour
{
    public InventoryButton Button;
    public Image[] ChargeImages;
    public Sprite EmptyCharge;
    public Sprite FullCharge;

    private void OnCharcgesChange()
    {
        for (int i = 0; i < Button.MaxCharges; i++)
        {
            if (i < Button.CurrentCharges)
            {
                ChargeImages[i].sprite = FullCharge;
            }
            else
            {
                ChargeImages[i].sprite = EmptyCharge;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Button.ChargesChange.AddListener(OnCharcgesChange);
        for (int i = 0; i < Button.MaxCharges; i++)
        {
            ChargeImages[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
