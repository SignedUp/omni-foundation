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
        /// <remarks>Should return </remarks>
        /// <returns>Representation of the user identity.</returns>
        UserCredential GetUserAuthentication();
    }
}