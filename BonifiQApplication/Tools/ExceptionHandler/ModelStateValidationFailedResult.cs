﻿using BonifiQ.Domain.Settings.ErrorHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BonifiQ.Application.Tools.ExceptionHandler
{
    public class ModelStateValidationFailedResult : ObjectResult
    {
        public ModelStateValidationFailedResult(ModelStateDictionary modelState)
            : base(new ErrorResponse(modelState))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}