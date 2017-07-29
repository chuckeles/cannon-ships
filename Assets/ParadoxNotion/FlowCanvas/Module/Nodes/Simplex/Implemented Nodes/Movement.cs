using UnityEngine;
using System.Collections;
using ParadoxNotion.Design;

#if UNITY_5_5_OR_NEWER
using NavMeshAgent = UnityEngine.AI.NavMeshAgent;
#endif

namespace FlowCanvas.Nodes{

	[Category("Functions/Navigation")]
	[Description("Moves a NavMeshAgent object with pathfinding to target destination")]
	public class MoveTo : LatentActionNode<NavMeshAgent, Vector3, float, float>{
		private NavMeshAgent agent;
		public override IEnumerator Invoke(NavMeshAgent agent, Vector3 destination, float speed, float stoppingDistance){
			this.agent = agent;
			agent.speed = speed;
			agent.stoppingDistance = stoppingDistance;
			
			if (agent.speed > 0){
				agent.SetDestination(destination);
			} else {
				agent.Warp(destination);
			}

			while (agent.pathPending || agent.remainingDistance > stoppingDistance){
				yield return null;
			}
		}

		public override void OnBreak(){	agent.ResetPath(); }
	}
}