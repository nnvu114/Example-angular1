using System;
using Petronas.SampleApp.Common.Helpers;

namespace Petronas.SampleApp.Model.Feedback
{
    public class Feedback<T> : Feedback
    {
        /// <summary>
        /// Gets return value of the method
        /// </summary>
        public T Data { get; private set; }

        public Feedback(bool success, string message = null, T data = default(T))
            : base(success, message)
        {
            Data = data;
        }

        public Feedback(bool success, T data) : this(success, null, data) { }

        public Feedback()
        {

        }
    }

    public class Feedback
    {
        /// <summary>
        /// Gets or sets the flag indicating whether the call was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the message that the caller should know
        /// </summary>
        public string Message { get; set; }

        public Feedback(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }

        public Feedback()
        {

        }

        public Feedback(Exception exception)
        {
            Success = false;
            Message = string.Empty.GetErrorMessage(exception);
        }
    }
}