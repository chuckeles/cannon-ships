using NodeCanvas.Framework;
using UnityEngine;

public class EnergyPod : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<Ship>() == null) {
            return;
        }

        var myBlackboard = GetComponent<Blackboard>();
        var myEnergy = myBlackboard.GetVariable<float>("energy");

        var otherBlackboard = other.gameObject.GetComponent<Blackboard>();
        var otherEnergy = otherBlackboard.GetVariable<float>("energy");
        var otherMaxEnergy = otherBlackboard.GetVariable<float>("maxEnergy");

        var newEnergy = Mathf.Min(otherEnergy.GetValue() + myEnergy.GetValue(), otherMaxEnergy.GetValue());
        otherEnergy.SetValue(newEnergy);

        var particles = transform.GetChild(0);
        particles.parent = null;
        particles.GetComponent<ParticleSystem>().Play();
        Destroy(particles.gameObject, 1);

        Destroy(gameObject);
    }

}
