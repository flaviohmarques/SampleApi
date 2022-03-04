using SampleApi.Data.DTO.Request;
using SampleApi.Data.DTO.Response;
using SampleApi.Data.Entities;
using System.Threading.Tasks;

namespace SampleApi.Data.Contracts
{
    public interface IManagerService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest, string ipAddress);
        Task<User> GetById(string id);
        Task<User> AddUser(UserRequest userRequest);
        Task<Role> AddRole(RoleRequest roleRequest);
        Task<AuthenticateResponse> RefreshToken(string token, string ipAddress);
        Task<bool> RevokeToken(string token, string ipAddress);
        Task<OperationsAgreement> RegisterAgreement(AgreementRequest agreementRequest);
        Task<string> EnableAgreement(EnableAgreementRequest enableAgreementRequest);
        Task<Client> AddClient(ClientRequest clientRequest);
        Task<OperationsAgreement> UpdateAgreement(AgreementRequest agreementRequest);
        Task<string> AddConnectedServices(ConnectedServicesRequest connectedServicesRequest);
        Task<string> AddParametersUserRobot(ParametersUserRobotRequest parametersUserRobotRequest);
    }
}
