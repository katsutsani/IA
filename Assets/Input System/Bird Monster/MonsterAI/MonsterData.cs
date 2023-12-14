using BehaviorTree;
using System.Collections.Generic;

public class MonsterData : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float rangeSee = 6f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckRangePlayer(transform),
                new GoToPlayer(transform),
            }),
            new Patrol(transform, waypoints),
        });
        return root;
    }
}
