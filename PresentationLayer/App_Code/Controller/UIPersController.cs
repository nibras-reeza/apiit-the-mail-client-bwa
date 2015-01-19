using System.Web.Http;
using TheMailClient.Domain.Model;

namespace TheMailClient.Presentation.App_Code.Controller
{
    public class UIPersController : ApiController
    {
        public PersonalizationConfig Get()
        {
            PersonalizationConfig p = new PersonalizationConfig();

            return p;
        }

        public void Post([FromBody]string value)
        {
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete()
        {
        }
    }
}