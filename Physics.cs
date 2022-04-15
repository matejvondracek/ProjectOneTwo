using System;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectOneTwo;

public class Physics
{
    private readonly Barrier[] obstacles = new Barrier[100];
    private readonly Player1[] Entities = new Player1[5]; ///Player1 class should be replaced by Player - a supertype
    private readonly Attack[] Attacks = new Attack[5];

    private Vector2 gravity = new Vector2(0, 5);
    private int obstacle_count = -1, entity_count = -1, attack_count = -1;

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

    private void ChangeVector(ref Player1 entity, Vector2 move)
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

    public void MoveUpdate()
    {       
        for (int e = 0; e <= entity_count; e++)
        {
            if (!Entities[e].dead)
            {
                Entities[e].move += gravity * Entities[e].fall;                

                //checkes to see, if the player is standing on ground
                ObstacleCollision(ref Entities[e]);
                EntityCollision(ref Entities[e]);

                Entities[e].GravityAcceleration();

                Entities[e].Jump();               

                if (Entities[e].is_in_jump)
                {                   
                    //making sure player cant jump into anything above them
                    ObstacleCollision(ref Entities[e]);
                    EntityCollision(ref Entities[e]);
                }
                
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
            ChangeVector(ref entity, move);
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
                Vector2 move = barrier.Check(entity.hitbox, entity.move);
                ChangeVector(ref entity, move);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int e = 0; e <= entity_count; e++)
        {
          spriteBatch.Draw(Entities[e].image, new Rectangle(Convert.ToInt32(Entities[e].pos.X), Convert.ToInt32(Entities[e].pos.Y), 16 * 6, 16 * 6), Color.White);
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
