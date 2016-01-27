﻿using BaoVietCore.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaoVietCore.Services
{
    public class WebService : ServiceBase, IWebService
    {
        HttpClient Client;
        internal CancellationTokenSource CancelToken;

        public WebService(Manager man) : base (man)
        {
            Client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            CancelToken = new CancellationTokenSource();
        }

        public async Task<string> GetString(string url)
        {
            var response = await MakeRequest(url);
            return await response.Content.ReadAsStringAsync();
        }


        private async Task<HttpResponseMessage> MakeRequest(string url)
        {
            try
            {
                DateTimeOffset startTime = DateTimeOffset.UtcNow;
                HttpResponseMessage response;

                Debug.WriteLine(url);
                response = await Client.GetAsync(url, CancelToken.Token).ConfigureAwait(false);

                //RequestTelemetry request = new RequestTelemetry(httpRequestMessage.RequestUri.AbsoluteUri, startTime, startTime - DateTimeOffset.UtcNow, ((int)response.StatusCode).ToString(), response.IsSuccessStatusCode);
                //InSight.TrackRequest(request);

                return response;
            }
            catch (OperationCanceledException e)
            {
                Debug.WriteLine("Request cancelled: " + e.Message);
                CancelToken = new CancellationTokenSource();
                return new HttpResponseMessage(HttpStatusCode.PreconditionFailed);
            }
            catch(Exception e)
            {
                Debug.WriteLine("Exception " + e.Message);
                return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
        }

        /// <summary>
        /// Cancel all of current requests, except upload requests
        /// </summary>
        public void CancelCurrentRequests()
        {
            CancelToken.Cancel();
            CancelToken = new CancellationTokenSource();
        }
    }
}
