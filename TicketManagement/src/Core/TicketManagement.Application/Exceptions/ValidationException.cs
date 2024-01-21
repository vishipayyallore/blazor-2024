﻿using FluentValidation.Results;

namespace TicketManagement.Application.Exceptions;

public class ValidationException : Exception
{
    public List<string> ValidationErrors { get; set; }

    public ValidationException(ValidationResult validationResult)
    {
        ValidationErrors = [];

        foreach (ValidationFailure? validationError in validationResult.Errors)
        {
            ValidationErrors.Add(validationError.ErrorMessage);
        }
    }
}
