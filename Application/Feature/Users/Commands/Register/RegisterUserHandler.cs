using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserResponse> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;
    public RegisterUserHandler(MovieReservationSystemApiDbContext context, IMapper mapper, IJwtService jwtService, IPasswordHasher passordHasher) {
        _context = context;
        _mapper = mapper;
        _jwtService = jwtService;
        _passwordHasher = passordHasher;
    }

    public async Task<UserResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken) {
        var isEmailExisted = await _context.UserTable
            .AnyAsync(x => x.Email == command.RegisterUserDto.Email);

        if (isEmailExisted)
            throw new InvalidOperationException("Email already existed");

        var theNewUser = _mapper.Map<User>(command.RegisterUserDto);
            
        theNewUser.PasswordHashed = _passwordHasher.HashPassword(command.RegisterUserDto.Password);

        _context.UserTable.Add(theNewUser);
        await _context.SaveChangesAsync();

        return new UserResponse {
            Name = theNewUser.Name,
            Email = theNewUser.Email
        };
    }
}