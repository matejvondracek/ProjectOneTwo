using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public static class Physics
{
    private readonly static Barrier[] obstacles = new Barrier[100];
    public static ProjectOneTwo.Player1[] Entities= new ProjectOneTwo.Player1[5]; ///Player1 class should be replaced by Player - a supertype
    
    private static Vector2 gravity = new Vector2(0, 5);  
    private static int obstacle_count = 0, entity_count = -1;
    
    
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

    public static void AddEntity(ref ProjectOneTwo.Player1 entity) 
    {
        Entities[++entity_count] = entity;
    }

    public static void MoveUpdate()
    {       
        for (int e = 0; e <= entity_count; e++)
        {
            for (int i = 0; i <= obstacle_count; i++) 
            {
                Entities[e].move += gravity;
                Barrier barrier = obstacles[i];
                Vector2 vector2 = barrier.Check(Entities[e].pos, Entities[e].move);
                if (vector2.Length() != 0)
                {
                    if (vector2.Length() < Entities[e].move.Length())
                    {
                        Entities[e].move = vector2;
                    }
                }
                else
                {
                    Entities[e].move = new Vector2(0, 0);
                    break;
                }
            }
        }
        
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        for (int e = 0; e <= entity_count; e++)
        {
            ///spriteBatch.Draw(Entities[e].image, new Rectangle(Convert.ToInt32(Entities[e].pos.X), Convert.ToInt32(Entities[e].pos.Y), 16 * 6, 16 * 6), Color.White);
        }
    }
}
