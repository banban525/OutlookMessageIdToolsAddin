using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;
using Microsoft.VisualStudio.Tools.Office.Runtime.Security;

namespace VSTOCustomActions
{
    public class CustomActions
    {
        private static string GetPublicKey(Session session)
        {
            return session["VSTOCustomAction_PublicKey"];
        }
        private static string GetManifestLocation(Session session)
        {
            return session["VSTOCustomAction_ManifestLocation"];
        }
        private static void ErrorMessage(string message, Session session)
        {
            using (Record r = new Record(message))
            {
                session.Message(InstallMessage.Error, r);
            }
        }

        [CustomAction]
        public static ActionResult AddToInclusionList(Session session)
        {
            session.Log("Start AddToInclusionList");
            try
            {
                SecurityPermission permission =
                    new SecurityPermission(PermissionState.Unrestricted);
                permission.Demand();
            }
            catch (SecurityException)
            {
                ErrorMessage("You have insufficient privileges to " +
                    "register a trust relationship. Start Excel " +
                    "and confirm the trust dialog to run the addin.", session);
                return ActionResult.Failure;
            }

            Uri deploymentManifestLocation = null;
            if (Uri.TryCreate(GetManifestLocation(session),
                UriKind.RelativeOrAbsolute, out deploymentManifestLocation) == false)
            {
                ErrorMessage("The location of the deployment manifest is missing or invalid.", session);
                return ActionResult.Failure;
            }
            session.Log("deploymentManifestLocation:" + deploymentManifestLocation.ToString());
            session.Log("GetPublicKey:" + GetPublicKey(session));

            AddInSecurityEntry entry = new AddInSecurityEntry(deploymentManifestLocation, GetPublicKey(session));
            UserInclusionList.Add(entry);

            session.CustomActionData.Add("VSTOCustomAction_ManifestLocation", deploymentManifestLocation.ToString());

            session.Log("End AddToInclusionList");
            return ActionResult.Success;

        }

        [CustomAction]
        public static ActionResult RemoveFromInclusionList(Session session)
        {
            string uriString = session.CustomActionData["VSTOCustomAction_ManifestLocation"];
            if (!string.IsNullOrEmpty(uriString))
            {
                Uri deploymentManifestLocation = new Uri(uriString);
                UserInclusionList.Remove(deploymentManifestLocation);
            }
            return ActionResult.Success;
        }

    }
}
