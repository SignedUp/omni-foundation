using Omniscient.Foundation.Security;
namespace Omniscient.Foundation.ApplicationModel.Security
{
    /// <summary>
    /// Defines an Extender contract for prompting a user for his/her credential.
    /// </summary>
    public interface ICredentialServiceExtenderContract
    {
        /// <summary>
        /// Prompts a user for his/her credential and returns it.
        /// </summary>
        /// <returns>User credentials.</returns>
        void NegociateAuthentication(SecurePrincipal principal);
    }
}