using BehaviorTree;
using System.Collections.Generic;

public class MonsterDataBird : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float rangeSee = 6f;
    public static float rangeAttack = 2f;

    protected override NodeEmile SetupTree()
    {
        NodeEmile root = new Selector(new List<NodeEmile>
        {
            new Sequence(new List<NodeEmile>
            {
                new CheckRangeAttack(transform),
                new Attack(transform),
            }),
            new Sequence(new List<NodeEmile>
            {
                new CheckRangePlayer(transform),
                new GoToPlayer(transform),
            }),
            new Patrol(transform, waypoints),
        });
        return root;
    }
}
