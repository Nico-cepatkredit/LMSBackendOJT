using LMSBackend.Application.Common.Interfaces.IService;
using LMSBackend.Application.Common.Interfaces.IRepository;
using LMSBackend.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Globalization;
using LMSBackend.Application.Features.TokenRotation.Dtos;

namespace LMSBackend.Application.Features.TokenRotation.Commands
{
    public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, (string AccessToken, string RefreshToken)>
    {
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserAccountsRepository _userAccountRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CreateTokenCommandHandler> _logger;
        private readonly IMapper _mapper;
        public CreateTokenCommandHandler(ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository, IUserAccountsRepository userAccountRepository, HttpClient httpClient,
            IConfiguration configuration, ILogger<CreateTokenCommandHandler> logger, IMapper mapper)
        {
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _userAccountRepository = userAccountRepository;
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(string AccessToken, string RefreshToken)> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            // Log start of token creation process
            _logger.LogInformation("Starting token creation process for user {UserId} on device {DeviceId}.", request.UserId, request.DeviceId);

            // Get the base URL from appsettings.json
            var apiBaseUrl = _configuration["BackendSettings:ApiBaseUrl"];
            var userId = request.UserId.ToString(); // Convert GUID to string for API call
            _logger.LogInformation("Making API call to {ApiBaseUrl}/getAllUserDataById/{UserId}", apiBaseUrl, userId);
            var response = await _httpClient.GetAsync($"{apiBaseUrl}/getAllUserDataById/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get user data for user {UserId}. API call failed.", request.UserId);
                // Handle failure in calling the API
                throw new Exception("Failed to get user data.");
            }

            // Parse response content
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("API Response: {Content}", content);
            var userResult = JsonSerializer.Deserialize<JsonElement>(content);

            // Log the user data
            string userResultJson = JsonSerializer.Serialize(userResult);
            Console.WriteLine("User data retrieved: {0}", userResultJson);
            _logger.LogInformation("User data retrieved. Checking if user is online...");

            // Check if the 'message' property exists and is "User is not online."
            if (userResult.TryGetProperty("message", out var messageProperty) && messageProperty.GetString() == "User is not online.")
            {
                _logger.LogWarning("User {UserId} is not online.", request.UserId);
                // Return an error if the user is not online
                throw new InvalidOperationException("User is not online.");
            }

            _logger.LogInformation("User {UserId} is online. Proceeding to add user account.", request.UserId);

            // Deserialize the JsonElement into the UserAccounts class
            // var userAccount = JsonSerializer.Deserialize<UserAccounts>(userResult.ToString()); // Deserialize into UserAccounts

            // Proceed to add user account if the user is online
            _logger.LogInformation("User {UserId} is online. Proceeding to add user account.", request.UserId);

            var userAccountsDto = _mapper.Map<UserAccountsDto>(userResult);

            // Format the datetime fields correctly for SQL (ensure these match the expected SQL format)
            _logger.LogInformation("User {RecDate} RecDate", userAccountsDto.RecDate);
            _logger.LogInformation("User {ModDate} ModDate", userAccountsDto.ModDate);
            _logger.LogInformation("User {PasswordDate} PasswordDate", userAccountsDto.PasswordDate);
            _logger.LogInformation("User {OTPTimeStamp} OTPTimeStamp", userAccountsDto.OTPTimeStamp);
            _logger.LogInformation("User {SessionPingDate} SessionPingDate", userAccountsDto.SessionPingDate);

            // Proceed to add user account if the user is online

            var hi = await _userAccountRepository.GetUserById(request.UserId);

            // Generate access and refresh tokens
            var accessToken = _tokenService.GenerateJwtToken("userRole", "branch", "department", request.DeviceId, request.UserId);
            var refreshToken = _tokenService.GenerateRefreshToken();
            _logger.LogInformation("Tokens generated for user {UserId}. AccessToken and RefreshToken created.", request.UserId);

            var refreshTokenEntity = new RefreshToken
            {
                UserId = request.UserId,
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false,
                DeviceId = request.DeviceId
            };
            await _refreshTokenRepository.SaveRefreshTokenAsync(refreshTokenEntity);
            _logger.LogInformation("Refresh token saved for user {UserId}.", request.UserId);

            return (accessToken, refreshToken);
        }
    }
}
