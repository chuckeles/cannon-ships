using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;

using FlowCanvas;

namespace NodeCanvas.DialogueTrees{

	[Name("FlowScript")]
	[Category("Nested")]
	[Description("Executes a FlowScript. Use the Finish flowscript node to continue the dialogue. The actor selected here will also be used in the flowcript for 'Self'")]
	public class DTNestedFlowScript : DTNode, IGraphAssignable {

		[SerializeField]
		private BBParameter<FlowScript> _flowScript = null;
		private Dictionary<FlowScript, FlowScript> instances = new Dictionary<FlowScript, FlowScript>();

		public override int maxOutConnections{
			get {return 2;}
		}

		public FlowScript flowScript{
			get {return _flowScript.value;}
			set {_flowScript.value = value;}
		}

		Graph IGraphAssignable.nestedGraph{
			get {return flowScript;}
			set {flowScript = (FlowScript)value;}
		}

		Graph[] IGraphAssignable.GetInstances(){ return instances.Values.ToArray(); }


		protected override Status OnExecute(Component agent, IBlackboard bb){
			if (flowScript == null){
				return Error("FlowScript is null");
			}

			CheckInstance();
			status = Status.Running;
			flowScript.StartGraph(finalActor.transform, graphBlackboard, false, OnFlowScriptFinish);
			StartCoroutine( UpdateGraph() );
			return status;
		}

		IEnumerator UpdateGraph(){
			while (status == Status.Running){
				flowScript.UpdateGraph();
				yield return null;
			}
		}

		void OnFlowScriptFinish(bool success){
			status = success? Status.Success : Status.Failure;
			DLGTree.Continue(success? 0 : 1);
		}

		public override void OnGraphPaused(){
			if (IsInstance(flowScript)){
				flowScript.Pause();
			}
		}

		public override void OnGraphStoped(){
			if (IsInstance(flowScript)){
				flowScript.Stop();
			}
		}

		bool IsInstance(FlowScript fs){
			return instances.Values.Contains(fs);
		}

		void CheckInstance(){

			if (IsInstance(flowScript)){
				return;
			}

			FlowScript instance = null;
			if (!instances.TryGetValue(flowScript, out instance)){
				instance = Graph.Clone<FlowScript>(flowScript);
				instances[flowScript] = instance;
			}

            instance.agent = graphAgent;
		    instance.blackboard = graphBlackboard;
			flowScript = instance;
		}

		////////////////////////////////////////
		///////////GUI AND EDITOR STUFF/////////
		////////////////////////////////////////
		#if UNITY_EDITOR

		public override string GetConnectionInfo(int i){
			return i == 0? "Success" : "Failure";
		}
		
		protected override void OnNodeGUI(){
			
			GUILayout.Label(_flowScript.ToString());

			if (flowScript == null){
				if (!Application.isPlaying && GUILayout.Button("CREATE NEW"))
					Node.CreateNested<FlowScript>(this);
			}
		}

		protected override void OnNodeInspectorGUI(){
			base.OnNodeInspectorGUI();
			EditorUtils.BBParameterField("FlowScript", _flowScript);
		}

		#endif
	}
}