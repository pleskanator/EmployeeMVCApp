using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MikesService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUser" in both code and config file together.
    [ServiceContract]
    public interface IUser
    {
        [OperationContract]
        List<Objects.Employee> GetEmployeeInfo(string firstName, string lastName);

        [OperationContract]
        bool SaveEmployeeInfo(string firstName, string lastName, string address, string email, string phone);
    }
}
