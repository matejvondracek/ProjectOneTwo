using System;
using Microsoft.Xna.Framework;

public static class Physics
{
    private static Vector2 gravity = new Vector2(0, 5);
    private readonly static Barrier[] obstacles = new Barrier[100];
    private static int obstacle_count = 0;
    ///static Player[] entities = new  ///player class should be replaced by entity - supertype
    public static string name;
    public static Vector2 pos, move;
    
    private static void AddBarrierBlock(Rectangle rectangle)
    {
        obstacles[++obstacle_count] = new Barrier(new Vector2(rectangle.X, rectangle.Y + rectangle.Height), new Vector2(rectangle.X, rectangle.Y));
        obstacles[++obstacle_count] = new Barrier(new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height), new Vector2(rectangle.X, rectangle.Y + rectangle.Height));
        obstacles[++obstacle_count] = new Barrier(new Vector2(rectangle.X + rectangle.Width, rectangle.Y), new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height));
        obstacles[++obstacle_count] = new Barrier(new Vector2(rectangle.X, rectangle.Y), new Vector2(rectangle.X + rectangle.Width, rectangle.Y));
    }

    public static void LoadMap()
    {
        //rectangle arround screen
        obstacles[0] = new Barrier(new Vector2(0, 0), new Vector2(0, 1080));
        obstacles[1] = new Barrier(new Vector2(0, 1080 - 6 * 16), new Vector2(1920, 1080 - 6 * 16));
        obstacles[2] = new Barrier(new Vector2(1920 - 6 * 16, 1080), new Vector2(1920 - 6 * 16, 0));
        obstacles[3] = new Barrier(new Vector2(1920, 0), new Vector2(0, 0));
        obstacle_count = 3;

        //jumping blocks      
        AddBarrierBlock(new Rectangle(-1 * 6, (136 - 16) * 6, 136 * 6, 60 * 6));
        AddBarrierBlock(new Rectangle((105 - 16) * 6, (120 - 16) * 6, (39 + 16) * 6, (31 + 16) * 6));
    }

    public static void AddEntity(string _name, Vector2 _pos, Vector2 _move) ///rectange4le
    {
        name = _name;
        pos = _pos;
        move = _move;
    }
    
    public static void Update()
    {
        //gravity
        move += gravity;
        
        ///additional loop
        for (int i = 0; i <= obstacle_count; i++) 
        {
            Barrier barrier = obstacles[i];
            Vector2 vector2 = barrier.Check(pos, move);
            if (vector2.Length() != 0)
            {
                if (vector2.Length() < move.Length())
                {
                    move = vector2;
                }
            }
            else
            {                
                move = new Vector2(0, 0);
            }
        }
    }

    public static Vector2 GetPos(string name)
    {
        return pos + move;
    }

    public static void AddAttack(Rectangle rectangle, int damage)
    {

    }
}
