using BehaviorTree;
using System.Collections.Generic;
using System.Diagnostics;

public class MonsterDataNinja : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float rangeAttackDistance = 6f;
    public static float rangeAttack = 2f;
    public static float HealthSpeed = 2f;
    public static float MaxHealth = 10f;
    public static float speed = 2f;

    protected override NodeEmile SetupTree()
    {
        NodeEmile root = new Selector(new List<NodeEmile>
        {
            new Sequence(new List<NodeEmile>
            {
                new CheckCover(transform),
                new GoToCover(transform),
                new Health(transform),
            }),
            new Selector(new List<NodeEmile>
            {
                new Sequence(new List<NodeEmile>
                {
                    new CheckRangeAttack(transform),
                    new Attack(transform),
                }),
                new Sequence(new List<NodeEmile>
                {
                    new CheckAttackDistance(transform),
                    new AttackDistance(transform),
                }),
            }),
            new Sequence(new List<NodeEmile>
            {
                new CheckNoAttack(transform),
                new Patrol(transform, waypoints),
            }),
        });
        return root;
    }
}
