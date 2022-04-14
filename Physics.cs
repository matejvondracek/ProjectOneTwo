using System;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lidgren.Network;
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

    public string GetState()
    {
        string state = "";
       
        for (int e = 0; e <= entity_count; e++)
        {
            //players position
            state += "Player" + (e + 1).ToString() +  ".Pos(" + Entities[e].pos.X.ToString() + "," + Entities[e].pos.Y.ToString() + ");";

            //players life
            state += "Player" + (e + 1).ToString() + ".Life(" + Entities[e].life + ");";

            //players death count
            state += "Player" + (e + 1).ToString() + ".Death(" + Entities[e].times_dead + ");";

            //players picture index
        }
            
        return state;
    }

    public void SetState(string state)
    {
        for (int e = 0; e <= entity_count; e++)
        {
            int start, start2, comma, end, x = 0, y = 0;
            //players position
            start = state.IndexOf("Player" + (e + 1).ToString() + ".Pos(");
            if (start != -1)
            {
                start2 = state.IndexOf("(", start) + 1;
                comma = state.IndexOf(",", start2);
                end = state.IndexOf(")", start2) - 1;
                x = Convert.ToInt32(state.Substring(start2, comma - start2));
                y = Convert.ToInt32(state.Substring(comma + 1, end - comma - 1));
            }
            Entities[e].pos = new Vector2(x, y);

            /*
            //players life
            state += "Player" + (e + 1).ToString() + ".Life(" + Entities[e].life + ");";

            //players death count
            state += "Player" + (e + 1).ToString() + ".Death(" + Entities[e].times_dead + ");";*/
        }

    }

}
