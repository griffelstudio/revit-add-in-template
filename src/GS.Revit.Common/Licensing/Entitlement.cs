// You may not decompile, reverse engineer, disassemble or otherwise reproduce this software to a human perceivable form.
// You may not modify, rent or resell for profit this software, or create derivative works based on this software without permission of author.
// You may not publicize or distribute any registration code algorithms, information, or registration codes used by this software without permission of author.
// You accept responsibility for any network usage costs or any other costs incurred by using this software.

using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web.Script.Serialization;
using Settings = GS.Revit.Common.Properties.Entitlement;

[assembly: InternalsVisibleTo("GS.Revit.Template.Addin")]
[assembly: InternalsVisibleTo("GS.Revit.Tests")]
namespace GS.Revit.Common.Licensing
{
    /// <summary>
    /// This class handles user license validation.
    /// </summary>
    internal static class Entitlement
    {
        /// <example>https://apps.autodesk.com/webservices/checkentitlement?userid=CZGFPBBQ2P6D&appid=8451869436709222290</example> 
        private const string Url = @"https://apps.autodesk.com/webservices/checkentitlement?userid={0}&appid={1}";
        private const double MaxAllowedFreeDays = 2.5;

        /// <summary>
        /// Gets result whether license check was successful.
        /// </summary>
        public static bool IsValid { get; private set; } = false;

        internal static string Response { get; private set; }

        /// <summary>
        /// Validates license using Autodesk Entitlement API.
        /// </summary>
        /// <param name="userID">Get it from the current doc.Application.LogInUserId.</param>
        public static void Validate(string userId, string secret)
        {
            if (!NeedToCheck())
            {
                IsValid = true;
                return;
            }

            var url = string.Format(Url, userId, secret);
            var client = new WebClient();
            try
            {
                var response = client.DownloadString(url);

                var deserializer = new JavaScriptSerializer();
                var entitlementResponse = deserializer.Deserialize<EntitlementResponse>(response);

                Response = entitlementResponse.Message;
                IsValid = entitlementResponse.IsValid;

                if (IsValid)
                {
                    Settings.Default.TimeStamp = DateTime.Now;
                    Settings.Default.Save();
                }

                // Dump response just in case.
                var dirToSave = Path.Combine(Path.GetTempPath(), "GS");
                Directory.CreateDirectory(dirToSave);
                var saveResponseTo = Path.Combine(dirToSave, "Entitlement Response.txt");
                File.WriteAllText(saveResponseTo, response);
            }
            catch (Exception e)
            {
                Response = e.Message;
            }
        }

        private static bool NeedToCheck()
        {
            double daysPassedSinceLastCheck;
            try
            {
                daysPassedSinceLastCheck = DateTime.Now.Subtract(Settings.Default.TimeStamp).TotalDays;
            }
            catch
            {
                // Impossible date, check the license.
                return true;
            }

            if (daysPassedSinceLastCheck > MaxAllowedFreeDays
                || daysPassedSinceLastCheck < 0) // Real time on computer was changed.
            {
                return true;
            }

            // License was successfully checked recently, no need to check it again this time.
            return false;
        }
    }
}
