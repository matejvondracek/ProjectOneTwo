using Microsoft.Xna.Framework;

class Barrier
{
	public Rectangle rect;
	
	public Barrier(Rectangle _rect)
    {
		rect = _rect;
    }

	public Barrier(int Ax, int Ay, int Bx, int By)
    {
		rect = new Rectangle(Ax * 6, Ay * 6, (Bx - Ax) * 6, (By - Ay) * 6);
    }

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
				Rectangle hitbox2 = new Rectangle((int)(hitbox.X + move_arr[i].X), (int)(hitbox.Y + move_arr[i].Y), hitbox.Width, hitbox.Height);
				int k = 0;
				int precision = 100;
				for (int j = 0; j <= precision; j++)
				{
					if (hitbox2.Intersects(rect)) 
					{
						move_arr[i] = Vector2.Multiply(move_arr[i], 0.9f);
					}
					else break;
					k = j;
				}
				if (k == precision) move_arr[i] = new Vector2();
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
