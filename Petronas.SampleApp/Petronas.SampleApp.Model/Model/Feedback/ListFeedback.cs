using System.Collections.Generic;

namespace Petronas.SampleApp.Data.Feedback
{
    public class ListFeedback<T> : Feedback<IList<T>>
    {
        /// <summary>
        /// Gets or sets number of total items. This is different from number of items in Data property, which is normally a page size.
        /// </summary>
        public int Total { get; set; }

        public ListFeedback(bool success, string message = null, IList<T> data = null, int total = 0)
            : base(success, message, data)
        {
            Total = total;
        }

        public ListFeedback(bool success, IList<T> data, int total) : this(success, null, data, total) { }

        public ListFeedback()
        {
            
        }
    }
}