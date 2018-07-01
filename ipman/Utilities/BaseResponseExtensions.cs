using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ipman.shared.WebServiceModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IPMan.Utilities
{
    public static class BaseResponseExtensions
    {
        /// <summary>
        /// checks modelstate and, if invalid, preloads the error (and returns true), otherwsie just returns false
        /// </summary>
        /// <param name="response"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static bool InitializeFromModelStateIfInvalid(this BaseResponse response, ModelStateDictionary modelState)
        {
            //check if the model is valid
            if (modelState.IsValid)
                return false;

            //we're done already, yay!
            response.ResponseError = new ResponseError
            {
                ErrorMessage = "ModelState validation failed",
                ErrorDetails = modelState.Values.SelectMany(
                    v => v.Errors.Select(b => b.ErrorMessage)
                    .Union(
                        v.Errors.Select(b => b.Exception?.Message))
                )
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .ToList(),
                ErrorType = ResponseError.ResponseErrorType.ModelValidation
            };

            return true;
        }
    }
}
