using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGUI : MonoBehaviour
{
    public Sprite sprite;

    private List<GameObject> tinkerHps;
    private List<GameObject> strongHps;
    private List<GameObject> mobileHps;

    public void SetHp(List<GameObject> target, string parent, int hp)
    {
        target.ForEach(x => Destroy(x));
        target.Clear();

        for (int i = 0; i < hp; i++)
        {
            GameObject obj = new GameObject();
            obj.transform.parent = transform.Find(parent);
            obj.transform.position = transform.Find(parent).position + new Vector3(i * 30, 0, 0);
            obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            Image img = obj.AddComponent<Image>();
            img.sprite = sprite;
            obj.SetActive(true);
            target.Add(obj);
        }
    }

    void Start()
    {
        tinkerHps = new List<GameObject>();
        strongHps = new List<GameObject>();
        mobileHps = new List<GameObject>();

        SetHp(tinkerHps, "Tinker Health", 3);
        SetHp(strongHps, "Strong Health", 3);
        SetHp(mobileHps, "Mobile Health", 3);

        GameObject.Find("TinkerCharacterWithMesh").GetComponent<Health>().HealthEvent += new Health.HealthDel((int hp) =>
        {
            SetHp(tinkerHps, "Tinker Health", hp);
        });
        GameObject.Find("StrongCharacterWithMesh").GetComponent<Health>().HealthEvent += new Health.HealthDel((int hp) =>
        {
            SetHp(strongHps, "Strong Health", hp);
        });
        GameObject.Find("MobileCharacterWithMesh").GetComponent<Health>().HealthEvent += new Health.HealthDel((int hp) =>
        {
            SetHp(mobileHps, "Mobile Health", hp);
        });
    }
}
