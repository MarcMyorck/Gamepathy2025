using UnityEngine;

public class CartPhysics : MonoBehaviour
{
    public Rigidbody connectedBody;        // the moving body to attach to
    public Vector3 cornerLocalOnCube = new Vector3(0.5f,0.5f,0);      // which corner on the cube (local space of this)
    public Vector3 attachPointWorld;       // the world-space target on the connected body (optional)
    public bool useAttachPointWorld = false;
    public float breakForce = Mathf.Infinity;
    public float breakTorque = Mathf.Infinity;

    void Start()
    {
        if (connectedBody == null) { Debug.LogError("connectedBody required"); return; }

        // create joint
        var joint = gameObject.AddComponent<ConfigurableJoint>();
        joint.connectedBody = connectedBody;
        joint.autoConfigureConnectedAnchor = false;

        // anchor is corner position in cube local space
        joint.anchor = cornerLocalOnCube;

        // compute connectedAnchor: local point on connectedBody
        Vector3 worldPoint;
        if (useAttachPointWorld)
            worldPoint = attachPointWorld;
        else
            worldPoint = connectedBody.transform.TransformPoint(Vector3.zero); // fallback to connected body origin

        // Convert the world attach point to connectedBody local space
        joint.connectedAnchor = connectedBody.transform.InverseTransformPoint(worldPoint);

        // lock translation so that that local corner stays at the connected point
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;

        // free rotation
        joint.angularXMotion = ConfigurableJointMotion.Free;
        joint.angularYMotion = ConfigurableJointMotion.Free;
        joint.angularZMotion = ConfigurableJointMotion.Free;

        joint.breakForce = breakForce;
        joint.breakTorque = breakTorque;
    }
}
