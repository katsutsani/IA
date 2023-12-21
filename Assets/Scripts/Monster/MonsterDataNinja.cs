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

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckCover(transform),
                new GoToCover(transform),
                new Health(transform),
            }),
            new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckRangeAttack(transform),
                    new Attack(transform),
                }),
                /*new Sequence(new List<Node>
                {
                    new CheckAttackDistance(transform),
                    new Attack(transform),
                }),*/
            }),
            new Sequence(new List<Node>
            {
                new CheckNoAttack(transform),
                new Patrol(transform, waypoints),
            }),
        });
        return root;
    }
}
