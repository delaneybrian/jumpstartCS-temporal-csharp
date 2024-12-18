﻿namespace JumpstartCS.TemporalTickets.Definitions.Exceptions
{
    [Serializable]
    public class InvalidCustomerException : Exception
    {
        public InvalidCustomerException()
           : base() { }

        public InvalidCustomerException(string message)
            : base(message) { }

        public InvalidCustomerException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
