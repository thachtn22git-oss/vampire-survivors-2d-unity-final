using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public DamageNumber prefab;
    public static DamageNumberController Instance;

    public  void Awake()
    {
       if (Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    public void CreateNumber(float value, Vector3 location)
    {
        DamageNumber damageNumber = Instantiate(prefab, location, transform.rotation, transform);
        damageNumber.SetValue(Mathf.RoundToInt(value));
    }
}
