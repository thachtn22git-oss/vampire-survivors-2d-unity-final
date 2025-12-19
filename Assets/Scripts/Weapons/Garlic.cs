using Unity.VisualScripting;
using UnityEngine;

public class Garlic : Weapon
{
    [SerializeField] private GameObject prefab;
    private float spawncounter;

    void Update()
    {
        spawncounter -= Time.deltaTime; 
        if (spawncounter <= 0)
        {
            spawncounter = stats[weaponLevel].cooldown;
            Instantiate(prefab, transform.position, transform.rotation, transform);
        }
    }
}
