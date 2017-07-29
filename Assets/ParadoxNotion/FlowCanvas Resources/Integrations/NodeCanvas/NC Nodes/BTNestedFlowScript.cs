using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;

using FlowCanvas;

namespace NodeCanvas.BehaviourTrees{

	[Name("FlowScript")]
	[Category("Nested")]
	[Description("Executes a nested FlowScript. Returns Running while the FlowScript is active. You can Finish the FlowScript with the 'Finish' node and return Success or Failure")]
	public class BTNestedFlowScript : BTNode, IGraphAssignable{

		[SerializeField]
		private BBParameter<FlowScript> _flowScript = null;
		private Dictionary<FlowScript, FlowScript> instances = new Dictionary<FlowScript, FlowScript>();

		public FlowScript flowScript{
			get {return _flowScript.value;}
			set {_flowScript.value = value;}
		}

		Graph IGraphAssignable.nestedGraph{
			get {return flowScript;}
			set {flowScript = (FlowScript)value;}
		}

		Graph[] IGraphAssignable.GetInstances(){ return instances.Values.ToArray(); }


		protected override Status OnExecute(Component agent, IBlackboard blackboard){
			
			if (flowScript == null){
				return Status.Failure;
			}

			if (status == Status.Resting){
				CheckInstance();
				status = Status.Running;
				flowScript.StartGraph(agent, blackboard, false, OnFlowScriptFinished);
			}

			if (status == Status.Running){
				flowScript.UpdateGraph();
			}

			return status;
		}

		void OnFlowScriptFinished(bool success){
			if (status == Status.Running){
				status = success? Status.Success : Status.Failure;
			}
		}

		protected override void OnReset(){
			if (IsInstance(flowScript)){
				flowScript.Stop();
			}
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
		
		protected override void OnNodeGUI(){
			
			GUILayout.Label(_flowScript.ToString());

			if (flowScript == null){
				if (!Application.isPlaying && GUILayout.Button("CREATE NEW"))
					Node.CreateNested<FlowScript>(this);
			}
		}

		protected override void OnNodeInspectorGUI(){
			EditorUtils.BBParameterField("FlowScript", _flowScript);

			if (flowScript == null){
				return;
			}

	    	var defParams = flowScript.GetDefinedParameters();
	    	if (defParams.Length != 0){

		    	EditorUtils.TitledSeparator("Defined Nested FlowScript Parameters");
		    	GUI.color = Color.yellow;
		    	UnityEditor.EditorGUILayout.LabelField("Name", "Type");
				GUI.color = Color.white;
		    	var added = new List<string>();
		    	foreach(var bbVar in defParams){
		    		if (!added.Contains(bbVar.name)){
			    		UnityEditor.EditorGUILayout.LabelField(bbVar.name, bbVar.varType.FriendlyName());
			    		added.Add(bbVar.name);
			    	}
		    	}
		    	if (GUILayout.Button("Check/Create Blackboard Variables")){
		    		flowScript.CreateDefinedParameterVariables(graphBlackboard);
		    	}
		    }

		}

		#endif
	}
}