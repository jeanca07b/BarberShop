using BarberShop.Application.Common;
using BarberShop.Application.Interfaces;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using MediatR;

public class CreateAppointmentCommandHandler
    : IRequestHandler<CreateAppointmentCommand, Result<Guid>>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IBarberRepository _barberRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IBarberScheduleRepository _scheduleRepository;
    private readonly IBarberBreakRepository _breakRepository;
    private readonly ICustomerRepository _customerRepository;

    public CreateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IBarberRepository barberRepository,
        IServiceRepository serviceRepository,
        IBarberScheduleRepository scheduleRepository,
        IBarberBreakRepository breakRepository,
        ICustomerRepository customerRepository)
    {
        _appointmentRepository = appointmentRepository;
        _barberRepository = barberRepository;
        _serviceRepository = serviceRepository;
        _scheduleRepository = scheduleRepository;
        _breakRepository = breakRepository;
        _customerRepository = customerRepository;
    }

    public async Task<Result<Guid>> Handle(
     CreateAppointmentCommand request,
     CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (customer is null)
            return Result<Guid>.Failure("Customer not found");

        var barber = await _barberRepository.GetByIdAsync(request.BarberId);
        if (barber is null)
            return Result<Guid>.Failure("Barber not found");


        var service = await _serviceRepository.GetByIdAsync(request.ServiceId);
        if (service is null)
            return Result<Guid>.Failure("Service not found");


        var endTime = request.StartTime.Add(
            TimeSpan.FromMinutes(service.DurationInMinutes));

        var schedule = await _scheduleRepository
            .GetByBarberAndDayAsync(request.BarberId, request.Date.DayOfWeek);

        if (schedule is null)
            return Result<Guid>.Failure("Barber does not work this day");

        if (request.StartTime < schedule.StartTime ||
            endTime > schedule.EndTime)
            return Result<Guid>.Failure("Outside working hours");


        var breaks = await _breakRepository
            .GetByBarberAndDayAsync(request.BarberId, request.Date.DayOfWeek);

        var overlapsBreak = breaks.Any(b =>
            request.StartTime < b.EndTime &&
            endTime > b.StartTime);

        if (overlapsBreak)
            return Result<Guid>.Failure("Barber is on break");


        var appointments = await _appointmentRepository
            .GetByBarberAndDateAsync(request.BarberId, request.Date);

        var overlaps = appointments.Any(a =>
            request.StartTime < a.EndTime &&
            endTime > a.StartTime);

        if (overlaps)
            return Result<Guid>.Failure("Time slot already taken");

        var appointment = new Appointment(
            request.BusinessId,
            request.CustomerId,
            request.BarberId,
            request.ServiceId,
            request.Date,
            request.StartTime,
            endTime,
            service.Price
        );

        await _appointmentRepository.AddAsync(appointment);

        return Result<Guid>.Success(appointment.Id);
    }
}