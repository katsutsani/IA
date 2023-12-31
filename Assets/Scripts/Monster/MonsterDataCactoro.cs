using BehaviorTree;
using System.Collections.Generic;

public class MonsterDataCactoro : Tree
{
    public static float rangeAttackDistance = 500;

    protected override NodeEmile SetupTree()
    {
        NodeEmile root = new Selector(new List<NodeEmile>
        {
            new Sequence(new List<NodeEmile>
            {
                new CheckRangeAttack(transform),
                new AttackDistance(transform),
            }),
        });
        return root;
    }
}
