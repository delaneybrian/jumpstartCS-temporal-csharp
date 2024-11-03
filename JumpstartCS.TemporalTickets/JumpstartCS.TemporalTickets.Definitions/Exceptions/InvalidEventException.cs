namespace JumpstartCS.TemporalTickets.Definitions.Exceptions
{
    [Serializable]
    public class InvalidEventException : Exception
    {
        public InvalidEventException()
           : base() { }

        public InvalidEventException(string message)
            : base(message) { }

        public InvalidEventException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
