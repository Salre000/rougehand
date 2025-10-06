using Unity.VisualScripting;
using UnityEngine;

public class JokerObjectAnime : MonoBehaviour
{

    private Animator animator;
    private JokerObject jokerObject;

    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        jokerObject=transform.parent.GetComponent<JokerObject>();
    }

    private void Update()
    {
        if (!jokerObject.IsEnd()) return;



    }

    public void End() 
    {
        //オブジェクトの削除時のアニメーション
        BreakUtility.StartBreak(transform.parent.gameObject);
        Destroy(transform.parent.gameObject);


    }


}
