﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Security;
using Microsoft.Azure.Management.Security.Models;
using Microsoft.Azure.Test.HttpRecorder;
using Microsoft.Rest.Azure;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using SecurityCenter.Tests.Helpers;
using Xunit;

namespace SecurityCenter.Tests
{
    public class SecurityAlertsTests : TestBase
    {
        #region Test setup

        public static TestEnvironment TestEnvironment { get; private set; }

        private static SecurityCenterClient GetSecurityCenterClient(MockContext context)
        {
            if (TestEnvironment == null && HttpMockServer.Mode == HttpRecorderMode.Record)
            {
                TestEnvironment = TestEnvironmentFactory.GetTestEnvironment();
            }

            var handler = new RecordedDelegatingHandler { StatusCodeToReturn = HttpStatusCode.OK, IsPassThrough = true };

            var securityCenterClient = HttpMockServer.Mode == HttpRecorderMode.Record
                ? context.GetServiceClient<SecurityCenterClient>(TestEnvironment, handlers: handler)
                : context.GetServiceClient<SecurityCenterClient>(handlers: handler);

            securityCenterClient.AscLocation = "centralus";

            return securityCenterClient;
        }

        #endregion

        #region Alerts

        [Fact]
        public void SecurityAlerts_List()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                var securityCenterClient = GetSecurityCenterClient(context);
                var alerts = securityCenterClient.Alerts.List();
                ValidateAlerts(alerts);
            }
        }

        [Fact]
        public async Task SecurityAlerts_GetResourceGroupLevelAlerts()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                var securityCenterClient = GetSecurityCenterClient(context);

                var alerts = await securityCenterClient.Alerts.ListAsync();
                var enumerator = alerts.GetEnumerator();
                enumerator.MoveNext();

                while (!enumerator.Current.Id.Contains("resourceGroups") && enumerator.MoveNext()) ;

                Assert.NotNull(enumerator.Current);

                var alert = securityCenterClient.Alerts.GetResourceGroupLevelAlerts(enumerator.Current.Name, Regex.Match(enumerator.Current.Id, @"(?<=resourceGroups/)[^/]+?(?=/)").Value);
                ValidateAlert(alert);
            }
        }

        [Fact]
        public async Task SecurityAlerts_GetSubscriptionLevelAlert()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                var securityCenterClient = GetSecurityCenterClient(context);

                var alerts = await securityCenterClient.Alerts.ListAsync();
                var enumerator = alerts.GetEnumerator();
                enumerator.MoveNext();

                while (enumerator.Current.Id.Contains("resourceGroups") && enumerator.MoveNext()) ;

                Assert.NotNull(enumerator.Current);

                var alert = securityCenterClient.Alerts.GetSubscriptionLevelAlert(enumerator.Current.Name);
                ValidateAlert(alert);
            }
        }

        [Fact]
        public async Task SecurityAlerts_ListByResourceGroup()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                var securityCenterClient = GetSecurityCenterClient(context);
                var alerts = await securityCenterClient.Alerts.ListAsync();
                var enumerator = alerts.GetEnumerator();
                enumerator.MoveNext();

                while (!enumerator.Current.Id.Contains("resourceGroups") && enumerator.MoveNext()) ;

                var rgAlerts = securityCenterClient.Alerts.ListByResourceGroup(Regex.Match(enumerator.Current.Id, @"(?<=resourceGroups/)[^/]+?(?=/)").Value);
                ValidateAlerts(rgAlerts);
            }
        }

        [Fact]
        public async Task SecurityAlerts_ListResourceGroupLevelAlertsByRegion()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                var securityCenterClient = GetSecurityCenterClient(context);
                var alerts = await securityCenterClient.Alerts.ListAsync();
                var enumerator = alerts.GetEnumerator();
                enumerator.MoveNext();

                while (!enumerator.Current.Id.Contains("resourceGroups") && enumerator.MoveNext()) ;

                securityCenterClient.AscLocation = Regex.Match(enumerator.Current.Id, @"(?<=locations/)[^/]+?(?=/)").Value;
                var rgAlerts = securityCenterClient.Alerts.ListResourceGroupLevelAlertsByRegion(Regex.Match(enumerator.Current.Id, @"(?<=resourceGroups/)[^/]+?(?=/)").Value);
                ValidateAlerts(rgAlerts);
            }
        }

        [Fact]
        public async Task SecurityAlerts_ListSubscriptionLevelAlertsByRegion()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                var securityCenterClient = GetSecurityCenterClient(context);
                var alerts = await securityCenterClient.Alerts.ListAsync();
                var enumerator = alerts.GetEnumerator();
                enumerator.MoveNext();

                while (enumerator.Current.Id.Contains("resourceGroups") && enumerator.MoveNext()) ;

                securityCenterClient.AscLocation = Regex.Match(enumerator.Current.Id, @"(?<=locations/)[^/]+?(?=/)").Value;

                var regionAlerts = securityCenterClient.Alerts.ListSubscriptionLevelAlertsByRegion();
                ValidateAlerts(regionAlerts);
            }
        }

        [Fact]
        public async Task SecurityAlerts_UpdateResourceGroupLevelAlertState()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                var securityCenterClient = GetSecurityCenterClient(context);
                var alerts = await securityCenterClient.Alerts.ListAsync();
                var enumerator = alerts.GetEnumerator();
                enumerator.MoveNext();

                while (!enumerator.Current.Id.Contains("resourceGroups") && enumerator.MoveNext()) ;

                securityCenterClient.AscLocation = Regex.Match(enumerator.Current.Id, @"(?<=locations/)[^/]+?(?=/)").Value;

                securityCenterClient.Alerts.UpdateResourceGroupLevelAlertState(enumerator.Current.Name, "Dismiss", Regex.Match(enumerator.Current.Id, @"(?<=resourceGroups/)[^/]+?(?=/)").Value);
            }
        }

        [Fact]
        public async Task SecurityAlerts_UpdateSubscriptionLevelAlertState()
        {
            using (var context = MockContext.Start(this.GetType().FullName))
            {
                var securityCenterClient = GetSecurityCenterClient(context);
                var alerts = await securityCenterClient.Alerts.ListAsync();
                var enumerator = alerts.GetEnumerator();
                enumerator.MoveNext();

                while (enumerator.Current.Id.Contains("resourceGroups") && enumerator.MoveNext()) ;

                securityCenterClient.AscLocation = Regex.Match(enumerator.Current.Id, @"(?<=locations/)[^/]+?(?=/)").Value;

                securityCenterClient.Alerts.UpdateSubscriptionLevelAlertState(enumerator.Current.Name, "Dismiss");
            }
        }

        #endregion

        #region Validations

        private void ValidateAlerts(IPage<Alert> alertPage)
        {
            Assert.True(alertPage.IsAny());

            alertPage.ForEach(ValidateAlert);
        }

        private void ValidateAlert(Alert alert)
        {
            Assert.NotNull(alert);
        }

        #endregion
    }
}
