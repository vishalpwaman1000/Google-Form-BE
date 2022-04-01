
using Google_Form.Models;
using System.Threading.Tasks;

namespace Google_Form.DataAccessLayer
{
    public interface IGoogleFormDL
    {
        public Task<RegistrationResponse> Registration(RegistrationRequest request);
    }
}
