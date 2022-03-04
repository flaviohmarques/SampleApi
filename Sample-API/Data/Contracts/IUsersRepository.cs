using SampleApi.Data.DTO.Request;
using SampleApi.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApi.Data.Contracts
{
    public interface IUsersRepository
    {
        Task<User> GetUser(string usuario);
        Task<User> GetUserById(string id);
        Task<Client> AddClient(Client cliente);
        Task<User> AddUser(User user);
        Task<Role> AddRole(Role role);
        Task<string> EnableAgreement(User user, string convenio);
        Task<OperationsAgreement> AddAgreement(OperationsAgreement conveniosOperacoes);
        Task UpdateUser(User user);
        Task<User> GetUserRefreshToken(string token);
        Task<ParametersAgreement> AddParametersAgreement(ParametersAgreement parametroConvenio);
        Task<bool> UpdateAgreement(OperationsAgreement conveniosOperacoes);
        Task DeleteOperationsAgreement(string convenio);
        Task AddConnectedService(ConnectedServicesRequest adicionarServicosRequest);
        Task<List<ConnectedServices>> GetConnectedServices(string tipo);
        Task AddParametersUserRobot(ParametersUserRobotRequest parametroUsuarioRoboRequest);
    }
}
