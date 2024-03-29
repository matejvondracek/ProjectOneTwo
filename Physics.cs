﻿using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectOneTwo;
using System.Collections.Generic;

public class Physics
{
    readonly List<Barrier> Obstacles = new List<Barrier>();
    readonly List<Player1> Entities = new List<Player1>(); 
    readonly List<Attack> Attacks = new List<Attack>();

    Vector2 gravity = new Vector2(0, 5);

    public Physics() 
    {
    }

    private void AddBarrier(int Ax, int Ay, int Bx, int By)
    {
       Obstacles.Add(new Barrier(Ax, Ay, Bx, By));
    }

    public void LoadMap()
    {
        //3 walls on screen bezels
        AddBarrier(320, 0, 321, 180);
        //AddBarrier(0, -1, 320, 0);
        AddBarrier(-1, 0, 0, 180);

        //map blocks
        AddBarrier(0, 132, 46, 180);
        AddBarrier(82, 140, 230, 180);
        AddBarrier(274, 136, 320, 180);
    }


    public void AddEntity(ref Player1 entity)
    {
        Entities.Add(entity);
    }


    #region updates
    private bool OldAttacks(Attack attack)
    {
        return attack.imageDuration <= 0;
    }

    public void AttacksUpdate()
    {
        foreach (Attack attack in Attacks)
        {
            if (attack.imageDuration > 0)
            {
                //keep attacks for image rendering
                attack.damage = 0;
                attack.knockback = new Vector2();
            }       
        }

        //delete old attacks to save memory
        Attacks.RemoveAll(OldAttacks);

        //make attacks
        foreach (Player1 entity in Entities)
        {            
            if (entity.attack != null)
            {
                Attacks.Add(entity.attack);
            }          
        }

        //check attacks
        foreach (Player1 entity in Entities)
        {
            foreach (Attack attack in Attacks)
            {
                if (attack.Check(entity.hitbox))
                {
                    
                    if (attack.damage != 0)
                    {
                        entity.life -= attack.damage;
                        Vector2 direction = attack.knockback;
                        direction.Normalize();
                        entity.dash = new Dash(direction, 20, 10);
                        if (attack.gives_stun)
                        {
                            entity.stunned = true;
                            entity.stun_timer = 60;
                        }

                        //sound effect
                        Game1.self.Sounds["sword_hit"].Volume = 1f * Game1.self.effectsVolume;
                        Game1.self.Sounds["sword_hit"].Play();
                    }                 
                }
            }
        }
    }

    public void MoveUpdate()
    {       
        foreach (Player1 entity in Entities)
        {
            if (!entity.dead)
            {
                if (entity.dash == null) entity.move += gravity * entity.fall;                

                ObstacleCollision(entity);
                EntityCollision(entity);

                entity.GravityAcceleration();

                entity.Jump();

                EntityCollision(entity);

                //makes the movement
                entity.pos += entity.move;
                entity.Update();
            }          
        }        
    }
    #endregion

    #region collisions

    private void ObstacleCollision(Player1 entity)
    {
        foreach (Barrier obstacle in Obstacles)
        {
            Vector2 move = obstacle.Check(entity.hitbox, entity.move);

            //checkes if entity is standing on the obstacle
            if ((entity.move.Y > 0) && (move.Length() < entity.move.Length())) entity.standing = true;
            else if (entity.move.Y != 0) entity.standing = false;

            entity.move = move;
        }
    }

    private void EntityCollision(Player1 _entity)
    {
        foreach (Player1 entity in Entities)
        {
            if (entity != _entity)
            {
                Barrier barrier = new Barrier(entity.hitbox);            
                Vector2 move1 = barrier.Check(_entity.hitbox, _entity.move);
                Vector2 move2 = barrier.Check(_entity.hitbox, _entity.move + new Vector2(0, -5));

                //checks if the entity is standing on another entity
                if ((_entity.move.Y > 0) && (move1.Length() < _entity.move.Length())) _entity.standing = true;
                else if (_entity.move.Y != 0) _entity.standing = false;

                //to prevent character pinning
                if (move1.Length() == move2.Length())
                    _entity.move.Y = 0;
                else _entity.move = move1;
            }
        }
    }

    #endregion

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (Player1 entity in Entities)
        {
            spriteBatch.Draw(entity.image, entity.drawbox, Color.White);
            //spriteBatch.Draw(test, entity.hitbox, Color.Red);
        }
        foreach (Attack attack in Attacks)
        {
            attack.Draw(spriteBatch);
        }
        /*foreach (Barrier obstacle in Obstacles)
        {
            spriteBatch.Draw(test, obstacle.rect, Color.Green);
        }*/
    }

    public ScreenManager.Winner GameRules()
    {
        //checkes whether any player is off screen
        Rectangle screen = new Rectangle(0, -500, 1920, 1080 + 500);
        Rectangle exile = new Rectangle(2000, 2000, 1000, 1000);
        foreach (Player1 entity in Entities)
        {
            if ((!screen.Contains(entity.pos.ToPoint())) && (!exile.Contains(entity.pos.ToPoint())))
            {
                entity.life = 0;
                entity.dead = true;
                entity.times_dead += 1;

                //sound effect
                Game1.self.Sounds["female_scream"].Volume = 0.4f * Game1.self.effectsVolume;
                Game1.self.Sounds["female_scream"].Play();
            }
        }

        //handeling death
        foreach (Player1 entity in Entities)
        {
            if (entity.life <= 0 && !entity.dead)
            {
                entity.dead = true;
                entity.times_dead += 1;
                Game1.self.Sounds["dying"].Volume = 0.7f * Game1.self.effectsVolume;
                Game1.self.Sounds["dying"].Play();
            }               
            if (entity.dead && (!exile.Contains(entity.pos.ToPoint())))
            {
                if (entity.times_dead >= 3)
                {
                    if (entity.type == 1) return ScreenManager.Winner.Player2;
                    else return ScreenManager.Winner.Player1;
                }
                else
                {
                    entity.pos = new Vector2(2020, 2020);
                    entity.Update();
                    Timer timer = new Timer(3000);   
                    timer.Elapsed += entity.Reset;
                    timer.Enabled = true;
                    timer.AutoReset = false;
                }
                
            }
        }

        return ScreenManager.Winner.None;
    }
}
