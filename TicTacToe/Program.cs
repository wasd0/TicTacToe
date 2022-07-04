namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Run("XXX OO. ...");
            Run("OXO XO. .XO");
            Run("OXO XOX OX.");
            Run("XOX OXO OXO");
            Run("... ... ...");
            Run("XOX XOO XO.");
            Run("XOO XOO XX.");
            Run(".O. XO. XOX");

            Console.ReadKey();
        }

        public static void Run(string description)
        {
            Console.WriteLine(description.Replace(" ", Environment.NewLine));
            Console.WriteLine(GetGameResult(CreateFromString(description)));
            Console.WriteLine();

            Console.WriteLine();
        }

        public static Mark[,] CreateFromString(string str)
        {
            var field = str.Split(' ');
            var ans = new Mark[3, 3];
            for (int x = 0; x < field.Length; x++)
                for (var y = 0; y < field.Length; y++)
                    ans[x, y] = field[x][y] == 'X' ? Mark.Cross : (field[x][y] == 'O' ? Mark.Circle : Mark.Empty);
            return ans;
        }

        public static GameResult GetGameResult(Mark[,] field)
        {
            Mark columnsWinning = CheckWinningLine(FillColums(field));
            Mark rowsWinning = CheckWinningLine(FillRows(field));
            Mark diagonalsWinning = CheckWinningLine(FillDiagonal(field));

            if (columnsWinning == Mark.Cross || rowsWinning == Mark.Cross || diagonalsWinning == Mark.Cross)
                return GameResult.CrossWin;
            else if (columnsWinning == Mark.Circle || rowsWinning == Mark.Circle || diagonalsWinning == Mark.Circle)
                return GameResult.CircleWin;
            else
                return GameResult.Draw;
        }

        public static Mark[][] FillColums(Mark[,] field)
        {
            Mark[][] colums = GetColumnsOrRows(field);

            for (int i = 0; i < colums.Length; i++)
                for (int j = 0; j < colums[i].Length; j++)
                    colums[i][j] = field[j, i];

            return colums;
        }

        public static Mark[][] FillRows(Mark[,] field)
        {
            Mark[][] rows = GetColumnsOrRows(field);

            for (int i = 0; i < rows.Length;i++)
                for (int j = 0; j < rows[i].Length; j++)
                    rows[i][j] = field[i, j];

            return rows;
        }

        public static Mark[][] FillDiagonal(Mark[,] field)
        {
            int linesLength = (int)Math.Sqrt(field.Length);

            Mark[][] lines = new Mark[2][];
            lines[0] = new Mark[linesLength];
            lines[1] = new Mark[linesLength];

            for (int i = 0; i < linesLength; i++)
            {
                lines[0][i] = field[i, i];
                lines[1][i] = field[i, linesLength - i - 1];
            }

            return lines;
        }

        private static Mark[][] GetColumnsOrRows(Mark[,] field)
        {
            int count = (int)Math.Sqrt(field.Length);
            Mark[][] lines = new Mark[count][];

            for (int i = 0; i < count; i++)
                lines[i] = new Mark[count];

            return lines;
        }

        public static Mark CheckWinningLine(Mark[][] lines)
        {
            if (lines != null)
                for (int i = 0; i < lines.Length; i++)
                    if (lines[i][0] == lines[i][1] && lines[i][1] == lines[i][2])
                        if (lines[i][0] != Mark.Empty)
                            return lines[i][0];
            return Mark.Empty;
        }
    }

    public enum Mark
    {
        Empty,
        Cross,
        Circle
    }

    public enum GameResult
    {
        CrossWin,
        CircleWin,
        Draw
    }
}