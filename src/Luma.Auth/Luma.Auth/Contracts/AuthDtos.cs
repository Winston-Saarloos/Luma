﻿namespace Luma.Auth.Contracts;

public record RegisterRequest(string Email, string Password, string? DisplayName);