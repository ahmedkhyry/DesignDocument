using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace DesignDocument.DAL;

public class DataService
{
	private static IOrganizationService _service;

	public static IOrganizationService Service(string organizationUrl, string userName, string password, bool isIfd = false)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		try
		{
			if (_service == null)
			{
				if (isIfd)
				{
					ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
				}
				ClientCredentials clientCredentials = new ClientCredentials();
				clientCredentials.UserName.UserName = userName;
				clientCredentials.UserName.Password = password;
				Uri uri = new Uri(organizationUrl);
				OrganizationServiceProxy val = new OrganizationServiceProxy(uri, (Uri)null, clientCredentials, (ClientCredentials)null);
				val.EnableProxyTypes();
				_service = (IOrganizationService)(object)val;
			}
			return _service;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public static IOrganizationService Service()
	{
		return _service;
	}
}
