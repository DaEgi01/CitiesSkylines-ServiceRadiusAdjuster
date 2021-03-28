using System;
using System.Text;

namespace ServiceRadiusAdjuster
{
    public sealed class ErrorMessageBuilder
    {
        private readonly StringBuilder _sb = new();

        public string Build(string methodName, Exception exception)
        {
            if (methodName is null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return Build(methodName, exception.ToString());
        }

        public string Build(string methodName, string error)
        {
            if (methodName is null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            _sb.Length = 0;

            return _sb.Append(methodName)
                .AppendLine(" failed!")
                .AppendLine(error)
                .ToString();
        }
    }
}
