﻿namespace AMAPP.API.Utils;

public class ReportGenerationException : Exception
{
    public ReportGenerationException(string message) : base(message) { }
    
    public ReportGenerationException(string message, Exception innerException) 
        : base(message, innerException) { }
}