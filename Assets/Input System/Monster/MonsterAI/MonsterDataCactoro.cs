using BehaviorTree;
using System.Collections.Generic;

public class MonsterDataCactoro : Tree
{
    public static float rangeAttack = 500;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckRangeAttack(transform),
                new Attack(transform),
            }),
        });
        return root;
    }
}
