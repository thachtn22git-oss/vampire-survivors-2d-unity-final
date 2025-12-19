using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;
    private float floatspeed;
    void Start()
    {
        floatspeed = Random.Range(0.1f, 1.5f);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * floatspeed;
    }

    public void SetValue(int value)
    {
        damageText.text = value.ToString();
    }
}
