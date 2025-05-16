using UnityEngine;
using System.Collections;

public class PlayerBattleController : MonoBehaviour
{
    [Header("References")]
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform childTransform = transform.Find("Walking");
        GameObject childGameObject = childTransform.gameObject;
        animator = childGameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        Debug.Log("Player attacks!");
        StartCoroutine(AttackAnimation());
    }

    private IEnumerator AttackAnimation()
    {
        animator.SetBool("Punching", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Punching", false);
    }
}
