namespace tictactoe
{
    public class Team
    {
        public readonly int Id;
        private static int _lastUsedId;

        public readonly string Signature;

        public Team(string signature)
        {
            Id = _lastUsedId + 1;
            _lastUsedId = Id;

            Signature = signature;
        }
    }
}
