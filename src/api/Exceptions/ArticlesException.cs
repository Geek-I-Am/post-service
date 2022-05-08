using System;

namespace Boleyn.Service.Exceptions;

public class ArticlesException : Exception
{
    public ArticlesException(string title, string message) : base(message) => Title = title;
    public string Title { get; set; }
}