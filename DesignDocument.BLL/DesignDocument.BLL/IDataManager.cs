using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Client;

namespace DesignDocument.BLL;

internal interface IDataManager
{
	OrganizationServiceProxy Service(string organizationUrl, string userName, string password);

	void PrintFormFields();

	List<string> GetFieldDetails(string fieldLogicalName);

	List<string> FetFormDetails(string fieldLogicalName);

	List<string> GetProcessDetails(string proccessName);
}
