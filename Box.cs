namespace tictactoe
{
    public class Box
    {
        public int Value = Empty;
        public int TeamId;
        public bool Selected = false;        

        public const int Empty = 0;
        public const int Used = 1;
    }
}
