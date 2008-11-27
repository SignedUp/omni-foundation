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
        /// <returns>Representation of the user identity.</returns>
        UserCredential GetUserAuthentication();
    }
}