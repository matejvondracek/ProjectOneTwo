using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class Barrier
{
	private float minX, maxX, minY, maxY, A, B, C;
	public Barrier(Vector2 pos1, Vector2 pos2) ///position vectors
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

	private Vector2 Distance(float iX, float iY, Vector2 pos, Vector2 move)
    {
		if ((Convert.ToInt32(iX) > maxX) || (Convert.ToInt32(iX) < minX) || (Convert.ToInt32(iY) > maxY) || (Convert.ToInt32(iY) < minY))
		{
			return move;
		}
		else
		{
			return new Vector2(Convert.ToInt32(iX - pos.X), Convert.ToInt32(iY - pos.Y));
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
				return move; ///should never return
            }

		}
        else
        {
			float q = - A / _A;
			float iY = - (C +_C * q) / (B + _B * q);
			float iX = (-B * iY - C) / A;
			return Distance(iX, iY, pos, move);
        }		
    }
}
