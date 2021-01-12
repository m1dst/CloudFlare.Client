﻿using System;
using CloudFlare.Client.Client;
using CloudFlare.Client.Contexts;
using CloudFlare.Client.Interfaces;
using CloudFlare.Client.Models;

namespace CloudFlare.Client
{
    public class CloudFlareClient : ICloudFlareClient
    {
        protected bool IsDisposed { get; private set; }

        public IAccounts Accounts { get; }
        public IUsers Users { get; }
        public IZones Zones { get; }

        private IConnection _connection;

        /// <summary>
        /// Initialize CloudFlare Client
        /// </summary>
        /// <param name="authentication">Authentication which can be ApiKey and Token based</param>
        public CloudFlareClient(IAuthentication authentication)
        {
            IsDisposed = false;

            _connection = new ApiConnection(authentication);

            Accounts = new Accounts(_connection);
            Users = new Users(_connection);
            Zones = new Zones(_connection);
        }

        /// <summary>
        /// Initialize CloudFlare Client
        /// </summary>
        /// <param name="emailAddress">Email address</param>
        /// <param name="apiKey">CloudFlare API Key</param>
        public CloudFlareClient(string emailAddress, string apiKey) : this(new ApiKeyAuthentication(emailAddress, apiKey)) { }

        /// <summary>
        /// Initialize CloudFlare Client
        /// </summary>
        /// <param name="apiToken">Authentication with api token</param>
        public CloudFlareClient(string apiToken) : this(new ApiTokenAuthentication(apiToken)) { }

        /// <summary>
        /// Destruct CloudFlare Client
        /// </summary>
        ~CloudFlareClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed || !disposing)
                return;

            if (_connection == null)
            {
                return;
            }

            _connection.Dispose();
            _connection = null;

            IsDisposed = true;
        }
    }
}
