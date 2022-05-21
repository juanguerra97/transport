using seminario.Application.Common.Interfaces;

namespace seminario.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
