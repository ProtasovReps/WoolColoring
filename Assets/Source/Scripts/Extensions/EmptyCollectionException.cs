using System;

namespace Extensions
{
    public class EmptyCollectionException : Exception
    {
        private const string ErrorMessage = "The collection is empty";

        public override string Message => ErrorMessage;
    }
}
