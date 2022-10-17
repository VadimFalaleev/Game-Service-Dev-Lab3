using UnityEngine;

public class DragonPicker : MonoBehaviour
{
    public GameObject energyShieldPrefab;
    public int numEnergyShield = 3;
    public float energyShieldBottomY = -6;
    public float energyShieldRadius = 1.5f;

    private void Start()
    {
        for(int i = 1; i <= numEnergyShield; i++)
        {
            GameObject tShieldGo = Instantiate(energyShieldPrefab);
            tShieldGo.transform.position = new(0, energyShieldBottomY, 0);
            tShieldGo.transform.localScale = new(1 * i, 1 * i, 1 * i);
        }
    }
}