using Chess.Core;
using Terminal.Gui.Drawing;
using Terminal.Gui.ViewBase;
using TuiAttribute = Terminal.Gui.Drawing.Attribute;

namespace Chess.Tui;

/// <summary>
/// Renders a <see cref="Board"/> as an 8x8 grid of piece sprites (see
/// <see cref="PieceSprites"/>), using the board/piece colors from
/// thomas-mauran/chess-tui's "Classic" skin.
/// </summary>
public sealed class BoardSpriteView : View
{
    private static readonly Color LightSquare = new(240, 217, 181);
    private static readonly Color DarkSquare = new(181, 136, 99);
    private static readonly Color WhitePiece = new(255, 255, 255);
    private static readonly Color BlackPiece = new(20, 20, 20);

    private readonly Board _board;

    public BoardSpriteView(Board board)
    {
        _board = board;
        Width = Board.Size * PieceSprites.Width;
        Height = Board.Size * PieceSprites.Height;
    }

    protected override bool OnDrawingContent(DrawContext? context)
    {
        base.OnDrawingContent(context);

        for (int rank = 0; rank < Board.Size; rank++)
        {
            // Screen row 0 is the top of the view, which should show Black's
            // back rank (rank index 7), so the rank is flipped into a row.
            int rowBase = (Board.Size - 1 - rank) * PieceSprites.Height;

            for (int file = 0; file < Board.Size; file++)
            {
                bool isLightSquare = (file + rank) % 2 != 0;
                Piece? piece = _board.GetPiece(file, rank);
                string[] sprite = piece is null ? PieceSprites.Blank : PieceSprites.Get(piece.Type);

                Color background = isLightSquare ? LightSquare : DarkSquare;
                Color foreground = piece?.Color == PieceColor.White ? WhitePiece : BlackPiece;

                SetAttribute(new TuiAttribute(foreground, background));

                int colBase = file * PieceSprites.Width;

                for (int spriteRow = 0; spriteRow < PieceSprites.Height; spriteRow++)
                {
                    Move(colBase, rowBase + spriteRow);
                    AddStr(sprite[spriteRow]);
                }
            }
        }

        return true;
    }
}
