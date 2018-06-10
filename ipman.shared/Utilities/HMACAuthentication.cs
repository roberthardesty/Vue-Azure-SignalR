using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ipman.shared.Utilities
{
    public class HMACAuthentication
    {
        public class AuthHeaderParts
        {
            public string Signature { get; }
            public string Nonce { get; }
            public string Timestamp { get; }

            public AuthHeaderParts(string signature, string nonce, string timestamp)
            {
                Signature = signature;
                Nonce = nonce;
                Timestamp = timestamp;
            }
        }

        /// <summary>
        /// custom scheme identifier for the auth header
        /// </summary>
        public const string AuthHeaderCustomScheme = "jav";

        /// <summary>
        /// generate the HMAC signature based on all the provided values
        /// </summary>
        /// <param name="version"></param>
        /// <param name="requestHttpMethod"></param>
        /// <param name="requestUri"></param>
        /// <param name="requestTimeStamp"></param>
        /// <param name="nonce"></param>
        /// <param name="requestContentBytes"></param>
        /// <returns></returns>
        public static string CreateSignatureString(string version, string requestHttpMethod, string requestUri, string requestTimeStamp,
            string nonce, byte[] requestContentBytes)
        {
            var requestContentBase64String = string.Empty;
            if (requestContentBytes != null && requestContentBytes.Length > 0)
            {
                //hash the request body, then if anything gets modified later we'll know because hashes won't match
                requestContentBase64String = Cryptography.SimpleHashBase64(requestContentBytes);
            }

            //encode the requesturi
            var encodedUri = WebUtility.UrlEncode(requestUri);

            //create the raw signature string
            string signatureRawData = $"{requestHttpMethod}{encodedUri}{requestTimeStamp}{nonce}{requestContentBase64String}";

            //create a "key" from the app version then convert the key into a byte array
            var secretKeyByteArray = Convert.FromBase64String(Cryptography.SimpleHashBase64(version));

            //also need the sig as bytes
            var signature = Encoding.UTF8.GetBytes(signatureRawData);

            using (var hmac = new HMACSHA256(secretKeyByteArray))
            {
                //hash the sig
                var signatureBytes = hmac.ComputeHash(signature);

                //return a string of the hash
                return Convert.ToBase64String(signatureBytes);
            }
        }

        /// <summary>
        /// builds the auth header, to be included in client requests
        /// </summary>
        /// <param name="signatureString"></param>
        /// <param name="nonce"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static AuthenticationHeaderValue BuildAuthenticationHeader(string signatureString, string nonce, string timestamp)
        {
            return new AuthenticationHeaderValue(AuthHeaderCustomScheme, $"{signatureString}:{nonce}:{timestamp}");
        }

        /// <summary>
        /// extracts the individual parts from the auth header
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static AuthHeaderParts ReadAuthHeader(AuthenticationHeaderValue header)
        {
            //make sure we had the header and it matches our custom scheme
            if (header == null || header.Scheme != AuthHeaderCustomScheme)
                return null;

            //split out the auth header parts
            var authHeaderParts = header.Parameter.Split(':');

            //make sure there were 3 parts
            if (authHeaderParts.Length != 3)
                return null;

            //return the header parts object with the values set
            return new AuthHeaderParts(authHeaderParts[0], authHeaderParts[1], authHeaderParts[2]);

            
        }
    }
}
