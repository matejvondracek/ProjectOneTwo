﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectOneTwo;

public static class Physics
{
    private readonly static Barrier[] obstacles = new Barrier[100];
    private readonly static Player1[] Entities = new Player1[5]; ///Player1 class should be replaced by Player - a supertype
    private readonly static Attack[] Attacks = new Attack[5];


    private static Vector2 gravity = new Vector2(0, 5);
    private static int obstacle_count = -1, entity_count = -1, attack_count = -1;

    private static void AddBarrier(int Ax, int Ay, int Bx, int By)
    {
        obstacles[++obstacle_count] = new Barrier(Ax, Ay, Bx, By);
    }

    public static void LoadMap()
    {
        //4 walls on screen bezels
        AddBarrier(0, 180, 320, 181);
        AddBarrier(320, 0, 321, 180);
        AddBarrier(0, -1, 320, 0);
        AddBarrier(-1, 0, 0, 180);

        //world blocks
        AddBarrier(0, 136, 135, 179);
        AddBarrier(104, 120, 143, 151);
    }


    public static void AddEntity(ref Player1 entity)
    {
        Entities[++entity_count] = entity;
    }

    public static void AttacksUpdate()
    {
        if (attack_count != -1) 
        {
            Array.Clear(Attacks, 0, attack_count);
            attack_count = -1;
        }       

        for (int e = 0; e <= entity_count; e++)
        {
            if (Entities[e].attack != new Rectangle(0, 0, 0, 0))
            {
                Attacks[++attack_count] = new Attack(Entities[e].attack, Entities[e].damage, Entities[e].knockback);
            }
        }

        for (int e = 0; e <= entity_count; e++)
        {
            for (int i = 0; i <= attack_count; i++)
            {
                if (Attacks[i].Check(Entities[e].pos))
                {
                    Entities[e].life -= Attacks[i].damage;
                    Entities[e].move += Attacks[i].knockback;
                }                                  
            }
        }
    }

    private static void ChangeVector(ref Player1 entity, Vector2 move)
    {
        if (move.Length() != 0)
        {
            if (move.Length() < entity.move.Length())
            {
                entity.move = move;
            }
        }
        else
        {
            entity.move = new Vector2(0, 0);
        }
        
    }

    public static void MoveUpdate()
    {       
        for (int e = 0; e <= entity_count; e++)
        {
            Entities[e].move += gravity;
            
            //collision with obstacles
            for (int i = 0; i <= obstacle_count; i++)
            {
                Barrier barrier = obstacles[i];
                Vector2 move = barrier.Check(Entities[e].hitbox, Entities[e].move);
                ChangeVector(ref Entities[e], move);
            }   

            //collision with entities
            for (int i = 0; i <= entity_count; i++)
            {
                if (i != e)
                {
                    ///Barrier barrier = new Barrier(Entities[i].hitbox); ///ideal solution but doesnt work
                    Barrier barrier = new Barrier(new Rectangle(Convert.ToInt32(Entities[i].pos.X - 13 * 6), Convert.ToInt32(Entities[i].pos.Y) - 13 * 6, (Entities[i].Width + 13) * 6, (Entities[i].Height + 13) * 6)); ///not ideal but does work
                    Vector2 move = barrier.Check(Entities[e].hitbox, Entities[e].move);
                    ChangeVector(ref Entities[e], move);
                }
                
            }

            //makes the movement
            Entities[e].pos += Entities[e].move;
            Entities[e].hitbox = new Rectangle(Entities[e].pos.ToPoint(), new Point(Entities[e].Width, Entities[e].Height));
        }        
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        for (int e = 0; e <= entity_count; e++)
        {
          spriteBatch.Draw(Entities[e].image, new Rectangle(Convert.ToInt32(Entities[e].pos.X), Convert.ToInt32(Entities[e].pos.Y), 16 * 6, 16 * 6), Color.White);
        }
    }
}
