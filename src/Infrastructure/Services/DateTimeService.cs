using CleanArchitectureWebApp.Application.Common.Interfaces;

namespace CleanArchitectureWebApp.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
