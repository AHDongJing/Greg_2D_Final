using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhisycsCheck : MonoBehaviour
{
    private PlayerController playerController;
    private CapsuleCollider2D coll;
    private Rigidbody2D rb;
    [Header("手动检测碰撞体")]
    public bool manual;

    [Header("手动检测脚本是否属于palyer")]
    public bool isPlayer;
    [Header("物体脚底位移差值")]
    public Vector2 bottomOffset;
    [Header("物体左侧位移差值")]
    public Vector2 leftOffset;
    [Header("物体右侧位移差值")]
    public Vector2 rightOffset;
    //检测范围
    public float checkRaduis;
    //碰撞到哪个层
    public LayerMask groundLayer;

    [Header("状态")]
    //是否碰触到地面
    public bool isGround = true;
    [Header("左侧撞墙检测")]
    public bool touchLeftWall;
    [Header("右侧撞墙检测")]
    public bool touchRightWall;
    [Header("爬墙检测")]
    public bool onWall;



    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        //自动墙壁碰撞检测，如果时非手动的情况下
        if (!manual)
        {
            //右侧碰撞体等于 物体身上的碰撞体范围+碰撞体位移值/2
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            //左侧碰撞体的x范围直接是负的右侧位移差值即可，高度两边都一样
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
        //如果挂载脚本的的ispalyer物体为true 再去获取PlayerController
        if (isPlayer)
        {
            playerController = GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }
    public void Check()
    {
        if (onWall)
        {
            //爬墙的时候检测地面需要给一个y轴的位移差值，解决落地后碰撞体比人先碰触到地面的问题
            isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRaduis, groundLayer);
        }
        else
        {
            //碰触地面，以挂载脚本的（物体中心点，范围的多少，是否碰撞到层）如果碰触到地面的话把结果返回给isGround一个布尔值
            isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, 0), checkRaduis, groundLayer);
        }

        //左侧碰触墙体检测(检测逻辑和碰触地面相同)
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(leftOffset.x, leftOffset.y), checkRaduis, groundLayer);
        //右侧碰触墙体检测(检测逻辑和碰触地面相同)
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(rightOffset.x, rightOffset.y), checkRaduis, groundLayer);

        //只有player的情况下才去判断爬墙检测
        if (isPlayer)
        {
            //爬墙检测(撞在左墙和右墙同时按住了方向键并且不在地面上)
            onWall = (touchLeftWall && playerController.inputDirection.x < 0f || touchRightWall && playerController.inputDirection.x > 0f) && rb.velocity.y < 0f;
        }



    }
    //当物体被选中时一直绘制
    private void OnDrawGizmosSelected()
    {
        //在物体底部绘制一个球形的检测范围
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRaduis);

        //在物体左侧绘制一个球形检测范围
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(leftOffset.x, leftOffset.y), checkRaduis);

        //在物体右侧绘制一个球形检测范围
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(rightOffset.x, rightOffset.y), checkRaduis);


    }
}
