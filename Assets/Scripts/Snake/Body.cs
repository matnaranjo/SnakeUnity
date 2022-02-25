using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{

    [SerializeField]
    private GameObject BodyBlock=null;

    [SerializeField]
    private Transform Head = null;

    private GameObject FoodParticle = null;

    // public static GameObject[] SnakeBody = new GameObject[] {};
    public static List<GameObject> SnakeBody = new List<GameObject>();

    private float HeadPosX;
    private float HeadPosY;

    private float FoodPosX;
    private float FoodPosY;
    // Start is called before the first frame update
    void Start()
    {
        FindFood();
    }

    // Update is called once per frame
    void Update()
    {
        FindFood();
        AddBodySegment();
    }

    private void AddBodySegment(){
        HeadPosX = Head.transform.position.x;
        HeadPosY = Head.transform.position.y;
        Debug.Log("si entro");
        if ((HeadPosX>FoodPosX-0.1f && HeadPosX<FoodPosX+0.1f ) && (HeadPosY>FoodPosY-0.1f && HeadPosY<FoodPosY+0.1f)){
            int BodyLength=SnakeBody.Count;
            GameObject NewBlock = Instantiate(BodyBlock,Head.transform.position, Head.transform.rotation);
            SnakeBody.Add(NewBlock);
        }
    }

    private void FindFood(){
            FoodParticle = GameObject.FindWithTag("Food");
            FoodPosX = FoodParticle.transform.position.x;
            FoodPosY = FoodParticle.transform.position.y;
    }
}
