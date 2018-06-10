using System.Collections.Generic;
using System.Text;

namespace ipman.shared.WebServiceModels
{
    public static class ResponseErrorExtensions
    {
        public static string ToFormattedString(this ResponseError responseError)
        {
            return $@"
Log ID: {responseError.ErrorLogId}
Type: {responseError.ErrorType}
Message: {responseError.ErrorMessage}
Details: {responseError.ErrorDetails}";
        }
    }
    public class ResponseError
    {
        public enum ResponseErrorType
        {
            ModelValidation,
            Exception,
            Other
        }

        /// <summary>
        /// basic message that could be shown to a user
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// for things like all the fields that failed model validation
        /// </summary>
        public List<string> ErrorDetails { get; set; }

        /// <summary>
        /// id of related entry in log if applicable 
        /// </summary>
        public int? ErrorLogId { get; set; }

        public ResponseErrorType ErrorType { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(ErrorType);
            sb.Append(" ");
            sb.Append(ErrorMessage);
            if (ErrorLogId.HasValue) sb.Append(" #").Append(ErrorLogId.Value);
            if (ErrorDetails != null && ErrorDetails.Count > 0)
            {
                sb.Append(":");
                sb.AppendLine();
                foreach (var line in ErrorDetails)
                {
                    sb.Append(" * ").AppendLine(line);
                }
            }
            return sb.ToString(); 
        }
    }
}
