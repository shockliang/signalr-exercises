using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagePackExercise.Services
{
    public interface IRandomUserService
    {
        Task<IEnumerable<RandomUser>> GetUsers(int max = 10);
    }
}