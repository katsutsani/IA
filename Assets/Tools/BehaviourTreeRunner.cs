using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    Behaviour_tree tree;
    // Start is called before the first frame update
    void Start()
    {
        tree = ScriptableObject.CreateInstance<Behaviour_tree>();   

        var log1 = ScriptableObject.CreateInstance<DebugLogNode>();
        log1.message = "hello 1";

        var pause1 = ScriptableObject.CreateInstance<WaitNode>();

        var log2 = ScriptableObject.CreateInstance<DebugLogNode>();
        log2.message = "hello 2";

        var pause2 = ScriptableObject.CreateInstance<WaitNode>();

        var log3 = ScriptableObject.CreateInstance<DebugLogNode>();
        log3.message = "hello 3";

        var pause3 = ScriptableObject.CreateInstance<WaitNode>();


        var sequencer = ScriptableObject.CreateInstance<SequencerNode>();
        sequencer.children.Add(log1);
        sequencer.children.Add(pause1);
        sequencer.children.Add(log2);
        sequencer.children.Add(pause2);
        sequencer.children.Add(log3);
        sequencer.children.Add(pause3);

        var loop = ScriptableObject.CreateInstance<RepeatNode>();
        loop.child = sequencer;


        tree.rootNode = loop;

    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
}
