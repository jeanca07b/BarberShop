using BarberShop.Application.Interfaces;
using BarberShop.Application.Queries;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Enums;
using BarberShop.Domain.Repositories;

namespace BarberShop.Application.Appointments.Queries;

public class GetAvailableTimeSlotsQueryHandler
{
    private readonly IBarberScheduleRepository _scheduleRepository;
    private readonly IBarberBreakRepository _breakRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IServiceRepository _serviceRepository;

    public GetAvailableTimeSlotsQueryHandler(
        IBarberScheduleRepository scheduleRepository,
        IBarberBreakRepository breakRepository,
        IAppointmentRepository appointmentRepository,
        IServiceRepository serviceRepository)
    {
        _scheduleRepository = scheduleRepository;
        _breakRepository = breakRepository;
        _appointmentRepository = appointmentRepository;
        _serviceRepository = serviceRepository;
    }

    public async Task<List<TimeSpan>> Handle(GetAvailableTimeSlotsQuery query)
    {
        if (query.Date.Date < DateTime.UtcNow.Date)
            return new List<TimeSpan>();

        var service = await _serviceRepository.GetByIdAsync(query.ServiceId);
        if (service is null)
            return new List<TimeSpan>();

        var duration = TimeSpan.FromMinutes(service.DurationInMinutes);

        var dayOfWeek = query.Date.DayOfWeek;

        var schedule = await _scheduleRepository
            .GetByBarberAndDayAsync(query.BarberId, dayOfWeek);

        if (schedule is null || !schedule.IsWorkingDay)
            return new List<TimeSpan>();

        var breaks = await _breakRepository
            .GetByBarberAndDayAsync(query.BarberId, dayOfWeek);

        var appointments = await _appointmentRepository
            .GetByBarberAndDateAsync(query.BarberId, query.Date.Date);

        var availableSlots = new List<TimeSpan>();

        var current = schedule.StartTime;

        while (current + duration <= schedule.EndTime)
        {
            var slotStart = current;
            var slotEnd = current + duration;

            bool overlapsBreak = breaks.Any(b =>
                slotStart < b.EndTime &&
                slotEnd > b.StartTime);

            bool overlapsAppointment = appointments.Any(a =>
                a.Status != AppointmentStatus.Cancelled &&
                slotStart < a.EndTime &&
                slotEnd > a.StartTime);

            if (!overlapsBreak && !overlapsAppointment)
            {
                availableSlots.Add(slotStart);
            }

            current += duration;
        }

        return availableSlots;
    }
}