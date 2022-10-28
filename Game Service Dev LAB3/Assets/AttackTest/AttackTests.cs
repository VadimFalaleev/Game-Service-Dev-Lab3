//using UnityEngine;

//public class AttackTests : MonoBehaviour
//{
//    public float attackSpeedFirst;
//    public static int hp1;

//    public float attackSpeedSecond;
//    public static int hp2;

//    void Start()
//    {
//        GameObject first = GameObject.Find("First");
//        HealthBar heathBar1 = first.GetComponent<HealthBar>();
//        hp1 = HealthBar1.hp;

//        GameObject second = GameObject.Find("Second");
//        HealthBar heathBar2 = second.GetComponent<HealthBar>();
//        hp2 = HealthBar2.hp;

//        InvokeRepeating("First", 0.1f, attackSpeedFirst);
//        InvokeRepeating("Second", 0.1f, attackSpeedSecond);
//    }

//    void Update()
//    {
//        if (hp2 < 0 || hp1 < 0)
//            CancelInvoke();
//    }

//    public void First()
//    {
//        hp2 -= Random.Range(80, 90);

//        if (hp2 < 0)
//        {
//            Debug.Log("First Win");
//        }
//    }

//    public void Second()
//    {
//        hp1 -= Random.Range(160, 170);

//        if (hp1 < 0)
//        {
//            Debug.Log("Second Win");
//        }
//    }
//}