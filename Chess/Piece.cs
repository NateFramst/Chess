using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    internal class Piece
    {
        public int piece, x, y;
        public bool hasMoved, colour, inCheck, captured;
        public Image image;

        public Piece(int _piece, int _x, int _y, bool _colour, bool _hasMoved, Image _image, bool _captured)
        {
            piece = _piece;
            x = _x;
            y = _y;
            colour = _colour;
            hasMoved = _hasMoved;
            image = _image;
            captured = _captured;
        }

        public bool MoveCheck(List<Piece> pieceList, Rectangle R)
        {
            if (piece == 0) //pawn (done)
            {
                if (R.X != x)
                {
                    int tempX = R.X - x;
                    int tempY = R.Y - y;

                    if (!colour)
                    {
                        if (tempX > 0 && tempY < 0) //upRightMove
                        {
                            if (tempX / 50 == (y - R.Y) / 50 && tempX / 50 == 1)
                            {
                                GameScreen.pawnCapture = true;
                                goto BREAK;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if (tempX < 0 && tempY < 0) //upLeftMove
                        {
                            if ((x - R.X) / 50 == (y - R.Y) / 50 && (x - R.X) / 50 == 1)
                            {
                                GameScreen.pawnCapture = true;
                                goto BREAK;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (tempX > 0 && tempY > 0) //downRightMove
                        {
                            if (tempX / 50 == tempY / 50 && tempX / 50 == 1)
                            {
                                GameScreen.pawnCapture = true;
                                goto BREAK;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if (tempX < 0 && tempY > 0) //downLeftMove
                        {
                            if ((x - R.X) / 50 == tempY / 50 && (x - R.X) / 50 == 1)
                            {
                                GameScreen.pawnCapture = true;
                                goto BREAK;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }

                if (!colour && R.X == x)
                {
                    int temp = y - R.Y;

                    if (hasMoved)
                    {
                        if (temp > 50 || temp < 0 || R.X < x || R.X > (x + 50))
                        {
                            return false;
                        }
                        GameScreen.pawnCapture = false;
                        GameScreen.columnMove = true;
                        goto BREAK;
                    }
                    else
                    {
                        if (temp > 100 || temp < 0 || R.X < x || R.X > (x + 50))
                        {
                            return false;
                        }
                        GameScreen.pawnCapture = false;
                        GameScreen.columnMove = true;
                        goto BREAK;
                    }
                }
                else if (R.X == x)
                {
                    int temp = R.Y - y;

                    if (hasMoved)
                    {
                        if (temp > 50 || temp < 0 || R.X < x || R.X > (x + 50))
                        {
                            return false;
                        }
                        GameScreen.pawnCapture = false;
                        GameScreen.columnMove = true;
                        goto BREAK;
                    }
                    else
                    {
                        if (temp > 100 || temp < 0 || R.X < x || R.X > (x + 50))
                        {
                            return false;
                        }
                        GameScreen.pawnCapture = false;
                        GameScreen.columnMove = true;
                        goto BREAK;
                    }
                }
                else
                {
                    return false;
                }
            }

            else if (piece == 1) //knight (done)
            {
                if (R.X - x == 100) //right
                {
                    if (R.Y - y == 50) //down
                    {
                        goto BREAK;
                    }
                    else if (y - R.Y == 50) //up
                    {
                        goto BREAK;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (x - R.X == 100) //left
                {
                    if (R.Y - y == 50) //down
                    {
                        goto BREAK;
                    }
                    else if (y - R.Y == 50) //up
                    {
                        goto BREAK;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (R.Y - y == 100) //down
                {
                    if (R.X - x == 50) //right
                    {
                        goto BREAK;
                    }
                    else if (x - R.X == 50) //left
                    {
                        goto BREAK;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (y - R.Y == 100) //up
                {
                    if (R.X - x == 50) //right
                    {
                        goto BREAK;
                    }
                    else if (x - R.X == 50) //left
                    {
                        goto BREAK;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }

            else if (piece == 2) //bishop (done)
            {
                int tempX = R.X - x;
                int tempY = R.Y - y;

                if (Math.Abs(tempX) == Math.Abs(tempY))
                {
                    if (tempX > 0 && tempY > 0) //downRightMove
                    {
                        if ((R.X - x) / 50 == (R.Y - y) / 50)
                        {
                            goto BREAK;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (tempX > 0 && tempY < 0) //upRightMove
                    {
                        if ((R.X - x) / 50 == (y - R.Y) / 50)
                        {
                            goto BREAK;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (tempX < 0 && tempY > 0) //downLeftMove
                    {
                        if ((x - R.X) / 50 == (R.Y - y) / 50)
                        {
                            goto BREAK;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (tempX < 0 && tempY < 0) //upLeftMove
                    {
                        if ((x - R.X) / 50 == (y - R.Y) / 50)
                        {
                            goto BREAK;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            else if (piece == 3) //rook (done)
            {
                if (R.X == x)
                {
                    GameScreen.columnMove = true;
                    GameScreen.rowMove = false;
                    goto BREAK;
                }
                else if (R.Y == y)
                {
                    GameScreen.rowMove = true;
                    GameScreen.columnMove = false;
                    goto BREAK;
                }
                else
                {
                    return false;
                }
            }

            else if (piece == 4) //queen (done)
            {
                if (R.X == x)
                {
                    GameScreen.columnMove = true;
                    GameScreen.rowMove = false;
                    goto BREAK;
                }
                else if (R.Y == y)
                {
                    GameScreen.rowMove = true;
                    GameScreen.columnMove = false;
                    goto BREAK;
                }

                int tempX = R.X - x;
                int tempY = R.Y - y;

                if (tempX > 0 && tempY > 0) //downRightMove
                {
                    if ((R.X - x) / 50 == (R.Y - y) / 50)
                    {
                        goto BREAK;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (tempX > 0 && tempY < 0) //upRightMove
                {
                    if ((R.X - x) / 50 == (y - R.Y) / 50)
                    {
                        goto BREAK;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (tempX < 0 && tempY > 0) //downLeftMove
                {
                    if ((x - R.X) / 50 == (R.Y - y) / 50)
                    {
                        goto BREAK;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (tempX < 0 && tempY < 0) //upLeftMove
                {
                    if ((x - R.X) / 50 == (y - R.Y) / 50)
                    {
                        goto BREAK;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            else //king (done)
            {
                int tempX = R.X - x;
                int tempY = R.Y - y;

                if (Math.Abs(tempX / 50) == 1)
                {
                    if (tempX > 0 && tempY > 0) //downRightMove
                    {
                        if ((R.X - x) / 50 == (R.Y - y) / 50)
                        {
                            goto BREAK;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (tempX > 0 && tempY < 0) //upRightMove
                    {
                        if ((R.X - x) / 50 == (y - R.Y) / 50)
                        {
                            goto BREAK;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (tempX < 0 && tempY > 0) //downLeftMove
                    {
                        if ((x - R.X) / 50 == (R.Y - y) / 50)
                        {
                            goto BREAK;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (tempX < 0 && tempY < 0) //upLeftMove
                    {
                        if ((x - R.X) / 50 == (y - R.Y) / 50)
                        {
                            goto BREAK;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if (R.X == x && ((R.Y - y) == 50 || (y - R.Y) == 50))
                {
                    GameScreen.columnMove = true;
                    goto BREAK;
                }
                else if (R.Y == y && ((R.X - x) == 50 || (x - R.X) == 50))
                {
                    GameScreen.rowMove = true;
                    goto BREAK;
                }
                else
                {
                    return false;
                }
            }

        BREAK:

            if ((piece == 0 && GameScreen.pawnCapture == false) || piece == 3 || piece == 4)
            {
                int temp = 0;
                if (GameScreen.rowMove)
                {
                    if (R.X > x)
                    {
                        temp = (R.X - x - 50) / 50;
                    }
                    else
                    {
                        temp = (x - 50 - R.X) / 50;
                    }
                }
                else if (GameScreen.columnMove)
                {
                    if (R.Y > y)
                    {
                        temp = (R.Y - y - 50) / 50;
                    }
                    else
                    {
                        temp = (y - 50 - R.Y) / 50;
                    }
                }

                for (int i = 0; i < temp; i++)
                {
                    int X = 0;
                    int Y = 0;

                    if (GameScreen.rowMove)
                    {
                        if (R.X > x)
                        {
                            X = x + (i * 50) + 50;
                        }
                        else
                        {
                            X = x - (i * 50) - 50;
                        }
                        Y = y;
                    }

                    else if (GameScreen.columnMove)
                    {
                        if (R.Y > y)
                        {
                            Y = y + (i * 50) + 50;
                        }
                        else
                        {
                            Y = y - (i * 50) - 50;
                        }
                        X = x;
                    }

                    for (int j = 0; j < pieceList.Count; j++)
                    {
                        if (GameScreen.selected != j)
                        {
                            if (pieceList[j].x == X && pieceList[j].y == Y)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            if (piece == 2 || piece == 4)
            {
                int X, Y, temp;
                X = Y = temp = 0;
                bool downRight, upRight, downLeft, upLeft;
                downRight = upRight = downLeft = upLeft = false;
                int tempX = R.X - x;
                int tempY = R.Y - y;

                if (tempX > 0 && tempY > 0) //downRightMove
                {
                    if ((R.X - x) / 50 == (R.Y - y) / 50)
                    {
                        temp = (R.X - x - 50) / 50;
                        downRight = true;
                    }
                }
                else if (tempX > 0 && tempY < 0) //upRightMove
                {
                    if ((R.X - x) / 50 == (y - R.Y) / 50)
                    {
                        temp = (R.X - x - 50) / 50;
                        upRight = true;
                    }
                }
                else if (tempX < 0 && tempY > 0) //downLeftMove
                {
                    if ((x - R.X) / 50 == (R.Y - y) / 50)
                    {
                        temp = (x - R.X - 50) / 50;
                        downLeft = true;
                    }
                }
                else if (tempX < 0 && tempY < 0) //upLeftMove
                {
                    if ((x - R.X) / 50 == (y - R.Y) / 50)
                    {
                        temp = (x - R.X - 50) / 50;
                        upLeft = true;
                    }
                }

                for (int i = 0; i < temp; i++)
                {
                    if (downRight)
                    {
                        X = x + (i * 50) + 50;
                        Y = y + (i * 50) + 50;
                    }
                    else if (upRight)
                    {
                        X = x + (i * 50) + 50;
                        Y = y - (i * 50) - 50;
                    }
                    else if (downLeft)
                    {
                        X = x - (i * 50) - 50;
                        Y = y + (i * 50) + 50;
                    }
                    else if (upLeft)
                    {
                        X = x - (i * 50) - 50;
                        Y = y - (i * 50) - 50;
                    }

                    foreach (Piece p in pieceList)
                    {
                        if (X == p.x && Y == p.y)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}