namespace JumpstartCS.TemporalTickets.Definitions.Exceptions
{
    [Serializable]
    public class InsufficientTicketsAvaibableException : Exception
    {
        public InsufficientTicketsAvaibableException()
           : base() { }

        public InsufficientTicketsAvaibableException(string message)
            : base(message) { }

        public InsufficientTicketsAvaibableException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
