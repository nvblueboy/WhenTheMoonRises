using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    public float speed, interactionRadius;
    private bool isBlocked;
    private CharacterController controller;
    private Animator animator;
    private string animationPrefix;

    private float yPosition;

	// Use this for initialization
	void Start () {
        controller = this.GetComponent<CharacterController>();

        animator = GetComponent<Animator>();

        // This will be changed based on day or night scene
        animationPrefix = Constants.NightPrefix;

        yPosition = this.transform.position.y;

        if(controller==null) {
            Debug.LogError("There's no Character controller on: " + transform.name);
        }

        Vector3 playerPostion = GameController.getLastPlayerPosition();

        Debug.Log("Player position: {" + playerPostion.x + ", " + playerPostion.y + ", " + playerPostion.z + "}");
        if (playerPostion != Vector3.zero && GameController.getCurrentScene().Contains("Day"))
        {            
            gameObject.transform.position = playerPostion;
        }
	}
	
	// Update is called once per frame
	void Update () {

        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        //Debug.Log("Blocked: " + isBlocked);
        if(!isBlocked)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRadius);
                int i = 0;
                while (i < hitColliders.Length)
                {
                    if (hitColliders[i].gameObject.tag == "Interactable")
                    {
                        Interaction[] interactions = hitColliders[i].gameObject.GetComponents<Interaction>();
                        foreach (Interaction interaction in interactions)
                        {
                            interaction.interact();
                        }
                    }
                    i++;
                }
            }            

            // For demo only
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Exiting...");
                Application.Quit();
            }

            Vector2 movement = new Vector2(horiz, vert).normalized;

            float angle = Mathf.Atan2(vert, horiz) * Mathf.Rad2Deg;

            if (angle > -22.5 && angle <= 22.5)
            {
                movement = new Vector2(1, 0).normalized;
                if (horiz > .02f)
                {
                    animator.runtimeAnimatorController = Resources.Load(
                    animationPrefix + Constants.WalkRight) as RuntimeAnimatorController;
                }
                //Debug.Log("Horizontal: " + horiz);            
            }
            else if (angle > -67.5 && angle <= -22.5)
            {
                movement = new Vector2(1, -1).normalized;
            }
            else if (angle > -112.5 && angle <= -67.5)
            {
                movement = new Vector2(0, -1).normalized;
                animator.runtimeAnimatorController = Resources.Load(
                    animationPrefix + Constants.WalkFront) as RuntimeAnimatorController;
            }
            else if (angle > -157.5 && angle <= -112.5)
            {
                movement = new Vector2(-1, -1).normalized;
            }
            else if (angle > 157.5 || angle < -157.5)
            {
                movement = new Vector2(-1, 0).normalized;
                animator.runtimeAnimatorController = Resources.Load(
                    animationPrefix + Constants.WalkLeft) as RuntimeAnimatorController;
            }
            else if (angle > 112.5 && angle <= 157.5)
            {
                movement = new Vector2(-1, 1).normalized;
            }
            else if (angle > 67.5 && angle <= 112.5)
            {
                movement = new Vector2(0, 1).normalized;
                animator.runtimeAnimatorController = Resources.Load(
                    animationPrefix + Constants.WalkBack) as RuntimeAnimatorController;
            }
            else if (angle > 22.5 && angle <= 67.5)
            {
                movement = new Vector2(1, 1).normalized;
            }


            if (horiz == 0 && vert == 0)
            {
                movement = new Vector2(0, 0);
                animator.speed = 0;
            }
            else
            {
                animator.speed = .1f * speed;
            }

            Vector3 movement3d = new Vector3(movement.x, 0, movement.y);

            //animator.speed = movement3d.x * speed + movement3d.y * speed; 


            controller.Move(movement3d * speed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        }
        else
        {
            controller.Move(Vector3.zero);
            animator.speed = 0;
        }
    }
    
    public void setPlayerCanMove(bool canMove)
    {        
        isBlocked = !canMove;
    }            
}
