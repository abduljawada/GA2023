using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Attack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private SpriteRenderer spriteRen;
    private void Start()
    {
        spriteRen = attackPoint.gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine("SpriteSwitch");
            // Detect enemies in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            // Damage enemies that are in range
            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Health>().LoseHealth();
                Debug.Log("Hit");
            }
        }
    }

    // draws an indicator circle of the damage radius
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);   
    }
   private IEnumerator SpriteSwitch()
    {
        spriteRen.enabled = true;
        
        yield return new WaitForSeconds(0.3f);
        spriteRen.enabled = false;
    }
}