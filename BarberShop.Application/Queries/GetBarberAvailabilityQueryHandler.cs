using BarberShop.Application.Interfaces;
using BarberShop.Application.Queries;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Enums;
using BarberShop.Domain.Repositories;
using MediatR;

public class GetBarberAvailabilityQueryHandler
    : IRequestHandler<GetBarberAvailabilityQuery, List<AvailabilitySlotDto>>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IBarberScheduleRepository _scheduleRepository;
    private readonly IBarberBreakRepository _breakRepository;

    public GetBarberAvailabilityQueryHandler(
        IAppointmentRepository appointmentRepository,
        IBarberScheduleRepository scheduleRepository,
        IBarberBreakRepository breakRepository)
    {
        _appointmentRepository = appointmentRepository;
        _scheduleRepository = scheduleRepository;
        _breakRepository = breakRepository;
    }

    public async Task<List<AvailabilitySlotDto>> Handle(
    GetBarberAvailabilityQuery request,
    CancellationToken cancellationToken)
    {
        var dayOfWeek = request.Date.DayOfWeek;

        var schedule = await _scheduleRepository
            .GetByBarberAndDayAsync(request.BarberId, dayOfWeek);

        if (schedule is null || !schedule.IsWorkingDay)
            return new List<AvailabilitySlotDto>();


        var breaks = await _breakRepository
            .GetByBarberAndDayAsync(request.BarberId, dayOfWeek);


        var appointments = await _appointmentRepository
            .GetByBarberAndDateAsync(request.BarberId, request.Date);

        var activeAppointments = appointments
            .Where(a => a.Status != AppointmentStatus.Cancelled)
            .ToList();

        var slots = new List<AvailabilitySlotDto>();
        var current = schedule.StartTime;

        while (current.Add(TimeSpan.FromMinutes(request.SlotDurationInMinutes))
               <= schedule.EndTime)
        {
            var slotEnd = current.Add(TimeSpan.FromMinutes(request.SlotDurationInMinutes));

            bool overlapsAppointment = activeAppointments.Any(a =>
                current < a.EndTime &&
                slotEnd > a.StartTime);

            bool overlapsBreak = breaks.Any(b =>
                current < b.EndTime &&
                slotEnd > b.StartTime);

            bool isAvailable = !overlapsAppointment && !overlapsBreak;

            slots.Add(new AvailabilitySlotDto(
                current,
                slotEnd,
                isAvailable
            ));

            current = slotEnd;
        }

        return slots;
    }
}