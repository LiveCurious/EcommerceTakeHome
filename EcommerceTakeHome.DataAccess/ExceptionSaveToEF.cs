using System;
using System.Runtime.Serialization;

namespace EcommerceTakeHome.DataAccess
{
    [Serializable]
    public class ExceptionSaveToEF : Exception
    {
        private Exception e;

        public ExceptionSaveToEF()
        {
        }

        public ExceptionSaveToEF(Exception e)
        {
            this.e = e;
        }

        public ExceptionSaveToEF(string message) : base(message)
        {
        }

        public ExceptionSaveToEF(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExceptionSaveToEF(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}