using System;
using Microsoft.Xna.Framework;

public static class Physics
{
    static float gravity = 5f;
    static Barrier[] obstacles = new Barrier[4];
    ///static Player[] entities = new  ///player class should be replaced by entity - supertype
    public static string name;
    public static Vector2 pos, move;
    public static void LoadMap()
    {
        obstacles[0] = new Barrier(new Vector2(0, 0), new Vector2(0, 1080));
        obstacles[1] = new Barrier(new Vector2(0, 1080 - 6 * 16), new Vector2(1920, 1080 - 6 * 16));
        obstacles[2] = new Barrier(new Vector2(1920 - 6 * 16, 1080), new Vector2(1920 - 6 * 16, 0));
        obstacles[3] = new Barrier(new Vector2(1920, 0), new Vector2(0, 0));
    }

    public static void AddEntity(string _name, Vector2 _pos, Vector2 _move)
    {
        name = _name;
        pos = _pos;
        move = _move;
    }
    
    public static void Update()
    {
        ///additional loop
        for (int i = 0; i < obstacles.Length; i++) 
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
}
