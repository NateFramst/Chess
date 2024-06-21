using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class GameScreen : UserControl
    {
        Brush darkBlueBrush = new SolidBrush(Color.DarkBlue);
        Brush lightGrayBrush = new SolidBrush(Color.LightGray);
        Brush yellowBrush = new SolidBrush(Color.Yellow);

        Image WhitePawn = Properties.Resources.WhitePawn;
        Image WhiteRook = Properties.Resources.WhiteRook;
        Image WhiteKnight = Properties.Resources.WhiteKnight;
        Image WhiteBishop = Properties.Resources.WhiteBishop;
        Image WhiteQueen = Properties.Resources.WhiteQueen;
        Image WhiteKing = Properties.Resources.WhiteKing;

        Image BlackPawn = Properties.Resources.BlackPawn;
        Image BlackRook = Properties.Resources.BlackRook;
        Image BlackKnight = Properties.Resources.BlackKnight;
        Image BlackBishop = Properties.Resources.BlackBishop;
        Image BlackQueen = Properties.Resources.BlackQueen;
        Image BlackKing = Properties.Resources.BlackKing;

        List<Piece> pieceList = new List<Piece>();
        List<Rectangle> gridList = new List<Rectangle>();

        bool click = false;
        bool select = false;
        bool turn = true; //true = white turn, false = black turn
        int capturedPieces1, capturedPieces2, multiplier1, multiplier2;

        public static int selected = -1;
        public static bool columnMove, rowMove, pawnCapture;

        Point clickPoint;

        public GameScreen()
        {
            InitializeComponent();

            Start();
        }

        private void GameScreen_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left))
            {
                clickPoint = PointToClient(Cursor.Position);
                click = true;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            bool legalMove = true;
            if (click)
            {
                Rectangle tempRec = new Rectangle(clickPoint.X, clickPoint.Y, 1, 1);
                if (select)
                {
                    foreach (Rectangle r in gridList)
                    {
                        if (tempRec.IntersectsWith(r))
                        {
                            if (pieceList[selected].MoveCheck(pieceList, r) && ((turn && !pieceList[selected].colour) || (!turn && pieceList[selected].colour)))
                            {
                                legalMove = true;
                            }
                            else
                            {
                                legalMove = false;
                            }

                            bool pawnCaptureCheck = false;
                            for (int i = 0; i < pieceList.Count; i++)
                            {
                                Rectangle tempRec3 = new Rectangle(pieceList[i].x, pieceList[i].y, 50, 50);
                                if (tempRec3.IntersectsWith(tempRec))
                                {
                                    if (pieceList[i].colour == pieceList[selected].colour)
                                    {
                                        selected = i;
                                        select = true;
                                        legalMove = false;
                                        goto BREAK;
                                    }
                                    else
                                    {
                                        if ((pieceList[selected].piece > 0 && legalMove) || (pawnCapture && legalMove))
                                        {
                                            if (!pieceList[i].colour)
                                            {
                                                pieceList[i].y = 0 + (capturedPieces1 * 50);
                                                pieceList[i].x = multiplier1 * 50;
                                                pieceList[i].captured = true;
                                                capturedPieces1++;
                                            }
                                            else
                                            {
                                                pieceList[i].y = 0 + (capturedPieces2 * 50);
                                                pieceList[i].x = this.Width - 50 - (multiplier2 * 50);
                                                pieceList[i].captured = true;
                                                capturedPieces2++;
                                            }

                                            if (capturedPieces1 % 8 == 0 && capturedPieces1 > 0)
                                            {
                                                multiplier1++;
                                                capturedPieces1 = 0;
                                            }

                                            if (capturedPieces2 % 8 == 0 && capturedPieces2 > 0)
                                            {
                                                multiplier2++;
                                                capturedPieces2 = 0;
                                            }

                                            pawnCaptureCheck = true;
                                            break;
                                        }
                                        else
                                        {
                                            legalMove = false;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (pawnCapture && !pawnCaptureCheck)
                            {
                                legalMove = false;
                            }

                            if (legalMove)
                            {
                                Rectangle tempRec2;
                                if (!pieceList[selected].colour)
                                {
                                    tempRec2 = new Rectangle(pieceList[12].x, pieceList[12].y, 50, 50);
                                }
                                else
                                {
                                    tempRec2 = new Rectangle(pieceList[28].x, pieceList[28].y, 50, 50);
                                }

                                if (turn)
                                {
                                    for (int j = 0; j < pieceList.Count; j++)
                                    {
                                        if (j != 12 && pieceList[j].colour && !pieceList[j].captured)
                                        {
                                            if (pieceList[selected].piece == 5)
                                            {
                                                if (pieceList[j].MoveCheck(pieceList, r))
                                                {
                                                    if (pieceList[j].piece == 0 && pawnCapture)
                                                    {
                                                        legalMove = false;
                                                        break;
                                                    }
                                                    else if (pieceList[j].piece > 0)
                                                    {
                                                        legalMove = false;
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                int x, y;
                                                x = pieceList[selected].x;
                                                y = pieceList[selected].y;
                                                pieceList[selected].x = r.X;
                                                pieceList[selected].y = r.Y;
                                                if (pieceList[j].MoveCheck(pieceList, tempRec2))
                                                {
                                                    if (pieceList[j].piece == 0 && pawnCapture)
                                                    {
                                                        legalMove = false;
                                                        pieceList[selected].x = x;
                                                        pieceList[selected].y = y;
                                                        break;
                                                    }
                                                    else if (pieceList[j].piece > 0)
                                                    {
                                                        legalMove = false;
                                                        pieceList[selected].x = x;
                                                        pieceList[selected].y = y;
                                                        break;
                                                    }
                                                }
                                                pieceList[selected].x = x;
                                                pieceList[selected].y = y;
                                            }
                                        }
                                    }
                                    pawnCapture = false;
                                }
                                else if (!turn)
                                {
                                    for (int j = 0; j < pieceList.Count; j++)
                                    {
                                        if (j != 28 && !pieceList[j].colour && !pieceList[j].captured)
                                        {
                                            if (pieceList[selected].piece == 5)
                                            {
                                                if (pieceList[j].MoveCheck(pieceList, r))
                                                {
                                                    if (pieceList[j].piece == 0 && pawnCapture)
                                                    {
                                                        legalMove = false;
                                                        break;
                                                    }
                                                    else if (pieceList[j].piece > 0)
                                                    {
                                                        legalMove = false;
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (pieceList[j].MoveCheck(pieceList, tempRec2))
                                                {
                                                    int x, y;
                                                    x = pieceList[selected].x;
                                                    y = pieceList[selected].y;
                                                    pieceList[selected].x = r.X;
                                                    pieceList[selected].y = r.Y;
                                                    if (pieceList[j].MoveCheck(pieceList, tempRec2))
                                                    {
                                                        if (pieceList[j].piece == 0 && pawnCapture)
                                                        {
                                                            legalMove = false;
                                                            pieceList[selected].x = x;
                                                            pieceList[selected].y = y;
                                                            break;
                                                        }
                                                        else if (pieceList[j].piece > 0)
                                                        {
                                                            legalMove = false;
                                                            pieceList[selected].x = x;
                                                            pieceList[selected].y = y;
                                                            break;
                                                        }
                                                    }
                                                    pieceList[selected].x = x;
                                                    pieceList[selected].y = y;
                                                }
                                            }
                                        }
                                    }
                                    pawnCapture = false;
                                }

                                if (pieceList[selected].piece == 5)
                                {
                                    for (int j = 0; j < pieceList.Count; j++)
                                    {
                                        if (j != selected && pieceList[j].colour != pieceList[selected].colour && !pieceList[j].captured)
                                        {
                                            if (pieceList[j].MoveCheck(pieceList, r))
                                            {
                                                if (pieceList[j].piece == 0 && pawnCapture)
                                                {
                                                    legalMove = false;
                                                    break;
                                                }
                                                else if (pieceList[j].piece > 0)
                                                {
                                                    legalMove = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    pawnCapture = false;
                                }
                            }

                            if (legalMove)
                            {
                                if (pieceList[selected].piece == 0)
                                {
                                    if (!pieceList[selected].colour)
                                    {
                                        if (r.Y == 50)
                                        {
                                            pieceList[selected].piece = 4;
                                            pieceList[selected].image = WhiteQueen;
                                        }
                                    }
                                    else
                                    {
                                        if (r.Y == 400)
                                        {
                                            pieceList[selected].piece = 4;
                                            pieceList[selected].image = BlackQueen;
                                        }
                                    }
                                }

                                pieceList[selected].y = r.Y;
                                pieceList[selected].x = r.X;
                                pieceList[selected].hasMoved = true;
                                selected = -1;
                                select = false;
                                columnMove = false;
                                rowMove = false;
                                pawnCapture = false;
                                turn = !turn;

                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (Rectangle r in gridList)
                    {
                        if (tempRec.IntersectsWith(r))
                        {
                            for (int i = 0; i < pieceList.Count; i++)
                            {
                                if (pieceList[i].x == r.X && pieceList[i].y == r.Y && ((turn && !pieceList[i].colour) || (!turn && pieceList[i].colour)))
                                {
                                    selected = i;
                                    select = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            BREAK:
                click = false;
            }
            Refresh();
        }

        void Start()
        {
            //gridList starts at point (150, 50)
            int startX = (this.Width / 2) - 250;
            int startY = (this.Height / 2) - 200;
            int squareSize = 50;
            int multiplier = 1;
            capturedPieces1 = capturedPieces2 = multiplier1 = multiplier2 = 0;

            //each gridList square is 50 pixels X 50 pixels
            for (int i = 0; i < 64; i++)
            {
                //creating each gridList square and adding it to a list
                if (gridList.Count % 8 == 0 && gridList.Count > 0)
                {
                    startY += squareSize;
                    multiplier = 1;
                    Rectangle square = new Rectangle(startX + squareSize * multiplier, startY, squareSize, squareSize);
                    gridList.Add(square);
                }
                else
                {
                    Rectangle square = new Rectangle(startX + squareSize * multiplier, startY, squareSize, squareSize);
                    gridList.Add(square);
                }
                multiplier++;
            }

            //creating the pieces and adding them to the list
            int piece, x, y;
            bool colour;
            int counter = 0;
            Image image;
            for (int i = 0; i < 32; i++)
            {
                Piece tempPiece;
                if (i < 8 || (i > 15 && i < 24))
                {
                    piece = 0;
                }
                else if (i == 9 || i == 14 || i == 25 || i == 30)
                {
                    piece = 1;
                }
                else if (i == 10 || i == 13 || i == 26 || i == 29)
                {
                    piece = 2;
                }
                else if (i == 8 || i == 15 || i == 24 || i == 31)
                {
                    piece = 3;
                }
                else if (i == 11 || i == 27)
                {
                    piece = 4;
                }
                else
                {
                    piece = 5;
                }

                if (i < 16)
                {
                    colour = false;
                }
                else
                {
                    colour = true;
                }

                if (i < 8)
                {
                    y = 350;
                }
                else if (i > 7 && i < 16)
                {
                    y = 400;
                }
                else if (i > 15 && i < 24)
                {
                    y = 100;
                }
                else
                {
                    y = 50;
                }

                x = 150 + (counter * 50);
                counter++;
                if (counter % 8 == 0)
                {
                    counter = 0;
                }

                if (!colour)
                {
                    if (piece == 0)
                    {
                        image = WhitePawn;
                    }
                    else if (piece == 1)
                    {
                        image = WhiteKnight;
                    }
                    else if (piece == 2)
                    {
                        image = WhiteBishop;
                    }
                    else if (piece == 3)
                    {
                        image = WhiteRook;
                    }
                    else if (piece == 4)
                    {
                        image = WhiteQueen;
                    }
                    else
                    {
                        image = WhiteKing;
                    }
                }
                else
                {
                    if (piece == 0)
                    {
                        image = BlackPawn;
                    }
                    else if (piece == 1)
                    {
                        image = BlackKnight;
                    }
                    else if (piece == 2)
                    {
                        image = BlackBishop;
                    }
                    else if (piece == 3)
                    {
                        image = BlackRook;
                    }
                    else if (piece == 4)
                    {
                        image = BlackQueen;
                    }
                    else
                    {
                        image = BlackKing;
                    }
                }

                tempPiece = new Piece(piece, x, y, colour, false, image, false);
                pieceList.Add(tempPiece);
            }
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            int counter = 0;
            int divisor = 8;
            bool baller = false;
            for (int i = 0; i < gridList.Count; i++)
            {
                counter++;
                if (counter % 2 == 0)
                {
                    e.Graphics.FillRectangle(darkBlueBrush, gridList[i]);
                }
                else
                {
                    e.Graphics.FillRectangle(lightGrayBrush, gridList[i]);
                }

                if (counter % divisor == 0)
                {
                    if (baller)
                    {
                        counter = 0;
                        divisor = 8;
                        baller = false;
                    }
                    else
                    {
                        counter = 1;
                        divisor = 9;
                        baller = true;
                    }
                }

                if (selected >= 0)
                {
                    if (gridList[i].X == pieceList[selected].x && gridList[i].Y == pieceList[selected].y)
                    {
                        e.Graphics.FillRectangle(yellowBrush, gridList[i]);
                    }
                }
            }

            for (int i = 0; i < pieceList.Count; i++)
            {
                if (pieceList[i].piece == 0)
                {
                    e.Graphics.DrawImage(pieceList[i].image, pieceList[i].x + 4, pieceList[i].y + 5);
                }
                else if (pieceList[i].piece == 1)
                {
                    e.Graphics.DrawImage(pieceList[i].image, pieceList[i].x + 7, pieceList[i].y);
                }
                else if (pieceList[i].piece == 2)
                {
                    e.Graphics.DrawImage(pieceList[i].image, pieceList[i].x + 8, pieceList[i].y);
                }
                else if (pieceList[i].piece == 3)
                {
                    e.Graphics.DrawImage(pieceList[i].image, pieceList[i].x + 6, pieceList[i].y);
                }
                else if (pieceList[i].piece == 4)
                {
                    e.Graphics.DrawImage(pieceList[i].image, pieceList[i].x + 10, pieceList[i].y);
                }
                else
                {
                    e.Graphics.DrawImage(pieceList[i].image, pieceList[i].x + 12, pieceList[i].y);
                }
            }
        }
    }
}