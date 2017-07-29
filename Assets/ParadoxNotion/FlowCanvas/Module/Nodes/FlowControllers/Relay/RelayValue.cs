using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ParadoxNotion.Design;

namespace FlowCanvas.Nodes{

	[Description("Can be used with a RelayOutput of the same (T) type to get the input port value.")]
	[Category("Flow Controllers/Relay")]
	public class RelayValueInput<T> : FlowControlNode{

		[Tooltip("The identifier name of the relay")]
		public string identifier = "MyRelayValueName";
		[HideInInspector]
		public ValueInput<T> port{get; private set;}

		public override string name{
			get {return string.Format("@ {0}", identifier);}
		}

		protected override void RegisterPorts(){
			port = AddValueInput<T>("Value");
		}		
	}

	[Description("Returns the chosen RelayInput source value.\nOnly RelayInputs of the same (T) type can be chosen.")]
	[Category("Flow Controllers/Relay")]
	public class RelayValueOutput<T> : FlowControlNode {
		
		[SerializeField]
		private string _sourceInputUID;
		private string sourceInputUID{
			get {return _sourceInputUID;}
			set {_sourceInputUID = value;}
		}

		private object _sourceInput;
		private RelayValueInput<T> sourceInput{
			get
			{
				if (_sourceInput == null){
					_sourceInput = graph.GetAllNodesOfType<RelayValueInput<T>>().FirstOrDefault(i => i.UID == sourceInputUID);
					if (_sourceInput == null){
						_sourceInput = new object();
					}
				}
				return _sourceInput as RelayValueInput<T>;
			}
			set { _sourceInput = value; }
		}

		public override string name{
			get {return string.Format("{0}", sourceInput != null? sourceInput.ToString() : "@ NONE");}
		}

		protected override void RegisterPorts(){
			AddValueOutput<T>("Value", ()=>{ return sourceInput != null? sourceInput.port.value : default(T); });
		}

		////////////////////////////////////////
		///////////GUI AND EDITOR STUFF/////////
		////////////////////////////////////////
		#if UNITY_EDITOR		

		protected override void OnNodeInspectorGUI(){
			var relayInputs = graph.GetAllNodesOfType<RelayValueInput<T>>();
			var currentInput = relayInputs.FirstOrDefault(i => i.UID == sourceInputUID);
			var newInput = EditorUtils.Popup< RelayValueInput<T> >("Relay Input Source", currentInput, relayInputs);
			if (newInput != currentInput){
				sourceInputUID = newInput != null? newInput.UID : null;
				sourceInput = newInput != null? newInput : null;
			}
		}

		#endif
	}
}