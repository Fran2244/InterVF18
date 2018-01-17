using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Look")]
public class LookDecition : Decision
{
    public override bool Decide(StateController controller)
    {
        RaycastHit hit;

        Debug.DrawRay(controller.eye.position, controller.eye.forward.normalized * 3.0f, Color.green);

        if (Physics.SphereCast(controller.eye.position,
                              0.3f,
                              controller.eye.forward,
                              out hit,
                              3.0f)
           && hit.collider.CompareTag("Enemy"))
        {
            controller.target = hit.transform;
            return true;
        }
        else
        {
            return false;
        }
    }
}
