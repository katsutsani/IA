using BehaviorTree;
using System.Collections.Generic;

public class MonsterData : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float rangeSee = 6f;
    public static float rangeAttack = 2f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckRangeAttack(transform),
                new Attack(transform),
            }),
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
