﻿namespace FoodZOAI.PaymentGateway.CustomException
{
    public class SecurityException : Exception
    {
        public SecurityException(string message) : base(message)
        {
        }

        public SecurityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
