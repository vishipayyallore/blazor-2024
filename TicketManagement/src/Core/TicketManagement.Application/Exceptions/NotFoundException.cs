﻿namespace TicketManagement.Application.Exceptions;

public class NotFoundException(string name, object key) : Exception($"{name} ({key}) is not found")
{
}
