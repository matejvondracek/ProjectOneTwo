using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Xna.Framework;
using ProjectOneTwo;

class Barrier
{
	//private readonly float minX, maxX, minY, maxY, A, B, C;
	private Rectangle rect;
	
	public Barrier(Rectangle _rect)
    {
		rect = _rect;
    }

	public Barrier(int Ax, int Ay, int Bx, int By)
    {
		Ax -= 13;
		Ay -= 13;
		rect = new Rectangle(Ax * 6, Ay * 6, (Bx - Ax) * 6, (By - Ay) * 6);
    }
	
	/*public Barrier(Vector2 pos1, Vector2 pos2) ///position vectors
	{
		//sets funkční obor úsečky
		if (pos1.X > pos2.X)
        {
			minX = pos2.X;
			maxX = pos1.X;
        }
        else 
		{
			minX = pos1.X;
			maxX = pos2.X;
		}

		if (pos1.Y > pos2.Y)
		{
			minY = pos2.Y;
			maxY = pos1.Y;
		}
		else
		{
			minY = pos1.Y;
			maxY = pos2.Y;
		}

		//calculates obecnou rovnici přímky
		A = pos2.Y - pos1.Y;
		B = pos1.X - pos2.X;
		C = -A * pos1.X - B * pos1.Y;
	}

	/*private double Angle(Vector2 vector1, Vector2 vector2)
    {
		return Math.Atan2(vector2.Y - vector1.Y, vector2.X - vector1.X);
	}
	
	private Vector2 Distance(float iX, float iY, Vector2 pos, Vector2 move)
    {
		Vector2 s_bar = new Vector2(-B, A);
		Vector2 n_bar = new Vector2(A, B);
		Vector2 s_move = move;
		s_bar.Normalize();
		n_bar.Normalize();
		s_move.Normalize();

		if ((Convert.ToInt32(iX) > maxX) || (Convert.ToInt32(iX) < minX) || (Convert.ToInt32(iY) > maxY) || (Convert.ToInt32(iY) < minY)) ///around barrier
		{
			return move;
		}
		else
		{
			if ((iX == pos.X) && (iY == pos.Y)) ///if the object is currently on the barrier
            {
				if (s_move == n_bar) ///moving from barrier at 90 deg angle
                {
					return move;
                }			
			    if (s_bar.X == 0) ///if the barrier is vertical
                {
					if (n_bar.X * s_move.X > 0) ///if the object is trying to move from the barrier
                    {
						return move;
                    }
					return new Vector2(0, move.Y); ///allows the object to slide along
                }
				if (s_bar.Y == 0) ///if the barrier is horizontal
                {
					if (n_bar.Y * s_move.Y > 0) ///if the object is trying to move from the barrier
					{
						return move;
					}
					return new Vector2(move.X, 0); ///allows the object to slide along
				}
			}
			return new Vector2(Convert.ToInt32(iX - pos.X), Convert.ToInt32(iY - pos.Y)); ///moves the object onto the barrier
		}
    }

	public Vector2 Check(Vector2 pos, Vector2 move) ///checkes how far can object travel 
    {
		//calculates obecnou rovnici přímky
		float _A = move.Y;
		float _B = - move.X;
		float _C = -_A * pos.X - _B * pos.Y;

		//calculates intersection
		if (A * B * _A * _B == 0)
        {
			if (((A == 0) && (_A == 0)) || ((B == 0) && (_B == 0))) ///movement parallel to barrier
            {
				return move;
			}
			else
            {
				if ((B == 0)  && (A != 0)) ///horizontal barrier
				{
					float iX = - C / A;
					float iY = (- _A * iX - _C) / _B;
					return Distance(iX, iY, pos, move);
				}
				if ((A == 0)  && (B != 0)) ///vertical barrier
                {
					float iY = - C / B;
					float iX = (- _B * iY - _C) / _A;
					return Distance(iX, iY, pos, move);
                }				
				if ((_B == 0) && (_A != 0)) ///vertical movement
				{
					float iX = -_C / _A;
					float iY = (- A * iX - C) / B;					
					return Distance(iX, iY, pos, move);
				}
				if ((_A == 0) && (_B != 0)) ///horizontal movement 
				{
					float iY = - _C / _B;
					float iX = (- B * iY - C) / A;										
					return Distance(iX, iY, pos, move);
				}
				return move * 50; ///should never return
            }

		}
        else
        {
			float q = - A / _A;
			float iY = - (C +_C * q) / (B + _B * q);
			float iX = (-B * iY - C) / A;
			return Distance(iX, iY, pos, move);
        }		
    }*/

	public Vector2 Check(Rectangle hitbox, Vector2 move)
    {		
		if (move.Length() == 0) return new Vector2(0, 0);
		else
        {
			Vector2[] move_arr = new Vector2[4];
			move_arr[0] = move;
			move_arr[1] = new Vector2(move.X, 0);
			move_arr[2] = new Vector2(0, move.Y);

			for (int i = 0; i <= 2; i++)
			{
				Rectangle hitbox2 = new Rectangle(Convert.ToInt32(hitbox.X + move_arr[i].X), Convert.ToInt32(hitbox.Y + move_arr[i].Y), hitbox.Width, hitbox.Height);
				int k = 0;
				for (int j = 0; j < 20; j++)
				{
					if (hitbox2.Intersects(rect)) 
					{
						move_arr[i] = Vector2.Multiply(move_arr[i], 0.8f);
					}
					else break;
					k = j;
				}
				if (k == 19) move_arr[i] = new Vector2();
			}
			if ((move_arr[0].Length() >= move_arr[1].Length()) && (move_arr[0].Length() >= move_arr[2].Length()))
			{
				return move_arr[0];

			}
			else if ((move_arr[1].Length() >= move_arr[0].Length()) && (move_arr[1].Length() >= move_arr[2].Length()))
			{
				return move_arr[1];
			}
			else return move_arr[2];

		}
    }
}
