namespace Omniscient.Foundation.Security
{
    ///<summary>
    ///</summary>
    public interface IRoleProvider
    {
        ///<summary>
        ///</summary>
        ///<param name="username"></param>
        ///<returns></returns>
        string[] GetRolesForUser(string username);
    }
}
