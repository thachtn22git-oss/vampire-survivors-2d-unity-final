using UnityEngine;
using System.Collections.Generic;

public class GarlicPrefab : MonoBehaviour
{
    public Garlic weapon;
    private Vector3 targetSize;
    private float timer;
    public List<Enemy> enemiesInRange;
    public float counter;
    void Start()
    {
        weapon = GameObject.Find("Garlic").GetComponent<Garlic>();
        // Destroy(gameObject, weapon.duration);
        targetSize = Vector3.one * weapon.stats[weapon.weaponLevel].range;
        transform.localScale = Vector3.zero;
        timer = weapon.stats[0].duration;
        AudioController.Instance.PlayModifiedSound(AudioController.Instance.areaWeaponSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime * 5);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            targetSize = Vector3.zero;
            if (transform.localScale.x == 0)
            {
                Destroy(gameObject);
                AudioController.Instance.PlayModifiedSound(AudioController.Instance.areaWeaponDespawn);
            }
        }
        // deal damage to enemies in range
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            counter = weapon.stats[weapon.weaponLevel].speed;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                enemiesInRange[i].TakeDamage(weapon.stats[weapon.weaponLevel].damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collider.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collider.GetComponent<Enemy>());
        }       
    }
}
