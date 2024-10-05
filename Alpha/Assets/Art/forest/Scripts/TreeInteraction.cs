//using UnityEngine;

//public class TreeInteraction : MonoBehaviour
//{
//    public GoldenApple goldenApple;
//    private bool isTreeCut = false;
//    private Animator treeAnimator;

//    private void Start()
//    {
//        treeAnimator = GetComponent<Animator>();
//        if (treeAnimator == null)
//        {
//            Debug.LogError("Animator component not found on Tree!");
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player") && !isTreeCut)
//        {
//            if (GameManager.HasAxe)
//            {
//                CutTree();
//            }
//            else
//            {
//                Debug.Log("You need an axe to cut this tree!");
//            }
//        }
//    }

//    private void CutTree()
//    {
//        isTreeCut = true;
//        StartTreeFallingAnimation();
//        StartAppleFallingAnimation();
//    }

//    private void StartTreeFallingAnimation()
//    {
//        if (treeAnimator != null)
//        {
//            treeAnimator.SetTrigger("Cut");
//            Debug.Log("Tree falling animation started");
//        }
//    }

//    private void StartAppleFallingAnimation()
//    {
//        if (goldenApple != null)
//        {
//            goldenApple.StartFalling();
//            Debug.Log("Apple falling animation triggered");
//        }
//        else
//        {
//            Debug.LogError("Golden Apple reference is not set!");
//        }
//    }
//}


using UnityEngine;

public class TreeInteraction : MonoBehaviour
{
    public GoldenApple goldenApple;
    public float interactionDistance = 2f; // Distance within which the player can interact with the tree
    private bool isTreeCut = false;
    private Animator treeAnimator;
    private Transform player;

    private void Start()
    {
        treeAnimator = GetComponent<Animator>();
        if (treeAnimator == null)
        {
            Debug.LogError("Animator component not found on Tree!");
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player not found!");
        }
    }

    private void Update()
    {
        if (!isTreeCut && IsPlayerInRange() && Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.HasAxe)
            {
                CutTree();
            }
            else
            {
                Debug.Log("You need an axe to cut this tree!");
            }
        }
    }

    private bool IsPlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= interactionDistance;
    }

    private void CutTree()
    {
        isTreeCut = true;
        StartTreeFallingAnimation();
        StartAppleFallingAnimation();
    }

    private void StartTreeFallingAnimation()
    {
        if (treeAnimator != null)
        {
            treeAnimator.SetTrigger("Cut");
            Debug.Log("Tree falling animation started");
        }
    }

    private void StartAppleFallingAnimation()
    {
        if (goldenApple != null)
        {
            goldenApple.StartFalling();
            Debug.Log("Apple falling animation triggered");
        }
        else
        {
            Debug.LogError("Golden Apple reference is not set!");
        }
    }
}

