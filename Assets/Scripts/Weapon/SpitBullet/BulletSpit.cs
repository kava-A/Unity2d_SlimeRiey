using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ˮ�ӵ�
<<<<<<< HEAD
/// ��������ҡ�NPC������������ײ���
/// ײ�������˺�������Ʒ������
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
/// </summary>
public class BulletSpit : Weapon
{
    [SerializeField, Tooltip("�ӵ�������Ч�����������壬��δ����״̬")] public GameObject[] effect;
    [SerializeField, Tooltip("��ը��ЧԤ����")] public GameObject explosion;

    private int index = 0;
    GameObject go;//������ʱ�洢��ը��Ч
    private void Start()
    {
        rb.velocity = transform.right * speed;
        //Destroy(gameObject, range);
    }
    private void OnEnable()
    {
        index = Random.Range(0, effect.Length);//�����ȡһ���ӵ�������Ч
        effect[index].SetActive(true);

        go = Instantiate(explosion);//������ը��Ч
        go.SetActive(false);//���ñ�ը��ЧΪδ����
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            effect[index].SetActive(false);

            Destroy(gameObject);
        }
<<<<<<< HEAD
        else if (!collision.CompareTag("Player")&&!collision.CompareTag("NPC")&&!collision.CompareTag("Weapon"))
=======
        else if (!collision.CompareTag("Player"))
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
        {
            Destroy(gameObject);

        }
    }
<<<<<<< HEAD
    private void OnDisable()
    {
        effect[index].SetActive(false);
        go.transform.position = transform.position;//���ñ�ը��Ч����
        go.transform.rotation = Quaternion.identity;//����ת
        go.SetActive(true);
    }
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    /// <summary>
    /// ����Ϸ��������ʱ���ñ�ը��Ч
    /// </summary>
    private void OnDestroy()
    {
<<<<<<< HEAD
        
=======
        go.transform.position = transform.position;//���ñ�ը��Ч����
        go.transform.rotation = Quaternion.identity;//����ת
        go.SetActive(true);
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    }
}
