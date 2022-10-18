using System.Collections.Generic; // new
using UnityEngine;
using UnityEngine.SceneManagement; // new

public class DragonPicker : MonoBehaviour
{
    public GameObject energyShieldPrefab;
    public int numEnergyShield = 3;
    public float energyShieldBottomY = -6;
    public float energyShieldRadius = 1.5f;
    public List<GameObject> shieldList; // new

    private void Start()
    {
        shieldList = new(); // new

        for (int i = 1; i <= numEnergyShield; i++)
        {
            GameObject tShieldGo = Instantiate(energyShieldPrefab);
            tShieldGo.transform.position = new(0, energyShieldBottomY, 0);
            tShieldGo.transform.localScale = new(1 * i, 1 * i, 1 * i);
            shieldList.Add(tShieldGo); // new
        }
    }

    public void DragonEggDestroyed()
    {
        GameObject[] tDragonEggArray = GameObject.FindGameObjectsWithTag("Dragon Egg");
        foreach (GameObject tGO in tDragonEggArray)
            Destroy(tGO);

        int shieldIndex = shieldList.Count - 1; // new
        GameObject tShieldGo = shieldList[shieldIndex]; // new
        shieldList.RemoveAt(shieldIndex); // new
        Destroy(tShieldGo); // new

        if (shieldList.Count == 0) // new
            SceneManager.LoadScene("_0Scene"); // new
    }
}