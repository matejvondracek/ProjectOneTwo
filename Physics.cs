using System;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectOneTwo;
using System.Collections.Generic;
using System.Linq;

public class Physics
{
    private readonly static Barrier[] obstacles = new Barrier[100];
    private readonly static Player1[] Entities = new Player1[5]; ///Player1 class should be replaced by Player - a supertype
    readonly static List<Attack> Attacks = new List<Attack>();

    private Vector2 gravity = new Vector2(0, 5);
    private int obstacle_count = -1, entity_count = -1;

    public Physics() 
    {
    }

    private void AddBarrier(int Ax, int Ay, int Bx, int By)
    {
        obstacles[++obstacle_count] = new Barrier(Ax, Ay, Bx, By);
    }

    public void LoadMap()
    {
        //4 walls on screen bezels
        //AddBarrier(0, 180, 320, 181);
        AddBarrier(320, 0, 321, 180);
        AddBarrier(0, -1, 320, 0);
        AddBarrier(-1, 0, 0, 180);

        //world blocks
        AddBarrier(0, 136, 135, 179);
        AddBarrier(104, 120, 143, 151);
    }


    public void AddEntity(ref Player1 entity)
    {
        Entities[++entity_count] = entity;
    }

    public void AttacksUpdate()
    {
        if (Attacks.Count() != 0) 
        {
            Attacks.Clear();
        }       

        for (int e = 0; e <= entity_count; e++)
        {
            if (Entities[e].attack != new Rectangle())
            {
                Attacks.Add(new Attack(Entities[e].attack, Entities[e].damage, Entities[e].knockback, Entities[e].A_image));
            }
        }

        for (int e = 0; e <= entity_count; e++)
        {
            foreach (Attack attack in Attacks)
            {
                if (attack.Check(Entities[e].hitbox)) 
                {
                    Entities[e].life -= attack.damage;
                }
            }
        }
    }

    public void MoveUpdate()
    {       
        for (int e = 0; e <= entity_count; e++)
        {
            if (!Entities[e].dead)
            {
                Entities[e].move += gravity * Entities[e].fall;                

                ObstacleCollision(ref Entities[e]);
                EntityCollision(ref Entities[e]);

                Entities[e].GravityAcceleration();

                Entities[e].Jump();               
                
                //makes the movement
                Entities[e].pos += Entities[e].move;
                Entities[e].hitbox = new Rectangle(Entities[e].pos.ToPoint(), new Point(Entities[e].Width, Entities[e].Height));
            }          
        }        
    }

    private void ObstacleCollision(ref Player1 entity)
    {
        for (int i = 0; i <= obstacle_count; i++)
        {
            Barrier barrier = obstacles[i];
            Vector2 move = barrier.Check(entity.hitbox, entity.move);
            //ChangeVector(ref entity, move);
            entity.move = move;
        }
    }

    private void EntityCollision(ref Player1 entity)
    {
        for (int i = 0; i <= entity_count; i++)
        {
            if (Entities[i] != entity)
            {
                ///Barrier barrier = new Barrier(Entities[i].hitbox); ///ideal solution but doesnt work
                Barrier barrier = new Barrier(new Rectangle(Convert.ToInt32(Entities[i].pos.X - 13 * 6), Convert.ToInt32(Entities[i].pos.Y) - 13 * 6, (Entities[i].Width + 13) * 6, (Entities[i].Height + 13) * 6)); ///not ideal but does work
                Vector2 move1 = barrier.Check(entity.hitbox, entity.move);
                Vector2 move2 = barrier.Check(entity.hitbox, entity.move + new Vector2(0, -5));

                //to prevent character pinning
                if (move1.Length() == move2.Length()) entity.move.Y = 0;
                else entity.move = move1;//ChangeVector(ref entity, move1);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int e = 0; e <= entity_count; e++)
        {
          spriteBatch.Draw(Entities[e].image, new Rectangle(Convert.ToInt32(Entities[e].pos.X), Convert.ToInt32(Entities[e].pos.Y), 16 * 6, 16 * 6), Color.White);   
        }
        foreach (Attack attack in Attacks)
        {
            attack.Draw(spriteBatch);
        }
    }

    public ScreenManager.Winner GameRules()
    {
        //checkes whether any player is off screen
        Rectangle screen = new Rectangle(0, 0, 1920, 1080);
        Rectangle exile = new Rectangle(2000, 2000, 1000, 1000);
        for (int e = 0; e <= entity_count; e++)
        {
            if ((!screen.Contains(Entities[e].pos.ToPoint())) && (!exile.Contains(Entities[e].pos.ToPoint())))
            {
                Entities[e].life = 0;
                Entities[e].dead = true;
                Entities[e].times_dead += 1;
            }
        }

        //handeling death
        for (int e = 0; e <= entity_count; e++)
        {
            if (Entities[e].life <= 0) Entities[e].dead = true;
            if (Entities[e].dead && (!exile.Contains(Entities[e].pos.ToPoint())))
            {
                if (Entities[e].times_dead >= 3)
                {
                    if (e == 0) return ScreenManager.Winner.Player2;
                    else return ScreenManager.Winner.Player1;
                }
                else
                {
                    Entities[e].pos = new Vector2(2020, 2020);
                    Timer timer = new Timer(1000);   ///maybe not the smartest way
                    timer.Elapsed += Entities[e].Reset;
                    timer.Enabled = true;
                    timer.AutoReset = false;
                }
                
            }
        }

        return ScreenManager.Winner.None;
    }
}
